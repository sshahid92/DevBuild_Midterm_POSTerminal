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

        public void PayCash()
        {

        }

        public void PayCredit()
        {

        }

        public void PayCheck()
        {

        }

    }

    public enum PaymentType
    {
        Cash, Credit, Check
    }

    
}
