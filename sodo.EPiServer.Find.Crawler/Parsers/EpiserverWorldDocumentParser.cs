using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using sodo.EPiServer.Find.Crawler;
using EPiServer.Find.UnifiedSearch;

namespace sodo.EPiServer.Find.Crawler.Parsers
{
    public class EpiserverWorldDocumentParser : IParser
    {        
        //TODO
        public IEnumerable<ISearchContent> GetResults(string url)
        {
            // var htmlDocument = SendRequest(url).Wait();
            IDocument htmlDocument = null;

            var title = htmlDocument.QuerySelector("h1")?.TextContent?.Trim();
            var releaseDate = htmlDocument.QuerySelector(".pdate")?.TextContent?.Trim();
            var sapo = htmlDocument.QuerySelector("h2.sapo")?.TextContent?.Trim();
            var body = RemoveHtmlTags(htmlDocument.QuerySelector("#mainContent")?.TextContent?.Trim());

            var linkId = string.Empty;
            

            var retVal = new EPiServerWorldBlogContent
            { 
                SearchHitUrl = url,

            };



            return null;
        }

        private async Task<IDocument> SendRequest(string url)
        {
            //var requester = new AngleSharp.Network.Default.HttpRequester();
            //requester.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36";
            //var configuration = AngleSharp.Configuration.Default.WithDefaultLoader(requesters: new[] { requester });
            //var document = BrowsingContext.New(configuration).OpenAsync(url); // Asynchronously get the document in a new context 


            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            return document;
        }

        private string RemoveHtmlTags(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return null;

            return Regex.Replace(html, "<.+?>", string.Empty);
        }
    }
}
