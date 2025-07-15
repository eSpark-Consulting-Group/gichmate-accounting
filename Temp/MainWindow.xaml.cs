using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashboardWPFApp12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Start(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Register(object sender, RoutedEventArgs e)
{
    RegisterWindow registerWindow = new RegisterWindow();
    registerWindow.Show(); // Use ShowDialog() if you want it to be modal
}

        private void Btn_Login(object sender, RoutedEventArgs e)
        {

        }
    }
}