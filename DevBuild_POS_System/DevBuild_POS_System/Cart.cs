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
        public int Quantity { get; set; }
        private readonly double taxRate = 0.06;

        public Cart()
        {
        }

        public Cart(Menu item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public double GetSubTotal()
        {
            double subTotal = Item.Price * Quantity;
            return subTotal;
        }

        public double GetSalesTaxTotal()
        {
            double subTotal = GetSubTotal();
            double tax = subTotal * taxRate;
            return tax;
        }

        public double GetGrandTotal()
        {
            double subTotal = GetSubTotal();
            double tax = GetSalesTaxTotal();
            double grandTotal = subTotal + tax;
            return grandTotal;
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
