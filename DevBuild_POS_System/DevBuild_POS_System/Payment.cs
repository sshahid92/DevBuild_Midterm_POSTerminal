using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DevBuild_POS_System
{
    class Payment
    {
        public PaymentType PaymentType { get; set; }
        private readonly string _visa = @"^4[0-9]{12}(?:[0-9]{3})?$";
        private readonly string _mastercard = @"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$";
        private readonly string _americanExpress = @"^3[47][0-9]{13}$";
        private readonly string _discover = @"^6(?:011|5[0-9]{2})[0-9]{12}$";
        private readonly string _dinersClub = @"^6(?:011|5[0-9]{2})[0-9]{12}$";
        private readonly string _jcb = @"^6(?:011|5[0-9]{2})[0-9]{12}$";
        private readonly string _aeCvv = @"^[0-9]{4}$";
        private readonly string _cvv = @"^[0-9]{3}$";
        private readonly string _accountNumber = @"^[0-9]{10,12}$";
        private readonly string _routingNumber = @"^[0-9]{9}$";

        public Payment()
        {
        }

        public double? PayCash(double grandTotal, double tenderedCashAmount)
        {
            if(tenderedCashAmount < grandTotal)
            {
                Console.WriteLine("That's not enough cash, we need more!");
                return null;
            }
            else if (tenderedCashAmount >= grandTotal)
            {
                double change = tenderedCashAmount - grandTotal;
                return change;
            }
            else
            {
                return null;
            }
        }

        public string PayCredit(string creditCardNumber, int month, int year, string cvv)
        {
            if (Regex.IsMatch(creditCardNumber, _visa))
            {
                if((month > 0 && month < 13) && (year > 2017 && year < 10000))
                {
                    if(Regex.IsMatch(cvv, _cvv))
                    {
                        return "Visa";
                    }
                    Console.WriteLine("Please enter a valid CVV.");
                    return "invalid";
                }
                Console.WriteLine("Enter a valid date");
                return "invalid";
            }
            else if (Regex.IsMatch(creditCardNumber, _mastercard))
            {
                if ((month > 0 && month < 13) && (year > 2017 && year < 10000))
                {
                    if (Regex.IsMatch(cvv, _cvv))
                    {
                        return "Mastercard";
                    }
                    Console.WriteLine("Please enter a valid CVV.");
                    return "invalid";
                }
                Console.WriteLine("Enter a valid date");
                return "invalid";
            }
            else if (Regex.IsMatch(creditCardNumber, _americanExpress))
            {
                if ((month > 0 && month < 13) && (year > 2017 && year < 10000))
                {
                    if (Regex.IsMatch(cvv, _aeCvv))
                    {
                        return "American Express";
                    }
                    Console.WriteLine("Please enter a valid CVV.");
                    return "invalid";
                }
                Console.WriteLine("Enter a valid date");
                return "invalid";
            }
            else if (Regex.IsMatch(creditCardNumber, _discover))
            {
                if ((month > 0 && month < 13) && (year > 2017 && year < 10000))
                {
                    if (Regex.IsMatch(cvv, _cvv))
                    {
                        return "Discover";
                    }
                    Console.WriteLine("Please enter a valid CVV.");
                    return "invalid";
                }
                Console.WriteLine("Enter a valid date");
                return "invalid";
            }
            else if (Regex.IsMatch(creditCardNumber, _dinersClub))
            {
                Console.WriteLine("We do not except Diners Club");
                return "invalid";
            }
            else if (Regex.IsMatch(creditCardNumber, _jcb))
            {
                Console.WriteLine("We do not except JCB");
                return "invalid";
            }
            else
            {
                return "invalid";
            }
        }

        public string PayCheck(string accountNumber, string bankRoutingNumber)
        {
            if (Regex.IsMatch(accountNumber, _accountNumber))
            {
                if (Regex.IsMatch(bankRoutingNumber, _routingNumber))
                {
                    return "checkValid";
                }
                Console.WriteLine("Enter a routing number");
                return "invalid";
            }
            Console.WriteLine("Enter a valid account number");
            return "invalid";
        }

    }

    public enum PaymentType
    {
        Cash, Credit, Check
    }

    
}
