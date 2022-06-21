using NewsNowProject.Domain.ConstantsData;
using NewsNowProject.Domain.Model;
using NewsNowProject.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static NewsNowProject.Domain.Model.EnumModel;

namespace NewsNowProject.Domain.Data
{
    public static class ArticleData
    {
        public static AddUpdateArticleViewModel AddArticle(AddArticleViewModel model)
        {
            using (var db = new ApplicationDbContext())
            {
                var list = new List<Article>();
                var listEdit = new List<Article>();

                foreach (var item in model.Articles)
                {
                    var article = db.Articles.FirstOrDefault(x => x.Url == item.Url && x.Source == model.Source);

                    if (article == null)
                    {
                        list.Add(item);
                    }
                    else if (article.Title != item.Title)
                    {
                        article.Title = item.Title;
                        listEdit.Add(article);
                    }   
                }

                var hasValueListEdit = listEdit.Any();

                if (hasValueListEdit)
                {
                    foreach (var updateItem in listEdit)
                    {
                        db.Entry(updateItem).State = EntityState.Modified;
                    }
                }

                var hasValueList = list.Any();

                if (hasValueList)
                {
                    db.Articles.AddRange(list);
                }

                if(hasValueListEdit || hasValueList)
                {
                    db.SaveChanges();
                }

                if (hasValueList)
                {
                    TaskJobsData.AddRangeTaskJobs(list, JobsType.GetArticleDetails);
                }

                return new AddUpdateArticleViewModel() {
                                                           AddArticleList = Enumerable.Reverse(list).ToList(),
                                                           UpdateArticleList = listEdit
                                                       };
            }
        }

        public static ArticleViewModel GetArticleDataModel(ArticleParameterViewModel parameters)
        {
            using (var db = new ApplicationDbContext())
            {
                var articlesData = db.Articles.Where(x => parameters.NextArticleId != null ? x.ArticleID < parameters.NextArticleId : true && x.Status == EnumModel.ArticleStatus.Active);

                if (!parameters.IsAllFiterSelected)
                    articlesData = articlesData.Where(x => x.Source == SourceData.Advertising || parameters.Filter.Contains(x.Source));

                int? nextPage = null;

                if (parameters.NextArticleId == null)
                {
                    var count = articlesData.Count();
                    var countPage =(int)Math.Ceiling((double)count / HomeConstants.PageSize);
            
                    nextPage = countPage - (count % HomeConstants.PageSize == 0 ? 1 : 2);

                    if (nextPage <= 0)
                        nextPage = null;
                }

                var articles = articlesData.OrderByDescending(x => x.Date)
                                           .ThenByDescending(x => x.ArticleID)
                                           .Take(HomeConstants.PageSize)
                                           .Select(x => new ArticleModel() {
                                                 ArticleID = x.ArticleID,
                                                 Date = x.Date,
                                                 Source = x.Source,
                                                 Status = x.Status,
                                                 Title = x.Title,
                                                 Url = x.Url
                                           }).ToList();

                var nextArticleId = articles?.Min(x => x.ArticleID);

                var isAutoLoadEnabled = nextArticleId == null || articles.Count() != HomeConstants.PageSize ? false : true;

                return new ArticleViewModel()
                {
                    Articles = articles,
                    NextArticleId = nextArticleId,
                    IsAutoLoadEnabled = isAutoLoadEnabled,
                    NextPage = nextPage
                };
            }
        }

        public static ArticleViewModel GetArticleDataModelByPage(ArticleParameterViewModel parameters)
        {
            using (var db = new ApplicationDbContext())
            {
                var articlesData = db.Articles.Where(x => x.Status == ArticleStatus.Active);

                if (!parameters.IsAllFiterSelected)
                    articlesData = articlesData.Where(x => x.Source == SourceData.Advertising || parameters.Filter.Contains(x.Source));

                var articles = articlesData.OrderBy(x => x.Date).ThenBy(x => x.ArticleID)
                                           .Skip(((int)parameters.Page - 1) * HomeConstants.PageSize)
                                           .Take(HomeConstants.PageSize)
                                           .OrderByDescending(x => x.Date).ThenByDescending(x => x.ArticleID)
                                           .Select(x => new ArticleModel()
                                            {
                                                ArticleID = x.ArticleID,
                                                Date = x.Date,
                                                Source = x.Source,
                                                Status = x.Status,
                                                Title = x.Title,
                                                Url = x.Url
                                            }).ToList();

                var count = articlesData.Count();
                var countPage = (int)Math.Ceiling((double)count / HomeConstants.PageSize);

                countPage = countPage - (count % HomeConstants.PageSize == 0 ? 1 : 2);
                int? nextPage = parameters.Page - 1;
                int? prevPage = parameters.Page + 1;

                if (nextPage == 0)
                    nextPage = null;

                if (prevPage >= countPage)
                    prevPage = null;

                return new ArticleViewModel()
                {
                    Articles = articles,
                    IsAutoLoadEnabled = false,
                    NextPage = nextPage,
                    PrevPage = prevPage,
                    PrevPageOnIndex = prevPage == null ? true : false
                };
            }
        }

        public static ArticleModel GetArticleById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var article = db.Articles.Find(id);
                if (article == null)
                    return null;

                return new ArticleModel() {
                    ArticleID = article.ArticleID,
                    Date = article.Date,
                    Description = article.Description,
                    Image = article.Image,
                    Photo = article.Photo,
                    Source = article.Source,
                    Status = article.Status,
                    Text = article.Text,
                    Title = article.Title,
                    Url = article.Url,
                    Video = article.Video
                };
            }
        }

        public static List<Article> GetArticlesFromID(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Articles.Where(x => x.ArticleID > id).OrderBy(x => x.ArticleID).ToList();
            }
        }

        public static int? GetLastArticleID()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Articles.OrderByDescending(x=>x.ArticleID).FirstOrDefault()?.ArticleID;
            }
        }

        public static void UpdateDetailsForArticle(int id, Article model)
        {
            using (var db = new ApplicationDbContext())
            {
                var article = db.Articles.FirstOrDefault(x => x.ArticleID == id);
                if (article != null)
                {
                    article.Image = model.Image;
                    article.Description = model.Description;
                    article.Text = model.Text;
                    article.Photo = model.Photo;
                    article.Video = model.Video;
                    article.Status = model.Status;
                    db.Entry(article).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public static bool CheckForDubbing(Article model)
        {
            using (var db = new ApplicationDbContext())
            {
                var date = model.Date.AddDays(-1);
                return db.Articles.Any(x => x.ArticleID != model.ArticleID && date < x.Date && x.Title == model.Title) ? false : true;
            }
        }
    }
}
