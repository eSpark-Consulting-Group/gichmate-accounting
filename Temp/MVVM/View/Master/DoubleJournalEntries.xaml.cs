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
    /// Interaction logic for DoubleJournalEntries.xaml
    /// </summary>
    public partial class DoubleJournalEntries : Window
    {
        public DoubleJournalEntries()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            UpDownBox.Value = null;
            DatePickBox.SelectedDate = null;
            FromAccCreditBox.SelectedIndex = -1;
            ToAccDebitBox.SelectedIndex = -1;
            AmountBox.Clear();
            NarrationBox.Clear();
            SupplierOnlyCheckBox.IsChecked = false;
            CustomerOnlyCheckBox.IsChecked = false;
            OthersCheckBox.IsChecked = false;
            AllSelectCheckBox.IsChecked = false;

        }
    }
}
