using NewsNowProject.Domain.Model;
using System.Collections.Generic;

namespace NewsNowProject.Domain.ViewModel
{
    public class AddArticleViewModel
    {
        public List<Article> Articles { get; set; }
        public EnumModel.SourceData Source { get; set; }
    }
}
