using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunicationLibrary
{
    public class PrinterSerialCommunication : SerialComunication
    {
        public PrinterSerialCommunication(string printerMessage)
            : base(printerMessage)
        { }

        public SerialPort GetPrinterPort(string printerAnswerMessage)
        {
            return base.GetDevice(printerAnswerMessage);
        }

        public string PrinterDataReceive()
        {
            return base.Receive();
        }

        public void PrinterDataSend(string printerMessage)
        {
            base.Send(printerMessage);
        }
    }
}
