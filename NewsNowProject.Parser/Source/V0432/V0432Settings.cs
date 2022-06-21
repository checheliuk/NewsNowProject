using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;

namespace NewsNowProject.Parser.Source.V0432
{
    public class V0432Settings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://www.0432.ua";
        public string Url { get; set; } = "https://www.0432.ua";
        public EnumModel.SourceData Source { get; set; } = EnumModel.SourceData.V0432;
        public int IntervalInMinutes { get; set; } = 5;
        public string Title { get; set; } = "Сайт Винницы 0432.ua - лента новостей и последние события в городе";
        public string Icon { get; set; } = "v0432";
        public string Name { get; set; } = "0432.ua";
    }
}
