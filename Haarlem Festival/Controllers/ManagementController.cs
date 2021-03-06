﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haarlem_Festival.Models;
using Haarlem_Festival.ViewModels;
using System.Data.SqlClient;
using System.Web.Mvc.Html;

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

            ///Hier is de oplossing voor alle problemen van humanity maar het mag niet van gerwin
            VerbodenViewBagCode();

            PopulateEventsDropDownList();
            //we gebruiken nu lekker een mooie viewdata
            //var dropdownVD = CreatePerformerDropList();
            //ViewData["StudDataVD"] = dropdownVD;
            //zo mooi
            ManagementViewModel viewModel = PopulateViewModel();

            return View("Index", viewModel);

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(FormCollection formCollection)
        {
            VerbodenViewBagCode();
            DateTime startDate = Convert.ToDateTime(formCollection["startDate"]);
            DateTime endDate = Convert.ToDateTime(formCollection["endDate"]);
            ManagementViewModel viewModel = FilteredViewModel(startDate,endDate);
            return View("Index", viewModel);
        }
        public ManagementViewModel FilteredViewModel(DateTime startDate, DateTime endDate)
        {

            List<Event> events = new List<Event>();
            List<Jazz> jazzs = new List<Jazz>();
            List<Talking> talkings = new List<Talking>();
            List<Performer> performers = EventDB.Performer.ToList();
            ManagementViewModel managementViewModel = new ManagementViewModel();
            FillList(ref events, ref jazzs, ref talkings,ref startDate,endDate);
            while (startDate != endDate)
            {
                FillList(ref events, ref jazzs, ref talkings, ref startDate, endDate);
            }
            managementViewModel.events = events;
            managementViewModel.jazz = jazzs;
            managementViewModel.performer = performers;
            managementViewModel.talking = talkings;

            return managementViewModel;
        }

        public void FillList(ref List<Event> events,ref  List<Jazz> jazz,ref List<Talking> talking,ref DateTime startDate, DateTime endDate)
        {
            foreach (var eve in EventDB.Events.ToList())
            {
                if (eve.EventStart.Day == startDate.Day)
                {
                    events.Add(eve);
                }
            }
            foreach (var jazzs in EventDB.Jazz.ToList())
            {
                if (jazzs.EventStart.Day == startDate.Day)
                {
                    jazz.Add(jazzs);
                }
            }
            foreach (var talk in EventDB.Talking.ToList())
            {
                if (talk.EventStart.Day == startDate.Day)
                {
                    talking.Add(talk);
                }
            }
            if (startDate != endDate) {
                startDate = startDate.AddDays(1);
            }
        }
        public ActionResult SaveJazz([Bind(Include = "Location,Seats,Hall")]Jazz e, [Bind(Include = "PerformerName")]Performer p, DateTime Date, DateTime EventStart, DateTime EventEnd)
        {
            //ok aangezien de datetime niet aangepast kan worden met e.Eventstart.Date moet er eerst een hele datetime gemaakt worden
            DateTime jazzTimeStart = new DateTime(Date.Year, Date.Month, Date.Day, EventStart.Hour, EventStart.Minute, EventStart.Second);
            DateTime jazzTimeEnd = new DateTime(Date.Year, Date.Month, Date.Day, EventEnd.Hour, EventEnd.Minute, EventEnd.Second);
            e.EventStart = jazzTimeStart;
            e.EventEnd = jazzTimeEnd;
            EventDB.Jazz.Add(e);
            EventDB.Performer.Add(p);
            EventDB.SaveChanges();
            ManagementViewModel viewModel = PopulateViewModel();
            return View("index", viewModel);
        }
        public ActionResult UpdateJazz()
        {
            return View("index");
        }
        public ManagementViewModel PopulateViewModel()
        {
            List<Event> events = EventDB.Events.ToList();
            List<Jazz> jazzs = EventDB.Jazz.ToList();
            List<Talking> talkings = EventDB.Talking.ToList();
            List<Performer> performers = EventDB.Performer.ToList();
            ManagementViewModel managementViewModel = new ManagementViewModel();
            managementViewModel.events = events;
            managementViewModel.jazz = jazzs;
            managementViewModel.performer = performers;
            managementViewModel.talking = talkings;

            return managementViewModel;

        }

        public ActionResult ShowEventEdit(int id)
        {

            ManagementViewModel viewModel = PopulateViewModel();
            ViewBag.SelectedEvent = DB.Jazz.Find(id);
            return View("_EditEvent", viewModel);
        }

        public void VerbodenViewBagCode()
        {
            List<Jazz> Jazz = DB.Jazz.ToList();
            List<Performer> Performers = DB.Performer.ToList();
            ViewBag.Jazz = Jazz;
            ViewBag.Performer = Performers;

            //Code om Datefilter te maken
            DateTime lastDate = Jazz[0].EventStart.Date;
            List<string> Dates = new List<string>();
            Dates.Add(Convert.ToString(Jazz[0].EventStart.Date));
            foreach (var fun in Jazz)
            {
                if (fun.EventStart.Date == lastDate)
                {

                }
                else
                {
                    lastDate = fun.EventStart.Date;
                    Dates.Add(Convert.ToString(fun.EventStart.Date));
                }
                
            }
            SelectList startDateSelect = new SelectList(Dates);
            ViewBag.StartDate = startDateSelect;
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
        public Jazz NewJazz(Jazz e, int id)
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
            List<Event> events = DB.Events.ToList();
            List<Jazz> Jazz = DB.Jazz.ToList();
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