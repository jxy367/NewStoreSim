using System;
using System.Collections.Generic;

namespace NewStoreSim
{
    class Sim
    {
        GaussianRandomNumberGenerator grng = new GaussianRandomNumberGenerator();
        int max_time;
        int milliseconds_per_refresh;
        //Variables for generating customers
        double avg_customers;
        double sd_customers;
        int avg_items;
        int sd_items;
        int avg_time;
        int sd_time;
        double avg_processing_time;
        double sd_processing_time;
        int num_registers;
        int output_frequency;

        //Constructor
        public Sim(double avg_customers, double sd_customers, int avg_items, int sd_items, int avg_time, int sd_time, double avg_processing_time, double sd_processing_time, int num_registers, int milliseconds_per_refresh, int max_time, int output_frequency)
        {
            this.avg_customers = avg_customers;
            this.sd_customers = sd_customers;
            this.avg_items = avg_items;
            this.sd_items = sd_items;
            this.avg_time = avg_time;
            this.sd_time = sd_time;
            this.avg_processing_time = avg_processing_time;
            this.sd_processing_time = sd_processing_time;
            this.num_registers = num_registers;
            this.milliseconds_per_refresh = milliseconds_per_refresh;
            this.max_time = max_time;
            this.output_frequency = output_frequency;
        }

        public double Run()
        {
            CustomerCalculator cc = new CustomerCalculator(avg_customers, sd_customers, max_time, milliseconds_per_refresh);
            CustomerGenerator cg = new CustomerGenerator(avg_items, sd_items, avg_time, sd_time);
            ShoppingCustomers sc = new ShoppingCustomers();
            Registers rs = new Registers(num_registers, avg_processing_time, sd_processing_time, max_time);
            FinishedCustomers fc = new FinishedCustomers();
            int current_time = 0;

            while (current_time <= max_time || NumCustomersInStore(sc, rs) > 0)
            {
                current_time += milliseconds_per_refresh; //Increase time
                List<Customer> new_customers = new List<Customer>();
                if (current_time <= max_time)
                {
                    int num_new_customers = cc.Update(current_time); //Calculate number of new customers
                    new_customers = cg.GenerateCustomers(num_new_customers); //Make new customers
                }
                List<Customer> new_waiting_customers = sc.Update(new_customers, current_time); //Update the shopping customers
                List<Customer> new_finished_customers = rs.Update(new_waiting_customers, current_time); //Update the customers waiting in line for registers
                fc.Update(new_finished_customers); //Add finished customers to finished customers object.

                if(current_time == max_time)
                {
                    Console.WriteLine("The store is now closed\n");
                }

                //Values I want outputted to console
                if (current_time % output_frequency == 0)
                {
                    TimeSpan t = TimeSpan.FromMilliseconds(current_time);
                    string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                            t.Hours,
                                            t.Minutes,
                                            t.Seconds,
                                            t.Milliseconds);
                    Console.WriteLine("Current time: {0}", answer);
                    //Console.WriteLine("Current Customer Calculator value: {0}", cc.GetModValue());
                    Console.WriteLine("Current number of customers finding items: {0}", sc.GetNumCustomers());
                    Console.WriteLine("Expected number of registers: {0}", rs.CalculateRegisters(current_time));
                    foreach (Register r in rs.GetRegisters())
                    {
                        Console.WriteLine("Register {0} has {1} customers and {2} items", r.GetId(), r.GetNumCustomers(), r.GetNumItems());
                    }
                    Console.WriteLine("Current number of finished customers: {0}", fc.GetNumCustomers());
                    Console.WriteLine("Current number of purchased items: {0}", fc.GetNumItems());
                    Console.WriteLine("-------\n");
                }
            }
            TimeSpan h = TimeSpan.FromMilliseconds(current_time);
                    string a = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                            h.Hours,
                                            h.Minutes,
                                            h.Seconds,
                                            h.Milliseconds);
                    Console.WriteLine("Current time: {0}", a);

