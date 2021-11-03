using System.Web;
using System.Web.Optimization;

namespace SIGEM_BIDSS
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                      "~/Content/template/plugins/font-awesome/css/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/adminlte").Include(
                  "~/Content/template/dist/css/adminlte.min.css"));

            bundles.Add(new StyleBundle("~/Content/sweetalert2").Include(
                 "~/Content/template/plugins/sweetalert2/sweetalert2.min.css"));


            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Content/template/plugins/jquery/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery-ui").Include(
                     "~/Content/template/plugins/jquery-ui/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                        "~/Content/template/plugins/bootstrap/js/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/adminlte").Include(
                        "~/Content/template/dist/js/adminlte.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/dataTables").Include(
                        "~/Content/template/plugins/datatables/jquery.dataTables.js," +
                        "~/Content/template/plugins/datatables/dataTables.bootstrap4.js"));

            bundles.Add(new ScriptBundle("~/Scripts/sweetalert2").Include(
                        "~/Content/template/plugins/sweetalert2/sweetalert2.min.js"));
        }
    }
}
