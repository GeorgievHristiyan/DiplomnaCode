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

        public ArduinoSerialCommunication(string arduinoMessage)
            :base(arduinoMessage)
        { }
        

        public string ArduinoDataReceive()
        {
            return base.Receive();
        }

        public void ArduinoDataSend(string arduinoMessage)
        {
            base.Send(arduinoMessage);
        }
        public SerialPort GetArduinoPort(string arduinoAnswerMessage)
        {
            return base.GetDevice(arduinoAnswerMessage);
        }

        public SerialPort GetArduinoPort(string arduinoAnswerMessage, int baudRate)
        {
            return base.GetDevice(arduinoAnswerMessage, baudRate);
        }
    }
}
