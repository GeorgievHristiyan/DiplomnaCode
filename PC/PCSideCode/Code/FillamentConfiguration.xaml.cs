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
    /// Interaction logic for FillamentConfiguration.xaml
    /// </summary>
    public partial class FillamentConfiguration : Page
    {
        public FillamentConfiguration()
        {
            InitializeComponent();
        }

        public FillamentConfiguration(Fillament new_filament)
        {
            FillamentSingleton.AddFillament(new_filament);
            foreach (var fillament in FillamentSingleton.GetFillaments())
            {
                this.FillamentsListBox.Items.Add(fillament.Name);
            }
        }

        private void AddFilamentButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddFilamentPage());
        }

        private void StartPrintingButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PrintingPage());
        }
    }
}
