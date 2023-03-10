using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleHelix.Feature.Article.Processors
{
    public class HttpRequestProcessor404 : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (Sitecore.Context.Item != null || Sitecore.Context.Site == null || Sitecore.Context.Database == null || Sitecore.Context.Database.Name == "core")
            {
                return;
            }
            var pageNotFound = Sitecore.Context.Database.GetItem(@"{E5DF7366-3783-4C6E-BC71-61F22EE0AA23}");
            if (pageNotFound == null)
                return;
            args.ProcessorItem = pageNotFound;
            Sitecore.Context.Item = pageNotFound;
            args.HttpContext.Response.StatusCode = 404;
        }
    }
}