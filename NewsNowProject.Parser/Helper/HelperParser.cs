using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Source.Advertising;
using NewsNowProject.Parser.Source.IVinInfo;
using NewsNowProject.Parser.Source.MistoVnUa;
using NewsNowProject.Parser.Source.MyVin;
using NewsNowProject.Parser.Source.V0432;
using NewsNowProject.Parser.Source.Vezha;
using NewsNowProject.Parser.Source.VinGovUa;
using NewsNowProject.Parser.Source.VinnitsaInfo;
using NewsNowProject.Parser.Source.VinnitsaOk;
using NewsNowProject.Parser.Source.VlasnoInfo;
using NewsNowProject.Parser.Source.Vn20minut;
using NewsNowProject.Parser.Source.VnDepo;
using NewsNowProject.Parser.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace NewsNowProject.Parser.Helper
{
    public static class HelperParser
    {
        private static List<SourceParserViewModel> source = new List<SourceParserViewModel>
        {
            new SourceParserViewModel() { Setting = new Vn20minutSettings(), Parser = new Vn20minutParser() },
            new SourceParserViewModel() { Setting = new V0432Settings(), Parser = new V0432Parser() },
            new SourceParserViewModel() { Setting = new VezhaSettings(), Parser = new VezhaParser() },
            new SourceParserViewModel() { Setting = new VinGovUaSettings(), Parser = new VinGovUaParser() },
            new SourceParserViewModel() { Setting = new MyVinSettings(), Parser = new MyVinParser() },
            new SourceParserViewModel() { Setting = new VinnitsaInfoSettings(), Parser = new VinnitsaInfoParser() },
            new SourceParserViewModel() { Setting = new VlasnoInfoSettings(), Parser = new VlasnoInfoParser() },
            new SourceParserViewModel() { Setting = new MistoVnUaSettings(), Parser = new MistoVnUaParser() },
            new SourceParserViewModel() { Setting = new VnDepoSettings(), Parser = new VnDepoParser() },            
            new SourceParserViewModel() { Setting = new IVinInfoSettings(), Parser = new IVinInfoParser() },
            //new SourceParserViewModel() { Setting = new VinnitsaOkSettings(), Parser = new VinnitsaOkParser() },
        };

        public static string GetUrl(string siteUrl, string url)
        {
            return url.Contains(siteUrl) ? url : siteUrl + (url.StartsWith("/") ? url : "/" + url);
        }

        public static SourceContentViewModel GetSourceContent()
        {
            var advertising = new AdvertisingSettings();
            var model = new SourceContentViewModel()
            {
                Advertising = new SourceModel()
                {
                    Source = advertising.Source,
                    Title = advertising.Title,
                    Icon = advertising.Icon,
                    Name = advertising.Name,
                    SourceName = advertising.ToString()
                },
                SourceContent = source.Select(x => new SourceModel()
                {
                    Source = x.Setting.Source,
                    Title = x.Setting.Title,
                    Icon = x.Setting.Icon,
                    Name = x.Setting.Name,
                    SourceName = x.Setting.Source.ToString()
                }).ToList()
            };

            return model;
        }

        public static List<SourceParserViewModel> GetSourceParser()
        {
            return source;
        }
    }
}
