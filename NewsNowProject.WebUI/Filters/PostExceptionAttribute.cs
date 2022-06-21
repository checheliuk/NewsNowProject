using System;
using System.Web.Mvc;
using NewsNowProject.Domain.Data;
using NewsNowProject.Domain.Model;

namespace NewsNowProject.WebUI.Filters
{
    public class PostExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var error = new ErrorLog()
            {
                Message = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                Time = DateTime.Now
            };

            ErrorLogData.SaveErrorException(error);
        }
    }
}