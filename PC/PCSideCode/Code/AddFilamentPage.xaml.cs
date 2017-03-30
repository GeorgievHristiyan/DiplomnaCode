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
    /// Interaction logic for AddFilamentPage.xaml
    /// </summary>
    public partial class AddFilamentPage : Page
    {
        public AddFilamentPage()
        {
            InitializeComponent();
        }

        private void AddFilamentButton_Click(object sender, RoutedEventArgs e)
        {
            Fillament new_fillament = new Fillament();
            new_fillament.Name = this.NameTextBox.Text;
            new_fillament.Color = this.ColorTextBox.Text;
            new_fillament.Length = int.Parse(this.LengthTextBox.Text);
            new_fillament.Material = this.MaterialTextBox.Text;

            this.NavigationService.Navigate(new FillamentConfiguration(new_fillament));
        }
    }
}
