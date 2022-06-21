using NewsNowProject.ConsoleAppJobs.Jobs.FaceBookPosting;
using NewsNowProject.ConsoleAppJobs.Jobs.GetArticleDetails;
using NewsNowProject.ConsoleAppJobs.Jobs.IntagramPosting;
using NewsNowProject.ConsoleAppJobs.Jobs.Parser;
using NewsNowProject.ConsoleAppJobs.Jobs.ProxyList;
using NewsNowProject.Domain.Data;
using NewsNowProject.Domain.Model;
using System;

namespace NewsNowProject.ConsoleAppJobs
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Running();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                var error = new ErrorLog()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    Time = DateTime.Now
                };

                ErrorLogData.SaveErrorException(error);
            }
        }

        static void Running()
        {
            Console.WriteLine("Start working: {0}", DateTime.Now);
            ProxyListScheduler.Start();
            ParserScheduler.Start();
            GetArticleDetailsScheduler.Start();
            FacebookPostingScheduler.Start();
            InstagramPostingScheduler.Start();                    
        }
    }
}
