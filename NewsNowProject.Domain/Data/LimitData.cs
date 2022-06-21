using NewsNowProject.Domain.Model;
using System;
using System.Data.Entity;
using System.Linq;
using static NewsNowProject.Domain.Model.EnumModel;

namespace NewsNowProject.Domain.Data
{
    public static class LimitData
    {
        public static void UpdateLimit(Limit model)
        {
            using (var db = new ApplicationDbContext())
            {
                var item = db.Limits.FirstOrDefault(x => x.Type == model.Type);
             
                if (item == null)
                {
                    item = new Limit
                    {
                        Time = DateTime.Now,
                        Status = model.Status,
                        Value = model.Value,
                        Type =  model.Type         
                    };

                    db.Limits.Add(item);
                }
                else
                {
                    item.Time = DateTime.Now;
                    item.Status = model.Status;
                    item.Value = model.Value;

                    db.Entry(item).State = EntityState.Modified;
                }

                db.SaveChanges();
            }
        }

        public static Limit GetLimitByType(LimitType type)
        {
            using (var db = new ApplicationDbContext())
            {
                var limit = db.Limits.FirstOrDefault(x => x.Type == type);
                if (limit == null)
                {
                    limit = new Limit() { Type = type, Time = DateTime.Now, Status = true, Value = String.Empty };
                    UpdateLimit(limit);
                }

                return limit;
            }
        }

        public static Limit GetActualLimitForProxy()
        {
            using (var db = new ApplicationDbContext())
            {
                var limit = db.Limits.FirstOrDefault(x => x.Type == LimitType.Proxy && x.Status);

                if(limit != null && limit.Time.AddMinutes(30) > DateTime.Now)
                    return limit;
            }

            return null;
        }

        public static void Disable(LimitType type)
        {
            using (var db = new ApplicationDbContext())
            {
                var item = db.Limits.FirstOrDefault(x => x.Type == type);
                if (item != null)
                {
                    item.Status = false;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
}
