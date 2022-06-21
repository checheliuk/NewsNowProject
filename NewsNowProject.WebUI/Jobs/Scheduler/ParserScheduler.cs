using NewsNowProject.Parser.Helper;
using NewsNowProject.WebUI.Jobs.Job;
using Quartz;
using Quartz.Impl;

namespace NewsNowProject.WebUI.Jobs.Scheduler
{
    public class ParserScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            foreach (var item in HelperParser.GetSourceParser())
            {
                var job = JobBuilder.Create<ParserJob>().Build();
                job.JobDataMap.Put("Parser", item.Parser);

                var trigger = TriggerBuilder.Create().WithIdentity(item.Setting.Source.ToString(), "Parser").StartNow()
                                                     .WithSimpleSchedule(x => x.WithIntervalInMinutes(item.Setting.IntervalInMinutes).RepeatForever())
                                                     .Build();
                scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}