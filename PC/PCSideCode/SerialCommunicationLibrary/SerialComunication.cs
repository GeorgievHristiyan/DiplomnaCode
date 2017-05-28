using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace SerialCommunicationLibrary
{
    public abstract class SerialComunication
    {
        private List<int> mostUsedBaudRates = SerialCommunicationBaudRatesConfiguration.BaudRates;
        public SerialComunication(string deviceMessage)
        {
            DeviceMessage = deviceMessage;
        }

        private  string DeviceMessage { get; set; }
        private  SerialPort Device { get; set; }
        private string RecievedData { get; set; } = string.Empty;

        private AutoResetEvent DataRecievied { get; set; }

        protected SerialPort GetDevice(string answerMessage)
        {
            string[] portsname = SerialPort.GetPortNames();

            foreach (var portname in portsname)
            {
                foreach (var baudrate in mostUsedBaudRates)
                {
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
            Device.DtrEnable = true;
        }

        private bool IsDeviceFound(string answerMessage)
        {
            try
            {
                if (!Device.IsOpen)
                {
                    Device.Open();

                    DataRecievied.WaitOne(2000);

                    RecievedData = Device.ReadExisting();

                    if (RecievedData.Equals(string.Empty))
                    {
                        DataRecievied.WaitOne(2000);

                        Send(DeviceMessage);

                        RecievedData = Device.ReadLine();
                    }

                    if (ChekForDevice(answerMessage))
                    {
                        return true;
                    }

                    Device.Close();

                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected SerialPort GetDevice(string answerMessage, int baudRate)
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

        private bool ChekForDevice(string checkMessage)
        {
            if (RecievedData.Contains(checkMessage))
            {
                return true;
            }
            return false;
        }

        public void Close()
        {
            Device.Close();
        }
        protected string Receive()
        {
            try
            {
                return Device.ReadLine();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected  void Send(string message)
        {
            try
            {
                if (Device.IsOpen)
                {
                    Device.WriteLine(message);
                }
                else
                {
                    Device.Open();
                    Device.WriteLine(message);
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
