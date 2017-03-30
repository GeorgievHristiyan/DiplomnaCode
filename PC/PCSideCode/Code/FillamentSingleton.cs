using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class FillamentSingleton
    {
        private static List<Fillament> fillaments = new List<Fillament>();

        public static bool AddFillament(Fillament new_fillament)
        {
            try
            {
                fillaments.Add(new_fillament);
                return true;
            }//TODO: OWN EXCEPTIONS
            catch (Exception)
            {
                return false;
                throw;
            }
           
        }

        public static List<Fillament> GetFillaments()
        {
            return fillaments;
        }
    }
}
