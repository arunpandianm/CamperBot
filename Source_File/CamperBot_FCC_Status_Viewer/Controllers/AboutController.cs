using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CamperBot_FCC_Status_Viewer.Controllers
{
    public class AboutController : Controller
    {
        // GET: About/DeveloperTeam
        public ActionResult DeveloperTeam()
        {
            return View();
        }

        // GET: About/FccStautsViewer
        public ActionResult FccStatusViewer()
        {
            return View();
        }
    }
}