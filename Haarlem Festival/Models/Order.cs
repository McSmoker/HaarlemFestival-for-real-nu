using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Haarlem_Festival.Models
{
    public class Order
    {
        //[Key]
        public int OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string RecipientEmail { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}