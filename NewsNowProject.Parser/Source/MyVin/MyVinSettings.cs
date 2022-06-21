using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;

namespace NewsNowProject.Parser.Source.MyVin
{
    public class MyVinSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "http://www.myvin.com.ua";
        public string Url { get; set; } = "http://www.myvin.com.ua";
        public EnumModel.SourceData Source { get; set; } = EnumModel.SourceData.MyVin;
        public int IntervalInMinutes { get; set; } = 5;
        public string Title { get; set; } = "Моя Вінниця - новини Вінниці";
        public string Icon { get; set; } = "myvin";
        public string Name { get; set; } = "Моя Вінниця";
    }
}
