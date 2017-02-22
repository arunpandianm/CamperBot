using Octokit;
using System;
using System.Collections.Generic;
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
            new GitHubClient(new ProductHeaderValue("Camperbot-FCC-Status-Viewer"));

        // GET: General/Dashboard
        // List the user current month and some other status
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
                var user = await client.User.Current();
                ViewBag.AvatarUrl = user.AvatarUrl;
                ViewBag.UserName = user.Name;

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

        // GET: General/UserRankViewer
        // List the user in rank order
        public async Task<ActionResult> UserRankViewer()
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
                var user = await client.User.Current();
                ViewBag.AvatarUrl = user.AvatarUrl;
                ViewBag.UserName = user.Name;

                //CamperBot_FCC_Status_Viewer.Models = namespace
                //UserRankViewerModel = class name
                //DatabaseConnection() = method in that class
                List<Models.rank_list> camperRankList = CamperBot_FCC_Status_Viewer.Models.UserRankViewerModel.GenerateRankList();
                return View(camperRankList);
            }
            catch (AuthorizationException)
            {
                // Either the accessToken is null or it's invalid. This redirects
                // to the GitHub OAuth login page. That page will redirect back to the
                // Authorize action.
                return Redirect(GetOauthLoginUrl());
            }
        }

        // GET: General/AllUserDetailsDatabase
        // Fetch all data from user table and generate a list
        public async Task<ActionResult> AllUserDetailsDatabase()
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
                var user = await client.User.Current();
                ViewBag.AvatarUrl = user.AvatarUrl;
                ViewBag.UserName = user.Name;

                List<Models.database_list> allUserDetailList = CamperBot_FCC_Status_Viewer.Models.FetchUserDetailsTableModel.FetchUserDetailsTable();
                return View(allUserDetailList);

            }
            catch (AuthorizationException)
            {
                // Either the accessToken is null or it's invalid. This redirects
                // to the GitHub OAuth login page. That page will redirect back to the
                // Authorize action.
                return Redirect(GetOauthLoginUrl());
            }
        }

        // GET: General/UserProfileViewer
        // Fetch the complete data of particular user and generate a list
        public async Task<ActionResult> UserProfileViewer(string userId)
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
                var user = await client.User.Current();
                ViewBag.AvatarUrl = user.AvatarUrl;
                ViewBag.UserName = user.Name;

                List<Models.user_profile_details> UserProfileDetails = CamperBot_FCC_Status_Viewer.Models.UserProfileModel.FetchUserProfileDetails(userId);
                return View(UserProfileDetails);
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
               new OauthTokenRequest(clientId, clientSecret, code)
               {
                   RedirectUri = new Uri("http://localhost:50568/general/authorize")
               });

                Session["OAuthToken"] = token.AccessToken;
            }
            return RedirectToAction("dashboard");
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
        public ActionResult Logout()
        {

            //For Clear Particul Value
            //Session.Abandon();
            //Session.Contents.Remove("OAuthToken");
            //Session.Contents.Remove("CSRF:State");

            //Session.Remove("OAuthToken");
            //Session.Remove("CSRF:State");
            //Session.Abandon();
            //Response.Redirect("Default.aspx");
            //HttpContext.Session.Abandon();
            //HttpContext.Request.Cookies.Clear();
            //Response.Cookies.Clear();
            //Session.Contents.CookieMode.
            //Response.Cookies["OAuthToken"].Expires = DateTime.Now.AddDays(-1);
            //HttpCookie currentUserCookie = HttpContext.Request.Cookies["OAuthToken"];
            //HttpContext.Response.Cookies.Remove("OAuthToken");
            //currentUserCookie.Expires = DateTime.Now.AddDays(-10);
            //currentUserCookie.Value = null;
            //HttpContext.Response.SetCookie(currentUserCookie);
            if (Request.Cookies["UserSettings"] != null)
            {
                HttpCookie myCookie = new HttpCookie("UserSettings");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            return RedirectToAction("dashboard");
            //return Redirect(Authorize());
            //Session.Remove("OAuthToken");
            //Session.Remove("OAuthToken");
            //Session.Clear();
            //Session.Contents.RemoveAll();
            //Session.Abandon(); // it will clear the session at the end of request

        }
    }
}