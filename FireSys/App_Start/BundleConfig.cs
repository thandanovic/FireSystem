using System.Web;
using System.Web.Optimization;

namespace FireSys
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region styles

            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                      "~/Content/bootstrap/css/bootstrap.css"));

            bundles.Add(new StyleBundle("~/css/shared").Include(
                      "~/Content/theme/metisMenu/metisMenu.min.css",
                      "~/Content/theme/dist/css/sb-admin-2.css",
                      "~/Content/theme/font-awesome/css/font-awesome.min.css",
                      "~/Styles/style.css",
                       "~/Content/plugins/jsgrid-1.5.3/jsgrid-theme.css",
                       "~/Content/plugins/jsgrid-1.5.3/jsgrid.css",
                       "~/Content/bootstrap-datepicker/css/bootstrap-datepicker.css"
                      ));

            bundles.Add(new StyleBundle("~/css/charts").Include(                      
                      "~/Content/theme/morrisjs/morris.css"));

            #endregion

            #region scripts
            bundles.Add(new ScriptBundle("~/jquery").Include(
                        "~/Content/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/jqueryval").Include(
                        "~/Content/jquery.validate/jquery.validate*"));

           

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/modernizr").Include(
                        "~/Content/modernizr-*"));

            bundles.Add(new ScriptBundle("~/js/bootstrap").Include( 
                "~/Content/bootstrap/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/js/shared").Include(
                       "~/Content/theme/metisMenu/metisMenu.min.js",                       
                       "~/Content/theme/dist/js/sb-admin-2.js",
                       "~/Scripts/shared/global.js",
                       "~/Scripts/shared/util.js",
                       "~/Content/plugins/notify/notify.js",
                       "~/Content/plugins/jsgrid-1.5.3/jsgrid.js",
                       "~/Content/bootstrap-datepicker/js/bootstrap-datepicker.js"
                       ));

            bundles.Add(new ScriptBundle("~/js/charts").Include(
                      "~/Content/theme/raphael/raphael.min.js"//,
                      //"~/Content/theme/morrisjs/morris.min.js",
                      //"~/Content/theme/data/morris-data.js"
                      ));

            #endregion

        }
    }
}
