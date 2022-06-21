using NewsNowProject.Domain.Model;
using NewsNowProject.Domain.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;

namespace NewsNowProject.Domain.Data
{
    public static class ProxyData
    {
        //public static void UpdateProxy(ProxyListViewModel model)
        //{
        //    using (var db = new ApplicationDbContext())
        //    {
        //        var item = db.Proxy.FirstOrDefault();
        //        var url = GetUrl(model);

        //        if (url != null)
        //        {
        //            if (item == null)
        //            {
        //                item = new Proxy
        //                {
        //                    Date = DateTime.Now,
        //                    Url = url,
        //                    IsActive = true
        //                };

        //                db.Proxy.Add(item);
        //            }
        //            else if (item.Url != url)
        //            {
        //                item.Date = DateTime.Now;
        //                item.Url = url;
        //                item.IsActive = true;
        //                db.Entry(item).State = EntityState.Modified;
        //            }

        //            db.SaveChanges();
        //        }
        //    }           
        //}

        //public static Proxy GetActualProxy()
        //{
        //    using (var db = new ApplicationDbContext())
        //    {
        //        return db.Proxy.ToList().Where(x => x.IsActive && x.Date.AddHours(1) > DateTime.Now ).FirstOrDefault();
        //    }
        //}

        //public static string GetUrl(ProxyListViewModel model)
        //{
        //    if (model.protocol != "http")
        //        return null;

        //    return model.protocol + "://" + model.ip + ":" + model.port;
        //}

        //public static void DeactivateProxy(string url)
        //{
        //    using (var db = new ApplicationDbContext())
        //    {
        //        var item = db.Proxy.Where(x => x.Url == url).FirstOrDefault();
        //        if (item != null)
        //        {
        //            item.IsActive = false;
        //            db.Entry(item).State = EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //    }
        //}
    }
}
