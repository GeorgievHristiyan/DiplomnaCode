using ExceptionsContainer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Code
{
    public class FillamentSingleton
    {
        private static List<Fillament> fillaments = new List<Fillament>();

        public static int FillamentsCount
        {
            get
            {
                return fillaments.Count;
            } 
        }

        public static Fillament GetFillament(int id)
        {
            try
            {
                return GetFillaments().Where((f) => f.Id == id).First();
            }
            catch (Exception)
            {
                throw new GetFillamentException("The Fillament you are looking for, does not exist");
            }
        }

        public static void AddFillament(Fillament new_fillament)
        {
            try
            {
                fillaments.Add(new_fillament);
            }
            catch (Exception ex)
            {
                throw new AddFilamentException(ex.Message);
            }
        }
        
        public static Fillament LastAdded()
        {
            return fillaments.Count == 0 ? null : fillaments.Last();
        }

        private static bool IsForFirstTimeGetFillaments { get; set; } = true;
        public static List<Fillament> GetFillaments()
        {
            try
            {
                if (IsForFirstTimeGetFillaments)
                {
                    using (StreamReader reader = new StreamReader(ConfigurationManager.AppSettings["FillamentsFilePath"]))
                    {
                        string line = string.Empty;
                        while ((line = reader.ReadLine()) != null)
                        {
                            CreateNewFillament(line);
                        }
                      
                    }
                    IsForFirstTimeGetFillaments = false;
                    return fillaments;
                }
                else
                {
                    return fillaments;
                }
            }
            catch (Exception)
            {
                //Move it something else
                using (StreamWriter writer = new StreamWriter(ConfigurationManager.AppSettings["FillamentsFilePath"]))
                {
                    writer.Write("");
                }
                return GetFillaments();
            }
        }

        private static string[] FillamentFields { get; set; }

        private static void CreateNewFillament(string fillament)
        {
            Fillament newFillament = new Fillament();

            FillamentFields = fillament.Split(',');

            newFillament.Id = int.Parse(FillamentFields[0]);
            newFillament.Name = FillamentFields[1];
            newFillament.Color = FillamentFields[2];
            newFillament.Length = FillamentFields[3];
            newFillament.Material = FillamentFields[4];

            AddFillament(newFillament);
        }

        public static bool IsFillamentChanged { get; set; } = false;

        public static Fillament UpdatedFillament { get; set; }
    }
}
