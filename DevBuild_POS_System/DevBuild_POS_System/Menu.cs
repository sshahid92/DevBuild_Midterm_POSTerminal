using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DevBuild_POS_System
{
    public class Menu
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public Menu()
        {

        }

        public Menu(int itemID, string itemName, string category, string description, double price)
        {
            ItemID = itemID;
            ItemName = itemName;
            Category = category;
            Description = description;
            Price = price;
        }

        public List<Menu> GetMenu()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "YemenCafeMenu.csv");
            var fileContents = ReadMenu(fileName);
            return fileContents;
        }

        #region ReadCSVFile
        public List<Menu> ReadMenu(string filename)
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
        #endregion

        public Menu GetProductDetails(List<Menu> menu, int itemID)
        {
            var productDetails = new Menu();
            foreach (var item in menu)
            {
                if (item.ItemID == itemID)
                {
                    productDetails = item;                    
                }
            }
            return productDetails;
        }

        //implement SetList (writing to the csv)
    }

    
}
