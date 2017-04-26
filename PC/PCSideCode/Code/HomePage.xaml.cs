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

namespace Code
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    /// //Singleton Pattern TODO!
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FillamentConfiguration());
        }

        private void StartPrintingButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PrintingPage());
        }
    }
}
