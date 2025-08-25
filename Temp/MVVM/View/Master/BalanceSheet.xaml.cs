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

namespace FlexMold.MVVM.View.Master
{
    /// <summary>
    /// Interaction logic for BalanceSheet.xaml
    /// </summary>
    public partial class BalanceSheet : Window
    {
        public BalanceSheet()
        {
            InitializeComponent();
        }

        private void ClearWindow_Click(object sender, RoutedEventArgs e)
        {
            DatePickerBox.SelectedDate = null;
            OpeningValidationComboBox.SelectedIndex = -1;
            ClosingValidationComboBox.SelectedIndex = -1;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
