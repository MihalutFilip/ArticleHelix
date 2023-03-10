using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System.Collections.Generic;

namespace ArticleHelix.Feature.Article.Models
{
    public class SearchModel : SearchResultItem
    {
        [IndexField("_name")]
        public virtual string ItemName { get; set; }

        [IndexField("title_t")]
        public virtual string Title { get; set; }

        [IndexField("description_t")]
        public virtual string Description { get; set; } 

        [IndexField("category_t")]
        public virtual string Category { get; set; }

        [IndexField("_fullpath")]
        public virtual string FullPath { get; set; }

        [IndexField("createdat_tdt")]
        public virtual DateTime CreatedAt { get; set; }
    }

    public class SearchResult
    {
        public string ItemName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string FullPath { get; set; }
        public virtual DateTime CreatedAt { get; set; }

        public string Image { get; set; }
    }
    /// <summary>
    /// Custom search result model for binding to front end
    /// </summary>
    public class SearchResults
    {
        public List<SearchResult> Results { get; set; }
    }
}