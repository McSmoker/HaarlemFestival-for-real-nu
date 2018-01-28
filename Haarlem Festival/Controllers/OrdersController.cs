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
        private List<CartItem> cartTickets;
        // GET: AddOrder
        [HttpPost]
        public ActionResult AddJazzTicketToCart(Jazz clickedTicket)
        {
            Jazz clickedEvent = db.Jazz.Include(j => j.Artist).SingleOrDefault(c => c.EventId == clickedTicket.EventId);

            if (Session["CartTickets"] != null)
                cartTickets = (List<CartItem>)Session["CartTickets"];
            else
                cartTickets = new List<CartItem>();

            var ticketAlreadyInCart = cartTickets.Where(t => t.JazzEvent.EventId == clickedEvent.EventId);

            if (ticketAlreadyInCart.ToList().Count == 0)
            {
                CartItem ticket = new CartItem
                {
                    JazzEvent = clickedEvent,
                    Amount = 1,
                    TicketType = TicketType.Single
                };

                cartTickets.Add(ticket);

                Session["CartTickets"] = cartTickets;

                return Json(new
                {
                    clickedEvent.EventId,
                    clickedEvent.Artist.PerformerName,
                    LocationHall = FormatEventLocationHall(clickedEvent.Location, clickedEvent.Hall),
                    EventDate = clickedEvent.EventStart.ToShortDateString(),
                    EventTime = FormatEventTime(clickedEvent.EventStart, clickedEvent.EventEnd),
                    ticket.Amount,
                    clickedEvent.Price
                });
            }
            else
                return Json(new
                {
                    fail = true
                });
        }

        [HttpPost]
        public ActionResult ChangeTicketAmount(CartItem changedTicket)
        {
            if (changedTicket != null)
            {
                cartTickets = (List<CartItem>)Session["CartTickets"];
                var ticketToBeChanged = cartTickets.SingleOrDefault(t => t.JazzEvent.EventId == changedTicket.JazzEvent.EventId);

                if (changedTicket.Amount > 0)
                {
                    try
                    {
                        ticketToBeChanged.Amount = changedTicket.Amount;
                    }
                    catch(Exception e)
                    {
                        throw new Exception("Ticket object doesn't exist", e);
                    }
                    return Json(new { });
                }
                else
                {
                    string removedId = "event-" + ticketToBeChanged.JazzEvent.EventId;
                    cartTickets.Remove(ticketToBeChanged);
                    return Json(new
                    {
                        removed = true,
                        removedId
                    });
                }
            }
            else
                return Json(new
                {
                    fail = true
                });
        }

        //[HttpPost]
        //public ActionResult AddPassePartoutTicketToCart(string passePartoutType)
        //{
        //    if (passePartoutType != null)
        //    {
        //        cartTickets = (List<CartItem>)Session["CartTickets"];

        //        CartItem passePartout = new CartItem {
        //            Amount = 1
        //        };

        //        if(passePartoutType == "pp-3day")
        //        {
        //            passePartout.TicketType = TicketType.PassePartoutFull;
        //            passePartout.PassePartoutDate = "Thursday -";
        //            passePartout.PassePartoutTime = "Saturday";
        //            passePartout.PassePartoutPrice = 80;
        //        }
        //        else if(passePartoutType == "pp-26/07/2018" || passePartoutType == "pp-27/07/2018" || passePartoutType == "pp-28/07/2018")
        //        {
        //            passePartout.TicketType = TicketType.PassePartoutSingle;
        //            passePartout.PassePartoutTime = "All day";
        //        }

        //        if (passePartoutType != "pp-3day")
        //        {
        //            passePartout.PassePartoutDate = passePartoutType.Substring(3);
        //            passePartout.PassePartoutPrice = 35;
        //        }
                    

        //        cartTickets.Add(passePartout);

        //        return Json(new
        //        {
        //            passePartout.TicketType,
        //            passePartout.PassePartoutDate,
        //            passePartout.PassePartoutTime
        //        });
        //    }
        //    else
        //        return Json(new
        //        {
        //            fail = true
        //        });
        //}

        private string FormatEventLocationHall(string location, string hall)
        {
            if (hall != null)
                return location + " | " + hall;
            else
                return location;
        }

        private string FormatEventTime(DateTime eventStart, DateTime eventEnd)
        {
            return eventStart.ToShortTimeString() + " - " + eventEnd.ToShortTimeString();
        }
    }
}