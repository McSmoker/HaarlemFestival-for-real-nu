using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haarlem_Festival.ViewModels;
using Haarlem_Festival.Repository;
using Haarlem_Festival.Models;
using System.IO;

namespace Haarlem_Festival.Controllers
{
    public class SiteManagementController : Controller
    {
        // GET: SiteManagement
        ManagementRepository repo = new ManagementRepository();
        public ActionResult Index()
        {
            ManagementViewModel viewModel = repo.FillViewModel();
            return View("Index", viewModel);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TextEdit(string[] txtEdit, FormCollection formCollection)
        {
            string textEdit = txtEdit[0];
            int EventId = Convert.ToInt32(formCollection["eventid"]);
            int perfId = Convert.ToInt32(formCollection["perfid"]);
            if (perfId == 0)
            {
                perfId = repo.GetPerformerID(EventId);
            }
            //mag niet Anne fix dit ok Anne ik heb het gefixt
            //using (var ctx = new HaarlemFestivalDB())
            //{
            //    ctx.Database.ExecuteSqlCommand("Update Performer set PerformerInfo={0} where PerformerId={1}", textEdit, performerId);
            //}
            repo.SetPerformerInfo(txtEdit, perfId);
            Performer perfToUpdate = new Performer();
            //foreach (var eve in EventDB.Jazz)
            //{
            //    if (EventId == eve.EventId)
            //        foreach (var perf in performerDB.Performer)
            //        {
            //            if (eve.PerformerId == perf.PerformerId)
            //            {
            //                perfToUpdate = perf;
            //            }
            //        }
            //}

            ManagementViewModel viewModel = repo.FillViewModel();
            return View("Index", viewModel);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetContent(FormCollection formCollection)
        {
            //vieze viewbag maar waarom is het zo vies
            //kan net zo goed session ofzo gebruiken maar whats the point 
            ManagementViewModel viewModel = repo.FillViewModel();
            viewModel.selected = Convert.ToInt32(formCollection["eventid"]);
            viewModel.selectedPerformer = Convert.ToInt32(formCollection["perfid"]);
            //ff snel voor een test
            return View("Index", viewModel);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetHomeContent(FormCollection formCollection)
        {
            int EventId = Convert.ToInt32(formCollection["eventid"]);
            ManagementViewModel viewModel = repo.FillViewModel();
            viewModel.selected= Convert.ToInt32(formCollection["eventid"]);
            return View("Index", viewModel);
        }
        public ActionResult FileUpload(HttpPostedFileBase file, string oripath,FormCollection formCollection)
        {
            string category = formCollection["category"];
            //moet dit in db het doet wel iets met db in memorystream
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(oripath);
                string path =  "emptypath";
                if (category == "jazz") {
                    path = System.IO.Path.Combine(
                    Server.MapPath("~/Content/img/jazz"), pic);
                }
                if (category == "talking")
                {
                    path = System.IO.Path.Combine(
                    Server.MapPath("~/Content/img/Talking"), pic);
                }
                if (category == "home")
                {
                    path = System.IO.Path.Combine(
                    Server.MapPath("~/Content/img/home"), pic);
                }

                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                if (category != "home")
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                }

            }
            // after successfully uploading redirect the user
            ManagementViewModel viewModel = repo.FillViewModel();
            return View("Index", viewModel);
        }
    }
}