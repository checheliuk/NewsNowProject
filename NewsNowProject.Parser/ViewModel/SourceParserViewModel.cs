using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;
using System.Collections.Generic;

namespace NewsNowProject.Parser.ViewModel
{
    public class SourceParserViewModel
    {
        public IParserSettings Setting { get; set; }
        public IParser<List<Article>> Parser { get; set; }
    }
}
