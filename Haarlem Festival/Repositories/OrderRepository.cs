using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haarlem_Festival.Models;

namespace Haarlem_Festival.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private HaarlemFestivalDB db = new HaarlemFestivalDB();
        public OrderRepository()
        {

        }

        public bool ProcessOrder(List<OrderItem> orderItems, PaymentData paymentData)
        {
            try
            {
                Order newOrder = new Order();
                //newOrder.OrderItems = new List<OrderItem>();
                //OrderItem newOrderItem = new OrderItem();
                foreach (var orderItem in orderItems)
                {
                    OrderItem newOrderItem = new OrderItem
                    {
                        //newOrder.OrderItems.Add(orderItem);
                        Amount = orderItem.Amount,
                        EventId = orderItem.EventId,
                        TicketType = orderItem.TicketType,
                        Order = newOrder
                    };

                    db.OrderItems.Add(newOrderItem);
                }

                newOrder.RecipientEmail = paymentData.EmailAddress;
                newOrder.OrderStatus = OrderStatus.Ordered;

                db.Orders.Add(newOrder);
                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}