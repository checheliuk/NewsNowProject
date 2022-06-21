using NewsNowProject.Domain.Data;
using NewsNowProject.Domain.Model;
using NewsNowProject.Domain.ViewModel;
using NewsNowProject.ProxyListApi.Core;
using Quartz;
using System;
using System.Configuration;

namespace NewsNowProject.ConsoleAppJobs.Jobs.ProxyList
{
    public class ProxyListJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                if (bool.Parse(ConfigurationManager.AppSettings["UseProxy"]))
                {
                    var proxy = new ProxyListWorkers();
                    LimitData.UpdateLimit(new Limit()
                    {
                         Type = EnumModel.LimitType.Proxy,
                         Status = true,
                         Value = GetUrl(proxy.GetResponse())
                    });
                }
            }
            catch (Exception ex)
            {
                ErrorLogData.SaveErrorException(new ErrorLog()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Time = DateTime.Now
                });
            }
        }

        public string GetUrl(ProxyListViewModel model)
        {
            if (model.protocol != "http")
                return null;

            return model.protocol + "://" + model.ip + ":" + model.port;
        }
    }
}
