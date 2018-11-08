using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBuild_POS_System
{
    class Payment
    {
        public PaymentType PaymentType { get; set; }

        public Payment(PaymentType paymentType)
        {
            PaymentType = paymentType;
        }

        public double? PayCash(double grandTotal, double cashAmount)
        {
            double change = cashAmount - grandTotal;
            if (change >= 0)
            {
                return change;
            }
            else
            {
                return null;
            }
        }

        public void PayCredit(int creditCardNumber, DateTime expiration, int cvv, double grandTotal)
        {

        }

        public void PayCheck(int accountNumber, int bankRoutingNumber, double grandTotal)
        {

        }

    }

    public enum PaymentType
    {
        Cash, Credit, Check
    }

    
}
