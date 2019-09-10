using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using sodo.EPiServer.Find.Crawler;
using EPiServer.Find.UnifiedSearch;
using sodo.EPiServer.Find.Crawler.CrawlObjects;

namespace sodo.EPiServer.Find.Crawler.Parsers
{
    public class EpiserverWorldForumParser : DocumentParserBase
    {
        int _delayInterval = 1000; // Slow down the crawling.

        /// <summary>
        /// Gets content results from source url.
        /// </summary>
        /// <param name="url">The source url.</param>
        /// <returns>The search contents.</returns>
        public override IEnumerable<ISearchContent> GetResults(string url)
        {
            IList<EPiServerWorldForumContent> retVal = new List<EPiServerWorldForumContent>();

            // Crawling start url
            IDocument document = SendRequest(url).Result;
            //var selector = $"#MainContentArea .forumBox a[href^='/'],a[href^='{document.BaseUrl.Scheme}://{document.BaseUrl.HostName}']";
            var selector = $"#MainContentArea .forumBox a[href^='/']";

            var hrefList = document.QuerySelectorAll(selector)
                .Select(a =>
                {
                    var aHref = a.GetAttribute("href");
                    // only if a start with /, we append the hostname to the beginning
                    var link = new UriBuilder((aHref.StartsWith("/") ? document.BaseUrl.HostName : string.Empty) + aHref).ToString();
                    return link;
                })
                .Distinct();

            var taskList = new List<Task>();
            foreach (var link in hrefList)
            {
                var t = Task.Run(async () =>
                {
                    var htmlDocument = await SendRequest(link);
                    EPiServerWorldForumContent content = ParseDocumentToContent(link, htmlDocument);
                    if (content != null)
                    {
                        retVal.Add(content);
                    }
                    Thread.Sleep(_delayInterval);
                });
                taskList.Add(t);
            }

            Task.WaitAll(taskList.ToArray());
            return retVal;
        }

        private async Task<IDocument> SendRequest(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            return document;
        }

        private EPiServerWorldForumContent ParseDocumentToContent(string link, IDocument htmlDocument)
        {
            if (htmlDocument == null)
            {
                return null;
            }            

            var title = htmlDocument.QuerySelector("h1")?.TextContent?.Trim();
            var releaseDate = htmlDocument.QuerySelector(".pdate")?.TextContent?.Trim();
            var summary = htmlDocument.QuerySelector("h2.sapo")?.TextContent?.Trim();
            var body = RemoveHtmlTags(htmlDocument.QuerySelector("#mainContent")?.TextContent?.Trim());
            

            return new EPiServerWorldForumContent
            {
                Id = BuildGuid(link),
                SearchTitle = title,
                SearchSummary = summary,
                SearchText = RemoveHtmlTags(body),
                SearchPublishDate = TryParsePubDate(releaseDate),
                // SearchCategories = new List<string>() { x.Category },
                SearchHitUrl = link
            };
        }
    }
}
