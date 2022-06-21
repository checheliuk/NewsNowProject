using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;

namespace NewsNowProject.Parser.Source.VnDepo
{
    public class VnDepoSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://vn.depo.ua/ukr";
        public string Url { get; set; } = "https://vn.depo.ua/ukr";
        public EnumModel.SourceData Source { get; set; } = EnumModel.SourceData.VnDepo;
        public int IntervalInMinutes { get; set; } = 5;
        public string Title { get; set; } = "Останні новини Вінниці. Найголовніші події міста і області.";
        public string Icon { get; set; } = "vndepo";
        public string Name { get; set; } = "VnDepo";
    }
}
