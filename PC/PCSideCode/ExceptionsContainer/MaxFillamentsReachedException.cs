using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionsContainer
{
    public class MaxFillamentsReachedException : Exception
    {
        public MaxFillamentsReachedException(string message)
            : base(message)
        {
        }
    }
}
