using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Haarlem_Festival.Models;

namespace Haarlem_Festival.Controllers
{
    public class OrdersController : Controller
    {
        private HaarlemFestivalDB db = new HaarlemFestivalDB();

        // GET: AddOrder
        [HttpPost]
        public ActionResult AddOrderItem(Jazz clickedTicket)
        {
            try
            {
                var ticket = db.Jazz.Include(j => j.Artist).SingleOrDefault(c => c.EventId == clickedTicket.EventId);
                var lastOrder = db.Orders.Last();
                
                if(Session["Order"] == null)
                {
                    Order currentOrder = new Order();
                    Session["Order"] = currentOrder;
                }

                if (Session["Order"] != null)
                {
                    Order currentOrder = (Order)Session["Order"];
                    currentOrder.OrderStatus = OrderStatus.Ordered;
                    //currentOrder.OrderItems.Add(new OrderItem(TicketType.Single, 1, ));
                }

                return Json(new
                {
                    msg = ticket.Artist.PerformerName
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}