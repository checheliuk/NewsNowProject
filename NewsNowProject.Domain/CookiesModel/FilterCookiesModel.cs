using static NewsNowProject.Domain.Model.EnumModel;

namespace NewsNowProject.Domain.CookiesModel
{
    public class FilterCookiesModel
    {
        public FilterCookiesModel()
        {
            FilterParametr = new SourceData[0];
            ShowWelcomeMessage = true;
        }

        public SourceData[] FilterParametr { get; set; }
        public bool ShowWelcomeMessage { get; set; }
    }
}
