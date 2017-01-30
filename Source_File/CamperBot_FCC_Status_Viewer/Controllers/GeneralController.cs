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
        // Display the current month status chart
        public ActionResult Dashboard()
        {
            List<Models.current_month_update_list> currentMonthUpdateList = CamperBot_FCC_Status_Viewer.Models.DashboardChartModel.FetchCurrentMonthStatus();
            return View(currentMonthUpdateList);
        }

        // GET: General/UserRankViewer
        // List the user in rank order
        public ActionResult UserRankViewer()
        {
            //CamperBot_FCC_Status_Viewer.Models = namespace
            //UserRankViewerModel = class name
            //DatabaseConnection() = method in that class
            List<Models.rank_list> camperRankList = CamperBot_FCC_Status_Viewer.Models.UserRankViewerModel.GenerateRankList();
            return View(camperRankList);
        }

        // GET: General/AllUserDetailsDatabase
        // Fetch all data from user table and generate a list
        public ActionResult AllUserDetailsDatabase()
        {
            List<Models.database_list> allUserDetailList = CamperBot_FCC_Status_Viewer.Models.FetchUserDetailsTableModel.FetchUserDetailsTable();
            return View(allUserDetailList);
        }

        // GET: General/UserProfileViewer
        // Fetch the complete data of particular user and generate a list
        public ActionResult UserProfileViewer(string userId)
        {
            List<Models.user_profile_details> UserProfileDetails = CamperBot_FCC_Status_Viewer.Models.UserProfileModel.FetchUserProfileDetails(userId);
            return View(UserProfileDetails);
        }
    }
}