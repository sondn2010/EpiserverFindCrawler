using System.Collections.Generic;
using EPiServer.Find.UnifiedSearch;

namespace sodo.EPiServer.Find.Crawler
{
    /// <summary>
    /// Interface, component base, quite interesting API.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Gets search contents from an url.
        /// </summary>
        /// <param name="url">The crawled url.</param>
        /// <returns>The list of crawled content.</returns>
        IEnumerable<ISearchContent> GetResults(string url);
    }
}
