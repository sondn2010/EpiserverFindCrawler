using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Find;
using sodo.EPiServer.Find.Crawler;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Crawler
{
    class Program
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private static int _deep;
        private static string _startUrl;
        private static int _delayInterval;
        private static IClient _client;
        private static string _errorMsg;
        private static IList<string> _startUrls = new List<string>()
        {
            "https://world.episerver.com/blogs/?feed=RSS",
            "https://world.episerver.com/forum/developer-forum/Episerver-Commerce/",
            "https://world.episerver.com/forum/developer-forum/EPiServer-Search/",
        };
        private static bool _gotError = false;
        
        static void Main(string[] args)
        {
            _log.Info($"Start crawling...");
            _client = Client.CreateFromConfig();

            try
            {
                foreach (var startUrl in _startUrls)
                {
                    if (string.IsNullOrWhiteSpace(startUrl))
                    {
                        continue;
                    }

                    var parser = ParserFactory.Instance.GetParser(startUrl);
                    if (parser == null)
                    {
                        continue;
                    }

                    var result = CrawlingThenIndex(parser, startUrl);
                    Console.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                if (_gotError) return;

                _gotError = true;
                _log.Error(ex.Message);
                return;
            }

            if (_gotError)
            {
                _log.Error($"Finish crawling... but got error.");
                return;
            }
            
            _log.Info($"Finish crawling!");
            Console.ReadLine();
        }
        
        static string CrawlingThenIndex(IParser parser, string url)
        {
            try
            {
                var results = parser.GetResults(url);

                if (results != null && results.Count() > 0)
                {
                    _client.Index(results);
                }
                Console.WriteLine($"indexed {url}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (_gotError) return "";

                _gotError = true;
                _errorMsg = $"Visiting page {url} but getting error {ex.Message}.";
                return _errorMsg;
            }

            return $"Finish crawling {url}.";
        }
        
    }
}