using AngleSharp.Parser.Html;
using System.Threading.Tasks;
using NewsNowProject.Parser.ViewModel;
using AngleSharp.Dom;
using NewsNowProject.Domain.Model;

namespace NewsNowProject.Parser.Core
{
    public class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;
        HtmlLoader loader;

        public IParser<T> Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
            parserSettings = parser.parserSettings;
            loader = new HtmlLoader(parserSettings);
        }

        public ParserWorker(IParser<T> parser, string proxyAddress)
        {
            this.parser = parser;
            parserSettings = parser.parserSettings;
            loader = new HtmlLoader(parserSettings, proxyAddress);
        }

        public async Task<SourceViewModel> GetSource()
        {
            var source = await loader.GetSource();
            var domParser = new HtmlParser();
            var document = await domParser.ParseAsync(source);

            return new SourceViewModel()
            {
                Element = parser.GetElement(document)
            };
        }

        public async Task<Article> GetSourcePage()
        {
            var source = await loader.GetSource();
            var domParser = new HtmlParser();
            var document = await domParser.ParseAsync(source);
            return parser.GetPage(document);
        }

        public T Worker(IElement element)
        {
            return parser.Parse(element);
        }
    }
}
