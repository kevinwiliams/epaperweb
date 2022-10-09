using System.Web;
using System.Web.Optimization;

namespace ePaperWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new Bundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new Bundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new Bundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new Bundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            bundles.Add(new Bundle("~/bundles/js").Include(
                      "~/assets/vendor/aos/aos.js",
                      "~/assets/vendor/bootstrap/js/bootstrap.bundle.min.js",
                      "~/assets/vendor/glightbox/js/glightbox.min.js",
                      "~/assets/vendor/isotope-layout/isotope.pkgd.min.js",
                      "~/assets/vendor/swiper/swiper-bundle.min.js",
                      "~/assets/js/main.js"));


            bundles.Add(new StyleBundle("~/bundles/css").Include(
                     "~/assets/vendor/aos/aos.css",
                     "~/assets/vendor/bootstrap/css/bootstrap.min.css",
                     "~/assets/vendor/bootstrap-icons/bootstrap-icons.css",
                     "~/assets/vendor/boxicons/css/boxicons.min.css",
                     "~/assets/vendor/glightbox/css/glightbox.min.css",
                     "~/assets/vendor/swiper/swiper-bundle.min.css",
                     "~/assets/css/style.css"));
        }
    }
}
