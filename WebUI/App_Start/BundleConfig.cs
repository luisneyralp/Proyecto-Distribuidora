using System.Web;
using System.Web.Optimization;

namespace WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/app.js",
                      "~/Scripts/toastr.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                      "~/Scripts/simpleCart.min.js",
                      "~/Scripts/memenu.js",
                      "~/Scripts/jquery.easydropdown.js",
                      "~/Scripts/responsiveslides.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/toastr.min.css",
                      "~/Content/custom.css"));
            bundles.Add(new StyleBundle("~/Content/temp").Include(
                      "~/Content/memenu.css",
                      "~/Content/style1.css"));
            bundles.Add(new StyleBundle("~/Content/assets").Include(
                      "~/Content/bootstrap.css",
                      "~/assets/font-awesome/css/font-awesome.min.css",
                      "~/assets/css/form-elements.css",
                      "~/assets/css/style.css"));
        }
    }
}
