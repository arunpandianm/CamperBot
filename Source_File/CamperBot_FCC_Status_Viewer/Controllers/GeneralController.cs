using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CamperBot_FCC_Status_Viewer.Controllers
{
    public class GeneralController : Controller
    {
        // GET: General/Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: General/UserProfileViewer
        public ActionResult UserProfileViewer()
        {
            return View();
        }
        // GET: General/UserRankViewer
        public ActionResult UserRankViewer()
        {
            return View();
        }
    }
}