using SerialCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Code
{
    /// <summary>
    /// Interaction logic for PrintingPage.xaml
    /// </summary>
    public partial class PrintingPage : Page
    {
        private Image BusyCicle { get; set; }

        private ArduinoSerialCommunication arduino = new ArduinoSerialCommunication();
        private Storyboard BusyCicleStoryBoard { get; set; }
        public PrintingPage()
        {
            InitializeComponent();
            BusyCicleStoryBoard = ((Storyboard)FindResource("BusyCicleStoryboard"));
            StartCommunicationProcess();

            //((Storyboard)FindResource("WaitStoryboard")).Begin();
        }

        private void StartCommunicationProcess()
        {
            //Multi-threaded for every busyCicle one thread start!
            Thread waitingBusyCicle = new Thread(() => EnableArduino());

            SetBusyCicle();
            ShowBusyCicle();

            waitingBusyCicle.Start();
        }

        private void EnableArduino()
        {
            if (arduino.ArduinoPort == null)
            {
                //Aruino is undefined 
                throw new Exception();
            }else
            {
                Thread.Sleep(3000);
                HideBusyCicle();
                //TODO : Change labels!
                EnablePrinter();
            }
        }

        private void EnablePrinter()
        {

        }

        private void SetBusyCicle(string busyCicleName)
        {
            BusyCicle = ((Image)FindResource(busyCicleName));
            RotateTransform random = new RotateTransform();// AngleProperty
            random.Angle = 0;
            random.CenterX = 16;
            random.CenterX = 15.6;
            BusyCicle.RenderTransform = random;
            
        }

        private void ShowBusyCicle() {
            BusyCicleStoryBoard.Begin(BusyCicle);
        }

        private void HideBusyCicle()
        {
            BusyCicleStoryBoard.Stop();
            BusyCicle.Visibility = Visibility.Hidden;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
