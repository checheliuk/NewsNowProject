using NewsNowProject.Domain.Data;
using NewsNowProject.Domain.Model;
using NewsNowProject.Domain.ViewModel;
using NewsNowProject.Parser.Core;
using NewsNowProject.WebUI.Hubs;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;

namespace NewsNowProject.WebUI.Jobs.Job
{
    public class ParserJob : IJob
    {
        public IParser<List<Article>> parserSource;
        ParserWorker<List<Article>> parser;

        public void Execute(IJobExecutionContext context)
        {
            Limit proxy = null;
            try
            {
                var parserSource = (IParser<List<Article>>)context.MergedJobDataMap["Parser"];
                var useProxy = bool.Parse(WebConfigurationManager.AppSettings["UseProxy"]);

                if (useProxy)
                {

                    proxy = LimitData.GetActualLimitForProxy();
                    if (proxy != null)
                        parser = new ParserWorker<List<Article>>(parserSource, proxy.Value);
                }

                if(!useProxy || proxy == null)
                    parser = new ParserWorker<List<Article>>(parserSource);

                var sourse = parser.GetSource().GetAwaiter().GetResult();

                if (SourceArticleData.CheckSourceArticle(new SourceArticle() {  Data = sourse.Element.TextContent,
                                                                                Date = DateTime.Now,
                                                                                Source = parser.Settings.Source }))
                {
                    var data = parser.Worker(sourse.Element);

                    if (data.Any())
                    {
                        var list = ArticleData.AddArticle(new AddArticleViewModel()
                        {
                            Articles = data,
                            Source = parser.Settings.Source
                        });

                        if (list.AddArticleList.Any() || list.UpdateArticleList.Any())
                        {
                            var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                            hubContext.Clients.All.displayMessage(
                            new
                            {
                                Articles = list.AddArticleList,
                                UpdateArticles = list.UpdateArticleList,
                                IsUpadateData = true
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "An error occurred while sending the request." && proxy != null)
                    LimitData.Disable(EnumModel.LimitType.Proxy);

                ErrorLogData.SaveErrorException(new ErrorLog()
                {
                    Message = "Source:" + parser.Settings.Source.ToString() + " " + ex.Message,
                    StackTrace = ex.StackTrace,
                    Time = DateTime.Now
                });
            }
        }
    }
}