using Quartz;
using Quartz.Impl;

namespace NewsNowProject.ConsoleAppJobs.Jobs.GetArticleDetails
{
    public class GetArticleDetailsScheduler
    {
        public static void Start()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            var job = JobBuilder.Create<GetArticleDetailsJob>().Build();
            var jobTrigger = TriggerBuilder.Create().WithIdentity("GetArticleDetailsScheduler", "GetArticleDetails").StartNow()
                                                        .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
                                                        .Build();

            scheduler.ScheduleJob(job, jobTrigger);
        }
    }
}
