using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Haarlem_Festival.Models;
using Haarlem_Festival.ViewModels;
using Haarlem_Festival.Repositories;

namespace Haarlem_Festival.Controllers
{
    public class JazzController : Controller
    {
        private IJazzRepository jazzRepository = new JazzRepository();
        
        // GET: /Jazz/
        public ActionResult Index()
        {
            InitCart();
            var jazzEvents = jazzRepository.GetAllJazzEvents();
            List<JazzViewModel> jvmList = GenerateJazzViewModels(jazzEvents);
            return View(jvmList);
        }

        private void InitCart()
        {
            if(Session["CartTickets"] == null)
                Session["CartTickets"] = new List<CartItem>();
        }

        private List<JazzViewModel> GenerateJazzViewModels(List<Jazz> events)
        {
            List<JazzViewModel> jvmList = new List<JazzViewModel>();
            foreach (var e in events)
            {
                JazzViewModel jvm = new JazzViewModel
                {
                    EventId = e.EventId,
                    EventStart = e.EventStart,
                    EventEnd = e.EventEnd,
                    Artist = e.Artist,
                    Hall = e.Hall,
                    Location = e.Location,
                    Seats = e.Seats,
                    TicketsSold = e.TicketsSold,
                    Price = e.Price
                };
                jvmList.Add(jvm);
            }
            return jvmList;
        }
    }
}
