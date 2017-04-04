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

        private bool areGetFillamentsAlready = false;
        private Brush DefaultColor { get; set; }

        private bool isNewFillamentAdded = false;
        public FillamentConfiguration()
        {
            InitializeComponent();
        }


        private void NewFillamentIsAdded()
        {
            this.FillamentsListBox.Items.Add(SetListboxItem(FillamentSingleton.LastAdded()));
        }

        private ListBoxItem SetListboxItem(Fillament fillament)
        {
            ListBoxItem currentItem = new ListBoxItem();
            currentItem.Content = $"{ fillament.Id }. { fillament.Name } ";

            currentItem.MouseEnter += CurrentItem_MouseEnter;
            currentItem.MouseLeave += CurrentItem_MouseLeave;

            currentItem.MouseDoubleClick += CurrentItem_MouseDoubleClick;

            return currentItem;
        }

        private void CurrentItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            isNewFillamentAdded = false;

            ListBoxItem currentItem = sender as ListBoxItem;
            this.NavigationService.Navigate(new EditFillamentPage(currentItem.Name));
        }

        private void CurrentItem_MouseLeave(object sender, MouseEventArgs e)
        {
            ListBoxItem curentItem = sender as ListBoxItem;
            curentItem.Background = DefaultColor;
        }

        private void CurrentItem_MouseEnter(object sender, MouseEventArgs e)
        {
            ListBoxItem curentItem = sender as ListBoxItem;
            DefaultColor = curentItem.Background;
            curentItem.Background = Brushes.Red;
        }

        private void AddFilamentButton_Click(object sender, RoutedEventArgs e)
        {
            isNewFillamentAdded = true;

            this.NavigationService.Navigate(new AddFilamentPage());
        }
      
        private void StartPrintingButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PrintingPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!areGetFillamentsAlready)
            {
                foreach (var fillament in FillamentSingleton.GetFillaments())
                {
                    this.FillamentsListBox.Items.Add(SetListboxItem(fillament));
                }
                areGetFillamentsAlready = true;
            }
            else
            {
                if (isNewFillamentAdded)
                {
                    NewFillamentIsAdded();
                }
            }
        }
    }
}
