using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;

namespace NewsNowProject.Parser.Source.MistoVnUa
{
    public class MistoVnUaSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "http://misto.vn.ua";
        public string Url { get; set; } = "http://misto.vn.ua";
        public EnumModel.SourceData Source { get; set; } = EnumModel.SourceData.MistoVnUa;
        public int IntervalInMinutes { get; set; } = 1440;
        public string Title { get; set; } = "Газета 'Місто'";
        public string Icon { get; set; } = "mistovnua";
        public string Name { get; set; } = "Газета 'Місто'";
    }
}
