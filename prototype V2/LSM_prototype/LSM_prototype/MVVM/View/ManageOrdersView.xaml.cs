using iText.Forms.Form.Element;
using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.ViewModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace LSM_prototype.MVVM.View
{
    /// <summary>
    /// Interaction logic for ManageOrdersView.xaml
    /// </summary>
    public partial class ManageOrdersView : UserControl
    {
        public ManageOrdersView()
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
                    row1.Height = new GridLength(2.75, GridUnitType.Star);
                    row2.Height = new GridLength(0, GridUnitType.Star);
                }
                else
                {
                    row1.Height = new GridLength(1, GridUnitType.Star);
                    row2.Height = new GridLength(9, GridUnitType.Star);
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.CheckBox checkBox && this.DataContext is ManageOrdersViewModel viewModel)
            {
                viewModel.ETAValue();
                viewModel.CalculateTotal();
                viewModel.DiscountCheckbox = true;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.CheckBox checkBox && this.DataContext is ManageOrdersViewModel viewModel)
            {
                viewModel.ETAValue();
                viewModel.CalculateTotal();
                viewModel.DiscountCheckbox = false;
            }
        }
    }
}