using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LSM_prototype.MVVM.View
{
    /// <summary>
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : UserControl
    {
        public InventoryView()
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
                    row1.Height = new GridLength(1, GridUnitType.Star);
                    row2.Height = new GridLength(2.5, GridUnitType.Star);
                }
                else
                {
                    row1.Height = new GridLength(1, GridUnitType.Star);
                    row2.Height = new GridLength(9, GridUnitType.Star);
                }
            }
        }

        private void num_only(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^.0-9]+").IsMatch(e.Text);
        }
    }
}