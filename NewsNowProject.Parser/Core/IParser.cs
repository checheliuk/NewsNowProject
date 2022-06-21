using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NewsNowProject.Domain.Model;

namespace NewsNowProject.Parser.Core
{
    public interface IParser<T> where T : class
    {
        T Parse(IElement document);
        IElement GetElement(IHtmlDocument document);
        IParserSettings parserSettings { get; set; }
        Article GetPage(IHtmlDocument document);
    }
}
