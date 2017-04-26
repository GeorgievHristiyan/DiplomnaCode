using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SerialCommunicationLibrary
{
    public abstract class SerialComunication
    {
        private List<int> mostUsedBaudRates = new List<int>() { 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 28800, 38400, 57600, 115200, 250000 };
        public SerialComunication(string deviceMessage)
        {
            DeviceMessage = deviceMessage;
        }

        private  string DeviceMessage { get; set; }
        private  SerialPort Device { get; set; }
        private string RecievedData { get; set; } = string.Empty;

        private AutoResetEvent DataRecievied { get; set; }

        public SerialPort GetDevice(string answerMessage)
        {
            string[] portsname = SerialPort.GetPortNames();

            foreach (var portname in portsname)
            {
                MessageBox.Show("Port");
                foreach (var baudrate in mostUsedBaudRates)
                {
                    MessageBox.Show("Baud");
                    SetDevicePort(portname, baudrate);

                    if (IsDeviceFound(answerMessage))
                    {
                        return Device;
                    }
                }
            }
            
            return null;
        }

        private void SetDevicePort(string portName, int baudRate)
        {
            Device = new SerialPort(portName, baudRate);
            DataRecievied = new AutoResetEvent(false);
            Device.DataReceived += Device_DataReceived;
        }

        private bool IsDeviceFound(string answerMessage)
        {
            if (!Device.IsOpen)
            {
                Device.Open();

                Send(DeviceMessage);

                DataRecievied.WaitOne(2000);

                if (ChekForDevice(answerMessage))
                {
                    return true;
                }

                Device.Close();

                return false;
            }
            return false;
        }
        public SerialPort GetDevice(string answerMessage, int baudRate)
        {
            string[] portsname = SerialPort.GetPortNames();

            foreach (var portname in portsname)
            {

                SetDevicePort(portname, baudRate);

                if (IsDeviceFound(answerMessage))
                {
                    return Device;
                }
            }
            return null;
        }

        private void Device_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            MessageBox.Show("Here");
            RecievedData = ((SerialPort)sender).ReadLine();
        }

        private bool ChekForDevice(string checkMessage)
        {
            return RecievedData.Equals(checkMessage + "\r");
        }
        public string Receive()
        {
            try
            {
                return Device.ReadLine();
            }
            catch (Exception)
            {
                //
                throw;
            }
            finally
            {
                Device.Close();
            }
        }

        public  void Send(string message)
        {
            try
            {
                if (Device.IsOpen)
                {
                    Device.Write(message);
                }
                else
                {
                    Device.Open();
                    Device.Write(message);
                }
            }
            catch (Exception)
            {
                //
                throw;
            }
        }
    }
}
