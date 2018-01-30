using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haarlem_Festival.ViewModels;
using Haarlem_Festival.Models;
using System.Web.Mvc;

namespace Haarlem_Festival.Repositories
{
    public class HaarlemFestivalRepository
    {
        
        public void UpdateTalking(Talking e, Performer p, Performer p2)
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
        //public ManagementViewModel FillViewModelSingleEvent()
        //{

        //}
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
    }
}