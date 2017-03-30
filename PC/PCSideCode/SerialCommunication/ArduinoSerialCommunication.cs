using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunication
{
    public class ArduinoSerialCommunication
    {

        private bool isarduinofinded = false;
        
        public ArduinoSerialCommunication() {

            ArduinoPort = GetArduinoPort();

        }    
        
        public SerialPort ArduinoPort { get; set; }

        public string Receive()
        {
            try
            {
                return ArduinoPort.ReadLine();
            }catch (Exception)
            {
                //
                throw;
            }
        }

        public void Send(string message)
        {
            try
            {
                ArduinoPort.WriteLine(message);
            }
            catch (Exception)
            {
                //
                throw;
            }
        }
        private SerialPort GetArduinoPort()
        {
            string[] portsname = SerialPort.GetPortNames();
            SerialPort arduinoport = new SerialPort();
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
                        using (ArduinoPort)
                        {
                            ArduinoPort.Open();

                            ArduinoPort.DataReceived += ArduinoPort_DataReceived1;

                            Send(HandshakeCommands.HelloFromPC.ToString());

                            if (isarduinofinded)
                            {
                                return ArduinoPort;
                            }
                        }
                    }
                }
                //
                throw new Exception();
            }
            //
            catch (Exception)
            {
                throw;
            }
        }

        private void ArduinoPort_DataReceived1(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort arduinoPin = (SerialPort)sender;
            if (Receive().Equals("Hello From Arduino"))
            {
                isarduinofinded = true;
            }
        }
    }
}
