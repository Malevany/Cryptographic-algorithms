using InfSecLab.Ciphers;
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

namespace InfSecLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SelectedCipher selectedCipher = new SelectedCipher();
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void CaesarCodeButton_Click(object sender, RoutedEventArgs e)
        {
            CaesarCodeWindow caesarCodeWindow = new CaesarCodeWindow(this, selectedCipher);
            caesarCodeWindow.Show();
            this.Hide();
        }

        private void BelazoCodeButton_Click(object sender, RoutedEventArgs e)
        {
            BelazoCodeWindow belazoCodeWindow = new BelazoCodeWindow(this, selectedCipher);
            belazoCodeWindow.Show();
            this.Hide();
        }
    }
}