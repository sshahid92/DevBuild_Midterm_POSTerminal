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
            int menuLength = customer.GetMenuLength();
            int orderItemNum = 0;

            customer.ViewMenu();
            var cart = new List<Cart>();
            while (orderItemNum < 1 || orderItemNum > menuLength)
            {
                Console.WriteLine("What would you like to order? Enter an item number to add it to the cart.");
                int.TryParse(Console.ReadLine(), out orderItemNum);
                if (orderItemNum < 1 || orderItemNum > menuLength)
                {
                    Console.WriteLine("That item is not on the menu.");
                }
            }

            int quantity = 0;
            while (quantity < 1 || quantity > 50)
            {
                Console.WriteLine("What quantity would you like: (Limit of 50) ");
                int.TryParse(Console.ReadLine(), out quantity);
                cart = customer.CreateCart(orderItemNum, quantity);
                if (quantity < 1 || quantity > 50)
                {
                    Console.WriteLine("Please enter a valid quantity.");
                }

            }

            Console.WriteLine("\nHere is your cart summary:\n ");
            customer.ViewCartSummary(cart);

            bool cont = true;
            while (cont)
            {
                Console.WriteLine("\nWhat would you like to do: (select a number between 1-5)\n" +
                                "1) Add to cart\n" +
                                "2) Change quantity of an item\n" +
                                "3) Remove from Cart\n" +
                                "4) Checkout\n" +
                                "5) Quit\n");
                int input = 0;
                bool isNum = false;
                while (!isNum || (input < 1 || input > 5))
                {
                    isNum = int.TryParse(Console.ReadLine(), out input);
                    if (!isNum || (input < 1 || input > 5))
                    {
                        Console.WriteLine("Please enter a valid response");
                    }

                }
                switch (input)
                {
                    case 1:
                        customer.ViewMenu();
                        cart = AddToCart(cart);
                        Console.WriteLine("\nHere is your cart summary: \n");
                        customer.ViewCartSummary(cart);
                        break;

                    case 2:
                        Console.WriteLine("\nHere is your cart summary: \n");
                        customer.ViewCartSummary(cart);
                        cart = UpdateCart(cart);
                        Console.WriteLine("\nHere is your cart summary: \n");
                        customer.ViewCartSummary(cart);
                        break;

                    case 3:
                        Console.WriteLine("\nHere is your cart summary: \n");
                        customer.ViewCartSummary(cart);
                        cart = RemoveCart(cart);

                        Console.WriteLine("\nHere is your cart summary: \n");
                        customer.ViewCartSummary(cart);
                        break;

                    case 4:
                        Console.WriteLine("\nHere is your cart summary: \n");
                        customer.ViewCartSummary(cart);
                        bool isPayment = false;
                        while (!isPayment)
                        {
                            Console.WriteLine("\nHow would you like to pay? (type out one of the following)\n" +
                                            "\tCash\n" +
                                            "\tCredit\n" +
                                            "\tCheck\n");
                            string paymentType = Console.ReadLine();
                            isPayment = Enum.IsDefined(typeof(PaymentType), paymentType.ToLower());
                            while (!isPayment)
                            {
                                Console.WriteLine("Please enter a valid response.");
                                paymentType = Console.ReadLine();
                                isPayment = Enum.IsDefined(typeof(PaymentType), paymentType.ToLower());
                            }

                            bool paymentSuccess = MakePayment(cart, paymentType);
                            if (paymentSuccess)
                            {
                                cart.Clear();
                            }
                        }
                        break;

                    case 5:
                        cont = false;
                        break;
                        
                }

            }
        }

        static List<Cart> AddToCart(List<Cart> cart)
        {
            var customer = new Customer();
            int menuLength = customer.GetMenuLength();
            int orderItemNum = 0;
            int quantity = 0;

            while (orderItemNum < 1 || orderItemNum > menuLength)
            {
                Console.WriteLine("Enter an item number to add it to the cart.");
                int.TryParse(Console.ReadLine(), out orderItemNum);
                if (orderItemNum < 1 || orderItemNum > menuLength)
                {
                    Console.WriteLine("That item is not on the menu.");
                }
            }
            
            while (quantity < 1 || quantity > 50)
            {
                Console.WriteLine("What quantity would you like: (Limit of 50) ");
                int.TryParse(Console.ReadLine(), out quantity);
                if (quantity < 1 || quantity > 50)
                {
                    Console.WriteLine("Please enter a valid quantity.");
                }

            }
                        
            customer.AddToCart(cart, orderItemNum, quantity);
            return cart;
        }

        static List<Cart> UpdateCart(List<Cart> cart)
        {
            var customer = new Customer();
            int menuLength = customer.GetMenuLength();
            int cartItemNum = 0;
            int quantity = 0;

            while (cartItemNum < 1 || cartItemNum > menuLength)
            {
                Console.WriteLine("Which cart item would you like to update? (select the ID)");
                int.TryParse(Console.ReadLine(), out cartItemNum);
                if (cartItemNum < 1 || cartItemNum > menuLength)
                {
                    Console.WriteLine("That item is not in the cart.");
                }
            }

            while (quantity < 1 || quantity > 50)
            {
                Console.WriteLine("What quantity would you like: (Limit of 50) ");
                int.TryParse(Console.ReadLine(), out quantity);
                if (quantity < 1 || quantity > 50)
                {
                    Console.WriteLine("Please enter a valid quantity.");
                }

            }

            customer.UpdateCart(cart, cartItemNum, quantity);
            return cart;
        }

        static List<Cart> RemoveCart(List<Cart> cart)
        {
            var customer = new Customer();
            int menuLength = customer.GetMenuLength();
            int cartItemNum = 0;
            
            while (cartItemNum < 1 || cartItemNum > menuLength)
            {
                Console.WriteLine("Which cart item would you like to remove? (select the ID)");
                int.TryParse(Console.ReadLine(), out cartItemNum);
                if (cartItemNum < 1 || cartItemNum > menuLength)
                {
                    Console.WriteLine("That item is not in the cart.");
                }
            }
            customer.RemoveFromCart(cart, cartItemNum);
            return cart;
        }

        static bool MakePayment(List<Cart> cartList, string paymentType)
        {
            Customer customer = new Customer();

            switch (paymentType)
            {
                case "cash":
                    customer.Cash(cartList);
                    return true;

                case "credit":
                    customer.Credit(cartList);
                    return true;

                case "check":
                    customer.Check(cartList);
                    return true;

                default:
                    return false;
            }
        }

    }
}
