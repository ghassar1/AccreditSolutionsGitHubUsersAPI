using AccreditSolutionsTest.Models.GitHubUsers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AccreditSolutionsTest.HelperClasses
{
    public class GitHubUsersAPICalls
    {
        public static UserRoot GetGitHubUserDetails(string userName)
        {
                using (var wb = new WebClient())
                {
                    wb.Headers.Add("user-agent", "Test FireFox");
                    var response = wb.DownloadString("https://api.github.com/users/" + userName);
                    return JsonConvert.DeserializeObject<UserRoot>(response);
                }
        }
        public static List<UserReposRoot> GetGitHubUserPublicReposDetails(string url)
        {
            using (var wb = new WebClient())
            {
                wb.Headers.Add("user-agent", "Test FireFox");
                var response = wb.DownloadString(url);
                return JsonConvert.DeserializeObject<List<UserReposRoot>>(response);
            }
        }
    }
}