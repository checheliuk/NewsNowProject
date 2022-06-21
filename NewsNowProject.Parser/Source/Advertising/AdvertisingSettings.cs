using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;

namespace NewsNowProject.Parser.Source.Advertising
{
    public class AdvertisingSettings : IParserSettings
    {
        public string BaseUrl { get; set; }
        public string Url { get; set; }
        public EnumModel.SourceData Source { get; set; } = EnumModel.SourceData.Advertising;
        public int IntervalInMinutes { get; set; } = 1;
        public string Title { get; set; } = "Реклама";
        public string Icon { get; set; } = "advertising";
        public string Name { get; set; } 
    }
}
