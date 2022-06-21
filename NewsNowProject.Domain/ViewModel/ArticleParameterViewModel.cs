using static NewsNowProject.Domain.Model.EnumModel;

namespace NewsNowProject.Domain.ViewModel
{
    public class ArticleParameterViewModel
    {
        public SourceData[] Filter { get; set; }
        public bool IsAllFiterSelected { get; set; }
        public int? NextArticleId { get; set; }
        public int? Page { get; set; }
    }
}
