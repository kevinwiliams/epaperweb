using System.Web;
using System.Web.Optimization;

namespace ePaperWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/moment.min.js"));

            bundles.Add(new Bundle("~/bundles/jqueryval").Include(
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new Bundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js"));

            bundles.Add(new Bundle("~/bundles/js").Include(
                      "~/assets/vendor/aos/aos.js",
                      "~/assets/vendor/glightbox/js/glightbox.min.js",
                      "~/assets/vendor/isotope-layout/isotope.pkgd.min.js",
                      "~/assets/vendor/swiper/swiper-bundle.min.js",
                      "~/assets/js/main.js"));


            bundles.Add(new StyleBundle("~/bundles/css").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/Site.css",
                     "~/assets/css/style.css",
                     "~/Content/bootstrap-datetimepicker.min.css"));
        }
    }
}
