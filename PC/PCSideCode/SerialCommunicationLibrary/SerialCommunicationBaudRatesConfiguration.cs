using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunicationLibrary
{
    public static class SerialCommunicationBaudRatesConfiguration
    {
        private static List<int> baudRates = new List<int>() { 9600, 250000, 115200, 57600, 57600, 2400, 14400, 19200, 28800, 38400 };
        public static List<int> BaudRates { get { return baudRates; } }
    }
}
