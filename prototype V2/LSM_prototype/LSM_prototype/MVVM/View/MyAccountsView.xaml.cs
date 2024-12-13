using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace LSM_prototype.MVVM.View
{
    /// <summary>
    /// Interaction logic for MyAccounts.xaml
    /// </summary>
    public partial class MyAccountsView : UserControl
    {
        public MyAccountsView()
        {
            InitializeComponent();
        }

        private void num_only(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^-0-9]+").IsMatch(e.Text);
        }
    }
}
