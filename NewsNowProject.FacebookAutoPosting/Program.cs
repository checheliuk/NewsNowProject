using NewsNowProject.Domain.Data;
using NewsNowProject.FacebookAutoPosting.Core;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace NewsNowProject.FacebookAutoPosting
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    RunningJobs();
                }
                catch (SchedulerException se)
                {
                    Console.WriteLine(se);
                }
            }
        }

        static void RunningJobs()
        {
            while (true)
            {
                var scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();
                var job = JobBuilder.Create<FacebookPostingJob>()
                                    .WithIdentity("JobFacebookPosting", "FacebookPosting")
                                    .Build();

                var trigger = TriggerBuilder.Create()
                                            .WithIdentity("TriggerFacebookPosting", "FacebookPosting")
                                            .StartNow()
                                            .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
                                            .Build();

                scheduler.ScheduleJob(job, trigger);
            }
        }
    }

    public class FacebookPostingJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);

            foreach (var article in ArticleData.GetArticleForPostring())
            {
                var postOnWallTask = facebookService.PostOnWallAsync(FacebookSettings.AccessToken, FacebookSettings.ID, "newsnow.vn.ua/home/article/"+ article.ArticleID);
                Task.WaitAll(postOnWallTask);
                Console.WriteLine("Posting article: {0} {1}", article.ArticleID, DateTime.Now);
            }
        }
    }

}
