using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NewsNowProject.Domain.Model;
using NewsNowProject.Domain.ViewModel;
using NewsNowProject.Parser.Core;
using NewsNowProject.Parser.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace NewsNowProject.Parser.Source.IVinInfo
{
    public class IVinInfoParser : IParser<List<Article>>
    {
        private List<string> leftImage = new List<string>()
        {
             "about:///images/ivin.png"
        };

        public IParserSettings parserSettings { get; set; }
        public IVinInfoParser()
        {
            parserSettings = new IVinInfoSettings();
        }
        public List<Article> Parse(IElement element)
        {
            var list = new List<Article>();
            var items = element.QuerySelectorAll(".container a");

            foreach (var item in items.Reverse())
            {
                var url = HelperParser.GetUrl(parserSettings.BaseUrl, item.Attributes.GetNamedItem("href").Value);
                var title = item.GetElementsByClassName("title").FirstOrDefault()?.TextContent.Trim();

                if (!String.IsNullOrEmpty(title) && title.Count() > 10)
                {
                    list.Add(new Article()
                    {
                        Title = title,
                        Source = EnumModel.SourceData.IVinInfo,
                        Date = DateTime.Now,
                        Url = url,
                        Status = EnumModel.ArticleStatus.Active,
                        Image = string.Empty,
                        Description = string.Empty
                    });
                }
            }

            return list;
        }

        public IElement GetElement(IHtmlDocument document)
        {
            return document.GetElementById("sorted-news");
        }

        public Article GetPage(IHtmlDocument document)
        {
            var model = new Article()
            {
                Status = EnumModel.ArticleStatus.Active
            };

            var imageQuery = document.QuerySelectorAll("meta[property=\"og:image\"]").FirstOrDefault();
            model.Image = imageQuery != null ? imageQuery.GetAttribute("content") : HelperSettings.Image;

            var descriptionQuery = document.QuerySelectorAll("meta[name=\"description\"]").FirstOrDefault();
            if (descriptionQuery == null)
                descriptionQuery = document.QuerySelectorAll("meta[name=\"Description\"]").FirstOrDefault();
            if (descriptionQuery == null)
                descriptionQuery = document.QuerySelectorAll("meta[property=\"og:description\"]").FirstOrDefault();
            if (descriptionQuery == null)
                descriptionQuery = document.QuerySelectorAll("meta[property=\"og:Description\"]").FirstOrDefault();

            model.Description = descriptionQuery != null ? descriptionQuery.GetAttribute("content") : HelperSettings.Description;

            /*var paga = document.GetElementById("full-news");
            if (paga != null)
            {
                var textList = new List<String>();

                foreach (var item in paga.QuerySelectorAll("p"))
                {
                    if (!string.IsNullOrEmpty(item.TextContent))
                        textList.Add(item.TextContent);
                }

                if (textList.Any())
                {
                    model.Text = new JavaScriptSerializer().Serialize(textList);
                }

                var imageList = new List<ImageModel>();

                foreach (var item in paga.QuerySelectorAll("img"))
                {
                    var imageElement = (IHtmlImageElement)item;

                    if (!leftImage.Contains(imageElement.Source))
                    {
                        var url = imageElement.Source.Replace("about://", parserSettings.BaseUrl);
                        imageList.Add(new ImageModel() { Url = url, Alt = imageElement.AlternativeText });
                    }
                }

                if(imageList.Any())
                {
                    model.Photo = new JavaScriptSerializer().Serialize(imageList);
                }

                model.Status = EnumModel.ArticleStatus.Parsered;
            }*/

            return model;
        }
    }
}
