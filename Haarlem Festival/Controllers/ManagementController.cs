using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haarlem_Festival.Models;
using Haarlem_Festival.ViewModels;
using System.Data.SqlClient;
using System.Web.Mvc.Html;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Haarlem_Festival.Repositories;

namespace Haarlem_Festival.Controllers
{
    public class ManagementController : Controller
    {
        ManagementRepository repo = new ManagementRepository();
        
        // Standaard managment View
        public ActionResult Index()
        {
            ManagementViewModel viewModel = repo.FillViewModel();
            return View("Index", viewModel);

        }
        //This is used to redirect to the correct View based on the selection
        public ActionResult CreateEvent(FormCollection formCollection)
        {
            string selectedEvent = formCollection["Event"];
            if (selectedEvent == "Jazz")
            {
                return View("CreateEvent");
            }
            if (selectedEvent == "Talking")
            {
                return View("CreateTalkingEvent");
            }
            ManagementViewModel viewModel = repo.FillViewModel();
            return View("Index", viewModel);
        }

        //wordt gebruikt in de new jazz view
        public ActionResult SaveJazz([Bind(Include = "Location,Seats,Hall,Price,Comment")]Jazz e, [Bind(Include = "PerformerName")]Performer p, DateTime Date, DateTime EventStart, DateTime EventEnd)
        {
            //ok aangezien de datetime niet aangepast kan worden met e.Eventstart.Date moet er eerst een hele datetime gemaakt worden
            DateTime jazzTimeStart = new DateTime(Date.Year, Date.Month, Date.Day, EventStart.Hour, EventStart.Minute, EventStart.Second);
            DateTime jazzTimeEnd = new DateTime(Date.Year, Date.Month, Date.Day, EventEnd.Hour, EventEnd.Minute, EventEnd.Second);
            //deze nieuwe datetime wordt aan event toegevoegd
            e.EventStart = jazzTimeStart;
            e.EventEnd = jazzTimeEnd;
            //stuurt ze naar repo waar opslag plaatsvind
            repo.NewJazz(e, p);
            ManagementViewModel viewModel = repo.FillViewModel();
            return View("index", viewModel);
        }
        //voor de talking view
        public ActionResult SaveTalking([Bind(Include = "Location,Seats,Price,Comment")]Talking e, [Bind(Include = "PerformerName")]Performer p, DateTime Date, DateTime EventStart, DateTime EventEnd, string PerformerName2)
        {
            DateTime jazzTimeStart = new DateTime(Date.Year, Date.Month, Date.Day, EventStart.Hour, EventStart.Minute, EventStart.Second);
            DateTime jazzTimeEnd = new DateTime(Date.Year, Date.Month, Date.Day, EventEnd.Hour, EventEnd.Minute, EventEnd.Second);
            //talking events have 2 performers so the second performer is created here
            Performer p2 = new Performer();
            p2.PerformerName = PerformerName2;
            e.EventStart = jazzTimeStart;
            e.EventEnd = jazzTimeEnd;
            //and bound to the event here
            e.SpeakerOne = p;
            e.SpeakerTwo = p2;
            //and lastly saved here
            repo.NewTalking(e, p, p2);
            ManagementViewModel viewModel = repo.FillViewModel();
            return View("index", viewModel);
        }
        public ActionResult UpdateJazz([Bind(Include = "EventId,Location,Seats,Hall,PerformerId,Price,Comment")]Jazz e, [Bind(Include = "PerformerName,PerformerId,PerformerInfo,PerformerImagePath")]Performer p, DateTime Date, DateTime EventStart, DateTime EventEnd)
        {
            //modelstate checking happens in repo now
            //if (ModelState.IsValid)
                e.Artist = p;
            DateTime jazzTimeStart = new DateTime(Date.Year, Date.Month, Date.Day, EventStart.Hour, EventStart.Minute, EventStart.Second);
            DateTime jazzTimeEnd = new DateTime(Date.Year, Date.Month, Date.Day, EventEnd.Hour, EventEnd.Minute, EventEnd.Second);
            e.EventStart = jazzTimeStart;
            e.EventEnd = jazzTimeEnd;
            repo.UpdateJazz(e, p);
            ManagementViewModel viewModel = repo.FillViewModel();
            return View("index", viewModel);
        }
        public ActionResult UpdateTalking([Bind(Include = "EventId,Location,Seats,Price,Comment")]Talking e, [Bind(Include = "PerformerName,PerformerId,PerformerInfo,PerformerImagePath")]Performer p, DateTime Date, DateTime EventStart, DateTime EventEnd, int PerformerId2, string PerformerName2, string PerformerInfo2, string PerformerImagePath2)
        {
            //because we have an updatetalking instead of newtalking here the entirity of Performer must be filled
            Performer p2 = new Performer();
            p2.PerformerId = PerformerId2;
            p2.PerformerImagePath = PerformerImagePath2;
            p2.PerformerInfo = PerformerInfo2;
            p2.PerformerName = PerformerName2;
            e.SpeakerOne = p;
            e.SpeakerTwo = p2;
            DateTime jazzTimeStart = new DateTime(Date.Year, Date.Month, Date.Day, EventStart.Hour, EventStart.Minute, EventStart.Second);
            DateTime jazzTimeEnd = new DateTime(Date.Year, Date.Month, Date.Day, EventEnd.Hour, EventEnd.Minute, EventEnd.Second);
            e.EventStart = jazzTimeStart;
            e.EventEnd = jazzTimeEnd;
            repo.UpdateTalking(e, p, p2);
            ManagementViewModel viewModel = repo.FillViewModel();
            return View("index", viewModel);
        }
        //opens the edit screens
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetSelectedEvent(FormCollection formCollection)
        {
            int EventId = Convert.ToInt32(formCollection["eventid"]);
            ManagementViewModel viewModel = repo.FillViewModelSingleEvent(EventId);
            return View("EditJazzEvent", viewModel);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetSelectedTalkingEvent(FormCollection formCollection)
        {
            int EventId = Convert.ToInt32(formCollection["eventid"]);
            ManagementViewModel viewModel = repo.FillViewModelSingleEvent(EventId);
            return View("EditTalkingEvent", viewModel);
        }
        //method used by exporttoexcel gets DB data to process
        public IList<Event> GetSalesList()
        {
            IList<Event> salesList = repo.GetSalesList();
            return salesList;
        }
        public ActionResult ExportToExcel()
        {
            //de gridview is de manier om de db data in tabel te krijgen 
            var gv = new GridView();
            gv.DataSource = this.GetSalesList();
            gv.DataBind();
            //opent de excel generator
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            //schrijft gv naar excel bestand
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            //sluit de excel generator 
            Response.Flush();
            Response.End();
            ManagementViewModel viewModel = repo.FillViewModel();
            return View("Index", viewModel);
        }        
        //public ActionResult SalesData()
        //{
        //    ManagementViewModel viewModel = repo.FillViewModel();
        //    return View("SalesData", viewModel);
        //}
        //public ActionResult ContentManagement()
        //{
        //    ManagementViewModel viewModel = PopulateViewModel();
        //    return View("ContentManagement", viewModel);
        //}
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult TextEdit(string[] txtEdit, FormCollection formCollection)
        //{
        //    HaarlemFestivalDB performerDB = new HaarlemFestivalDB();
        //    string textEdit = txtEdit[0];
        //    int EventId = Convert.ToInt32(formCollection["eventid"]);
        //    int performerId = 1;
        //    foreach (var eve in EventDB.Jazz)
        //    {
        //        if (EventId == eve.EventId)
        //            foreach (var perf in performerDB.Performer)
        //            {
        //                if (eve.PerformerId == perf.PerformerId)
        //                {
        //                    performerId = perf.PerformerId;
        //                }
        //            }
        //    }
        //    using (var ctx = new HaarlemFestivalDB())
        //    {
        //        ctx.Database.ExecuteSqlCommand("Update Performer set PerformerInfo={0} where PerformerId={1}", textEdit, performerId);
        //    }
        //    Performer perfToUpdate = new Performer();
        //    foreach (var eve in EventDB.Jazz)
        //    {
        //        if (EventId == eve.EventId)
        //            foreach (var perf in performerDB.Performer)
        //            {
        //                if (eve.PerformerId == perf.PerformerId)
        //                {
        //                    perfToUpdate = perf;
        //                }
        //            }
        //    }

        //    ManagementViewModel viewModel = PopulateViewModel();
        //    return View("ContentManagement", viewModel);
        //}


        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult GetContent(FormCollection formCollection)
        //{
        //    HaarlemFestivalDB performerDB = new HaarlemFestivalDB();
        //    int EventId = Convert.ToInt32(formCollection["eventid"]);
        //    foreach (var eve in EventDB.Jazz)
        //    {
        //        if (eve.EventId == EventId)
        //        {
        //            ViewBag.selected = eve.EventId;
        //        }
        //    }
        //    ManagementViewModel viewModel = PopulateViewModel();
        //    return View("ContentManagement", viewModel);
        //}
        //public ActionResult FileUpload(HttpPostedFileBase file,string oripath)
        //{
        //    if (file != null)
        //    {
        //        string pic = System.IO.Path.GetFileName(oripath);
        //        string path = System.IO.Path.Combine(
        //                               Server.MapPath("~/Content/img/jazz"), pic);
        //        // file is uploaded
        //        file.SaveAs(path);

        //        // save the image path path to the database or you can send image 
        //        // directly to database
        //        // in-case if you want to store byte[] ie. for DB
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            file.InputStream.CopyTo(ms);
        //            byte[] array = ms.GetBuffer();
        //        }

        //    }
        //    // after successfully uploading redirect the user
        //    ManagementViewModel viewModel = PopulateViewModel();
        //    return View("ContentManagement", viewModel);
        //}
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ContentManagent(FormCollection formCollection)
        //{
        //    VerbodenViewBagCode();
        //    DateTime startDate = Convert.ToDateTime(formCollection["Date"]);
        //    ManagementViewModel viewModel = SingleDateModel(startDate);
        //    return View("ContentManagement", viewModel);
        //}
        //public ManagementViewModel SingleDateModel(DateTime date)
        //{
        //    List<Event> events = EventDB.Events.ToList();
        //    List<Jazz> jazzs = EventDB.Jazz.ToList();
        //    List<Talking> talkings = EventDB.Talking.ToList();
        //    List<Performer> performers = EventDB.Performer.ToList();
        //    foreach (var e in events)
        //    {
        //        if (e.EventStart != date)
        //            events.Remove(e);
        //    }
        //    foreach (var e in jazzs)
        //    {
        //        if (e.EventStart != date)
        //            jazzs.Remove(e);
        //    }
        //    foreach (var e in talkings)
        //    {
        //        if (e.EventStart != date)
        //            talkings.Remove(e);
        //    }
        //    ManagementViewModel viewmodel = new ManagementViewModel();
        //    viewmodel.events = events;
        //    viewmodel.jazz = jazzs;
        //    viewmodel.talking = talkings;
        //    return viewmodel;
        //}

        //public ManagementViewModel SingleEventViewModel(int eventid)
        //{
        //    List<Event> events = EventDB.Events.ToList();
        //    var selectedEvent = events.Find(e => e.EventId == eventid);

        //    List<Jazz> jazzs = EventDB.Jazz.ToList();
        //    var selectedEventJazz = jazzs.Find(e => e.EventId == eventid);

        //    List<Talking> talkings = EventDB.Talking.ToList();
        //    var selectedEventTalking = talkings.Find(e => e.EventId == eventid);

        //    List<Performer> performers = EventDB.Performer.ToList();
        //    var selectedPerformerJazz = new Performer();
        //    var selectedPerformerTalkingOne = new Performer();
        //    var selectedPerformerTalkingTwo = new Performer();
        //    if (selectedEventJazz == null)
        //    {
        //        selectedPerformerTalkingOne = performers.Find(e => e.PerformerId == selectedEventTalking.SpeakerOne.PerformerId);
        //        selectedPerformerTalkingTwo = performers.Find(e => e.PerformerId == selectedEventTalking.SpeakerTwo.PerformerId);
        //        performers.Clear();
        //        performers.Add(selectedPerformerTalkingOne);
        //        performers.Add(selectedPerformerTalkingTwo);
        //    }
        //    if (selectedEventTalking == null)
        //    {
        //        selectedPerformerJazz = performers.Find(e => e.PerformerId == selectedEventJazz.PerformerId);
        //        performers.Clear();
        //        performers.Add(selectedPerformerJazz);
        //    }
        //    events.Clear();
        //    events.Add(selectedEvent);
        //    jazzs.Clear();
        //    jazzs.Add(selectedEventJazz);
        //    talkings.Clear();
        //    talkings.Add(selectedEventTalking);
        //    ////UGHHH FUCKING PERFORMERS
        //    //List<Performer> performers = new List<Performer>();
        //    //foreach (var eve in performers)
        //    //{
        //    //   if (eve.EventId != eventid)
        //    //  {
        //    //     events.Remove(eve);
        //    //}
        //    //}
        //    ManagementViewModel viewmodel = new ManagementViewModel();
        //    viewmodel.events = events;
        //    viewmodel.jazz = jazzs;
        //    viewmodel.talking = talkings;
        //    viewmodel.performer = performers;
        //    return viewmodel;
        //}
        //public ManagementViewModel FilteredViewModel(DateTime startDate, DateTime endDate)
        //{

        //    List<Event> events = new List<Event>();
        //    List<Jazz> jazzs = new List<Jazz>();
        //    List<Talking> talkings = new List<Talking>();
        //    List<Performer> performers = EventDB.Performer.ToList();
        //    ManagementViewModel managementViewModel = new ManagementViewModel();
        //    FillList(ref events, ref jazzs, ref talkings, ref startDate, endDate);
        //    while (startDate != endDate)
        //    {
        //        FillList(ref events, ref jazzs, ref talkings, ref startDate, endDate);
        //    }
        //    managementViewModel.events = events;
        //    managementViewModel.jazz = jazzs;
        //    managementViewModel.performer = performers;
        //    managementViewModel.talking = talkings;

        //    return managementViewModel;
        //}

        //public void FillList(ref List<Event> events, ref List<Jazz> jazz, ref List<Talking> talking, ref DateTime startDate, DateTime endDate)
        //{
        //    foreach (var eve in EventDB.Events.ToList())
        //    {
        //        if (eve.EventStart.Day == startDate.Day)
        //        {
        //            events.Add(eve);
        //        }
        //    }
        //    foreach (var jazzs in EventDB.Jazz.ToList())
        //    {
        //        if (jazzs.EventStart.Day == startDate.Day)
        //        {
        //            jazz.Add(jazzs);
        //        }
        //    }
        //    foreach (var talk in EventDB.Talking.ToList())
        //    {
        //        if (talk.EventStart.Day == startDate.Day)
        //        {
        //            talking.Add(talk);
        //        }
        //    }
        //    if (startDate != endDate)
        //    {
        //        startDate = startDate.AddDays(1);
        //    }
        //}

        //public ManagementViewModel PopulateViewModel()
        //{
        //    List<Event> events = EventDB.Events.ToList();
        //    List<Jazz> jazzs = EventDB.Jazz.ToList();
        //    List<Talking> talkings = EventDB.Talking.ToList();
        //    List<Performer> performers = EventDB.Performer.ToList();
        //    ManagementViewModel managementViewModel = new ManagementViewModel();
        //    managementViewModel.events = events;
        //    managementViewModel.jazz = jazzs;
        //    managementViewModel.performer = performers;
        //    managementViewModel.talking = talkings;

        //    return managementViewModel;

        //}

        //public ActionResult ShowEventEdit(int id)
        //{

        //    ManagementViewModel viewModel = PopulateViewModel();
        //    //ViewBag.SelectedEvent = DB.Jazz.Find(id);
        //    return View("_EditEvent", viewModel);
        //}

        //public void VerbodenViewBagCode()
        //{
        //    List<Jazz> Jazz = DB.Jazz.ToList();
        //    List<Performer> Performers = DB.Performer.ToList();
        //    ViewBag.Jazz = Jazz;
        //    ViewBag.Performer = Performers;

        //    //Code om Datefilter te maken
        //    DateTime lastDate = Jazz[0].EventStart.Date;
        //    List<string> Dates = new List<string>();
        //    Dates.Add(Convert.ToString(Jazz[0].EventStart.Date));
        //    foreach (var fun in Jazz)
        //    {
        //        if (fun.EventStart.Date == lastDate)
        //        {

        //        }
        //        else
        //        {
        //            lastDate = fun.EventStart.Date;
        //            Dates.Add(Convert.ToString(fun.EventStart.Date));
        //        }

        //    }
        //    SelectList startDateSelect = new SelectList(Dates);
        //    ViewBag.StartDate = startDateSelect;
        //}

        //public ActionResult CreateEvent(Jazz e, string BtnSubmit)
        //{
        //    switch (BtnSubmit)
        //    {
        //        case "Save Event":
        //            EventDB.Jazz.Add(e);
        //            EventDB.SaveChanges();
        //            return View("Index");
        //    }
        //    return View("CreateEvent");
        //    ///SaveEvent(e);
        //    ////return RedirectToAction("Index"); ;
        //}

        //public ActionResult EditEvent(Jazz e)
        //{
        //    return View("EditEvent");
        //}

        //public string SaveEvent(Jazz e)
        //{
        //    return e.EventEnd + "|" + e.EventStart + "|" + e.Location + "|" + e.Seats + "|" + e.TicketsSold + "|";
        //}
        /// <summary>
        /// deze jokes moeten in een busineeslayer genaamd EventBusinesslayer maybe die hierboven ook
        /// Deze jokes doorzetten gaat veel tijd kosten om te fixen dus w8 op input van iemand anders hierover
        /// </summary>
        /// <returns></returns>
        /// 
        //public Jazz NewJazz(Jazz e, int id)
        //{
        //    var eventToUpdate = EventDB.Events.Find(id);
        //    TryUpdateModel(eventToUpdate, "", new string[] { "EventStart,EventEnd,Location,Seats,Artist,Hall,TicketsSold" });
        //    return e;
        //}
        //private void PopulateEventsDropDownList(object selectedEvent = null)
        //{
        //    List<Event> Events = EventDB.Events.ToList();
        //    ViewBag.EventId = new SelectList(EventDB.Events.ToList(), "EventId", "Location", selectedEvent);
        //}

        //HaarlemFestivalDB DB = new HaarlemFestivalDB();// DB Entity Object  

        //public ActionResult TestView()
        //{
        //    //using viewdata  
        //    //dit helpt niet met de idiote database die we hebben waar je shit uit 3 tabellen moet halen fucking
        //    //var dropdownVD = new SelectList(DB.Events.ToList(), "EventId", "stud_name");
        //    var dropdownVD = CreatePerformerDropList();
        //    ViewData["StudDataVD"] = dropdownVD;
        //    //dus gaan we dit proberen --CreateDropList()

        //    //using viewbag  

        //    ViewBag.dropdownVD = dropdownVD;
        //    return View();
        //}

        //public SelectList CreatePerformerDropList()
        //{
        //    List<Event> events = DB.Events.ToList();
        //    List<Jazz> Jazz = DB.Jazz.ToList();
        //    List<Performer> Performers = DB.Performer.ToList();
        //    //Create a list of select list items - this will be returned as your select list
        //    List<SelectListItem> newList = new List<SelectListItem>();
        //    //deze zooi in een loopje doen op basis van events.Length
        //    //Create the select list item you want to add
        //    //geen oplossing nog voor het feit dat Er maar 3 talking en 24 Jazzs zijn maar daar kom ik nog wel uit
        //    for (int i = 0; i < Performers.Count; i++)
        //    {
        //        SelectListItem selListItem = new SelectListItem() { Value = Convert.ToString(events[i].EventId), Text = Performers[i].PerformerName };


        //        //Add select list item to list of selectlistitems
        //        newList.Add(selListItem);
        //    }

        //    //Return the list of selectlistitems as a selectlist
        //    return new SelectList(newList, "Value", "Text", null);

        //}

        //public JsonResult GetStudents()//ajax calls this function which will return json object  

        //{
        //    var resultData = DB.Events.Select(c => new { Value = c.EventId, Text = c.Location }).ToList();
        //    return Json(new { result = resultData }, JsonRequestBehavior.AllowGet);
        //}
    }
}