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
