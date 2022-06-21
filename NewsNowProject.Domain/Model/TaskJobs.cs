using System;
using System.ComponentModel.DataAnnotations;
using static NewsNowProject.Domain.Model.EnumModel;

namespace NewsNowProject.Domain.Model
{
    public class TaskJobs
    {
        [Key]
        public int TaskJobsID { get; set; }
        public JobsType Type { get; set; }
        public int ArticleID { get; set; }
        public DateTime Date { get; set; }
        public Article Article { get; set; }
    }
}
