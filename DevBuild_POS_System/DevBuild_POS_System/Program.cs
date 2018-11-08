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

            Console.WriteLine("Would you like to add anything else to your cart? (y/n)");
            string input = Console.ReadLine();

            if(input == "y")
            {
                customer.ViewMenu();
                Console.WriteLine("Enter an item number to add it to the cart.");
                int.TryParse(Console.ReadLine(), out orderItemNum);

                Console.Write("What quantity would you like: ");
                int.TryParse(Console.ReadLine(), out quantity);

                customer.AddToCart(cart, orderItemNum, quantity);
            }

            Console.WriteLine("Here is your cart summary:");
            customer.ViewCartSummary(cart);

            Console.WriteLine("Would you like to:\n" +
                            "1) Add to cart" +
                            "2) Remove from Cart");
            

            Console.ReadLine();
        }

        

    }
}
