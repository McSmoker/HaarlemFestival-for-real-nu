using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haarlem_Festival.ViewModels;
using Haarlem_Festival.Models;
using System.Web.Mvc;

namespace Haarlem_Festival.Repository
{
    public class ManagementRepository
    {
        
        public void UpdateTalking(Talking e, Performer p, Performer p2)
        {
            HaarlemFestivalDB db = new HaarlemFestivalDB();
            //zegt tegen db dat we deze event gaan aanpassen
            db.Entry(e).State = System.Data.Entity.EntityState.Modified;
            //kan een error geven waarom we gebruik maken van deze loop
            //het zou niet meer moeten hitten maar vond het wel een leuk stukje code 
            bool savefailed;
            do
            {
                savefailed = false;
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
                {
                    savefailed = true;
                    ex.Entries.Single().Reload();//eventid faalt
                    //de entry die faalt wordt gereload waardoor de volgende poging niet faalt
                }
            } while (savefailed);


            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            do
            {
                savefailed = false;
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
                {
                    savefailed = true;
                    ex.Entries.Single().Reload();//(wssperformerid)id faalt
                }
            } while (savefailed);
            db.Entry(p2).State = System.Data.Entity.EntityState.Modified;
            do
            {
                savefailed = false;
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
                {
                    savefailed = true;
                    ex.Entries.Single().Reload();//(wssperformerid)id faalt
                }
            } while (savefailed);
            //het is precies 30 amazing
        }
        public void UpdateJazz(Jazz e, Performer p)
        {
            HaarlemFestivalDB db = new HaarlemFestivalDB();
            db.Entry(e).State = System.Data.Entity.EntityState.Modified;
            bool savefailed;
            do
            {
                savefailed = false;
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
                {
                    savefailed = true;
                    ex.Entries.Single().Reload();//eventid faalt
                }
            } while (savefailed);


            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            do
            {
                savefailed = false;
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
                {
                    savefailed = true;
                    ex.Entries.Single().Reload();//(wssperformerid)id faalt
                }
            } while (savefailed);
        }
        public void NewTalking(Talking e, Performer p,Performer p2)
        {
            HaarlemFestivalDB db = new HaarlemFestivalDB();
            db.Talking.Add(e);
            db.Performer.Add(p);
            db.Performer.Add(p2);
            db.SaveChanges();
        }
        public void NewJazz(Jazz e, Performer p)
        {
            HaarlemFestivalDB db = new HaarlemFestivalDB();
            db.Jazz.Add(e);
            db.Performer.Add(p);
            db.SaveChanges();
        }
        public void SetPerformerInfo(string[] txtEdit,int performerid)
        {
            HaarlemFestivalDB db = new HaarlemFestivalDB();
            Performer perfToUpdate = db.Performer.Find(performerid);
            perfToUpdate.PerformerInfo = txtEdit[0];
            bool savefailed;
            db.Entry(perfToUpdate).State = System.Data.Entity.EntityState.Modified;
            do
            {
                savefailed = false;
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
                {
                    savefailed = true;
                    ex.Entries.Single().Reload();//(wssperformerid)id faalt
                }
            } while (savefailed);

        }

        public ManagementViewModel FillViewModel()
        {
            HaarlemFestivalDB db = new HaarlemFestivalDB();
            List<Event> events = db.Events.ToList();
            List<Jazz> jazzs = db.Jazz.ToList();
            List<Talking> talkings = db.Talking.ToList();
            List<Performer> performers = db.Performer.ToList();
            ManagementViewModel managementViewModel = new ManagementViewModel();
            managementViewModel.events = events;
            managementViewModel.jazz = jazzs;
            managementViewModel.performer = performers;
            managementViewModel.talking = talkings;

            return managementViewModel;
        }
        public ManagementViewModel FillViewModelSingleEvent(int eventid)
        {
            HaarlemFestivalDB EventDB = new HaarlemFestivalDB();
            List<Event> events = EventDB.Events.ToList();
            //gets the selected event (same for all list)
            var selectedEvent = events.Find(e => e.EventId == eventid);

            List<Jazz> jazzs = EventDB.Jazz.ToList();
            var selectedEventJazz = jazzs.Find(e => e.EventId == eventid);

            List<Talking> talkings = EventDB.Talking.ToList();
            var selectedEventTalking = talkings.Find(e => e.EventId == eventid);
            //because the difference in Jazz and Talking Performer amounts this split is necessary
            List<Performer> performers = EventDB.Performer.ToList();
            var selectedPerformerJazz = new Performer();
            var selectedPerformerTalkingOne = new Performer();
            var selectedPerformerTalkingTwo = new Performer();
            if (selectedEventJazz == null)
            {
                selectedPerformerTalkingOne = performers.Find(e => e.PerformerId == selectedEventTalking.SpeakerOne.PerformerId);
                selectedPerformerTalkingTwo = performers.Find(e => e.PerformerId == selectedEventTalking.SpeakerTwo.PerformerId);
                performers.Clear();
                performers.Add(selectedPerformerTalkingOne);
                performers.Add(selectedPerformerTalkingTwo);
            }
            if (selectedEventTalking == null)
            {
                selectedPerformerJazz = performers.Find(e => e.PerformerId == selectedEventJazz.PerformerId);
                performers.Clear();
                performers.Add(selectedPerformerJazz);
            }
            //clears the list that have been searched and adds ONLY the selected event
            events.Clear();
            events.Add(selectedEvent);
            jazzs.Clear();
            jazzs.Add(selectedEventJazz);
            talkings.Clear();
            talkings.Add(selectedEventTalking);
            //To reduce this giant method another method was created
            ManagementViewModel viewmodel = SingleEventViewModelFill(events,jazzs,talkings,performers);
            return viewmodel;
        }
        public ManagementViewModel SingleEventViewModelFill(List<Event> events, List<Jazz> jazzs, List<Talking> talkings, List<Performer> performers)
        {
            ManagementViewModel viewmodel = new ManagementViewModel();
            viewmodel.events = events;
            viewmodel.jazz = jazzs;
            viewmodel.talking = talkings;
            viewmodel.performer = performers;
            return viewmodel;
        }
        public int GetPerformerID(int EventId)
        {
            HaarlemFestivalDB db = new HaarlemFestivalDB();
            //stealthdb is noodzakelijk want anders komt de command stream is already openerror
            HaarlemFestivalDB stealthdb = new HaarlemFestivalDB();
            //set to 1 because
            int performerID =1;
            foreach (var eve in db.Jazz)
            {
                if (EventId == eve.EventId)
                    foreach (var perf in stealthdb.Performer)
                    {
                        if (eve.PerformerId == perf.PerformerId)
                        {
                            performerID = perf.PerformerId;
                        }
                    }
            }
            return performerID;
        }
        public int GetEventID(int selectedId)
        {
            HaarlemFestivalDB db = new HaarlemFestivalDB();
            int EventId=1;
            foreach (var eve in db.Jazz)
            {
                if (eve.EventId == selectedId)
                {
                    EventId = eve.EventId;
                }
            }
            return EventId;
        }
        public Volunteer LoginUser(Volunteer volModel)
        {
            Volunteer userdata = new Volunteer();
            HaarlemFestivalDB db = new HaarlemFestivalDB();
            userdata = db.Volunteers.Where(x => x.Name == volModel.Name && x.Password == volModel.Password).FirstOrDefault();
            return userdata;
        }
        public IList<Event> GetSalesList()
        {
            HaarlemFestivalDB db = new HaarlemFestivalDB();
            return db.Events.ToList();
        }
    }
}