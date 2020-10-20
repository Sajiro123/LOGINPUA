using System.Web;
using System.Web.Optimization;

namespace LOGINPUA
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            //Css Admin
            bundles.Add(new StyleBundle("~/Content/css/Admin").Include(
                      "~/Content/css_Admin/bootstrap-4.2.1.min.css",
                      "~/Content/css_Admin/fontawesome-5.7.2.css",
                      "~/Content/jquery.dataTables.min.css",
                      "~/Library/datatables.bootstrap.min.css",
                      "~/Content/css_Admin/menu.css"
                      ));
            //AngularJS Usuario
            bundles.Add(new ScriptBundle("~/AngularJs").Include(
                     "~/AngularJs/Library/angular.min.js",
                     "~/AngularJs/Library/angular-resource.min.js",
                     "~/AngularJs/Module/MyApp.js",
                     "~/AngularJs/Directive/MyDirective.js",
                     "~/AngularJs/Service/MyServices.js",
                     "~/AngularJs/Service/MyServicesGeneral.js",

                     "~/Scripts/jquery.dataTables.min.js",
                     "~/AngularJs/Library/dataTables.lightColumnFilter.min.js",
                     "~/AngularJs/Library/dataTables.columnFilter.js",

                     "~/AngularJs/Library/ui-bootstrap-tpls.min.js",
                     "~/AngularJs/Library/angular-datatables.min.js",
                     "~/AngularJs/Library/angular-datatables.bootstrap.min.js",
                     "~/AngularJs/Library/angular-datatables.columnfilter.min.js",
                     "~/AngularJs/Library/angular-datatables.light-columnfilter.js",

                     "~/AngularJs/Library/angular-ui-router.min.js",

                     "~/Scripts/scripts_Admin/jquery-3.3.1.min.js",
                     "~/Scripts/scripts_Admin/bootstrap-4.2.1.min.js",
                     "~/Scripts/scripts_Admin/jquery.dataTables.min.js",
                     "~/Scripts/scripts_Admin/jquery.mCustomScrollbar.concat.min.js",
                     //SE TIENE QUE DEFINIR Y LLAMAR A LAS LIBRERIAS DE ANGULAR, TALES COMO EL CONTROLADOR
                     "~/AngularJs/Controller/PermisosController.js",
                    //UTILIDAD
                    "~/AngularJs/Utilidad/Datatable.js",
                    "~/AngularJs/Utilidad/Html.js",

                    "~/AngularJs/Utilidad/Funciones.js"
                      ));


            bundles.Add(new ScriptBundle("~/AngularJs_1").Include(

                 "~/AngularJs/Library/angular.min.js",
                 "~/AngularJs/Library/angular-resource.min.js",
                   "~/AngularJs/Module/MyApp.js",

                 "~/AngularJs/Service/MyServices.js",
                 "~/AngularJs/Service/MyServicesGeneral.js",

                 "~/AngularJs/Library/angular-ui-router.min.js",
                 "~/Scripts/scripts_Admin/jquery.dataTables.min.js",
                 "~/AngularJs/Library/angular-datatables.min.js",

                "~/AngularJs/Utilidad/Funciones.js",

               "~/AngularJs/Controller/PermisosController.js",



                 "~/Scripts/scripts_Admin/jquery.mCustomScrollbar.concat.min.js"
                 //SE TIENE QUE DEFINIR Y LLAMAR A LAS LIBRERIAS DE ANGULAR, TALES COMO EL CONTROLADOR

                 //UTILIDAD

                 ));



            //AngularJS Admin
            bundles.Add(new ScriptBundle("~/Scripts/AngularJS/Admin").Include(
                     "~/AngularJs/Config/MyConfig.js",
                     "~/AngularJs/Controller/UsuarioController.js",
                     "~/AngularJs/Controller/EmpresaController.js",
                     "~/AngularJs/Controller/AccesosController.js",
                     "~/AngularJs/Controller/AdministradorController.js",
                     "~/AngularJs/Controller/HacomController.js"


                      ));

            //AngularJS Login
            bundles.Add(new ScriptBundle("~/AngularJs/Login").Include(
                    "~/AngularJs/Library/angular.min.js",
                    "~/AngularJs/Library/angular-resource.min.js",
                    "~/AngularJs/Module/MyAppLogin.js",
                    "~/AngularJs/Service/MyServicesLogin.js",
                    //SE TIENE QUE DEFINIR Y LLAMAR A LAS LIBRERIAS DE ANGULAR, TALES COMO EL CONTROLADOR
                    "~/AngularJs/Controller/LoginController.js",
                    "~/AngularJs/Utilidad/FuncionesLogin.js"
                      ));
            //SweetAlert2
            bundles.Add(new ScriptBundle("~/SweetAlert").Include(
                "~/Content/SweetAlert/promise-polyfill.js",
                  "~/Content/SweetAlert/sweetalert2@8.js"
                  ));
        }
    }
}
