using Quartz;
using Quartz.Impl;

namespace NewsNowProject.ConsoleAppJobs.Jobs.IntagramPosting
{
    public class InstagramPostingScheduler
    {
        public static void Start()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            var job = JobBuilder.Create<InstagramPostingJob>().Build();
            var jobTrigger = TriggerBuilder.Create().WithIdentity("InstagramPosting", "Posting").StartNow()
                                                        .WithSimpleSchedule(x => x.WithIntervalInMinutes(30).RepeatForever())
                                                        .Build();

            scheduler.ScheduleJob(job, jobTrigger);
        }
    }
}
