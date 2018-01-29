using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haarlem_Festival.Models;
using Haarlem_Festival.ViewModels;
using Haarlem_Festival.Repositories;

namespace Haarlem_Festival.Controllers
{
    public class AgendaController : Controller
    {
        private IOrderRepository orderRepository = new OrderRepository();
        private List<CartItem> cartItems;
        private List<OrderItem> orderItems;
        // GET: Agenda
        public ActionResult Index()
        {
            InitCart();
            List<CartItem> cartItems = (List<CartItem>)Session["CartTickets"];
            List<CartItemViewModel> civmList = GenerateCartItemViewModels(cartItems);
            return View(civmList);
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "EmailAddress,PaymentMethod")] PaymentData paymentData)
        {
            if(paymentData.EmailAddress != null && paymentData.PaymentMethod != null)
            {
                cartItems = (List<CartItem>)Session["CartTickets"];
                orderItems = new List<OrderItem>();

                foreach(var ci in cartItems)
                {
                    OrderItem orderItem = new OrderItem();
                    if (ci.JazzEvent != null)
                    {
                        orderItem.EventId = ci.JazzEvent.EventId;
                    }
                    else if(ci.TalkingEvent != null)
                    {
                        orderItem.EventId = ci.TalkingEvent.EventId;
                    }
                    orderItem.Amount = ci.Amount;
                    orderItem.TicketType = ci.TicketType;

                    orderItems.Add(orderItem);
                }

                if (orderRepository.ProcessOrder(orderItems, paymentData))
                {
                    Session["CartTickets"] = new List<CartItem>();
                    return Redirect(Url.Action("OrderSuccess", "Agenda", paymentData));
                }
                else
                    throw new Exception("whyyyyyyyyyyyy?");
            }

            return View();
        }


        public ActionResult OrderSuccess(PaymentData paymentData)
        {
            if (paymentData != null)
                return View(paymentData);
            else
                return View();
        }

        private void InitCart()
        {
            if (Session["CartTickets"] == null)
                Session["CartTickets"] = new List<CartItem>();
        }

        private List<CartItemViewModel> GenerateCartItemViewModels(List<CartItem> cartItems)
        {
            List<CartItemViewModel> civmList = new List<CartItemViewModel>();
            foreach (var ci in cartItems)
            {
                CartItemViewModel civm = new CartItemViewModel();
                if (ci.JazzEvent != null)
                {
                    civm.EventId = ci.JazzEvent.EventId;
                    civm.EventDate = ci.JazzEvent.EventStart.ToShortDateString();
                    civm.EventTime = ci.JazzEvent.EventStart.ToShortTimeString() + " - " + ci.JazzEvent.EventEnd.ToShortTimeString();
                    civm.Location = ci.JazzEvent.Location + " | " + ci.JazzEvent.Hall;
                    civm.PerformerOneName = ci.JazzEvent.Artist.PerformerName;
                    civm.PerformerTwoName = string.Empty;
                    civm.Price = ci.JazzEvent.Price;
                }
                else if(ci.TalkingEvent != null)
                {
                    civm.EventId = ci.TalkingEvent.EventId;
                    civm.EventDate = ci.TalkingEvent.EventStart.ToShortDateString();
                    civm.EventTime = ci.TalkingEvent.EventStart.ToShortTimeString() + " - " + ci.TalkingEvent.EventEnd.ToShortTimeString();
                    civm.Location = ci.TalkingEvent.Location;
                    civm.PerformerOneName = ci.TalkingEvent.SpeakerOne.PerformerName;
                    civm.PerformerTwoName = ci.TalkingEvent.SpeakerTwo.PerformerName;
                    civm.Price = ci.TalkingEvent.Price;
                }

                civm.Amount = ci.Amount;
                civm.TicketType = ci.TicketType;
                civmList.Add(civm);
            }
            return civmList;
        }
    }
}