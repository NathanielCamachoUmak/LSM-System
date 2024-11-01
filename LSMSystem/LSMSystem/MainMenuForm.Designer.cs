using System;
using System.Drawing;
using System.Windows.Forms;

namespace LSMSystem
{
    public partial class MainMenuForm : Form
    {
        private string userRole;

        // Constructor accepts the user role and customizes the UI accordingly
        public MainMenuForm(string role)
        {
            InitializeComponent();
            userRole = role;

            // Check user role and customize the UI accordingly
            if (userRole == "Employee")
            {
                // Hide admin-only functionalities
                btnReports.Enabled = false;
                btnEmployeeCustomer.Enabled = false;
            }
        }

        private void InitializeComponent()
        {
            // Initialize buttons and form layout
            btnEmployeeCustomer = new Button();
            btnTaskAssignment = new Button();
            btnServiceRecords = new Button();
            btnReports = new Button();
            SuspendLayout();

            // 
            // btnEmployeeCustomer
            // 
            btnEmployeeCustomer.Location = new Point(50, 30);
            btnEmployeeCustomer.Name = "btnEmployeeCustomer";
            btnEmployeeCustomer.Size = new Size(200, 50);
            btnEmployeeCustomer.TabIndex = 0;
            btnEmployeeCustomer.Text = "Employee & Customer Management";
            btnEmployeeCustomer.Click += BtnEmployeeCustomer_Click;

            // 
            // btnTaskAssignment
            // 
            btnTaskAssignment.Location = new Point(50, 90);
            btnTaskAssignment.Name = "btnTaskAssignment";
            btnTaskAssignment.Size = new Size(200, 50);
            btnTaskAssignment.TabIndex = 1;
            btnTaskAssignment.Text = "Task Assignment & Tracking";
            btnTaskAssignment.Click += BtnTaskAssignment_Click;

            // 
            // btnServiceRecords
            // 
            btnServiceRecords.Location = new Point(50, 150);
            btnServiceRecords.Name = "btnServiceRecords";
            btnServiceRecords.Size = new Size(200, 50);
            btnServiceRecords.TabIndex = 2;
            btnServiceRecords.Text = "Service Records";
            btnServiceRecords.Click += BtnServiceRecords_Click;

            // 
            // btnReports
            // 
            btnReports.Location = new Point(50, 210);
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(200, 50);
            btnReports.TabIndex = 3;
            btnReports.Text = "Reports Generation";
            btnReports.Click += BtnReports_Click;

            // 
            // MainMenuForm
            // 
            ClientSize = new Size(300, 300);
            Controls.Add(btnEmployeeCustomer);
            Controls.Add(btnTaskAssignment);
            Controls.Add(btnServiceRecords);
            Controls.Add(btnReports);
            Name = "MainMenuForm";
            Text = "Laptop Service Management System";
            ResumeLayout(false);
        }

        // Event handler for Employee & Customer Management button
        private void BtnEmployeeCustomer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Navigate to Employee & Customer Management module.");
            // Code to open the Employee & Customer Management Form here
        }

        // Event handler for Task Assignment & Tracking button
        private void BtnTaskAssignment_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Navigate to Task Assignment & Tracking module.");
            // Code to open the Task Assignment Form here
        }

        // Event handler for Service Records button
        private void BtnServiceRecords_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Navigate to Service Records module.");
            // Code to open the Service Records Form here
        }

        // Event handler for Reports Generation button
        private void BtnReports_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Navigate to Reports Generation module.");
            // Code to open the Reports Generation Form here
        }

        // Button declarations
        private Button btnEmployeeCustomer;
        private Button btnTaskAssignment;
        private Button btnServiceRecords;
        private Button btnReports;
    }
}
