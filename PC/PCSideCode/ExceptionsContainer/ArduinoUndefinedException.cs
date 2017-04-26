using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExceptionsContainer
{
    public class ArduinoUndefinedException : Exception
    {
        public ArduinoUndefinedException(string message)
            :base(message)
        {
            MessageBox.Show(message);
        }
    }
}
