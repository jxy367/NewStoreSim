using System;
using System.Collections.Generic;

namespace StoreSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Sim s = new Sim();
            s.Run(8);
            System.Threading.Thread.Sleep(10000);
            Console.ReadLine();
        }

    }

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

        //Constructor
        public Sim(double avg_customers, double sd_customers, int avg_items, int sd_items, int avg_time, int sd_time, double avg_processing_time, double sd_processing_time, int num_registers, int milliseconds_per_refresh, int max_time)
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
        }

        public void Run(double avg_customers, double sd_customers, int avg_items, int sd_items, int avg_time, int sd_time, int avg_processing_time, int sd_processing_time, int num_registers, int milliseconds_per_refresh, int max_time)
        {
            CustomerCalculator cc = new CustomerCalculator(avg_customers, sd_customers, max_time, milliseconds_per_refresh);
            CustomerGenerator cg = new CustomerGenerator(avg_items, sd_items, avg_time, sd_time);
            ShoppingCustomers sc = new ShoppingCustomers();
            Registers rs = new Registers(num_registers, avg_processing_time, sd_processing_time);
            FinishedCustomers fc = new FinishedCustomers();
            int current_time = 0;

            while (current_time <= max_time)
            {
                current_time += milliseconds_per_refresh; //Increase time
                int num_new_customers = cc.Update(current_time); //Calculate number of new customers
                List<Customer> new_customers = cg.GenerateCustomers(num_new_customers); //Make new customers
                List<Customer> new_waiting_customers = sc.Update(new_customers, current_time); //Update the shopping customers
                List<Customer> new_finished_customers = rs.Update(new_waiting_customers, current_time); //Update the customers waiting in line for registers
                fc.Update(new_finished_customers); //Add finished customers to finished customers object.

                //Values I want outputted to console
                if (current_time % (15 * 60 * 1000) == 0)
                {
                    Console.WriteLine("Current time: {0}", current_time);
                    Console.WriteLine("Current Customer Calculator value: {0}", cc.GetModValue());
                    Console.WriteLine("Current number of customers finding items: {0}", sc.GetNumCustomers());
                    foreach (Register r in rs.GetRegisters())
                    {
                        Console.WriteLine("Register {0} has {1} customers and {2} items", r.GetId(), r.GetNumCustomers(), r.GetNumItems());
                    }
                    Console.WriteLine("Current number of finished customers: {0}", fc.GetNumCustomers());
                    Console.WriteLine("-------\n");
                }
            }

            Console.WriteLine("Average Queue Time: {0}", fc.AverageWaitTime());

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
        Register[] list_of_registers;
        public Registers(int num_registers, double avg_processing_time, double sd_processing_time)
        {
            //There must be 1 or more registers open.
            if (num_registers < 1)
            {
                throw new ArgumentException("Number of registers must be greater than 0");
            }
            else
            {
                this.list_of_registers = new Register[num_registers];
                for (int i = 0; i < num_registers; i++)
                {
                    this.list_of_registers[i] = new Register(i + 1, avg_processing_time, sd_processing_time);
                }
            }
        }

        public void Add(Customer customer)
        {
            //Select the register with the fewest number of items
            Register best_register = list_of_registers[0];
            int num_items = best_register.GetNumItems();
            foreach (Register r in list_of_registers)
            {
                if (r.GetNumItems() < num_items)
                {
                    num_items = r.GetNumItems();
                    best_register = r;
                }
            }
            //Add customer to register with the fewest number of items
            best_register.Add(customer);
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

            //Add new waiting customers to registers
            foreach (Customer c in new_waiting_customers)
            {
                c.QueueStart(current_time);
                Add(c);
            }

            return finished_customers;
        }

        public Register[] GetRegisters()
        {
            return list_of_registers;
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

        public int GetId()
        {
            return register_id;
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
            double add_value = milliseconds_per_refresh * grng.GetRandomValue(multiplier * avg_customers, multiplier * sd_customers);
            double random_value = rand.NextDouble();
            if (random_value < add_value)
            {
                return 1;
            }
            return 0;
        }

        //Calculate multiplier
        public double CalculateMultiplier(int current_time)
        {
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
                return total_wait_time / finished_customers.Count;
            }
            return -1;

        }

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

}
