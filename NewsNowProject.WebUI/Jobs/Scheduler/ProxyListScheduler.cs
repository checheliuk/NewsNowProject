using NewsNowProject.WebUI.Jobs.Job;
using Quartz;
using Quartz.Impl;

namespace NewsNowProject.WebUI.Jobs.Scheduler
{
    //public class ProxyListScheduler
    //{
    //    public static void Start()
    //    {
    //        IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
    //        scheduler.Start();

    //        IJobDetail job = JobBuilder.Create<ProxyListJob>().Build();
    //        ITrigger jobTrigger = TriggerBuilder.Create().WithIdentity("ProxyListApi", "ProxyList").StartNow()
    //                                                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(30).RepeatForever())
    //                                                    .Build();

    //        scheduler.ScheduleJob(job, jobTrigger);
    //    }
    //}
}