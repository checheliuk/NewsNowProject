using NewsNowProject.Domain.Model;
using System.Collections.Generic;

namespace NewsNowProject.Domain.ViewModel
{
    public class ArticleViewModel
    {
        public List<ArticleModel> Articles { get; set; }
        public int? NextArticleId { get; set; }
        public bool IsAutoLoadEnabled { get; set; }
        public int? NextPage { get; set; }
        public int? PrevPage { get; set; }
        public int? Page { get; set; }
        public bool PrevPageOnIndex { get; set; }
    }
}
