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

namespace AccountsPRO.MVVM.View.Master
{
    /// <summary>
    /// Interaction logic for TransactionWithoutInventory.xaml
    /// </summary>
    public partial class TransactionWithoutInventory : Window
    {
        public TransactionWithoutInventory()
        {
            InitializeComponent();
        }

        private void TransactionAmount_TextPreviewInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
            // this is here because it will now only allow numeric data to be enterered.
        }



    }
}
