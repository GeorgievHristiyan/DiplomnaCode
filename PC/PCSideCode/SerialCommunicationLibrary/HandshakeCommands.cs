using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunication
{
    public enum HandshakeCommands
    {
        HelloFromPC,
        HelloFromArduino,
        HelloFrom3DPrinter,
        ColorIsSelected,
        MaterialIsPulledIn,
        MaterialIsPulledOut,
        BeginPrinting,
        EndPrinting,
        ReadyForNextObject
    }
}
