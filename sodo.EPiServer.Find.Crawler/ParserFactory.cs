using System;
using System.Collections.Generic;
using System.Linq;
using sodo.EPiServer.Find.Crawler.Parsers;

namespace sodo.EPiServer.Find.Crawler
{
    public class ParserFactory
    {
        Dictionary<string, IParser> AvailaibleParser;

        private static volatile ParserFactory _instance;   // declared to be volatile to ensure that assignment to the instance variable completes before the instance variable can be accessed
        private static object syncRoot = new Object();

        private ParserFactory()
        {
            //TODO: We can set this in attribute.
            AvailaibleParser = new Dictionary<string, IParser>
            {
                { "https://world.episerver.com/blogs/?feed=RSS", new EpiserverWorldBlogParser() },
                { "https://world.episerver.com/forum/", new EpiserverWorldForumParser() },
                { "https://world.episerver.com/documentation/", new EpiserverWorldDocumentParser() }
            };
        }

        public static ParserFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new ParserFactory();
                        }
                    }
                }
                return _instance;
            }
        }

        public virtual IParser GetParser(string url)
        {
            var parser = AvailaibleParser.Where(e => url.Contains(e.Key)).Select(e => e.Value).SingleOrDefault();
            return parser;
        }

        /// <summary>
        /// Gets host in url.
        /// </summary>
        /// <param name="url">The url.</param>
        private string GetHostInUrl(string url)
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

            if (url.StartsWith("/"))
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
