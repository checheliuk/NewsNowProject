using NewsNowProject.Domain.Model;

namespace NewsNowProject.Parser.Core
{
    public interface IParserSettings
    {
        string BaseUrl { get; set; }
        string Url { get; set; }
        EnumModel.SourceData Source { get; set; }
        int IntervalInMinutes { get; set; }
        string Title { get; set; }
        string Icon { get; set; }
        string Name { get; set; }
    }
}
