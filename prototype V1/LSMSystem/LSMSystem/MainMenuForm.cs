namespace LSMSystem
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ArcillaLogin loginForm = new ArcillaLogin();
            loginForm.ShowDialog();
        }
    }
}
