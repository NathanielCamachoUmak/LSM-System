using LSM_prototype.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LSM_prototype.MVVM.View
{
    /// <summary>
    /// Interaction logic for AccountsView.xaml
    /// </summary>
    public partial class AccountsView : UserControl
    {
        public AccountsView()
        {
            InitializeComponent();
        }

        private void Expander_StateChanged(object sender, RoutedEventArgs e)
        {
            // Cast the sender to an Expander
            Expander expander = sender as Expander;

            if (expander != null)
            {
                if (expander.IsExpanded)
                {
                    row1.Height = new GridLength(1.15, GridUnitType.Star);
                    row2.Height = new GridLength(1, GridUnitType.Star);
                }
                else
                {
                    row1.Height = new GridLength(1, GridUnitType.Star);
                    row2.Height = new GridLength(9, GridUnitType.Star);
                }
            }
        }
    }
}
