using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NewStoreSim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void run_button_Click(object sender, EventArgs e)
        {
            run_button.Enabled = false;

            double avg_customers;
            double sd_customers;
            int avg_items;
            int sd_items;
            int avg_time;
            int sd_time;
            double avg_processing_time;
            double sd_processing_time;
            int num_registers;
            int milliseconds_per_refresh;
            int max_time;
            int output_frequency;

            avg_customers = Convert.ToDouble(avg_customers_per_minute.Text) / 60 / 1000;
            sd_customers = Convert.ToDouble(sd_customers_per_minute.Text) / 60 / 1000;
            avg_items = Convert.ToInt32(avg_items_per_customer.Text);
            sd_items = Convert.ToInt32(sd_items_per_customer.Text);
            avg_time = Convert.ToInt32(avg_time_per_customer.Text) * 60 * 1000;
            sd_time = Convert.ToInt32(sd_time_per_customer.Text) * 60 * 1000;
            avg_processing_time = Convert.ToDouble(avg_time_per_item.Text);
            sd_processing_time = Convert.ToDouble(sd_time_per_item.Text);
            num_registers = Convert.ToInt32(number_of_registers.Text);
            milliseconds_per_refresh = Convert.ToInt32(ms_per_refresh.Text);
            max_time = Convert.ToInt32(end_time.Text);
            output_frequency = Convert.ToInt32(output_frequency_ms.Text);


            Sim s = new Sim(avg_customers, sd_customers, avg_items, sd_items, avg_time, sd_time, avg_processing_time, sd_processing_time, num_registers, milliseconds_per_refresh, max_time, output_frequency);
            double average_queue_time = s.Run();
            avg_queue_time.Text = Convert.ToString(average_queue_time);
            run_button.Enabled = true;
        }
    }
}
