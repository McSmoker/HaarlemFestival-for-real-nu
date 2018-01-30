using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haarlem_Festival.Models
{
    public class PaymentData
    {
        public string EmailAddress { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
    }
}