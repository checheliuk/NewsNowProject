using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;

namespace NewsNowProject.Parser.Source.VlasnoInfo
{
    public class VlasnoInfoSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "http://vlasno.info";
        public string Url { get; set; } = "http://vlasno.info";
        public EnumModel.SourceData Source { get; set; } = EnumModel.SourceData.VlasnoInfo;
        public int IntervalInMinutes { get; set; } = 5;
        public string Title { get; set; } = "Події у Вінниці, Вінницької області та всієї України | Новини Вінниці | Новости Винницы | ВЛАСНО.info";
        public string Icon { get; set; } = "vlasnoinfo";
        public string Name { get; set; } = "ВЛАСНО.info";
    }
}
