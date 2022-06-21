using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;

namespace NewsNowProject.Parser.Source.IVinInfo
{
    public class IVinInfoSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "http://i-vin.info/";
        public string Url { get; set; } = "http://i-vin.info/";
        public EnumModel.SourceData Source { get; set; } = EnumModel.SourceData.IVinInfo;
        public int IntervalInMinutes { get; set; } = 5;
        public string Title { get; set; } = "Найсвіжіші новини Вінниці, області та всієї України | iVin новини";
        public string Icon { get; set; } = "ivininfo";
        public string Name { get; set; } = "iVin Новини";
    }
}
