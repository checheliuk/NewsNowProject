using System;
using System.ComponentModel.DataAnnotations;

namespace NewsNowProject.Domain.Model
{
    public class SourceArticle
    {
        [Key]
        public int SourceArticleID { get; set; }

        public EnumModel.SourceData Source { get; set; }
        public string Data { get; set; }
        public DateTime Date { get; set; }
    }
}
