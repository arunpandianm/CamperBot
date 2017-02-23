using Octokit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace CamperBot_FCC_Status_Viewer.Controllers
{
    public class ActiveUserController : Controller
    {
        const string clientId = "28bfffe90ee14b851674";
        private const string clientSecret = "049d738c88dc3bb0a352eff6663a7c03b44311b0";
        readonly GitHubClient client =
            new GitHubClient(new ProductHeaderValue("Camperbot-FCC-Status-Viewer"));

        // GET: ActiveUser/TodayActiveUser
        public async Task<ActionResult> TodayActiveUser()
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

                List<Models.active_user_list> TodayActiveUserList = CamperBot_FCC_Status_Viewer.Models.ActiveUserModel.GenerateActiveUserList_today();
                return View(TodayActiveUserList);
            }
            catch (AuthorizationException)
            {
                // Either the accessToken is null or it's invalid. This redirects
                // to the GitHub OAuth login page. That page will redirect back to the
                // Authorize action.
                return Redirect(GetOauthLoginUrl());
            }
        }



        // GET: ActiveUser/MonthActiveUser
        public async Task<ActionResult> MonthActiveUser()
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

                List<Models.active_user_list1> MonthActiveUserList = CamperBot_FCC_Status_Viewer.Models.ActiveUserModel.GenerateActiveUserList_month();
                return View(MonthActiveUserList);
            }
            catch (AuthorizationException)
            {
                // Either the accessToken is null or it's invalid. This redirects
                // to the GitHub OAuth login page. That page will redirect back to the
                // Authorize action.
                return Redirect(GetOauthLoginUrl());
            }
        }

        // GET: ActiveUser/UserProfileViewer
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
    }
}