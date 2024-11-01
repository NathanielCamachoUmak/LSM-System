using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSMSystem
{
    public partial class LoginForm : Form
    {
        // Dictionary to store usernames, passwords, and roles
        private Dictionary<string, (string password, string role)> userAccounts = new Dictionary<string, (string password, string role)>
        {
            { "employee1", ("password1", "Employee") },
            { "employee2", ("password2", "Employee") },
            { "employee3", ("password3", "Employee") },
            { "admin", ("adminpassword", "Admin") }
        };

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Check if the username exists and the password is correct
            if (userAccounts.TryGetValue(username, out var userInfo) && userInfo.password == password)
            {
                lblError.Visible = false; // Hide error message if login is successful
                string role = userInfo.role;

                // Show main menu based on role
                MainMenuForm mainMenu = new MainMenuForm(role);
                mainMenu.Show();
                this.Hide(); // Hide the login form
            }
            else
            {
                lblError.Text = "Invalid username or password.";
                lblError.Visible = true;
            }
        }
    }
}
