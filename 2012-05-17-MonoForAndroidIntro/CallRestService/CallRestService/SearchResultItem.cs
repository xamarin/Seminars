using System;

namespace CallRestService
{
    class SearchResultItem
    {
        public SearchResultItem ()
        {
        }

        public string Title { get; set; }

        public string Url { get; set; }

        public override string ToString ()
        {
            return Title;
        }
    }
}

