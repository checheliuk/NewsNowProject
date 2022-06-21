using NewsNowProject.Domain.Data;
using NewsNowProject.Domain.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Quartz;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static NewsNowProject.Domain.Model.EnumModel;

namespace NewsNowProject.ConsoleAppJobs.Jobs.IntagramPosting
{
    public class InstagramPostingJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var limit = LimitData.GetLimitByType(LimitType.InstagramLimit);
            if (limit.Time.AddHours(1) < DateTime.Now)
            {
                var posts = TaskJobsData.GetListTaskJobsByType(JobsType.InstagramPosting);
                if (posts.Any())
                {
                    ChromeOptions options = new ChromeOptions();
                    options.EnableMobileEmulation("iPhone 6");
                    options.AddArguments("user-data-dir=C:\\Users\\Administrator\\AppData\\Local\\Google\\Chrome\\User Data");
                    IWebDriver driver = new ChromeDriver(InstagramPostingSettings.PathToChromeDriver, options);
                    Console.WriteLine("Instagram posting start");
                    try
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                        driver.Url = InstagramPostingSettings.Url;
                        //driver.FindElement(By.Name("username")).SendKeys(InstagramPostingSettings.Login);
                        //driver.FindElement(By.Name("password")).SendKeys(InstagramPostingSettings.Password);
                        //driver.FindElements(By.CssSelector("._qv64e._gexxb._4tgw8._njrw0")).Last().Click();

                        var article = posts.Last();
                        driver.FindElement(By.CssSelector("._k0d2z._ttgfw._mdf8w")).Click();
                        var path = ImagePosting.GetPath(article.Article.Title, article.Article.Date);
                        Thread.Sleep(500);
                        SendKeys.SendWait(path);
                        SendKeys.SendWait(@"{Enter}");
                        driver.FindElement(By.CssSelector("._9glb8")).Click();
                        driver.FindElement(By.CssSelector("._9r8i9")).Click();
                        driver.FindElement(By.CssSelector("._jtnds._o716c")).SendKeys(InstagramPostingSettings.Location);
                        driver.FindElements(By.CssSelector("._e0gd2")).First().Click(); // Set location
                        driver.FindElement(By.CssSelector("._qlp0q")).SendKeys(InstagramPostingSettings.Message + article.Article.ArticleID); // Message
                        driver.FindElement(By.CssSelector("._9glb8")).Click(); // Share
                        Console.WriteLine("Instagram posting article: {0} {1}", article.ArticleID, DateTime.Now);
                        ImagePosting.DeleteImage(path);
                        TaskJobsData.DeleteListTaskJobs(posts.Select(x => x.ArticleID).ToList(), JobsType.InstagramPosting);
                        LimitData.UpdateLimit(new Limit() { Type = LimitType.InstagramLimit, Time = DateTime.Now, Status = true, Value = string.Empty });
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
                    finally
                    {
                        driver.Close();
                    }
                }
            }
        }
    }
}
