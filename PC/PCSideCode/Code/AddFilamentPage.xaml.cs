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
        private FillamentConfiguration FillamentConfigPage { get; set; }
        public AddFilamentPage()
        {
            InitializeComponent();
        }

        private void AddFilamentButton_Click(object sender, RoutedEventArgs e)
        {
            Fillament newFillament = new Fillament();
            Fillament lastAddedFillament = FillamentSingleton.LastAdded();

            if (lastAddedFillament != null)
            {
                newFillament.Id = lastAddedFillament.Id + 1;
            }else
            {
                newFillament.Id = 1;
            }

            newFillament.Name = this.NameTextBox.Text;
            newFillament.Color = this.ColorTextBox.Text;
            newFillament.Length = int.Parse(this.LengthTextBox.Text);
            newFillament.Material = this.MaterialTextBox.Text;

            FillamentSingleton.AddFillament(newFillament);

            this.NavigationService.GoBack();
        }
    }
}
