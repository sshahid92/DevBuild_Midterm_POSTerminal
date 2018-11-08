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

        //figure out add to cart
        public void AddToCart(List<Cart> cartList, int itemID, int quantity)
        {
            var menu = new Menu();
            menu = menu.GetProductDetails(menu.GetMenu(), itemID - 1);
            var cartObject = new Cart(menu, quantity);
            cartList.Add(cartObject);
        }

        public void ViewCartSummary(List<Cart> cart)
        {
            foreach (var cartObject in cart)
            {
                Console.WriteLine($"\t{cartObject.Item.ItemID + 1}. {cartObject.Item.ItemName} - {cartObject.Item.Price:C} Quantity: {cartObject.Quantity}");
            }
        }

        public void RemoveFromCart()
        {

        }

        public void PlaceOrder()
        {

        }

    }
}
