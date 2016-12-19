using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CamperBot_FCC_Status_Viewer.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History/MonthlyWiseHistory
        public ActionResult MonthlyWiseHistory()
        {
            return View();
        }
        // GET: History/YearlyWiseHistory
        public ActionResult YearlyWiseHistory()
        {
            return View();
        }
    }
}