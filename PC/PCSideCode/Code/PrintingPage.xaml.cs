using Microsoft.Win32;
using SerialCommunication;
using SerialCommunicationLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// 
    /// Да се измерят размерите на тръбите за да може да се подаде колко да се избутва(величините се хардкодват в "C")
    public partial class PrintingPage : Page
    {
        private Image BusyCicle { get; set; }

        private Image Tick { get; set; }

        private ArduinoSerialCommunication arduino;
        private PrinterSerialCommunication printer;
        private Storyboard BusyCiclesStoryBoard { get; set; }

        private Task SerialCommunicationTask { get; set; }
        private CancellationTokenSource TokenSource { get; set; }
        private CancellationToken Token { get; set; }

        private Regex temperatureTemplate = new Regex(@"T:([^.]*)");

        private int ZetaBaudRate { get; set; }

        private int PrinterBaudRate { get; set; }

        private Regex NumericTextBoxTemplate { get; set; } = new Regex("D[0-9]");

        private bool IsArduinoConnected { get; set; } = false;
        private bool IsPrinterConnected { get; set; } = false;

        private string GCodeFilePath { get; set; }

        public PrintingPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BusyCiclesStoryBoard = ((Storyboard)FindResource("BusyCicleStoryboard"));
        }

        private void SetComunicationProcess(Action task)
        {
            TokenSource = new CancellationTokenSource();
            Token = TokenSource.Token;
            SerialCommunicationTask = new Task(task, Token);

            SerialCommunicationTask.Start();
        }
       
        private void ConnectToArduino()
        {
            Dispatcher.Invoke(() =>
            {
                ConnectAgainToArduino.IsEnabled = false;
                InitBusyCicle(0);
            });

            SerialPort arduinoPort;

            if (ZetaBaudRate != 0)
            {
                arduinoPort = arduino.GetArduinoPort("Hello From Arduino", ZetaBaudRate);
            }else
            {
                arduinoPort = arduino.GetArduinoPort("Hello From Arduino");
            }

            if (arduinoPort == null)
            {
                IsArduinoConnected = false;

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
                    IsArduinoConnected = true;
                    HideBusyCicle();

                    if (IsArduinoConnected && IsPrinterConnected)
                    {
                        AutoCompleteFillamentTextBox.IsEnabled = true;
                        BrowseGCodeButton.IsEnabled = true;
                    }
                });

                TokenSource.Dispose();
            }
        }

        private void ConnectToPrinter()
        {
            Dispatcher.Invoke(() =>
            {
                ConnectTo3DPrinterAgain.IsEnabled = false;
                InitBusyCicle(1);
            });

            SerialPort printerPort;

            if (PrinterBaudRate != 0)
            {
                printerPort = printer.GetPrinterPort("start",   PrinterBaudRate);
            }
            else
            {
                printerPort = printer.GetPrinterPort("start");
            }

            if (printerPort == null)
            {
                IsPrinterConnected = false;

                TokenSource.Cancel();

                System.Windows.MessageBox.Show("Printer is undefined");

                Dispatcher.Invoke(() =>
                {
                    BusyCiclesStoryBoard.Stop(BusyCicle);
                    ConnectTo3DPrinterAgain.IsEnabled = true;
                });
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    IsPrinterConnected = true;
                    HideBusyCicle();

                    if (IsArduinoConnected && IsPrinterConnected)
                    {
                        BrowseGCodeButton.IsEnabled = true;
                        AutoCompleteFillamentTextBox.IsEnabled = true;
                    }
                });

                TokenSource.Dispose();
            }
        }

        private void InitBusyCicle(int busyCicle)
        {
            SetBusyCicle(busyCicle);
            ShowBusyCicle();
        }
        private void SetCurrentBusyCicleTick(int busyCicle)
        {
            Tick = (Image)TickGrid.Children[busyCicle];
        }
        private void SetBusyCicle(int busyCicle)
        {
            BusyCicle = (Image)BusyCicleGrid.Children[busyCicle];
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.Angle = 0;
            rotateTransform.CenterX = 15;
            rotateTransform.CenterY = 15;
            BusyCicle.RenderTransform = rotateTransform;

            SetCurrentBusyCicleTick(busyCicle);
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
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (AutoCompleteFillamentTextBox.Background == Brushes.OrangeRed)
            {
                MessageBox.Show("You have to enter a valid fillament");
            }else
            {
                SetComunicationProcess(() => PullInMaterial());
            }
        }

        //Have to Rework it!
        private void PullInMaterial()
        {
            Dispatcher.Invoke(() =>
            {
                InitBusyCicle(2);
                arduino.ArduinoDataSend($"{ AutoCompleteFillamentTextBox.Text[0] }");
            });

            bool isMaterialExists = false;
            while (!isMaterialExists)
            {
                Thread.Sleep(5000);

                string pullingInMessage = arduino.ArduinoDataReceive();

                if (pullingInMessage.Contains("Ready for printing\r"))
                {
                    isMaterialExists = true;
                }else if(pullingInMessage.Contains("No Fillament\r"))
                {
                    MessageBox.Show("There is not a material, please reload it and try again.");
                    break;
                }
            }

            if (isMaterialExists)
            {
                Dispatcher.Invoke(() =>
                {
                    HideBusyCicle();
                });

                StartPrinting();
            }
        }

       

        private void StartPrinting()
        {
            Dispatcher.Invoke(() =>
            {
                InitBusyCicle(3);
            });

            try
            {
                using (StreamReader reader = new StreamReader(GCodeFilePath))
                {
                    string line = string.Empty;
                    while((line = reader.ReadLine()) != null)
                    {
                        printer.PrinterDataSend(line);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File not found");
            }
            catch (FileFormatException)
            {
                MessageBox.Show("File format is incorret");
            }

            Dispatcher.Invoke(() =>
            {
                HideBusyCicle();
            });

            CoolingTemperature();
        }

        private void CoolingTemperature()
        {
            Dispatcher.Invoke(() =>
            {
                InitBusyCicle(4);
            });

            string temperature = string.Empty;
            while (true)
            {
                printer.PrinterDataSend("M105;");
                Thread.Sleep(5000);
                temperature = printer.PrinterDataReceive();
                if (temperatureTemplate.IsMatch(temperature))
                {
                    var something = temperatureTemplate.Match(temperature).Groups;
                    if (int.Parse(temperatureTemplate.Match(temperature).Groups[1].ToString()) <= 20)
                    {
                        break;
                    }
                }
            }

            Dispatcher.Invoke(() =>
            {
                HideBusyCicle();
            });

            PullOutFillament();
        }

        private void PullOutFillament()
        {
            Dispatcher.Invoke(() =>
            {
                InitBusyCicle(5);
            });

            arduino.ArduinoDataSend("4");

            //Wait until get "Ready with pulling out"
            while (true)
            {
                Thread.Sleep(5000);

                string pullOutMessage = arduino.ArduinoDataReceive();

                if (pullOutMessage.Contains("Ready with pulling out\r"))
                {
                    break;
                }else if(pullOutMessage.Contains("No Fillament\r"))
                {
                    MessageBox.Show("There is not a material to pulling out");
                    break;
                }
            }

            Dispatcher.Invoke(() =>
            {
                HideBusyCicle();
            });

            TokenSource.Dispose();
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
            arduino = new ArduinoSerialCommunication("0");

            SetComunicationProcess(() => ConnectToArduino());
        }

        private void ConnectTo3DPrinterAgain_Click(object sender, RoutedEventArgs e)
        {
            printer = new PrinterSerialCommunication("M115;");

            SetComunicationProcess(() => ConnectToPrinter());
        }

        private void BaudRateButton_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void BrowseGCodeButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog gCodeFile = new OpenFileDialog();
            gCodeFile.Filter = "Text files (*.gcode) | *.gcode";
            try
            {
                if ((bool)gCodeFile.ShowDialog())
                {
                    if (gCodeFile.FileName != string.Empty)
                    {
                        GCodeFilePath = gCodeFile.FileName;
                        if (AutoCompleteFillamentTextBox.Background == Brushes.LightGreen)
                        {
                            StartButton.IsEnabled = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Dialog coud not be opened, please try again");
            }
        }

        private void SetPrinterBaudRate()
        {
            if (PrinterRateTextBox.Text != string.Empty)
            {
                PrinterBaudRate = int.Parse(PrinterRateTextBox.Text);
            }else
            {
                PrinterBaudRate = 0;
            }
        }
        private void PrinterMenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            SetPrinterBaudRate();
        }
        private void PrinterMenuItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetPrinterBaudRate();
        }
        private void PrinterRateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            CheckForOnlyNumbers(e);
        }

        private void SetZetaBaudRate()
        {
            if (ZetaRateTextBox.Text != string.Empty)
            {
                ZetaBaudRate = int.Parse(ZetaRateTextBox.Text);
            }else
            {
                ZetaBaudRate = 0;
            }
        }
        private void ZetaMenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            SetZetaBaudRate();
        }
        private void ZetaMenuItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetZetaBaudRate();
        }
        private void ZetaRateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            CheckForOnlyNumbers(e);
        }

        private void CheckForOnlyNumbers(KeyEventArgs e)
        {
            e.Handled = !NumericTextBoxTemplate.IsMatch(e.Key.ToString());
        }
    }
}