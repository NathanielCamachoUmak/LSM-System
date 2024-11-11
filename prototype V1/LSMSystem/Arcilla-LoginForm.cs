using static System.Net.Mime.MediaTypeNames;

namespace WinformLogin
{
    public partial class ArcillaLoginForm : Form
    {
        public ArcillaLoginForm()
        {
            InitializeComponent();
            this.AcceptButton = button1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text;
            string password = passwordBox.Text;
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
