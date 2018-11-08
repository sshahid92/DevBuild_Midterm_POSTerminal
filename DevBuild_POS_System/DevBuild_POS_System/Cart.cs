using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBuild_POS_System
{
    class Cart
    {
        public Menu Item { get; set; }
        public double SubTotal { get; set; }
        public double GrandTotal { get; set; }
        public double Tax { get; set; }
        public int Quantity { get; set; }

        public Cart(Menu item, int quantity)
        {
            Item = item;
            //SubTotal = subTotal;
            //GrandTotal = grandTotal;
            //Tax = tax;
            Quantity = quantity;
        }

        public double GetSubTotal()
        {
            return SubTotal;
        }

        public double GetGrandTotal()
        {
            return GrandTotal;
        }

        public double GetSalesTaxTotal()
        {
            return Tax;
        }

        

        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void DecreaseQuantity(int quantity)
        {
            Quantity -= quantity;
        }
    }

    
}
