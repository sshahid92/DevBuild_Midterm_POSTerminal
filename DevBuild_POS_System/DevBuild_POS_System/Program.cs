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
            Console.WriteLine("Welcome to Yemen Cafe!" +
                            "\nLocated at 8740 Joseph Campau Ave, Hamtramck, MI 48212" +
                            "\nPress enter to view our menu.");
            Console.ReadLine();
            var customer = new Customer();
            customer.ViewMenu();

            Console.WriteLine("What would you like to order? Enter an item number to add it to the cart.");
            int.TryParse(Console.ReadLine(), out int orderItemNum);

            Console.Write("What quantity would you like: ");
            int.TryParse(Console.ReadLine(), out int quantity);
            var cart = customer.CreateCart(orderItemNum, quantity);

            Console.WriteLine("Would you like to: (select 1, 2 or 3\n" +
                            "1) Add to cart\n" +
                            "2) Change quantity of an item" +
                            "3) Remove from Cart\n" +
                            "4) Checkout\n");
            string input = Console.ReadLine();

            if(input == "1")
            {
                customer.ViewMenu();
                Console.WriteLine("Enter an item number to add it to the cart.");
                int.TryParse(Console.ReadLine(), out orderItemNum);

                Console.Write("What quantity would you like: ");
                int.TryParse(Console.ReadLine(), out quantity);

                customer.AddToCart(cart, orderItemNum, quantity);
            }
            if (input == "2")
            {
                Console.WriteLine("Which cart item would you like to update? (select the ID)");
                customer.ViewCartSummary(cart);

                int.TryParse(Console.ReadLine(), out int cartItemNum);

                Console.Write("What quantity would you like: ");
                int.TryParse(Console.ReadLine(), out quantity);

                customer.UpdateCart(cart, cartItemNum, quantity);
                Console.WriteLine("Here is your cart summary: ");
                customer.ViewCartSummary(cart);
            }

            if (input == "3")
            {
                Console.WriteLine("Which cart item would you like to remove? (select the ID)");
                customer.ViewCartSummary(cart);

                int.TryParse(Console.ReadLine(), out int cartItemNum);
                
                customer.RemoveFromCart(cart, cartItemNum);
                Console.WriteLine("Here is your cart summary: ");
                customer.ViewCartSummary(cart);
            }

            if(input == "4")
            {
                Console.WriteLine("How would you like to pay?\n" +
                                "1) Cash\n" +
                                "2) Credit\n" +
                                "3) Check\n");

            }


            Console.WriteLine("Here is your cart summary: ");
            customer.ViewCartSummary(cart);

            
            


            Console.ReadLine();
        }

        

    }
}
