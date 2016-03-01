using Notes.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Notes.Web.Infrastructure.HtmlHelpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString Pagination(this HtmlHelper html, PagingInfo pagingInfo, Func<int, int, string> pageUrl)
        {
            TagBuilder pagingUl = new TagBuilder("ul");
            pagingUl.AddCssClass("pagination");
            
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.MergeAttribute("href", pageUrl(i, pagingInfo.ItemsPerPage));
                a.SetInnerText(i.ToString());

                if (i == pagingInfo.CurrentPage)
                    li.AddCssClass("active");

                li.InnerHtml += a;
                pagingUl.InnerHtml += li;
            }

            TagBuilder pagingSettingsUl = new TagBuilder("ul");
            pagingSettingsUl.AddCssClass("pager");

            for (int i = 0; i <= 4; i++)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");

                if (i == 0)
                {
                    a.MergeAttribute("href", pageUrl(1, int.MaxValue));
                    a.SetInnerText("All");
                }
                else
                {
                    a.MergeAttribute("href", pageUrl(1, i * 12));
                    a.SetInnerText((i * 12).ToString());
                }

                if (i * 12 == pagingInfo.ItemsPerPage || (i == 0 && pagingInfo.ItemsPerPage == int.MaxValue))
                {
                    a.AddCssClass("text-primary");
                    a.MergeAttribute("style", "text-decoration:underline");
                }

                li.InnerHtml += a;
                pagingSettingsUl.InnerHtml += li;
            }

            return MvcHtmlString.Create(pagingUl.ToString() + pagingSettingsUl.ToString());
        }

        public static MvcHtmlString StatusBar(this HtmlHelper html, StatusBarInfo statusBarInfo) 
        {
            if (statusBarInfo == null || String.IsNullOrWhiteSpace(statusBarInfo.Message))
                return MvcHtmlString.Empty;

            TagBuilder statusBar = new TagBuilder("div");

            statusBar.MergeAttributes(new Dictionary<string, string> 
            { 
                {"id", "status-bar"},
                {"class", statusBarInfo.CssClasses},
                {"style", "position:fixed;bottom:0;left:0;right:0;margin:0;z-index:9999999"}
            });

            statusBar.SetInnerText(statusBarInfo.Message);

            return MvcHtmlString.Create(statusBar.ToString());
        }

        public static MvcHtmlString StatusBar(this HtmlHelper html, string message, string cssClasses)
        {
            if (String.IsNullOrWhiteSpace(message) || String.IsNullOrWhiteSpace(cssClasses))
                return MvcHtmlString.Empty;

            TagBuilder statusBar = new TagBuilder("div");

            statusBar.MergeAttributes(new Dictionary<string, string> 
            { 
                {"id", "status-bar"},
                {"class", cssClasses},
                {"style", "position:fixed;bottom:0;left:0;right:0;margin:0;z-index:9999999"}
            });

            statusBar.SetInnerText(message);

            return MvcHtmlString.Create(statusBar.ToString());
        }
    }
}
