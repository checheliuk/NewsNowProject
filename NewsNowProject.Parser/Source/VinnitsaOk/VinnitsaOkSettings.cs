using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;

namespace NewsNowProject.Parser.Source.VinnitsaOk
{
    public class VinnitsaOkSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "http://vinnitsaok.com.ua";
        public string Url { get; set; } = "http://vinnitsaok.com.ua";
        public EnumModel.SourceData Source { get; set; } = EnumModel.SourceData.VinnitsaOk;
        public int IntervalInMinutes { get; set; } = 5;
        public string Title { get; set; } = "Всі новини Вінниці | Новости Винница | ВінницяOk | Найсвіжіші новини Вінниці, України та світу від журналістів порталу ВінницяОк: статті, коментарі та обговорення, блоги, фото, відео.";
        public string Icon { get; set; } = "vinnitsaok";
        public string Name { get; set; } = "ВинницаOK";
    }
}
