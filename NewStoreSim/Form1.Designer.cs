namespace NewStoreSim
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.avg_customers_per_minute = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.sd_customers_per_minute = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.avg_items_per_customer = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.sd_items_per_customer = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.sd_time_per_customer = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.avg_time_per_customer = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.sd_time_per_item = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.avg_time_per_item = new System.Windows.Forms.TextBox();
            this.run_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ms_per_refresh = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.number_of_registers = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.end_time = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.output_frequency_ms = new System.Windows.Forms.TextBox();
            this.avg_queue_time = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // avg_customers_per_minute
            // 
            this.avg_customers_per_minute.Location = new System.Drawing.Point(78, 84);
            this.avg_customers_per_minute.Name = "avg_customers_per_minute";
            this.avg_customers_per_minute.Size = new System.Drawing.Size(293, 38);
            this.avg_customers_per_minute.TabIndex = 0;
            this.avg_customers_per_minute.Text = "5";
            this.avg_customers_per_minute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(37, 30);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(401, 38);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "Average Customers Per Minute";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(539, 30);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(334, 38);
            this.textBox3.TabIndex = 3;
            this.textBox3.Text = "SD Customers Per Minute";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sd_customers_per_minute
            // 
            this.sd_customers_per_minute.Location = new System.Drawing.Point(558, 84);
            this.sd_customers_per_minute.Name = "sd_customers_per_minute";
            this.sd_customers_per_minute.Size = new System.Drawing.Size(293, 38);
            this.sd_customers_per_minute.TabIndex = 2;
            this.sd_customers_per_minute.Text = "1";
            this.sd_customers_per_minute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(37, 183);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(401, 38);
            this.textBox5.TabIndex = 5;
            this.textBox5.Text = "Average Items Per Customer";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // avg_items_per_customer
            // 
            this.avg_items_per_customer.Location = new System.Drawing.Point(78, 237);
            this.avg_items_per_customer.Name = "avg_items_per_customer";
            this.avg_items_per_customer.Size = new System.Drawing.Size(293, 38);
            this.avg_items_per_customer.TabIndex = 4;
            this.avg_items_per_customer.Text = "30";
            this.avg_items_per_customer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(535, 183);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(316, 38);
            this.textBox7.TabIndex = 7;
            this.textBox7.Text = "SD Items Per Customer";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sd_items_per_customer
            // 
            this.sd_items_per_customer.Location = new System.Drawing.Point(541, 237);
            this.sd_items_per_customer.Name = "sd_items_per_customer";
            this.sd_items_per_customer.Size = new System.Drawing.Size(293, 38);
            this.sd_items_per_customer.TabIndex = 6;
            this.sd_items_per_customer.Text = "10";
            this.sd_items_per_customer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(486, 325);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(387, 38);
            this.textBox9.TabIndex = 11;
            this.textBox9.Text = "SD Minutes for a Customer";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sd_time_per_customer
            // 
            this.sd_time_per_customer.Location = new System.Drawing.Point(541, 379);
            this.sd_time_per_customer.Name = "sd_time_per_customer";
            this.sd_time_per_customer.Size = new System.Drawing.Size(293, 38);
            this.sd_time_per_customer.TabIndex = 10;
            this.sd_time_per_customer.Text = "5";
            this.sd_time_per_customer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(12, 325);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(433, 38);
            this.textBox11.TabIndex = 9;
            this.textBox11.Text = "Average Minutes for a Customer";
            this.textBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // avg_time_per_customer
            // 
            this.avg_time_per_customer.Location = new System.Drawing.Point(78, 379);
            this.avg_time_per_customer.Name = "avg_time_per_customer";
            this.avg_time_per_customer.Size = new System.Drawing.Size(293, 38);
            this.avg_time_per_customer.TabIndex = 8;
            this.avg_time_per_customer.Text = "30";
            this.avg_time_per_customer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(513, 453);
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new System.Drawing.Size(338, 38);
            this.textBox13.TabIndex = 15;
            this.textBox13.Text = "SD Seconds Per Item";
            this.textBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sd_time_per_item
            // 
            this.sd_time_per_item.Location = new System.Drawing.Point(541, 507);
            this.sd_time_per_item.Name = "sd_time_per_item";
            this.sd_time_per_item.Size = new System.Drawing.Size(293, 38);
            this.sd_time_per_item.TabIndex = 14;
            this.sd_time_per_item.Text = "1";
            this.sd_time_per_item.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(51, 453);
            this.textBox15.Name = "textBox15";
            this.textBox15.ReadOnly = true;
            this.textBox15.Size = new System.Drawing.Size(373, 38);
            this.textBox15.TabIndex = 13;
            this.textBox15.Text = "Average Seconds Per Item";
            this.textBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // avg_time_per_item
            // 
            this.avg_time_per_item.Location = new System.Drawing.Point(78, 507);
            this.avg_time_per_item.Name = "avg_time_per_item";
            this.avg_time_per_item.Size = new System.Drawing.Size(293, 38);
            this.avg_time_per_item.TabIndex = 12;
            this.avg_time_per_item.Text = "3";
            this.avg_time_per_item.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // run_button
            // 
            this.run_button.Location = new System.Drawing.Point(361, 863);
            this.run_button.Name = "run_button";
            this.run_button.Size = new System.Drawing.Size(181, 73);
            this.run_button.TabIndex = 16;
            this.run_button.Text = "Run";
            this.run_button.UseVisualStyleBackColor = true;
            this.run_button.Click += new System.EventHandler(this.run_button_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(541, 586);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(316, 38);
            this.textBox1.TabIndex = 20;
            this.textBox1.Text = "Milliseconds Per Refresh";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ms_per_refresh
            // 
            this.ms_per_refresh.Location = new System.Drawing.Point(551, 640);
            this.ms_per_refresh.Name = "ms_per_refresh";
            this.ms_per_refresh.Size = new System.Drawing.Size(293, 38);
            this.ms_per_refresh.TabIndex = 19;
            this.ms_per_refresh.Text = "100";
            this.ms_per_refresh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(94, 586);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(259, 38);
            this.textBox6.TabIndex = 18;
            this.textBox6.Text = "Number of Registers";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // number_of_registers
            // 
            this.number_of_registers.Location = new System.Drawing.Point(88, 640);
            this.number_of_registers.Name = "number_of_registers";
            this.number_of_registers.Size = new System.Drawing.Size(293, 38);
            this.number_of_registers.TabIndex = 17;
            this.number_of_registers.Text = "8";
            this.number_of_registers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(622, 714);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(130, 38);
            this.textBox4.TabIndex = 24;
            this.textBox4.Text = "Max Time";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // end_time
            // 
            this.end_time.Location = new System.Drawing.Point(551, 768);
            this.end_time.Name = "end_time";
            this.end_time.Size = new System.Drawing.Size(293, 38);
            this.end_time.TabIndex = 23;
            this.end_time.Text = "39600000";
            this.end_time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(66, 714);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(330, 38);
            this.textBox10.TabIndex = 22;
            this.textBox10.Text = "Ouput frequency (ms)";
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // output_frequency_ms
            // 
            this.output_frequency_ms.Location = new System.Drawing.Point(88, 768);
            this.output_frequency_ms.Name = "output_frequency_ms";
            this.output_frequency_ms.Size = new System.Drawing.Size(293, 38);
            this.output_frequency_ms.TabIndex = 21;
            this.output_frequency_ms.Text = "900000";
            this.output_frequency_ms.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // avg_queue_time
            // 
            this.avg_queue_time.Location = new System.Drawing.Point(1145, 429);
            this.avg_queue_time.Name = "avg_queue_time";
            this.avg_queue_time.Size = new System.Drawing.Size(298, 38);
            this.avg_queue_time.TabIndex = 25;
            this.avg_queue_time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(1120, 366);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(355, 38);
            this.textBox8.TabIndex = 26;
            this.textBox8.Text = "Average Queue Time (ms)";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1607, 1335);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.avg_queue_time);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.end_time);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.output_frequency_ms);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ms_per_refresh);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.number_of_registers);
            this.Controls.Add(this.run_button);
            this.Controls.Add(this.textBox13);
            this.Controls.Add(this.sd_time_per_item);
            this.Controls.Add(this.textBox15);
            this.Controls.Add(this.avg_time_per_item);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.sd_time_per_customer);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.avg_time_per_customer);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.sd_items_per_customer);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.avg_items_per_customer);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.sd_customers_per_minute);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.avg_customers_per_minute);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox avg_customers_per_minute;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox sd_customers_per_minute;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox avg_items_per_customer;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox sd_items_per_customer;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox sd_time_per_customer;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox avg_time_per_customer;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox sd_time_per_item;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox avg_time_per_item;
        private System.Windows.Forms.Button run_button;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox ms_per_refresh;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox number_of_registers;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox end_time;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox output_frequency_ms;
        private System.Windows.Forms.TextBox avg_queue_time;
        private System.Windows.Forms.TextBox textBox8;
    }
}

