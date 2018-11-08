using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBuild_POS_System
{
    class Customer
    {
        public Customer()
        {

        }

        public void ViewMenu()
        {
            var menu = new Menu();
            var fullmenu = menu.GetMenu();
            string categoryCheck = "";
            foreach (var item in fullmenu)
            {
                if(categoryCheck != item.Category)
                {
                    Console.WriteLine(item.Category.ToUpper());
                    categoryCheck = item.Category;
                }
                Console.WriteLine($"{item.ItemID + 1}. {item.ItemName} - {item.Price:C}\n{item.Description}\n");
            }

        }

        public List<Cart> CreateCart(int itemID, int quantity)
        {
            var menu = new Menu();
            menu = menu.GetProductDetails(menu.GetMenu(), itemID-1);

            List<Cart> cartList = new List<Cart>();
            var cart = new Cart(menu, quantity);
            cartList.Add(cart);

            return cartList;
        }
        
        public void AddToCart(List<Cart> cartList, int itemID, int quantity)
        {
            var menu = new Menu();
            menu = menu.GetProductDetails(menu.GetMenu(), itemID - 1);
            var cartObject = new Cart(menu, quantity);
            cartList.Add(cartObject);
        }

        public void UpdateCart(List<Cart> cartList, int itemID, int quantity)
        {
            var menu = new Menu();
            menu = menu.GetProductDetails(menu.GetMenu(), itemID - 1);
            var cartObject = new Cart(menu, quantity);
            cartList.Add(cartObject);
            var cartItem = cartList.FirstOrDefault(x => x.Item.ItemID == itemID);
            if (cartItem != null) cartItem.Quantity = quantity;
        }

        public void ViewCartSummary(List<Cart> cart)
        {
            double subTotal = 0;
            double tax = 0;
            double grandTotal = 0;
            foreach (var cartObject in cart)
            {
                Console.WriteLine($"\t{cartObject.Item.ItemID + 1}. {cartObject.Item.ItemName} - {cartObject.Item.Price:C} x Quantity: {cartObject.Quantity}");
                subTotal += cartObject.GetSubTotal();
                tax += cartObject.GetSalesTaxTotal();
                grandTotal += cartObject.GetGrandTotal();

            }
            Console.WriteLine($"Subtotal: {subTotal:C}\n" +
                            $"Total tax: {tax:C}\n" +
                            $"Grand Total: {grandTotal:C}\n");            

        }

        public void RemoveFromCart(List<Cart> cartList, int itemID)
        {
            var cartItem = cartList.SingleOrDefault(c => c.Item.ItemID == itemID);
            if (cartItem != null)
                cartList.Remove(cartItem);
        }

        public void PlaceOrder()
        {

        }

    }
}
