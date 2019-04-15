using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using sodo.EPiServer.Find.Crawler;
using EPiServer.Find.UnifiedSearch;

namespace sodo.EPiServer.Find.Crawler.Parsers
{
    public class EpiserverWorldBlogParser : DocumentParserBase
    {
        public override IEnumerable<ISearchContent> GetResults(string url)
        {
            WebClient wclient = new WebClient();
            string RSSData = wclient.DownloadString(url);

            XDocument xml = XDocument.Parse(RSSData);
            var RSSFeedData = xml.Descendants("item").Select(x => new RSSFeed
            {
                Title = ((string)x.Element("title")),
                Link = ((string)x.Element("link")),
                Description = ((string)x.Element("description")),
                PubDate = ((string)x.Element("pubDate")),
                Category = ((string)x.Element("category")),
                Guid = ((string)x.Element("guid")),
            });
            
            var retVal = RSSFeedData.Select(x => new EPiServerWorldBlogContent
            { 
                Id = BuildGuid(x.Guid),
                SearchTitle = x.Title,
                SearchSummary = x.Title,
                SearchText = RemoveHtmlTags(x.Description),
                SearchPublishDate = TryParsePubDate(x.PubDate),
                SearchCategories = new List<string>() { x.Category },
                SearchHitUrl = x.Link
            });

            return retVal;
        }
    }

    public class RSSFeed
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string PubDate { get; set; }
        public string Category { get; set; }
        public string Guid { get; set; }
    }
}
