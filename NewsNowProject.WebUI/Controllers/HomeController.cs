using System.Web.Mvc;
using NewsNowProject.WebUI.Filters;
using NewsNowProject.Domain.Data;
using NewsNowProject.Domain.ViewModel;
using NewsNowProject.Parser.Helper;
using NewsNowProject.Domain.CookiesModel;
using NewsNowProject.Domain.ConstantsData;

namespace NewsNowProject.WebUI.Controllers
{
    public class HomeController : Controller
    {
        [GetException]
        public ActionResult Index(int? id)
        {
            var cookiesData = new FilterCookiesHelper().CreateAndGetCookies(HomeConstants.FilterCookies);
            Response.Cookies.Add(cookiesData.Cookie);
            var source = HelperParser.GetSourceContent();

            return View(new IndexHomeContentViewModel()
            {
                Advertising = source.Advertising,
                SourceContent = source.SourceContent,
                SourceContentCount = source.SourceContent.Count,
                CookiesName = HomeConstants.FilterCookies,
                FilterCookies = cookiesData.Model,
                ArticleData = ArticleData.GetArticleDataModel(new ArticleParameterViewModel() {
                    Filter = cookiesData.Model.FilterParametr,
                    NextArticleId = id,
                    IsAllFiterSelected = cookiesData.Model.FilterParametr.Length == 0 ||
                                             cookiesData.Model.FilterParametr.Length == source.SourceContent.Count
                                             ? true : false
                })
            });
        }

        [GetException]
        public ActionResult Page(int? id)
        {
            var cookiesData = new FilterCookiesHelper().Get(HomeConstants.FilterCookies);
            var source = HelperParser.GetSourceContent();

            return View("Index", new IndexHomeContentViewModel()
            {
                Advertising = source.Advertising,
                SourceContent = source.SourceContent,
                SourceContentCount = source.SourceContent.Count,
                CookiesName = HomeConstants.FilterCookies,
                FilterCookies = cookiesData,
                ArticleData = ArticleData.GetArticleDataModelByPage(new ArticleParameterViewModel()
                {
                    Page = id,
                    Filter = cookiesData.FilterParametr,
                    IsAllFiterSelected = cookiesData.FilterParametr.Length == 0 ||
                                             cookiesData.FilterParametr.Length == source.SourceContent.Count
                                             ? true : false
                })
            });
        }

        [GetException]
        public ActionResult Article(int id)
        {
            var source = HelperParser.GetSourceContent();

            return View(new ArticleHomeContentViewModel() {
                Advertising = source.Advertising,
                SourceContent = source.SourceContent,
                EnableH1 = true,
                Article = ArticleData.GetArticleById(id)
            });
        }

        [PostException]
        [HttpPost]
        public ActionResult get(ArticleParameterViewModel parameters)
        {
            return Json(ArticleData.GetArticleDataModel(parameters));
        }
    }
}