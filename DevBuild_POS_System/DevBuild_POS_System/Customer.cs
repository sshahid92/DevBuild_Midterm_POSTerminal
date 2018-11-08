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

        public int GetMenuLength()
        {
            var menu = new Menu();
            int count = menu.GetMenu().Count();
            return count;
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
            foreach (var item in cartList)
            {
                if (item.Item.ItemID == itemID - 1)
                {
                    item.Quantity = quantity;
                    break;
                }
            }
            Console.WriteLine("That item is not in the cart.");
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
            itemID -= 1;
            foreach (var item in cartList)
            {
                if(item.Item.ItemID == itemID)
                {
                    cartList.Remove(item);
                    break;
                }
            }
            Console.WriteLine("That item is not in the cart.");
        }

        public void PlaceOrder(PaymentType paymentType, double grandTotal)
        {
            
            switch (paymentType)
            {
                case PaymentType.Cash:
                    break;
                case PaymentType.Credit:

                    break;
                case PaymentType.Check:
                    break;
                default:
                    break;
            }
        }

        public void Cash(List<Cart> cartList)
        {
            var payment = new Payment();
            double? change = null;
            while (change == null)
            {
                bool isNum = false;
                double tenderedCashAmount = 0;
                double grandTotal = 0;

                foreach (var cartObject in cartList)
                {
                    grandTotal += cartObject.GetGrandTotal();
                }

                while (!isNum)
                {
                    Console.Write("Enter tendered cash amount:");
                    isNum = double.TryParse(Console.ReadLine(), out tenderedCashAmount);
                }

                change = payment.PayCash(grandTotal, tenderedCashAmount);
            }
            Console.WriteLine($"Your change is {change:C}. Thank you for your order.");
        }

        public void Credit()
        {
            var payment = new Payment();
            string paymentResult = "invalid";
            while (paymentResult == "invalid")
            {
                Console.Write("Enter a credit card number:");
                string creditCardNumber = Console.ReadLine();

                int month = 0;
                bool isMonth = false;
                while (!isMonth)
                {
                    Console.Write("Enter expiration month:");
                    isMonth = int.TryParse(Console.ReadLine(), out month);
                }

                int year = 0;
                bool isYear = false;
                while (!isYear)
                {
                    Console.Write("Enter expiration year:");
                    isMonth = int.TryParse(Console.ReadLine(), out year);
                }

                Console.Write("Enter CVV:");
                string cvv = Console.ReadLine();

                paymentResult = payment.PayCredit(creditCardNumber, month, year, cvv);

                Console.WriteLine($"Your {paymentResult} payment was successful. Thank you for your order");
            }

        }

        public void Check()
        {
            var payment = new Payment();
            string paymentResult = "invalid";
            while (paymentResult == "invalid")
            {
                Console.Write("Enter a bank account number:");
                string accountNumber = Console.ReadLine();
                               
                Console.Write("Enter a bank routing number:");
                string bankRoutingNumber = Console.ReadLine();

                paymentResult = payment.PayCheck(accountNumber, bankRoutingNumber);

                Console.WriteLine($"Your check payment was successful. Thank you for your order");
            }
        }

    }
}
