using NewsNowProject.Domain.Model;
using System;

namespace NewsNowProject.Domain.ViewModel
{
    public class ArticleModel 
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public EnumModel.SourceData Source { get; set; }
        public EnumModel.ArticleStatus Status { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string Photo { get; set; }
        public string Video { get; set; }
    }
}
