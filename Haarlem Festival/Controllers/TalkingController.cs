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
    public class TalkingController : Controller
    {
        private HaarlemFestivalDB db = new HaarlemFestivalDB();

        // GET: /Talking/
        public ActionResult Index()
        {
            return View(db.Talking.ToList());
        }

        // GET: /Talking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talking talking = db.Talking.Find(id);
            if (talking == null)
            {
                return HttpNotFound();
            }
            return View(talking);
        }

        // GET: /Talking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Talking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="EventId,EventDateTime,Location,Price,Seats,TicketsSold,Speaker1,Speaker2")] Talking talking)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(talking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(talking);
        }

        // GET: /Talking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talking talking = db.Talking.Find(id);
            if (talking == null)
            {
                return HttpNotFound();
            }
            return View(talking);
        }

        // POST: /Talking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="EventId,EventDateTime,Location,Price,Seats,TicketsSold,Speaker1,Speaker2")] Talking talking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(talking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(talking);
        }

        // GET: /Talking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talking talking = db.Talking.Find(id);
            if (talking == null)
            {
                return HttpNotFound();
            }
            return View(talking);
        }

        // POST: /Talking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Talking talking = db.Talking.Find(id);
            db.Events.Remove(talking);
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
