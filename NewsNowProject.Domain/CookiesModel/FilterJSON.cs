using static NewsNowProject.Domain.Model.EnumModel;

namespace NewsNowProject.Domain.CookiesModel
{
    public class FilterJSON
    {
        public SourceData[] filter { get; set; }
        public bool welcome { get; set; }
    }
}
