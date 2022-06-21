using NewsNowProject.Domain.Data;
using Quartz;
using System;
using System.Threading.Tasks;
using static NewsNowProject.Domain.Model.EnumModel;

namespace NewsNowProject.ConsoleAppJobs.Jobs.FaceBookPosting
{
    public class FacebookPostingJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);
            foreach (var article in TaskJobsData.GetListTaskJobsByType(JobsType.FaceBookPosting))
            {
                var postOnWallTask = facebookService.PostOnWallAsync(FacebookSettings.AccessToken, FacebookSettings.ID, FacebookSettings.Site + article.ArticleID);
                Task.WaitAll(postOnWallTask);
                Console.WriteLine("Facebook posting article: {0} {1}", article.ArticleID, DateTime.Now);
                TaskJobsData.DeleteTaskJobs(article.TaskJobsID);
            }
        }
    }
}
