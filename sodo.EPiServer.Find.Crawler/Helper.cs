using System;

namespace Crawler
{
    /// <summary>
    /// The helper class.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Gets host from an url.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>The host of url.</returns>
        public static string GetHostInUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return string.Empty;
            }

            Uri uri;
            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                return uri.Host;
            }

            if(url.StartsWith("/"))
            {
                return null;
            }

            var segmentsBySlash = url.Split('/');
            if (segmentsBySlash == null || segmentsBySlash.Length == 0)
            {
                return null;
            }
            return segmentsBySlash[0];
        }
    }
}