            //Console.WriteLine("Average Queue Time: {0}", fc.AverageWaitTime());
            return fc.AverageWaitTime();
        }

        public int NumCustomersInStore(ShoppingCustomers sc, Registers rs)
        {
            int num_customers = 0;
            num_customers += sc.GetNumCustomers();
            foreach(Register r in rs.GetRegisters())
            {
                num_customers += r.GetNumCustomers();
            }
            return num_customers;
        }
    }

    class Customer
    {
        readonly int items;
        readonly int shopping_time;
        int start_time;
        int processing_start_time;
        int processing_end_time;
        int queue_start_time;
        int queue_end_time;

        public Customer(int items, int time)
        {
            this.items = items;
            shopping_time = time;
        }

        //Set when the customer begins searching for items
        public void StartShoppping(int current_time)
        {
            start_time = current_time;
        }

        //Has the customer found all items?
        public bool FinishedShopping(int current_time)
        {
            return current_time - start_time >= shopping_time;
        }

        //Set when the customer begins waiting in line for the register
        public void QueueStart(int current_time)
        {
            queue_start_time = current_time;
            queue_end_time = -1;
        }

        //Set when the customer finishes waiting in line for the register
        public void QueueFinished(int current_time)
        {
            queue_end_time = current_time;
        }

        //Returns the total wait time for the customer
        public int QueueTime()
        {
            return queue_end_time - queue_start_time;
        }

        //Has the customer finished paying for items?
        public bool IsFinished()
        {
            return processing_end_time > processing_start_time;
        }

        //Set when processing begins
        public void ProcessingStart(int current_time)
        {
            processing_start_time = current_time;
            processing_end_time = -1;
        }

        //Set when processing ends
        public void ProcessingEnd(int current_time)
        {
            processing_end_time = current_time;
        }

        //Get the number of items the customer is purchasing
        public int GetItems()
        {
            return items;
        }

        //Get the time at which the customer began waiting in line
        public int GetStartTime()
        {
            return queue_start_time;
        }

        public int GetProcessingStartTime()
        {
            return processing_start_time;
        }

    }

    class CustomerGenerator
    {
        private readonly int avg_items;
        private readonly int sd_items;
        private readonly int avg_time;
        private readonly int sd_time;
        GaussianRandomNumberGenerator grng = new GaussianRandomNumberGenerator();

        public CustomerGenerator(int avg_items, int sd_items, int avg_time, int sd_time)
        {
            this.avg_items = avg_items;
            this.sd_items = sd_items;
            this.avg_time = avg_time;
            this.sd_time = sd_time;
        }

        //Make variable number of customers
        public List<Customer> GenerateCustomers(int num_customers)
        {
            List<Customer> new_customers = new List<Customer>();
            for (int i = 0; i < num_customers; i++)
            {
                new_customers.Add(Generate());
            }
            return new_customers;
        }

        //Make a new customer
        public Customer Generate()
        {
            //How many items will the customer buy? Must be greater than 0
            int items = (int)Math.Round(grng.GetRandomValue(avg_items, sd_items));
            while (items <= 0)
            {
                items = (int)Math.Round(grng.GetRandomValue(avg_items, sd_items));
            }

            //How long will the customer take to find all their items? Must be greater than 0
            int time = (int)Math.Round(grng.GetRandomValue(avg_time, sd_time));
            while (time <= 0)
            {
                time = (int)Math.Round(grng.GetRandomValue(avg_time, sd_time));
            }

            return new Customer(items, time);
        }

    }

    class Registers
    {
        List<Register> list_of_registers;
        int new_register_id;
        double avg_processing_time;
        double sd_processing_time;
        int max_time;

        public Registers(int num_registers, double avg_processing_time, double sd_processing_time, int max_time)
        {
            this.avg_processing_time = avg_processing_time;
            this.sd_processing_time = sd_processing_time;
            this.max_time = max_time;
            new_register_id = 1;
            //There must be 1 or more registers open.
            if (num_registers < 1)
            {
                throw new ArgumentException("Number of registers must be greater than 0");
            }
            else
            {
                list_of_registers = new List<Register>();
                for (int i = 0; i < num_registers; i++)
                {
                    list_of_registers.Add(new Register(new_register_id++, avg_processing_time, sd_processing_time));
                }
            }
        }

        public void Add(Customer customer)
        {
            Register best_register = null;
            list_of_registers.Sort(new RegisterComparerCustomer());
            foreach(Register r in list_of_registers)
            {
                if(best_register == null)
                {
                    if (!r.IsCapped())
                    {
                        best_register = r;
                    }
                }
            }
            best_register.Add(customer);
            list_of_registers.Sort(new RegisterComparerID());
        }

        public void AddRegisters(int num)
        {
            if(num < 1)
            {
                throw new ArgumentException("Number of registers to be added must be greater than 0");
            }
            for(int i=0; i < num; i++)
            {
                list_of_registers.Add(new Register(new_register_id++, avg_processing_time, sd_processing_time));
            }
        }

        public void UncapRegisters()
        {
            foreach(Register r in list_of_registers)
            {
                r.SetCapped(false);
            }
        }

        public List<Register> GetRegisters()
        {
            return list_of_registers;
        }

        public int CalculateRegisters(int current_time)
        {
            int length = 11;
            int[] num_registers = new int[11] { 12, 14, 16, 17, 17, 15, 14, 14, 13, 13, 12 };
            //int[] num_registers = new int[11] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
            for (int i = 0; i < length; i++)
            {
                if ((current_time >= (int)(i * max_time / (length - 1))) && (current_time < (int)((i + 1) * max_time / (length - 1))))
                {
                    return num_registers[i];
                }
            }
            return 8;
        }

        //Attempt to remove some number of registers, return the actual number removed
        public void RemoveRegisters(int num)
        {
            //Console.WriteLine("Removing {0} registers", num);
            //Console.WriteLine("Current have {0} registers", list_of_registers.Count);
            list_of_registers.Sort(new RegisterComparerCustomer());
            List<Register> registers_removed = new List<Register>();
            List<Register> registers_capped = new List<Register>();
            foreach(Register r in list_of_registers)
            {
                r.SetCapped(false);
                if(registers_removed.Count + registers_capped.Count < num)
                {
                    if (r.GetNumCustomers() == 0)
                    {
                        registers_removed.Add(r);
                    }
                    else
                    {
                        registers_capped.Add(r);
                    }
                    
                }
            }

            foreach(Register rc in registers_capped)
            {
                rc.SetCapped(true);
            }

            int hold = list_of_registers.Count;
            
            foreach (Register rr in registers_removed)
            {
                list_of_registers.Remove(rr);
            }

            //Console.WriteLine("Before removing: {0}, After removing: {1}, Length of remove list {2}", hold, list_of_registers.Count, registers_removed.Count);

            list_of_registers.Sort(new RegisterComparerID());

            //return registers_removed.Count;
        }

        //Update all registers and return any finished customers
        public List<Customer> Update(List<Customer> new_waiting_customers, int current_time)
        {
            //Find all new finished customers 
            List<Customer> finished_customers = new List<Customer>();
            foreach (Register r in list_of_registers)
            {
                Customer c = r.Update(current_time);
                if (c != null)
                {
                    if (c.IsFinished())
                    {
                        finished_customers.Add(c);
                    }
                }
            }

            //Remove or add registers as necessary
            /*
            if(current_time > 1080000)
            {
                int j = 0;
            }
            int new_num_registers = CalculateRegisters(current_time);
            if(new_num_registers < list_of_registers.Count)
            {
                RemoveRegisters(list_of_registers.Count - new_num_registers);
            }
            if(new_num_registers > list_of_registers.Count)
            {
                AddRegisters(new_num_registers - list_of_registers.Count);
                UncapRegisters();
            }
            */

            //Automatically add or remove registers as necessary
            UncapRegisters();
            if (AllGreaterThan(10) && !AllLessThan(20))
            {
                AddRegisters(1);
            }
            if (AllLessThan(4))
            {
                if(list_of_registers.Count > 1)
                {
                    RemoveRegisters(1);
                }
                
            }


            //Reset Register IDs
            ResetRegisterIds();


            //Add new waiting customers to registers
            foreach (Customer c in new_waiting_customers)
            {
                c.QueueStart(current_time);
                Add(c);
            }

            return finished_customers;
        }

        public void ResetRegisterIds()
        {
            list_of_registers.Sort(new RegisterComparerID());
            new_register_id = 1;
            foreach(Register r in list_of_registers)
            {
                r.SetId(new_register_id++);
            }
        }

        public bool AllLessThan(int num)
        {
            foreach(Register r in list_of_registers)
            {
                if(r.GetNumCustomers() >= num)
                {
                    return false;
                }
            }
            return true;
        }

        public bool AllGreaterThan(int num)
        {
            foreach (Register r in list_of_registers)
            {
                if (r.GetNumCustomers() <= num)
                {
                    return false;
                }
            }
            return true;
        }

    }

    class Register
    {
        List<Customer> line = new List<Customer>();
        Customer current_customer;
        int processing_time;
        int register_id;
        double avg_item_time;
        double sd_item_time;
        bool capped = false;
        GaussianRandomNumberGenerator grng = new GaussianRandomNumberGenerator();

        public Register(int register_id, double avg_processing_time, double sd_processing_time)
        {
            this.register_id = register_id;
            avg_item_time = avg_processing_time;
            sd_item_time = sd_processing_time;
        }

        //Add a customer to a line
        public void Add(Customer customer)
        {
            line.Add(customer);
        }

        //Count all the items in a register line
        public int GetNumItems()
        {
            int num_items = 0;
            if (current_customer != null)
            {
                num_items += current_customer.GetItems();
            }
            foreach (Customer c in line)
            {
                num_items += c.GetItems();
            }
            return num_items;
        }

        //Return number of customers at this register
        public int GetNumCustomers()
        {
            int num = 0;
            if (current_customer != null)
            {
                num += 1;
            }
            num += line.Count;
            return num;
        }

        //Return True if the register is currently helping a customer
        public bool IsBusy()
        {
            return current_customer != null;
        }

        public bool IsCapped()
        {
            return capped;
        }

        //Select next customer in line
        public void NextCustomer(int current_time)
        {
            if (current_customer == null && line.Count > 0)
            {
                current_customer = line[0];

                current_customer.QueueFinished(current_time);

                //Calculate time necessary to help customer
                processing_time = CalculateProcessingTime(current_customer.GetItems());

                //Remove customer from line
                line.Remove(current_customer);

                //Start the proc
                current_customer.ProcessingStart(current_time);
            }
        }

        //Calculate the processing time based upon the number of items a customer has
        public int CalculateProcessingTime(int num_items)
        {
            int total_processing_time = 0;
            for (int i = 0; i < num_items; i++)
            {
                int item_processing_time = (int)grng.GetRandomValue(avg_item_time, sd_item_time);
                while (item_processing_time <= 0)
                {
                    item_processing_time = (int)grng.GetRandomValue(avg_item_time, sd_item_time); //Value can't be less than 0
                }
                total_processing_time += item_processing_time;
            }
            total_processing_time += 30 * 1000; //Monetary transaction
            return total_processing_time;
        }

        //Update the state of this register
        public Customer Update(int current_time)
        {
            if (current_customer != null)
            {
                Customer output_customer = current_customer;
                if (current_time - current_customer.GetProcessingStartTime() > processing_time)
                {
                    current_customer.ProcessingEnd(current_time);
                    output_customer = current_customer;
                    current_customer = null;
                }
                return output_customer;
            }
            else
            {
                NextCustomer(current_time);
                return current_customer;
            }
        }

        public void SetId(int newId)
        {
            register_id = newId;
        }

        public int GetId()
        {
            return register_id;
        }

        public void SetCapped(bool capped)
        {
            this.capped = capped;
        }

    }

    //A class to hold the shopping but not yet queued customers.
    class ShoppingCustomers
    {
        List<Customer> list_customers = new List<Customer>();

        public List<Customer> Update(List<Customer> new_customers, int current_time)
        {
            List<Customer> move_to_registers = new List<Customer>();
            //Get all shoppers from the list who got their items (aka finished shopping but need to pay)
            foreach (Customer c in list_customers)
            {
                if (c.FinishedShopping(current_time))
                {
                    move_to_registers.Add(c);
                }
            }

            //Remove all shoppers who have got their items from the list
            foreach (Customer fc in move_to_registers)
            {
                list_customers.Remove(fc);
            }

            //Add new customers to the list
            foreach (Customer nc in new_customers)
            {
                nc.StartShoppping(current_time);
                list_customers.Add(nc);
            }

            return move_to_registers;
        }

        public int GetNumCustomers()
        {
            return list_customers.Count;
        }
    }

    class CustomerCalculator
    {
        double avg_customers; //per millisecond
        double sd_customers;
        double mod_value;
        int max_time;
        int milliseconds_per_refresh;
        GaussianRandomNumberGenerator grng = new GaussianRandomNumberGenerator();
        Random rand = new Random();

        public CustomerCalculator(double avg_customers, double sd_customers, int max_time, int milliseconds_per_refresh)
        {
            this.avg_customers = avg_customers;
            this.sd_customers = sd_customers;
            this.max_time = max_time;
            this.milliseconds_per_refresh = milliseconds_per_refresh;
            this.mod_value = 0;
        }

        public int Update(int current_time)
        {
            /*
            double multiplier = CalculateMultiplier(current_time);
            double add_value = grng.GetRandomValue(multiplier * avg_customers, multiplier * sd_customers);
            if (add_value < 0)
            {
                add_value = 0;
            }
            mod_value += milliseconds_per_refresh * add_value;
            int num_new_customers = (int)mod_value;
            mod_value = mod_value % 1;
            return num_new_customers;
            */
            double multiplier = CalculateMultiplier(current_time);
            double add_value = milliseconds_per_refresh * grng.GetRandomValue(avg_customers, sd_customers);
            double random_value = rand.NextDouble();
            if (random_value < add_value)
            {
                return (int)multiplier;
            }
            return 0;
        }

        //Calculate multiplier
        public double CalculateMultiplier(int current_time)
        {
            int length = 11;
            double[] multipliers = new double[11] {2, 3, 4, 4.2, 3.5, 3, 3, 3.5, 3.5, 2.5, 1}; //32.2
            //double[] multipliers = new double[11] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            for (int i=0; i<length; i++)
            {
                if ((current_time >= (int)(i * max_time / (length - 1))) && (current_time < (int)((i + 1) * max_time / (length - 1))))
                {
                    return multipliers[i];
                }
            }
            int peak_1_lower = (int)(2 * max_time / 10);
            int peak_1_upper = (int)(4.5 * max_time / 10);
            int peak_2_lower = (int)(7.5 * max_time / 10);
            int peak_2_upper = (int)(8.5 * max_time / 10);

            if (current_time > peak_1_lower && current_time < peak_1_upper)
            {
                return 1.0;
            }

            if (current_time > peak_2_lower && current_time < peak_2_upper)
            {
                return 1.0;
            }

            return 1.0;
        }


        public double GetModValue()
        {
            return this.mod_value;
        }

    }

    class FinishedCustomers
    {
        List<Customer> finished_customers = new List<Customer>();
        public void Update(List<Customer> new_finished_customers)
        {
            foreach (Customer c in new_finished_customers)
            {
                finished_customers.Add(c);
            }
        }

        //Average waiting time between got all items and left store.
        public int AverageWaitTime()
        {
            if (finished_customers.Count > 0)
            {
                double average_wait_time = 0;
                int total_wait_time = 0;
                int count = 0;
                foreach (Customer c in finished_customers)
                {
                    //Console.WriteLine(c.QueueTime());
                    if (total_wait_time >= 0)
                    {
                        total_wait_time += c.QueueTime();
                    }
                    count++;
                    average_wait_time += ((c.QueueTime() - average_wait_time) / count);
                }
                if (total_wait_time < 0)
                {
                    Console.WriteLine("Total wait time overflowed, using alternate calculation.");
                    return (int)average_wait_time;
                }
                Console.WriteLine("{0} = {1}", (int)(total_wait_time / finished_customers.Count), (int)average_wait_time);

                return (int)(total_wait_time / finished_customers.Count);
            }
            return -1;

        }

        //Total number of processed items
        public int GetNumItems()
        {
            int count = 0;
            foreach(Customer c in finished_customers)
            {
                count += c.GetItems();
            }
            return count;
        }

        //Total number of finished customers
        public int GetNumCustomers()
        {
            return finished_customers.Count;
        }
    }

    //Class just for generating random values in a Gaussian distribution
    class GaussianRandomNumberGenerator
    {
        Random rand = new Random();
        public double GetRandomValue(double avg, double sd)
        {
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         avg + sd * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }
    }

    //Class for sorting Registers by number of customers
    class RegisterComparerCustomer: IComparer<Register>
    {
        public int Compare(Register x, Register y)
        {
            if (x.GetNumCustomers().CompareTo(y.GetNumCustomers()) == 0)
            {
                return x.GetNumItems().CompareTo(y.GetNumItems());
            }
            else
            {
                return x.GetNumCustomers().CompareTo(y.GetNumCustomers());
            }
        }
    }

    //Class for sorting Register by ID
    class RegisterComparerID: IComparer<Register>
    {
        public int Compare(Register x, Register y)
        {
            return x.GetId().CompareTo(y.GetId());
        }
    }

}
