using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCode
{
    public class GCodeReader : StreamReader
    {
        public GCodeReader(string path) : base(path)
        {

        }
    }
}
