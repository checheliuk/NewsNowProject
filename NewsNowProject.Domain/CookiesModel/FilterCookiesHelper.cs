using System;
using System.Web;
using System.Web.Script.Serialization;

namespace NewsNowProject.Domain.CookiesModel
{
    public class FilterCookiesHelper
    {
        public FilterCookiesModel Get(string cookieName)
        {
            var settings = new FilterCookiesModel();
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value) && !cookie.Value.Equals("none"))
            {
                string cookieString = HttpUtility.UrlDecode(cookie.Value);
                var jsHelper = new JavaScriptSerializer();
                var obj = jsHelper.Deserialize<FilterJSON>(cookieString);
                if (obj != null)
                {
                    settings.FilterParametr = obj.filter;
                    settings.ShowWelcomeMessage = obj.welcome;
                }
            }

            return settings;
        }

        public HttpCookie CreateCookies(string cookieName)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];

            if (cookie == null)
            {
                var settings = new FilterCookiesModel();
                var jsonCookies = new FilterJSON()
                {
                    filter = settings.FilterParametr,
                    welcome = settings.ShowWelcomeMessage
                };
                var jsHelper = new JavaScriptSerializer();
                cookie = new HttpCookie(cookieName);
                cookie.Value = jsHelper.Serialize(jsonCookies);
                cookie.Expires = DateTime.Now.AddYears(1);
                cookie.Path = "/";
            }
            return cookie;
        }

        public FilterCookiesViewModel CreateAndGetCookies(string cookieName)
        {
            var data = new FilterCookiesViewModel();          
            var cookie = HttpContext.Current.Request.Cookies[cookieName];

            if (cookie == null)
            {
                var settings = new FilterCookiesModel();
                var jsonCookies = new FilterJSON()
                {
                    filter = settings.FilterParametr,
                    welcome = settings.ShowWelcomeMessage
                };

                var jsHelper = new JavaScriptSerializer();

                cookie = new HttpCookie(cookieName);
                cookie.Value = jsHelper.Serialize(jsonCookies);
                cookie.Expires = DateTime.Now.AddYears(1);
                cookie.Path = "/";
                data.Model = settings;
                
            } else {
                string cookieString = HttpUtility.UrlDecode(cookie.Value);
                var jsHelper = new JavaScriptSerializer();
                var obj = jsHelper.Deserialize<FilterJSON>(cookieString);

                if (obj != null)
                {
                    data.Model = new FilterCookiesModel()
                    {
                        FilterParametr = obj.filter,
                        ShowWelcomeMessage = obj.welcome
                    };
                }
            }

            data.Cookie = cookie;

            return data;
        }
    }
}
