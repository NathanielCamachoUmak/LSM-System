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
    public partial class ArcillaLogin : Form
    {
        public ArcillaLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = unBox.Text;
            string password = pwBox.Text;
            Accounts accountDetails = new Accounts();

            MessageBox.Show("Username: " + username + "\n" +
                            "Password: " + password);

            if (accountDetails.accountDetails.ContainsKey(username))
            {
                MessageBox.Show("username found");
                if (accountDetails.accountDetails[username] == password)
                {
                    MessageBox.Show("password match");
                }
                else
                {
                    MessageBox.Show("incorrect password");
                }
            }
            else
            {
                MessageBox.Show("username not found");
            }

            Close();
        }
    }

    class Accounts
    {
        public Dictionary<string, string> accountDetails = new Dictionary<string, string>
            {
                {"username1", "password1"},
                {"username2", "password2"},
                {"username3", "password3"},
            };
    }
}
