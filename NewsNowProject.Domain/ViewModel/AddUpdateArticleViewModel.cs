using NewsNowProject.Domain.Model;
using System.Collections.Generic;

namespace NewsNowProject.Domain.ViewModel
{
    public class AddUpdateArticleViewModel
    {
        public List<Article> AddArticleList { get; set; }
        public List<Article> UpdateArticleList { get; set; }
    }
}
