using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DevBuild_POS_System
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "YemenCafeMenu.csv");
            var fileContents = ReadMenu(fileName);

        }

        //public static string ReadFile(string fileName)
        //{
        //    using (var reader = new StreamReader(fileName))
        //    {
        //        return reader.ReadToEnd();
        //    }

        //}

        public static List<Menu> ReadMenu(string filename)
        {
            var completeMenu = new List<Menu>();

            using (var reader = new StreamReader(filename))
            {
                string line = "";
                reader.ReadLine();

                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    var menu = new Menu();

                    int itemID;
                    if (int.TryParse(values[0], out itemID))
                    {
                        menu.ItemID = itemID;
                    }

                    menu.ItemName = values[1];

                    menu.Category = values[2];

                    menu.Description = values[3];
                    
                    double price;
                    if (double.TryParse(values[4], out price))
                    {
                        menu.Price = price;
                    }

                    completeMenu.Add(menu);
                }
            }

            return completeMenu;
        }


    }
}
