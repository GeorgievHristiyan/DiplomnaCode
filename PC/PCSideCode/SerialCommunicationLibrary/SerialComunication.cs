﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunicationLibrary
{
    public abstract class SerialComunication
    {
        public SerialPort GetArduinoPort()
        {
            return new SerialPort();
        }
    }
}
