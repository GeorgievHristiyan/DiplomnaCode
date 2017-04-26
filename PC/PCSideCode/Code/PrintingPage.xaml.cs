using SerialCommunication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private Image Tick { get; set; }
        private ArduinoSerialCommunication arduino;
        private Storyboard BusyCiclesStoryBoard { get; set; }

        private Task SerialCommunicationTask { get; set; }
        private CancellationTokenSource TokenSource { get; set; }
        private CancellationToken Token { get; set; }

        private int BusyCicleNumber { get; set; } = 0;

        public PrintingPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BusyCiclesStoryBoard = ((Storyboard)FindResource("BusyCicleStoryboard"));
            arduino = new ArduinoSerialCommunication("0");

            SetComunicationProcess(() => ConnectToArduino());
        }

        private void SetComunicationProcess(Action task)
        {
            TokenSource = new CancellationTokenSource();
            Token = TokenSource.Token;
            SerialCommunicationTask = new Task(task, Token);

            SerialCommunicationTask.Start();
        }
        private void InitBusyCicle()
        {
            SetBusyCicle();
            ShowBusyCicle();
        }

        private void ConnectToArduino()
        {
            Dispatcher.Invoke(() =>
            {
                ConnectAgainToArduino.IsEnabled = false;
                InitBusyCicle();
            });

            SerialPort arduinoPort = arduino.GetArduinoPort("Hello From Arduino", 9600);

            if (arduinoPort == null)
            {
                TokenSource.Cancel();

                System.Windows.MessageBox.Show("Arduino is undefined");

                Dispatcher.Invoke(() =>
                {
                    BusyCiclesStoryBoard.Stop(BusyCicle);
                    ConnectAgainToArduino.IsEnabled = true;
                });
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    AutoCompleteFillamentTextBox.IsEnabled = true;
                    StartButton.IsEnabled = true;
                    HideBusyCicle();
                });
                //TODO : Change labels!
                //EnablePrinter();
            }
        }

        private void ConnectToPrinter()
        {
            InitBusyCicle();
            Thread.Sleep(300);
            HideBusyCicle();
        }


        private void SetCurrentBusyCicleTick()
        {
            Tick = (Image)TickGrid.Children[BusyCicleNumber];
        }
        private void SetBusyCicle()
        {
            BusyCicle = (Image)BusyCicleGrid.Children[BusyCicleNumber];
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.Angle = 0;
            rotateTransform.CenterX = 15;
            rotateTransform.CenterY = 15;
            BusyCicle.RenderTransform = rotateTransform;

            SetCurrentBusyCicleTick();
        }

        private void ShowBusyCicle()
        {
            BusyCiclesStoryBoard.Begin(BusyCicle, true);
        }

        private void HideBusyCicle()
        {
            BusyCiclesStoryBoard.Stop(BusyCicle);
            BusyCicle.Visibility = Visibility.Hidden;
            Tick.Visibility = Visibility.Visible;

            BusyCicleNumber++;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (AutoCompleteFillamentTextBox.Background == Brushes.OrangeRed)
            {
                MessageBox.Show("You have to enter a valid fillament");
            }else
            {
                arduino.ArduinoDataSend($"{ AutoCompleteFillamentTextBox.Text[0] }");
            }
        }

        private void AutoCompleteFillamentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string autoCompletedtext = (sender as TextBox).Text;

            resultStack.Children.Clear();
            

            foreach (Fillament fillament in FillamentSingleton.GetFillaments())
            {
                if (fillament.Name.ToLower().Contains(autoCompletedtext.ToLower()))
                {
                    if (autoCompletedtext == string.Empty)
                    {
                        AutoCompleteFillamentTextBox.Background = Brushes.OrangeRed;
                        AutoCompleteBorder.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        AutoCompleteBorder.Visibility = Visibility.Visible;
                    }
                    ShowAutoCompleteSuggestions($"{fillament.Id}. {fillament.Name}");
                }
            }
            if (resultStack.Children.Count == 0)
            {
                AutoCompleteFillamentTextBox.Background = Brushes.OrangeRed;
                AutoCompleteBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void ShowAutoCompleteSuggestions(string fillamentName)
        {
            TextBlock autoCompletedFillamentItem = new TextBlock();

            autoCompletedFillamentItem.Text = fillamentName;

            autoCompletedFillamentItem.MouseLeftButtonDown += (sender, e) =>
            {
                AutoCompleteFillamentTextBox.Text = (sender as TextBlock).Text;
                AutoCompleteFillamentTextBox.Background = Brushes.LightGreen;

                AutoCompleteBorder.Visibility = Visibility.Collapsed;
            };

            autoCompletedFillamentItem.MouseEnter += (sender, e) =>
            {
                autoCompletedFillamentItem.Background = Brushes.Aqua;
            };

            autoCompletedFillamentItem.MouseLeave += (sender, e) =>
            {
                autoCompletedFillamentItem.Background = Brushes.White;
            };

            resultStack.Children.Add(autoCompletedFillamentItem);
        }

        private void ConnectAgainToArduino_Click(object sender, RoutedEventArgs e)
        {
            SetComunicationProcess(() => ConnectToArduino());
        }
    }
}
