using System.Web;
using System.Web.Optimization;

namespace Notes.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/future-date-attribute.js",
                        "~/Scripts/form-submission.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap-theme-selector.js"));

            bundles.Add(new ScriptBundle("~/bundles/notetablesorter").Include(
                        "~/Scripts/jquery.tablesorter.min.js",
                        "~/Scripts/note-table-sorter-config.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/css/site.css",
                        "~/Content/css/tablesorter/tablesorter.css"));
        }
    }
}
