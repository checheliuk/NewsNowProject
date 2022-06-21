namespace NewsNowProject.Domain.Model
{
    public class EnumModel
    {
        public enum SourceData
        {
            Advertising = 1,
            Vezha = 2,
            VinnitsaInfo = 3,
            V0432 = 4,
            MyVin = 5,
            Vn20minut = 6,
            VlasnoInfo = 7,
            VnDepo = 8,
            IVinInfo = 9,
            VinGovUa = 10,
            VinnitsaOk = 11,
            MistoVnUa = 12,
        }

        public enum ArticleStatus
        {
            Active = 1,
            Deactivate = 2,
            Parsered = 3
        }

        public enum JobsType
        {
            GetArticleDetails = 1,
            FaceBookPosting = 2,
            InstagramPosting = 3,
        }

        public enum LimitType
        {
            InstagramLimit = 1,
            Proxy = 2,
            UpdateNews = 3
        }
    }
}
