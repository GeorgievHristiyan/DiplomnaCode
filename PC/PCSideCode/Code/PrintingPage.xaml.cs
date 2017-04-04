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

        private List<string>.Enumerator memoryService = new InMemoryDataService().GetBusyCicles().GetEnumerator();
        public PrintingPage()
        {
            InitializeComponent();
            BusyCicleStoryBoard = ((Storyboard)FindResource("BusyCicleStoryboard"));

            Thread communicationProcess = new Thread(() => StartCommunicationProcess());
            communicationProcess.Start();
            ((Storyboard)FindResource("WaitStoryboard")).Begin();
        }


        private void InitBusyCicle()
        {
            SetBusyCicle(memoryService.Current);
            memoryService.MoveNext();
            ShowBusyCicle();
        }
        private void StartCommunicationProcess()
        {
            InitBusyCicle();
            EnableArduino();
        }

        private void EnableArduino()
        {
            if (arduino.ArduinoPort == null)
            {
                //Aruino is undefined 
                throw new Exception();
            }
            else
            {
                Thread.Sleep(300);
                HideBusyCicle();
                //TODO : Change labels!
                EnablePrinter();
            }
        }

        private void EnablePrinter()
        {
            InitBusyCicle();
            Thread.Sleep(300);
            HideBusyCicle();
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
