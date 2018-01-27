using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Haarlem_Festival.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public TicketType TicketType { get; set; }
        public int Amount { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public OrderItem()
        {

        }

        public OrderItem(TicketType ticketType, int amount, int orderId, int eventId)
        {
            this.TicketType = ticketType;
            this.Amount = amount;
            this.OrderId = orderId;
            this.EventId = eventId;
        }
    }
}