using LSM_prototype.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
    /// Interaction logic for AddOrdersView.xaml
    /// </summary>
    public partial class AddOrdersView : UserControl
    {
        public AddOrdersView()
        {
            InitializeComponent();
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
    }
}