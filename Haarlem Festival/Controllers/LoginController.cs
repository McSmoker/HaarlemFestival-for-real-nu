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
        ManagementRepository repo = new ManagementRepository();
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
                    //pls dont kill me maar ik ga een viewbag voor de errormessage gebruiken lijkt me beter dan geen errormessage hebben
                    ViewBag.ErrorMessage = "User Not Found";
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
                    ViewBag.ErrorMessage = "User Not Found";
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