using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;

namespace NewsNowProject.Parser.Source.Vn20minut
{
    public class Vn20minutSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://vn.20minut.ua";
        public string Url { get; set; } = "https://vn.20minut.ua";
        public EnumModel.SourceData Source { get; set; } = EnumModel.SourceData.Vn20minut;
        public int IntervalInMinutes { get; set; } = 5;
        public string Title { get; set; } = "20 хвилин - Новини Вінниці";
        public string Icon { get; set; } = "vn20minut";
        public string Name { get; set; } = "20 хвилин";
    }
}
