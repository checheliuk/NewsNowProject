using NewsNowProject.WebUI.Jobs.Job;
using Quartz;
using Quartz.Impl;

namespace NewsNowProject.WebUI.Jobs.Scheduler
{
    public class GetUpdateNewsScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<GetUpdateNewsJob>().Build();
            ITrigger jobTrigger = TriggerBuilder.Create().WithIdentity("Update", "GetUpdateNews").StartNow()
                                                         .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
                                                         .Build();

            scheduler.ScheduleJob(job, jobTrigger);
        }
    }
}