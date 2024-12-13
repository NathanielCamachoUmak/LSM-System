using LSM_prototype.MVVM.Model;
using LSM_prototype.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LSM_prototype.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddOrdersView.xaml
    /// </summary>
    public partial class AddOrdersView : UserControl
    {
        public ObservableCollection<Accounts> User { get; } = CurrentUser.Instance.User;
        public AddOrdersView()
        {
            InitializeComponent();

            if (User[0].AccessLevel == "Admin")
            {
                employeeBox.IsHitTestVisible = true;
            }
            else
            {
                employeeBox.IsHitTestVisible = false;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.CheckBox checkBox && this.DataContext is AddOrdersViewModel viewModel)
            {
                viewModel.ETAValue();
                viewModel.CalculateTotal();
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.CheckBox checkBox && this.DataContext is AddOrdersViewModel viewModel)
            {
                viewModel.ETAValue();
                viewModel.CalculateTotal();
            }
        }

        private void CheckBox_Checked2(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.CheckBox checkBox && this.DataContext is AddOrdersViewModel viewModel)
            {
                viewModel.ETAValue();
                viewModel.CalculateTotal();
                viewModel.DiscountCheckbox = true;
            }
        }

        private void CheckBox_Unchecked2(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.CheckBox checkBox && this.DataContext is AddOrdersViewModel viewModel)
            {
                viewModel.ETAValue();
                viewModel.CalculateTotal();
                viewModel.DiscountCheckbox = false;
            }
        }

        private void num_only(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^-0-9]+").IsMatch(e.Text);
        }
    }
}