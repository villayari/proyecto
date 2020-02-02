using System.Web;
using System.Web.Optimization;

namespace Cibertec.Mvc
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/vendors.bundle.js",
                        "~/Scripts/app.bundle.js",
                        "~/Scripts/datagrid/datatables/datatables.bundle.js",
                        "~/Scripts/datagrid/datatables/datatables.export.js"));
            bundles.Add(new ScriptBundle("~/peity/jquery").Include(
                       "~/Scripts/statistics/peity/peity.bundle.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/vendors.bundle.css",
                      "~/Content/app.bundle.css",
                      "~/Content/datagrid/datatables/datatables.bundle.css",
                      "~/Content/theme-demo.css"));
        }
    }
}
