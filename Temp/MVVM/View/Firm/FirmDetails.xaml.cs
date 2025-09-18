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
using System.Windows.Shapes;

namespace AccountsPRO.MVVM.View.Firm
{
    /// <summary>
    /// Interaction logic for FirmDetails.xaml
    /// </summary>
    public partial class FirmDetails : Window
    {
        public FirmDetails()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var financialYear = new FinancialYearView();
            financialYear.Show();
            Close();
        }
    }
}
