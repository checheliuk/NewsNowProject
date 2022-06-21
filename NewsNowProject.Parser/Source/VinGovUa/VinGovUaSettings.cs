using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;

namespace NewsNowProject.Parser.Source.VinGovUa
{
    public class VinGovUaSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "http://vin.gov.ua";
        public string Url { get; set; } = "http://vin.gov.ua";
        public EnumModel.SourceData Source { get; set; } = EnumModel.SourceData.VinGovUa;
        public int IntervalInMinutes { get; set; } = 5;
        public string Title { get; set; } = "Офіційний вебсайт — Вінницька обласна державна адміністрація";
        public string Icon { get; set; } = "vingovua";
        public string Name { get; set; } = "ОДА";
    }
}
