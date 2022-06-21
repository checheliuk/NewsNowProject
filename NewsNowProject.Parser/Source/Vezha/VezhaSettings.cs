using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;

namespace NewsNowProject.Parser.Source.Vezha
{
    public class VezhaSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://vezha.vn.ua";
        public string Url { get; set; } = "https://vezha.vn.ua";
        public EnumModel.SourceData Source { get; set; } = EnumModel.SourceData.Vezha;
        public int IntervalInMinutes { get; set; } = 5;
        public string Title { get; set; } = "VeжА - Новини Вінниці";
        public string Icon { get; set; } = "vezha";
        public string Name { get; set; } = "VeжА";
    }
}
