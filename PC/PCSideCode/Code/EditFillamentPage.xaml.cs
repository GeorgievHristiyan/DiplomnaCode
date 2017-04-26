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
    /// Interaction logic for EditFillamentPage.xaml
    /// </summary>
    public partial class EditFillamentPage : Page
    {

        public EditFillamentPage(string fillament)
        {
            InitializeComponent();
            FillFillamentInfo(fillament);
            FillamentSingleton.IsFillamentChanged = false;
        }

        private void FillFillamentInfo(string current_fillament)
        {
            int currentFillamentId = int.Parse(current_fillament.Split('.').First());

            FillamentSingleton.UpdatedFillament = FillamentSingleton.GetFillament(currentFillamentId);

            this.NameTextBox.Text = FillamentSingleton.UpdatedFillament.Name;
            this.ColorTextBox.Text = FillamentSingleton.UpdatedFillament.Color;
            this.LengthTextBox.Text = FillamentSingleton.UpdatedFillament.Length.ToString();
            this.MaterialTextBox.Text = FillamentSingleton.UpdatedFillament.Material;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid current_grid = sender as Grid;
            Brush customBrush = new SolidColorBrush(Color.FromArgb(100, 165, 190, 232));
            current_grid.Background = customBrush;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid current_grid = sender as Grid;
            current_grid.Background = Brushes.White;

            TextBox currentGridTextBox = current_grid.Children.Cast<UIElement>().First() as TextBox;
            if (currentGridTextBox.IsEnabled)
            {
                currentGridTextBox.IsEnabled = false;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            grid.Children.Cast<UIElement>().First().IsEnabled = true;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            FillamentSingleton.UpdatedFillament.Name = this.NameTextBox.Text;
            FillamentSingleton.UpdatedFillament.Color = this.ColorTextBox.Text;
            FillamentSingleton.UpdatedFillament.Length = this.LengthTextBox.Text;
            FillamentSingleton.UpdatedFillament.Material = this.MaterialTextBox.Text;

            FillamentSingleton.IsFillamentChanged = true;

            this.NavigationService.GoBack();
        }
    }
}
