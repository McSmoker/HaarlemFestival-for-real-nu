using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Haarlem_Festival.Models;

namespace Haarlem_Festival.Controllers
{
    public class JazzController : Controller
    {
        private HaarlemFestivalDB db = new HaarlemFestivalDB();

        // GET: Jazz
        public ActionResult Index()
        {
            var events = db.Jazz.Include(j => j.Artist);
            return View(events.ToList());
        }

        // GET: Jazz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jazz jazz = db.Jazz.Find(id);
            if (jazz == null)
            {
                return HttpNotFound();
            }
            return View(jazz);
        }

        // GET: Jazz/Create
        public ActionResult Create()
        {
            ViewBag.PerformerId = new SelectList(db.Performer, "PerformerId", "PerformerName");
            return View();
        }

        // POST: Jazz/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,EventStart,EventEnd,Location,Seats,TicketsSold,Hall,PerformerId")] Jazz jazz)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(jazz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PerformerId = new SelectList(db.Performer, "PerformerId", "PerformerName", jazz.PerformerId);
            return View(jazz);
        }

        // GET: Jazz/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jazz jazz = db.Jazz.Find(id);
            if (jazz == null)
            {
                return HttpNotFound();
            }
            ViewBag.PerformerId = new SelectList(db.Performer, "PerformerId", "PerformerName", jazz.PerformerId);
            return View(jazz);
        }

        // POST: Jazz/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,EventStart,EventEnd,Location,Seats,TicketsSold,Hall,PerformerId")] Jazz jazz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jazz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PerformerId = new SelectList(db.Performer, "PerformerId", "PerformerName", jazz.PerformerId);
            return View(jazz);
        }

        // GET: Jazz/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jazz jazz = db.Jazz.Find(id);
            if (jazz == null)
            {
                return HttpNotFound();
            }
            return View(jazz);
        }

        // POST: Jazz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jazz jazz = db.Jazz.Find(id);
            db.Events.Remove(jazz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
