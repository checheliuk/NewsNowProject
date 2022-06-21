using NewsNowProject.Domain.Model;
using System.Collections.Generic;

namespace NewsNowProject.Domain.ViewModel
{
    public class ArticleHomeContentViewModel
    {
        public List<SourceModel> SourceContent { get; set; }
        public SourceModel Advertising { get; set; }
        public ArticleModel Article { get; set; }
        public bool EnableH1 { get; set; } = false;
    }
}
