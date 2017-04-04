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
using System.Windows.Shapes;

namespace Code
{
    /// <summary>
    /// Interaction logic for ResizableWin.xaml
    /// </summary>
    public partial class ResizableWin : Window
    {
        public ResizableWin()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            HomePage hp = new HomePage();
            this.Content = hp;
        }
    }
}
