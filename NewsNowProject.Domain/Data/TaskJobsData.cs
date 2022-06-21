using NewsNowProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static NewsNowProject.Domain.Model.EnumModel;

namespace NewsNowProject.Domain.Data
{
    public static class TaskJobsData
    {
        public static void AddTaskJobs(TaskJobs model)
        {
            using (var db = new ApplicationDbContext())
            {
                db.TaskJobs.Add(model);
                db.SaveChanges();
            }
        }

        public static void AddTaskJobsForPosting(int ArticleID, List<JobsType> list)
        {
            using (var db = new ApplicationDbContext())
            {
                var jobs = new List<TaskJobs>();
                foreach (var item in list)
                {
                    jobs.Add(new TaskJobs()
                    {
                        ArticleID = ArticleID,
                        Date = DateTime.Now,
                        Type = item
                    });
                }

                db.TaskJobs.AddRange(jobs);
                db.SaveChanges();
            }
        }


        public static void AddRangeTaskJobs(List<Article> list, JobsType type)
        {
            using (var db = new ApplicationDbContext())
            {
                var models = list.Select(x => new TaskJobs() {
                                             ArticleID = x.ArticleID,
                                             Date = DateTime.Now,
                                             Type = type
                                        }).ToList();

                db.TaskJobs.AddRange(models);
                db.SaveChanges();
            }
        }

        public static void DeleteTaskJobs(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var article = db.TaskJobs.FirstOrDefault(x => x.TaskJobsID == id);
                if (article != null)
                {
                    db.Entry(article).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteListTaskJobs(List<int> list, JobsType type)
        {
            using (var db = new ApplicationDbContext())
            {
                var taskJobs = db.TaskJobs.Where(x => x.Type == type && list.Contains(x.ArticleID));
                db.TaskJobs.RemoveRange(taskJobs);
                db.SaveChanges();               
            }
        }


        public static List<TaskJobs> GetListTaskJobsByType(JobsType type)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.TaskJobs.Include(x => x.Article).Where(x => x.Type == type).ToList();
            }
        }
    }
}
