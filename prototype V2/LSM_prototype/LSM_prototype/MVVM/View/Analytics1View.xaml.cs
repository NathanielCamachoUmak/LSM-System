using System.Windows.Controls;

namespace LSM_prototype.MVVM.View
{
    /// <summary>
    /// Interaction logic for Analytics1View.xaml
    /// </summary>
    public partial class Analytics1View : UserControl
    {
        public Analytics1View()
        {
            InitializeComponent();
            MyPieChart1.DataTooltip = null;
            MyPieChart2.DataTooltip = null;
        }
    }
}
