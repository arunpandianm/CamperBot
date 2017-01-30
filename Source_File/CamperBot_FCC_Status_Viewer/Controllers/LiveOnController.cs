using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CamperBot_FCC_Status_Viewer.Controllers
{
    public class LiveOnController : Controller
    {
        // GET: LiveOn/LiveUpdate
        // Get the live status of the user
        public ActionResult LiveUpdate()
        {
            return View();
        }
    }
}