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
    /// Interaction logic for DebitNoteView.xaml
    /// </summary>
    public partial class DebitNoteView : UserControl
    {
        public DebitNoteView()
        {
            InitializeComponent();
        }

        private void ToggleFields_Click(object sender, RoutedEventArgs e)
        {
            FieldContainer.Visibility = FieldContainer.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
    }
}
