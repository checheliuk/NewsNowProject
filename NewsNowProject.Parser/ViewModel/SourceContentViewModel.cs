using NewsNowProject.Domain.Model;
using System.Collections.Generic;


namespace NewsNowProject.Parser.ViewModel
{
    public class SourceContentViewModel
    {
        public List<SourceModel> SourceContent { get; set; }
        public SourceModel Advertising { get; set; }
    }
}
