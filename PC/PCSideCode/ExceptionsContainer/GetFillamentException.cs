using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExceptionsContainer
{
    public class GetFillamentException : Exception
    {
        public GetFillamentException(string message)
            :base(message)
        {
            MessageBox.Show(message);
        }
    }
}
