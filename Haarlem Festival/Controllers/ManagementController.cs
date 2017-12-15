using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haarlem_Festival.Models;
using Haarlem_Festival.ViewModels;
using System.Data.SqlClient;

namespace Haarlem_Festival.Controllers
{
    public class ManagementController : Controller
    {

        HaarlemFestivalDB EventDB = new HaarlemFestivalDB();
        // GET: Management
        public ActionResult Index()
        {
            EventViewModel eventListViewModel = new EventViewModel();

            //moet een list maken dus

            List<Event> Events = EventDB.Events.ToList();

            eventListViewModel.Location = Events[0].Location;
            eventListViewModel.EventStart = Events[0].EventStart;

            PopulateEventsDropDownList();

            return View("Index",eventListViewModel);
        }

        public ActionResult CreateEvent(Jazz e, string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save Event":
                EventDB.Jazz.Add(e);
                EventDB.SaveChanges();
                return View("Index");
            }
            return View("CreateEvent");
            ///SaveEvent(e);
            ////return RedirectToAction("Index"); ;
        }

        public ActionResult EditEvent(Jazz e)
        {
            return View("EditEvent");
        }

        public string SaveEvent(Jazz e)
        {
            return e.EventEnd + "|" + e.EventStart + "|" + e.Location + "|" + e.Seats + "|" + e.TicketsSold + "|";
        }
        /// <summary>
        /// deze jokes moeten in een busineeslayer genaamd EventBusinesslayer maybe die hierboven ook
        /// Deze jokes doorzetten gaat veel tijd kosten om te fixen dus w8 op input van iemand anders hierover
        /// </summary>
        /// <returns></returns>
        /// 
        public Jazz SaveJazz([Bind(Include = "EventStart,EventEnd,Location,Seats,Artist,Hall,TicketsSold")]Jazz e)
        {
            EventDB.Jazz.Add(e);
            EventDB.SaveChanges();
            return e;
        }
        public Jazz NewJazz(Jazz e,int id)
        {
            var eventToUpdate = EventDB.Events.Find(id);
            TryUpdateModel(eventToUpdate, "", new string[] { "EventStart,EventEnd,Location,Seats,Artist,Hall,TicketsSold" });
            return e;
        }
        private void PopulateEventsDropDownList(object selectedEvent = null)
        {
            List<Event> Events = EventDB.Events.ToList();
            ViewBag.EventId = new SelectList(EventDB.Events.ToList(), "EventId", "Location", selectedEvent);
        }

        HaarlemFestivalDB DB = new HaarlemFestivalDB();// DB Entity Object  

        public ActionResult TestPage()
        {
            //using viewdata  
            var dropdownVD = new SelectList(DB.Performer.ToList(), "PerformerId", "Location");
            ViewData["StudDataVD"] = dropdownVD;
            //using viewbag  
            ViewBag.dropdownVD = new SelectList(DB.Events.ToList(), "EventId", "Location");
            return View();
        }

        public JsonResult GetStudents()//ajax calls this function which will return json object  

        {
            var resultData = DB.Performer.Select(c => new { Value = c.PerformerId, Text = c.PerformerName }).ToList();
            return Json(new { result = resultData }, JsonRequestBehavior.AllowGet);
        }
    }
}