using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haarlem_Festival.Models;
using Haarlem_Festival.Repository;

namespace Haarlem_Festival.Controllers
{
    public class LoginController : Controller
    {
        HaarlemFestivalRepository repo = new HaarlemFestivalRepository();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Volunteer volModel,FormCollection formcollection)
        {
            string category = formcollection["submit"];
            var userDetails = repo.LoginUser(volModel);
            if (category == "SiteManagement")
            {
                if (userDetails == null)
                {
                    //errormessage meesturen
                    return View("Index");
                }
                else
                {
                    Session["userID"] = userDetails.VolunteerId;
                    return RedirectToAction("Index", "SiteManagement");
                }
            }
            if (category == "EventManagement")
            {
                if (userDetails == null)
                {
                    //errormessage meesturen
                    return View("Index");
                }
                else
                {
                    Session["userID"] = userDetails.VolunteerId;
                    return RedirectToAction("Index", "Management");
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}