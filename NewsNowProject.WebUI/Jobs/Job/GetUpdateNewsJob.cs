using NewsNowProject.Domain.Data;
using NewsNowProject.Domain.Model;
using NewsNowProject.WebUI.Hubs;
using Quartz;
using System;
using System.Linq;

namespace NewsNowProject.WebUI.Jobs.Job
{
    public class GetUpdateNewsJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            int articleID;

            if(Int32.TryParse(LimitData.GetLimitByType(EnumModel.LimitType.UpdateNews).Value, out articleID))
            {
                var data = ArticleData.GetArticlesFromID(articleID);

                if (data.Any())
                {
                    var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                    hubContext.Clients.All.displayMessage(
                    new
                    {
                        Articles = data,
                        IsUpadateData = true
                    });

                    LimitData.UpdateLimit(new Limit() { Type = EnumModel.LimitType.UpdateNews,
                                                        Status = true,
                                                        Value = data.Last().ArticleID.ToString() });
                }
            }
            else
            {
                LimitData.UpdateLimit(new Limit() { Type = EnumModel.LimitType.UpdateNews,
                                                    Status = true,
                                                    Value = ArticleData.GetLastArticleID().ToString() });
            }
        }
    }
}