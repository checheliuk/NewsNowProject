using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NewsNowProject.Domain.Model;
using NewsNowProject.Parser.Core;
using NewsNowProject.Parser.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsNowProject.Parser.Source.Vn20minut
{
    public class Vn20minutParser : IParser<List<Article>>
    {
        public IParserSettings parserSettings { get; set; }
        public Vn20minutParser()
        {
            parserSettings = new Vn20minutSettings();
        }
        public List<Article> Parse(IElement element)
        {
            var list = new List<Article>();
            var items = element.GetElementsByClassName("list-group-item");

            foreach (var item in items.Reverse())
            {
                var url = HelperParser.GetUrl(parserSettings.BaseUrl, item.Attributes.GetNamedItem("href").Value);
                var title = item.GetElementsByClassName("list-group-item-heading").FirstOrDefault()?.TextContent.Trim();

                if (!String.IsNullOrEmpty(title) && title.Count() > 10)
                { 
                    list.Add(new Article()
                    {
                        Title = title,
                        Source = parserSettings.Source,
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
            return document.GetElementById("today_news").GetElementsByClassName("last-news").FirstOrDefault();
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

            //var paga = document.GetElementById("full-news");
            //if (paga != null)
            //{
            //    foreach (var item in paga.QuerySelectorAll("p"))
            //    {
            //        model.Text += "<p>" + item.TextContent + "</p>";
            //    }

            //    foreach (var item in paga.QuerySelectorAll("img"))
            //    {
            //        model.Photo += "[" + item.TextContent + "]";
            //    }


            //    model.Status = EnumModel.ArticleStatus.Parsered;
            //}

            return model;
        }
    }
}
