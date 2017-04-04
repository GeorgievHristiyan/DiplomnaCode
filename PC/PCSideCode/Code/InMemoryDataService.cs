using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class InMemoryDataService
    {
        private List<string> busyCiclesName = new List<string>()
        {
            "EstablishingDeviceBusyCicle",
            "EstablishingPrinterCicle",
            "PullInLabelBusyCicle",
            "PrepareCicle",
            "PrintingBusyCicle"
        };
        public List<string> GetBusyCicles()
        {
            return busyCiclesName;
        }

    }
}
