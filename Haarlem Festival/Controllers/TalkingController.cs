using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Haarlem_Festival.Models;
using Haarlem_Festival.Interfaces;
using Haarlem_Festival.Repository;

namespace Haarlem_Festival.Controllers
{
    public class TalkingController : Controller
    {
        private ITalkingRepository talkrp = new TalkingRepository();

        // GET: /Talking/
        public ActionResult Index()
        {
            var talkingevents = talkrp.GetTalkingEvents();
            return View(talkingevents);
        }
    }
}
