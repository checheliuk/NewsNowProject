using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;


namespace NewsNowProject.Parser.Source.VinnitsaInfo
{
    public class VinnitsaInfoSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "http://www.vinnitsa.info";
        public string Url { get; set; } = "http://www.vinnitsa.info";
        public EnumModel.SourceData Source { get; set; } = EnumModel.SourceData.VinnitsaInfo;
        public int IntervalInMinutes { get; set; } = 5;
        public string Title { get; set; } = "Винница.info | Новости Винницы | Новини Вінниці";
        public string Icon { get; set; } = "vinnitsainfo";
        public string Name { get; set; } = "Винница.info";
    }
}
