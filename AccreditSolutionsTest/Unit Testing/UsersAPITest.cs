using AccreditSolutionsTest.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccreditSolutionsTest.Unit_Testing
{
    public class UsersAPITest
    {
        public UsersAPITest()
        {
            var userDetailsModel = GitHubUsersAPICalls.GetGitHubUserDetails("ghassar");
            if (userDetailsModel.public_repos > 0)
            {
                var userPublicRepos = GitHubUsersAPICalls.GetGitHubUserPublicReposDetails(userDetailsModel.repos_url).OrderByDescending(r => r.stargazers_count).Take(5).ToList();
                userDetailsModel.userReposList = userPublicRepos;
            }
            Console.WriteLine(userDetailsModel);
        }
    }
}