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
                OrderItem newOrderItem = new OrderItem();
                foreach (var orderItem in orderItems)
                {
                    newOrder.OrderItems.Add(orderItem);
                    newOrderItem = orderItem;
                }

                newOrder.RecipientEmail = paymentData.EmailAddress;
                newOrder.OrderStatus = OrderStatus.Ordered;
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