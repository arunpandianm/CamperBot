using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CamperBot_FCC_Status_Viewer.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings/UserExcluder
        public ActionResult UserExcluder()
        {
            return View();
        }
    }
}