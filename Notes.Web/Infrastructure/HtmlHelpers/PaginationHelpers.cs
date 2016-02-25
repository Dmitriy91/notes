using Notes.Web.Infrastructure.Models;
using System;
using System.Web.Mvc;

namespace Notes.Web.Infrastructure.HtmlHelpers
{
    public static class PaginationHelpers
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
    }
}
