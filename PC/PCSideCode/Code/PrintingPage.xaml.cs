using SerialCommunication;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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

        private ArduinoSerialCommunication arduino;
        private Storyboard BusyCicleStoryBoard { get; set; }

        private int BusyCicleNumber { get; set; } = 0;
        public PrintingPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BusyCicleStoryBoard = ((Storyboard)FindResource("BusyCicleStoryboard"));
            arduino = new ArduinoSerialCommunication();
            Thread serialCommunicationThread = new Thread(() => StartCommunicationProcess());
            serialCommunicationThread.Start();
        }
        private void InitBusyCicle()
        {
            SetBusyCicle();
            ShowBusyCicle();
        }
        private void StartCommunicationProcess()
        {

            Dispatcher.Invoke(() =>
            {
                InitBusyCicle();
            });

            EnableArduino();
        }

        private void EnableArduino()
        {
            SerialPort arduinoPort = arduino.GetArduinoPort();
            if (arduinoPort == null)
            {
                //Aruino is undefined 
                throw new Exception();
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    HideBusyCicle();
                });
                //TODO : Change labels!
                //EnablePrinter();
            }
        }

        private void EnablePrinter()
        {
            InitBusyCicle();
            Thread.Sleep(300);
            HideBusyCicle();
        }

        private void SetBusyCicle()
        {
            BusyCicle = (Image)ResourcesGrid.Children[BusyCicleNumber];
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.Angle = 0;
            rotateTransform.CenterX = 15;
            rotateTransform.CenterY = 15;
            BusyCicle.RenderTransform = rotateTransform;

            BusyCicleNumber++;
        }

        private void ShowBusyCicle()
        {
            BusyCicleStoryBoard.Begin(BusyCicle);
        }

        private void HideBusyCicle()
        {
            BusyCicleStoryBoard.Stop();
            BusyCicle.Visibility = Visibility.Hidden;
            
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            InitBusyCicle();
            arduino.Send($"{ HandshakeCommands.ColorIsSelected.ToString() }-{ textBox.Text }");
        }
    }
}
