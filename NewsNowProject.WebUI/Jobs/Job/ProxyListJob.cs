using NewsNowProject.Domain.Data;
using NewsNowProject.Domain.Model;
using NewsNowProject.ProxyListApi.Core;
using Quartz;
using System;
using System.Web.Configuration;

namespace NewsNowProject.WebUI.Jobs.Job
{
    //public class ProxyListJob : IJob
    //{
    //    public void Execute(IJobExecutionContext context)
    //    {
    //        try
    //        {
    //            if(bool.Parse(WebConfigurationManager.AppSettings["UseProxy"]))
    //            {
    //                var proxy = new ProxyListWorkers();
    //                ProxyData.UpdateProxy(proxy.GetResponse());
    //            }             
    //        }
    //        catch (Exception ex)
    //        {
    //            ErrorLogData.SaveErrorException(new ErrorLog()
    //            {
    //                Message = ex.Message,
    //                StackTrace = ex.StackTrace,
    //                Time = DateTime.Now
    //            });
    //        }
    //    }
    //}
}