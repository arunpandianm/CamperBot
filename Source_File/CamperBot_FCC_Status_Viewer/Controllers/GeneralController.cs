using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CamperBot_FCC_Status_Viewer.Controllers
{
    public class GeneralController : Controller
    {
        const string clientId = "28bfffe90ee14b851674";
        private const string clientSecret = "049d738c88dc3bb0a352eff6663a7c03b44311b0";
        readonly GitHubClient client =
            new GitHubClient(new ProductHeaderValue("Haack-GitHub-Oauth-Demo"));

        // GET: General/Dashboard
        // Display the current month status chart
        public async Task<ActionResult> Dashboard()
        {
            var accessToken = Session["OAuthToken"] as string;
            if (accessToken != null)
            {
                // This allows the client to make requests to the GitHub API on the user's behalf
                // without ever having the user's OAuth credentials.
                client.Credentials = new Credentials(accessToken);
            }

            try
            {
                // The following requests retrieves all of the user's repositories and
                // requires that the user be logged in to work.
                var repositories = await client.Repository.GetAllForCurrent();
                //var model = new DashboardChartModel;

                //return View(model);
                //var repositories = await client.Repository.GetAllForCurrent();
                //var model = new CamperBot_FCC_Status_Viewer.Models.IndexViewModel(repositories);
                List<Models.current_month_update_list> currentMonthUpdateList = CamperBot_FCC_Status_Viewer.Models.DashboardChartModel.FetchCurrentMonthStatus();
                return View(currentMonthUpdateList);
            }
            catch (AuthorizationException)
            {
                // Either the accessToken is null or it's invalid. This redirects
                // to the GitHub OAuth login page. That page will redirect back to the
                // Authorize action.
                return Redirect(GetOauthLoginUrl());
            }
        }

        // This is the Callback URL that the GitHub OAuth Login page will redirect back to.
        public async Task<ActionResult> Authorize(string code, string state)
        {
            if (!String.IsNullOrEmpty(code))
            {
                var expectedState = Session["CSRF:State"] as string;
                if (state != expectedState) throw new InvalidOperationException("SECURITY FAIL!");
                Session["CSRF:State"] = null;

                var token = await client.Oauth.CreateAccessToken(
                    new OauthTokenRequest(clientId, clientSecret, code));
                Session["OAuthToken"] = token.AccessToken;
            }

            return RedirectToAction("Index");
        }

        private string GetOauthLoginUrl()
        {
            string csrf = Membership.GeneratePassword(24, 1);
            Session["CSRF:State"] = csrf;

            // 1. Redirect users to request GitHub access
            var request = new OauthLoginRequest(clientId)
            {
                Scopes = { "user", "notifications" },
                State = csrf
            };
            var oauthLoginUrl = client.Oauth.GetGitHubLoginUrl(request);
            return oauthLoginUrl.ToString();
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