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
            //we gebruiken nu lekker een mooie viewdata
            var dropdownVD = CreatePerformerDropList();
            ViewData["StudDataVD"] = dropdownVD;
            //zo mooi
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

        public ActionResult TestView()
        {
            //using viewdata  
            //dit helpt niet met de idiote database die we hebben waar je shit uit 3 tabellen moet halen fucking
            //var dropdownVD = new SelectList(DB.Events.ToList(), "EventId", "stud_name");
            var dropdownVD = CreatePerformerDropList();
            ViewData["StudDataVD"] = dropdownVD;
            //dus gaan we dit proberen --CreateDropList()
            
            //using viewbag  

            ViewBag.dropdownVD = dropdownVD;
            return View();
        }

        public SelectList CreatePerformerDropList()
        {
            List<Event> events =  DB.Events.ToList();
            List<Jazz> Jazz =  DB.Jazz.ToList();
            List<Performer> Performers = DB.Performer.ToList();
            //Create a list of select list items - this will be returned as your select list
            List<SelectListItem> newList = new List<SelectListItem>();
            //deze zooi in een loopje doen op basis van events.Length
            //Create the select list item you want to add
            //geen oplossing nog voor het feit dat Er maar 3 talking en 24 Jazzs zijn maar daar kom ik nog wel uit
            for (int i = 0; i < Performers.Count; i++)
            {
                SelectListItem selListItem = new SelectListItem() { Value = Convert.ToString(events[i].EventId), Text = Performers[i].PerformerName };


                //Add select list item to list of selectlistitems
                newList.Add(selListItem);
            }

            //Return the list of selectlistitems as a selectlist
            return new SelectList(newList, "Value", "Text", null);

        }

        public JsonResult GetStudents()//ajax calls this function which will return json object  

        {
            var resultData = DB.Events.Select(c => new { Value = c.EventId, Text = c.Location }).ToList();
            return Json(new { result = resultData }, JsonRequestBehavior.AllowGet);
        }
    }
}