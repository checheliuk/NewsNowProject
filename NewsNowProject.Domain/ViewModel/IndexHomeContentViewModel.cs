using NewsNowProject.Domain.CookiesModel;
using NewsNowProject.Domain.Model;
using System.Collections.Generic;

namespace NewsNowProject.Domain.ViewModel
{
    public class IndexHomeContentViewModel
    {
        public List<SourceModel> SourceContent { get; set; }
        public SourceModel Advertising { get; set; }
        public int SourceContentCount { get; set; }
        public string CookiesName { get; set; }
        public FilterCookiesModel FilterCookies { get; set; }
        public ArticleViewModel ArticleData { get; set; }
    }
}
