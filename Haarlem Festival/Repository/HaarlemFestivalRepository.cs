using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haarlem_Festival.ViewModels;
using Haarlem_Festival.Models;
using System.Web.Mvc;

namespace Haarlem_Festival.Repository
{
    public class HaarlemFestivalRepository
    {
        HaarlemFestivalDB db = new HaarlemFestivalDB();
        public void UpdateTalking(Talking e, Performer p, Performer p2)
        {
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
        public void NewTalking(Talking e, Performer p)
        {
            db.Talking.Add(e);
            db.Performer.Add(p);
            db.SaveChanges();
        }
        public void NewJazz(Jazz e, Performer p)
        {
            db.Jazz.Add(e);
            db.Performer.Add(p);
            db.SaveChanges();
        }
    }
}