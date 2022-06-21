using Quartz;
using Quartz.Impl;

namespace NewsNowProject.ConsoleAppJobs.Jobs.FaceBookPosting
{
    public class FacebookPostingScheduler
    {
        public static void Start()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            var job = JobBuilder.Create<FacebookPostingJob>().Build();
            var jobTrigger = TriggerBuilder.Create().WithIdentity("FacebookPosting", "Posting").StartNow()
                                                        .WithSimpleSchedule(x => x.WithIntervalInMinutes(3).RepeatForever())
                                                        .Build();

            scheduler.ScheduleJob(job, jobTrigger);
        }
    }
}
