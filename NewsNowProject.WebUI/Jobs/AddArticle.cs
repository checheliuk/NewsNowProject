using NewsNowProject.Domain.Data;
using NewsNowProject.Domain.Model;
using NewsNowProject.Domain.ViewModel;
using NewsNowProject.Parser.Core;
using NewsNowProject.Parser.Source.MyVin;
using NewsNowProject.Parser.Source.V0432;
using NewsNowProject.Parser.Source.Vezha;
using NewsNowProject.Parser.Source.VinnitsaInfo;
using NewsNowProject.WebUI.Hubs;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsNowProject.WebUI.Jobs
{
    public class AddArticle : IJob
    {
        ParserWorker<List<Article>> parser;

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                parser = new ParserWorker<List<Article>>(new MyVinParser());

                var sourse = parser.GetSource().GetAwaiter().GetResult();

                if (SourceArticleData.CheckSourceArticle(new SourceArticle() {  Data = sourse.Element.TextContent,
                                                                                Date = DateTime.Now,
                                                                                Source = parser.Settings.Source, }))
                {
                    var data = parser.Worker(sourse.Element);

                    if (data.Any())
                    {
                        var addlist = ArticleData.AddArticle(new AddArticleViewModel() { Articles = data,
                                                                                         Source = parser.Settings.Source });
                        if(addlist.Any())
                            SendMessage(addlist);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SendMessage(List<Article> data)
        {
            // Получаем контекст хаба
            var context =
                Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            // отправляем сообщение
            context.Clients.All.displayMessage(new { Articles = data });
        }
    }
}