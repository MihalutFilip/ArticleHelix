using ArticleHelix.Feature.Article.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ArticleHelix.Feature.Article.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchArticles(string searchText)
        {
            var myResults = new SearchResults
            {
                Results = new List<SearchResult>()
            };
            var searchIndex = ContentSearchManager.GetIndex("sitecore_web_index");
            using (var searchContext = searchIndex.CreateSearchContext())
            {
                var searchResults = searchContext.GetQueryable<SearchModel>().Where(x =>
                    x.FullPath.Contains("articledemo") && x.Title != null && (x.ItemName.Contains(searchText) || x.Title.Contains(searchText) || x.Description.Contains(searchText) || x.Category.Contains(searchText))); // Search the index for items which match the predicate
                                                                                                                           // This will get all of the results, which is not reccomended
                var fullResults = searchResults.GetResults();
                foreach (var hit in fullResults.Hits)
                {
                    var item = Sitecore.Context.Database.GetItem(hit.Document.FullPath);

                    Sitecore.Data.Fields.ImageField imageField = item.Fields["Image"];

                    Sitecore.Data.Items.MediaItem image = new Sitecore.Data.Items.MediaItem(imageField.MediaItem);

                    string src = Sitecore.StringUtil.EnsurePrefix('/',

                    Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));

                    string imgTag = String.Format(@"<img src=""{0}"" alt=""{1}"" />", src, image.Alt);

                    myResults.Results.Add(new SearchResult
                    {
                        Description = hit.Document.Description,
                        Title = hit.Document.Title,
                        ItemName = hit.Document.ItemName,
                        Category = hit.Document.Category,
                        FullPath = hit.Document.FullPath,
                        CreatedAt = hit.Document.CreatedAt,
                        Image = imgTag
                    }); 
                }
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = myResults };
            }
        }
    }
}