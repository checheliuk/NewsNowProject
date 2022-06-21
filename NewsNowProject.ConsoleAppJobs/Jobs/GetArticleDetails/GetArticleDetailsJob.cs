using NewsNowProject.Domain.Data;
using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;
using NewsNowProject.Parser.Helper;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using static NewsNowProject.Domain.Model.EnumModel;

namespace NewsNowProject.ConsoleAppJobs.Jobs.GetArticleDetails
{
    public class GetArticleDetailsJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Limit proxy = null;
            ParserWorker<List<Article>> parser = null;
            var sourceParser = HelperParser.GetSourceParser();
            foreach (var article in TaskJobsData.GetListTaskJobsByType(JobsType.GetArticleDetails))
            {
                var parserSource = sourceParser.FirstOrDefault(x => x.Setting.Source == article.Article.Source);
                if (parserSource != null)
                {
                    string image = GetArticleDetailsSettings.Image;
                    string description = GetArticleDetailsSettings.Description;

                    try
                    {
                        var useProxy = bool.Parse(ConfigurationManager.AppSettings["UseProxy"]);
                        if (useProxy)
                            proxy = LimitData.GetActualLimitForProxy();

                        var source = parserSource.Parser;
                        source.parserSettings.Url = article.Article.Url;

                        parser = proxy != null ? new ParserWorker<List<Article>>(source, proxy.Value)
                                               : new ParserWorker<List<Article>>(source);

                        ArticleData.UpdateDetailsForArticle(article.ArticleID, parser.GetSourcePage().GetAwaiter().GetResult());

                        if (ArticleData.CheckForDubbing(article.Article))
                            TaskJobsData.AddTaskJobsForPosting(article.ArticleID, new List<JobsType>() { JobsType.FaceBookPosting, JobsType.InstagramPosting });

                        TaskJobsData.DeleteTaskJobs(article.TaskJobsID);

                        Console.WriteLine("End add details: {0} {1}", article.ArticleID, DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "An error occurred while sending the request." && proxy != null)
                            LimitData.Disable(EnumModel.LimitType.Proxy);

                        ErrorLogData.SaveErrorException(new ErrorLog()
                        {
                            Message = "GetArticle:" + parser.Settings.Source.ToString() + " " + ex.Message,
                            StackTrace = ex.StackTrace,
                            Time = DateTime.Now
                        });
                    }
                }
            }
        }
    }
}
