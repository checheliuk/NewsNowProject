using NewsNowProject.Domain.Model;

namespace NewsNowProject.Domain.Data
{
    public static class ErrorLogData
    {
        public static void SaveErrorException(ErrorLog data)
        {
            using (var db = new ApplicationDbContext())
            {
                db.ErrorLogs.Add(data);
                db.SaveChanges();
            }
        }
    }
}
