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

namespace FlexMold.MVVM.View.Accounts
{
    /// <summary>
    /// Interaction logic for CashPaymentView.xaml
    /// </summary>
    public partial class CashPaymentView : UserControl
    {
        public CashPaymentView()
        {
            InitializeComponent();
        }

        private void ToggleFields_Click(object sender, RoutedEventArgs e)
        {
            FieldContainer.Visibility = FieldContainer.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Visible;
        }
    }
}
