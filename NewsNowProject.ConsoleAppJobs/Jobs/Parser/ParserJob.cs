using NewsNowProject.Domain.Data;
using NewsNowProject.Domain.Model;
using NewsNowProject.Domain.ViewModel;
using NewsNowProject.Parser.Core;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace NewsNowProject.ConsoleAppJobs.Jobs.Parser
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
                var useProxy = bool.Parse(ConfigurationManager.AppSettings["UseProxy"]);

                if (useProxy)
                    proxy = LimitData.GetActualLimitForProxy();

                parser = proxy != null ? parser = new ParserWorker<List<Article>>(parserSource, proxy.Value)
                                       : new ParserWorker<List<Article>>(parserSource);

                var source = parser.GetSource().GetAwaiter().GetResult();
               

                if (source != null && source.Element != null &&
                   SourceArticleData.CheckSourceArticle(new SourceArticle()
                    {
                        Data = source.Element.TextContent,
                        Date = DateTime.Now,
                        Source = parser.Settings.Source
                    }))
                {
                    var data = parser.Worker(source.Element);

                    if (data.Any())
                    {
                        var list = ArticleData.AddArticle(new AddArticleViewModel()
                        {
                            Articles = data,
                            Source = parser.Settings.Source
                        });
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
