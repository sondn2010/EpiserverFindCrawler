using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EPiServer.Find.UnifiedSearch;

namespace sodo.EPiServer.Find.Crawler.Parsers
{
    /// <summary>
    /// The content parser abstract class.
    /// </summary>
    public abstract class DocumentParserBase : IParser
    {
        /// <summary>
        /// Gets ISearchContent from an url.
        /// </summary>
        /// <param name="url">The crawled url.</param>
        /// <returns>The list of crawled content.</returns>
        public abstract IEnumerable<ISearchContent> GetResults(string url);
        
        protected string BuildGuid(string link)
        {
            var retVal = RemoveSpecialCharaters(link).Replace("http", "");
            return retVal.Substring(0, Math.Min(99, retVal.Length));
        }

        protected string RemoveHtmlTags(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return null;

            return Regex.Replace(html, "<.+?>", string.Empty);
        }

        protected string RemoveSpecialCharaters(string input)
        {
            return Regex.Replace(input, "[^0-9a-zA-Z]+", "");
        }

        protected DateTime? TryParsePubDate(string dateTimeString)
        {
            DateTime date;
            if (!DateTime.TryParse(dateTimeString, out date))
            {
                return null;
            }
            return date;
        }
    }
}
