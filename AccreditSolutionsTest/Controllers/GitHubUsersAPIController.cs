using AccreditSolutionsTest.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AccreditSolutionsTest.Controllers
{
    public class GitHubUsersAPIController : Controller
    {
        // GET: GetHubUsersAPI
        [HttpGet]
        public ActionResult GetUserDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetUserDetails(string username)
        {
            try
            {
                var userDetailsModel = GitHubUsersAPICalls.GetGitHubUserDetails(username);
                if(userDetailsModel.public_repos > 0)
                {
                    var userPublicRepos = GitHubUsersAPICalls.GetGitHubUserPublicReposDetails(userDetailsModel.repos_url).OrderByDescending(r => r.stargazers_count).Take(5).ToList();
                    userDetailsModel.userReposList = userPublicRepos;
                    return Json(new { userDetailsModel = userDetailsModel, IsSuccess = true });
                }
                    return Json(new { userDetailsModel = userDetailsModel, IsSuccess = true });
            }
            catch (WebException ex)
            {
                var statusCode = ((HttpWebResponse)ex.Response).StatusCode.ToString();
                var errorMessage = "";
                switch (statusCode)
                {
                    case "NotFound":
                        errorMessage = "User Not Found";
                        break;
                    default:
                    errorMessage = "You may try again later";
                        break;
                }
                return Json(new { IsSuccess = false, message = errorMessage });
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, message = "An Error has occurred" });
            }
        }
    }
}