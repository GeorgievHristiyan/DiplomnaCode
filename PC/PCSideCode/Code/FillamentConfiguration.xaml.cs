using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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

        private int PrevFillamentsCount { get; set; } = 0;

        private ListBoxItem CurrentEditableListBoxItem { get; set; }
        public FillamentConfiguration()
        {
            InitializeComponent();
        }

        private void NewFillamentIsAdded()
        {
            PrevFillamentsCount++;
            this.FillamentsListBox.Items.Add(SetListboxItem(FillamentSingleton.LastAdded()));
        }

        private ListBoxItem SetListboxItem(Fillament fillament)
        {
            ListBoxItem currentItem = new ListBoxItem();
            currentItem.Content = $"{ fillament.Id }. { fillament.Name }";

            currentItem.MouseDoubleClick += CurrentItem_MouseDoubleClick;

            return currentItem;
        }

        private void CurrentItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CurrentEditableListBoxItem = sender as ListBoxItem;
            this.NavigationService.Navigate(new EditFillamentPage(CurrentEditableListBoxItem.Content.ToString()));
        }

        private void AddFilamentButton_Click(object sender, RoutedEventArgs e)
        {
            if (FillamentSingleton.FillamentsCount >= 3)
            {
                System.Windows.MessageBox.Show("You can not add more than 3 fillaments");
            }else
            {
                this.NavigationService.Navigate(new AddFilamentPage());
            }
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
                PrevFillamentsCount = FillamentSingleton.FillamentsCount;
            }
            else if (PrevFillamentsCount < FillamentSingleton.FillamentsCount)
            {
                NewFillamentIsAdded();
            }
            else if (FillamentSingleton.IsFillamentChanged)
            {
                CurrentEditableListBoxItem.Content = $"{ FillamentSingleton.UpdatedFillament.Id }. { FillamentSingleton.UpdatedFillament.Name }";
            }
        }
    }
}
