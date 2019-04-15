using System;
using System.Collections.Generic;
using EPiServer.Find;
using EPiServer.Find.UnifiedSearch;

namespace sodo.EPiServer.Find.Crawler.CrawlObjects
{
    public class ExternalSiteContentBase : ISearchContent
    {
        [Id]
        public string Id { get; set; }

        public string SearchTitle { get; set; }
        public string SearchSummary { get; set; }
        public string SearchText { get; set; }
        public string SearchHitUrl { get; set; }
        public string SearchTypeName { get; set; }
        public string SearchHitTypeName { get; set; }
        public string SearchSection { get; set; }
        public string SearchSubsection { get; set; }
        public IEnumerable<string> SearchAuthors { get; set; }
        public GeoLocation SearchGeoLocation { get; set; }
        public DateTime? SearchPublishDate { get; set; }
        public DateTime? SearchUpdateDate { get; set; }
        public Attachment SearchAttachment { get; set; }
        public string SearchAttachmentText { get; set; }
        public IEnumerable<string> SearchCategories { get; set; }
        public string SearchFilename { get; set; }
        public string SearchFileExtension { get; set; }
        public IDictionary<string, IndexValue> SearchMetaData { get; set; }

        GeoLocation ISearchContent.SearchGeoLocation
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        Attachment ISearchContent.SearchAttachment
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IDictionary<string, IndexValue> ISearchContent.SearchMetaData
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SearchHeaders;
        public string SearchSourceName;
        public IEnumerable<string> SearchBreadcrumb;
    }
}

