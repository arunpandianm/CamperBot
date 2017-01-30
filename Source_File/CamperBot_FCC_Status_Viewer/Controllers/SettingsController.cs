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
        // Display the user based on excluder list
        public ActionResult UserExcluder()
        {
            List<Models.database_list> allUserDetailList = CamperBot_FCC_Status_Viewer.Models.FetchUserDetailsTableModel.FetchUserDetailsTable();
            return View(allUserDetailList);
        }

        // GET: Settings/AddUserExclusion
        // Add user to excluder list
        public ActionResult AddUserExclusion(string userName)
        {
            CamperBot_FCC_Status_Viewer.Models.UserExclusionModel.AddUserExclusion(userName);
            return RedirectToAction("UserExcluder","Settings");
        }
        
        // GET: Settings/RemoveUserExclusion
        // Remove user from excluder list
        public ActionResult RemoveUserExclusion(string userName)
        {
            CamperBot_FCC_Status_Viewer.Models.UserExclusionModel.RemoveUserExclusion(userName);
            return RedirectToAction("UserExcluder", "Settings");
        }
    }
}