using SerialCommunicationLibrary;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SerialCommunication
{
    public class ArduinoSerialCommunication : SerialComunication
    {

        private bool isarduinofinded = false;

        public ArduinoSerialCommunication() { }
        
        private SerialPort ArduinoPort { get; set; }

        public string Receive()
        {
            try
            {
                return ArduinoPort.ReadLine();
            }
            catch (Exception)
            {
                //
                throw;
            }
            finally
            {
                ArduinoPort.Close();
            }
        }

        public void Send(string message)
        {
            try
            {
                if (ArduinoPort.IsOpen)
                {
                    ArduinoPort.Write(message);
                }
                else
                {
                    ArduinoPort.Open();
                    ArduinoPort.Write(message);
                }
            }
            catch (Exception)
            {
                //
                throw;
            }
        }
        public SerialPort GetArduinoPort()
        {
            string[] portsname = SerialPort.GetPortNames();

            //TODO: 3D Printer connection
            try
            {
                foreach (var portname in portsname)
                {
                    ArduinoPort = new SerialPort(portname, 9600);
                   
                    if (ArduinoPort.IsOpen)
                    {
                        continue;
                    }
                    else
                    {
                        ArduinoPort.Open();

                        Send("Hello From Pc");// HandshakeCommands.HelloFromPC.ToString());
                        Thread.Sleep(3000);
                        ChekForArduino();

                        if (isarduinofinded)
                        {
                            return ArduinoPort;
                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ChekForArduino()
        {
            string arduinoMessage = Receive();
            if (arduinoMessage.Equals("Hello From Arduino\r"))
            {
                isarduinofinded = true;
            }
        }
    }
}
