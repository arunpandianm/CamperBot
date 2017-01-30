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
        // Generate monthly graph
        public ActionResult MonthlyWiseHistory()
        {
            List<Models.monthly_update_list> myMonthlyUpdateList = CamperBot_FCC_Status_Viewer.Models.MonthlyChartModel.DatabaseConnection();
            return View(myMonthlyUpdateList);
        }
        // GET: History/YearlyWiseHistory
        // Generate yearly graph
        public ActionResult YearlyWiseHistory()
        {
            List<Models.yearly_update_list> myYearlyUpdateList = CamperBot_FCC_Status_Viewer.Models.YearlyChartModel.DatabaseConnection();
            return View(myYearlyUpdateList);
        }
    }
}