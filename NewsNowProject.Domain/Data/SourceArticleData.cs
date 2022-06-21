using NewsNowProject.Domain.Model;
using System.Data.Entity;
using System.Linq;

namespace NewsNowProject.Domain.Data
{
    public static class SourceArticleData
    {
        public static bool CheckSourceArticle(SourceArticle source)
        {
            using (var db = new ApplicationDbContext())
            {
                var model = db.SourceArticles
                              .FirstOrDefault(x => x.Source == source.Source);

                if (model == null)
                {
                    db.SourceArticles.Add(source);
                    db.SaveChanges();
                    return true;
                }
                else if (model.Data != source.Data)
                {
                    model.Data = source.Data;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
        }
    }
}
