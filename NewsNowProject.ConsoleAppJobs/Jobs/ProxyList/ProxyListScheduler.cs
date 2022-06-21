using Quartz;
using Quartz.Impl;

namespace NewsNowProject.ConsoleAppJobs.Jobs.ProxyList
{
    public class ProxyListScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<ProxyListJob>().Build();
            ITrigger jobTrigger = TriggerBuilder.Create().WithIdentity("ProxyListApi", "ProxyList").StartNow()
                                                        .WithSimpleSchedule(x => x.WithIntervalInMinutes(30).RepeatForever())
                                                        .Build();

            scheduler.ScheduleJob(job, jobTrigger);
        }
    }
}
