using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Payments
{
    public class Payment : EntityBase
    {
        public string CardholderFullName { get; set; }
        public string CreditCardNumber { get; set; }
        public string CVV { get; set; }
        public int CreditCardType { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
