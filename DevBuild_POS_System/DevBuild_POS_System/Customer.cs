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
                Console.WriteLine($"\tID: {cartObject.Item.ItemID + 1} - {cartObject.Item.ItemName} - {cartObject.Item.Price:C} x Quantity: {cartObject.Quantity}");
                subTotal += cartObject.GetSubTotal();
                tax += cartObject.GetSalesTaxTotal();
                grandTotal += cartObject.GetGrandTotal();

            }
            Console.WriteLine($"\n\tSubtotal: {subTotal:C}\n" +
                            $"\tTotal tax: {tax:C}\n" +
                            $"\tGrand Total: {grandTotal:C}\n");            

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

        
        public void Cash(List<Cart> cartList)
        {
            var payment = new Payment();
            double? change = null;
            double tenderedCashAmount = 0;
            double grandTotal = 0;
            double subTotal = 0;
            double tax = 0;

            while (change == null)
            {
                bool isNum = false;
                
                foreach (var cartObject in cartList)
                {
                    grandTotal += cartObject.GetGrandTotal();
                    subTotal += cartObject.GetSubTotal();
                    tax += cartObject.GetSalesTaxTotal();
                }

                while (!isNum)
                {
                    Console.Write("Enter tendered cash amount:");
                    isNum = double.TryParse(Console.ReadLine(), out tenderedCashAmount);
                }

                change = payment.PayCash(grandTotal, tenderedCashAmount);
            }
            Console.WriteLine("\n\tOrder Reciept:\n" +
                            $"\t\tPayment Type: Cash\n" +
                            $"\t\tSubtotal: {subTotal:C}\n" +
                            $"\t\tTotal tax: {tax:C}\n" +
                            $"\t\tGrand Total: {grandTotal:C}\n" +
                            $"\t\tTendered Cash Amount: {tenderedCashAmount:C}");
            Console.WriteLine($"\t\tChange: {change:C}. \nThank you for your order.");
        }

        public void Credit(List<Cart> cartList)
        {
            var payment = new Payment();
            string paymentResult = "invalid";
            int month = 0;
            int year = 0;
            string cvv = "";
            double grandTotal = 0;
            double subTotal = 0;
            double tax = 0;

            foreach (var cartObject in cartList)
            {
                grandTotal += cartObject.GetGrandTotal();
                subTotal += cartObject.GetSubTotal();
                tax += cartObject.GetSalesTaxTotal();
            }

            while (paymentResult == "invalid")
            {
                Console.Write("Enter a credit card number:");
                string creditCardNumber = Console.ReadLine();

                
                bool isMonth = false;
                while (!isMonth)
                {
                    Console.Write("Enter expiration month:");
                    isMonth = int.TryParse(Console.ReadLine(), out month);
                }

                
                bool isYear = false;
                while (!isYear)
                {
                    Console.Write("Enter expiration year:");
                    isYear = int.TryParse(Console.ReadLine(), out year);
                }

                Console.Write("Enter CVV:");
                cvv = Console.ReadLine();

                paymentResult = payment.PayCredit(creditCardNumber, month, year, cvv);
                if(paymentResult == "invalid")
                {
                    Console.WriteLine("Payment Unsuccessful");
                }
                
            }
            Console.WriteLine($"\n\tOrder Reciept:\n" +
                            $"\t\tPayment Type: {paymentResult} Credit\n" +
                            $"\t\tSubtotal: {subTotal:C}\n" +
                            $"\t\tTotal tax: {tax:C}\n" +
                            $"\t\tGrand Total: {grandTotal:C}\n" +
                            $"Your {paymentResult} payment was successful. Thank you for your order");

        }

        public void Check(List<Cart> cartList)
        {
            var payment = new Payment();
            string paymentResult = "invalid";
            double grandTotal = 0;
            double subTotal = 0;
            double tax = 0;

            foreach (var cartObject in cartList)
            {
                grandTotal += cartObject.GetGrandTotal();
                subTotal += cartObject.GetSubTotal();
                tax += cartObject.GetSalesTaxTotal();
            }

            while (paymentResult == "invalid")
            {
                Console.Write("Enter a bank account number:");
                string accountNumber = Console.ReadLine();
                               
                Console.Write("Enter a bank routing number:");
                string bankRoutingNumber = Console.ReadLine();

                paymentResult = payment.PayCheck(accountNumber, bankRoutingNumber);

                Console.WriteLine($"Your check payment was successful. Thank you for your order");
                if (paymentResult == "invalid")
                {
                    Console.WriteLine("Payment Unsuccessful");
                }
            }
            Console.WriteLine($"\n\tOrder Reciept:\n" +
                            $"\t\tPayment Type: {paymentResult} Credit\n" +
                            $"\t\tSubtotal: {subTotal:C}\n" +
                            $"\t\tTotal tax: {tax:C}\n" +
                            $"\t\tGrand Total: {grandTotal:C}\n" +
                            $"Your check payment was successful. Thank you for your order");
        }

    }
}
