using System.Web;
using System.Web.Optimization;

namespace Alb.Omdehsara.UI.MVC
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.8.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js")
                //  .Include("~/Scripts/jquery.ui.slider-rtl.pack.js")
                        );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/site")
                .Include("~/scripts/bootstrap.min.js")
                .Include("~/scripts/q.js")
                .Include("~/scripts/alb/tools.js")
                .Include("~/scripts/jquery.blockUI.js")
                .Include("~/scripts/skdslider.js")
                .Include("~/scripts/toastr.min.js")
                .Include("~/scripts/jquery.timeTo.min.js")
                );
            bundles.Add(new ScriptBundle("~/bundles/common-js")
                .Include("~/scripts/alb/common.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/fileupload-js")
           .Include("~/scripts/fileupload/tmpl.min.js")
           .Include("~/scripts/fileupload/canvas-to-blob.min.js")
           .Include("~/bundles/fileupload-js")
           .Include("~/scripts/fileupload/load-image.min.js")
           .Include("~/scripts/fileupload/jquery.iframe-transport.js")
           .Include("~/scripts/fileupload/jquery.fileupload.js")
           .Include("~/scripts/fileupload/jquery.fileupload-ip.js")
           .Include("~/scripts/fileupload/jquery.fileupload-ui.js")
           .Include("~/scripts/fileupload/locale.js")
           .Include("~/scripts/fileupload/main.js"));



            bundles.Add(new StyleBundle("~/bundles/fileupload-css")
            .Include("~/scripts/fileupload/jquery.fileupload-ui.css")
            );


            bundles.Add(new StyleBundle("~/content/toolscss")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/css/bootstrap-rtl.css")
                .Include("~/Content/flags.css")
                .Include("~/Content/skdslider.css")
                .Include("~/Content/toastr.css")
                .Include("~/Content/timeTo.css")
                );

            bundles.Add(new StyleBundle("~/content/sitecss")
                .Include("~/Content/site.css")
                .Include("~/Content/site-main.css")
                );


            bundles.Add(new ScriptBundle("~/bundles/jquery-mobile-js")
                .Include("~/scripts/jquery.mobile-1.2.1.min.js")
                );
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-image-gallery-js")
                .Include("~/scripts/bootstrap-image-gallery.min.js")
                );


            bundles.Add(new StyleBundle("~/bundles/bootstrap-image-gallery-css")
    .Include("~/Content/bootstrap-image-gallery.min.css")
    );

            Bundle jssor = new ScriptBundle("~/bundles/jssor-js")
                 .Include("~/scripts/jssor/jssor.slider.mini.js");
            jssor.Transforms.Clear();
            bundles.Add(jssor);
            
            bundles.Add(new StyleBundle("~/bundles/jquery-mobile-css")
    .Include("~/Content/jquery.mobile-1.2.1.css")
    );
            //bundles.Add(new StyleBundle("~/bundles/jqueryui-css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            //"~/Content/themes/base/jquery.ui.selectable.css",
            //            //"~/Content/themes/base/jquery.ui.accordion.css",
            //            //"~/Content/themes/base/jquery.ui.autocomplete.css",
            //            //"~/Content/themes/base/jquery.ui.button.css",
            //            //"~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            //"~/Content/themes/base/jquery.ui.tabs.css",
            //            //"~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css",
            //            "~/Content/themes/base/jquery.ui.slider-rtl.css")
            //            );

            bundles.Add(new ScriptBundle("~/bundles/angularjs")
            .Include("~/scripts/angularjs/angular.js")
            .Include("~/scripts/angularjs/angular-animate.js")
            .Include("~/scripts/angularjs/angular-route.js")
            //.Include("~/Scripts/angularjs/angular-sanitize.min.js")
            //.Include("~/Scripts/angularjs/angular-translate.min.js")
            .Include("~/Scripts/angularjs/angular-messages.min.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/angularjs-app-tools")
                .Include("~/Scripts/angular-tools/checkboxlist.js")
                .Include("~/Scripts/angular-tools/rzslider.js")
                .Include("~/Scripts/angular-tools/ui-bootstrap-tpls-0.11.2.min.js")
                .Include("~/Scripts/angular-tools/ng-infinite-scroll.min.js")
                .Include("~/Scripts/angular-tools/fcsaNumber.min.js")
                .Include("~/scripts/angular-tools/persian-datepicker-tpls.js")
                .Include("~/scripts/angular-tools/persiandate.js")
                .Include("~/scripts/tinymce/tinymce-angaular.js")
                );
            var angularjsAppBundle = new ScriptBundle("~/bundles/angularjs-app");
            angularjsAppBundle.Include("~/app/app.js")
                     .Include("~/app/directives/directives.js")
                     .Include("~/app/services/mainService.js")
                     .Include("~/app/services/constService.js")
                     .Include("~/app/services/locService.js")
                     .Include("~/app/services/userService.js")
                     .Include("~/app/services/utilService.js")
                     .Include("~/app/services/storeService.js")
                     .Include("~/app/services/pollingService.js")
                    // .Include("~/app/services/modalService.js")
                     .Include("~/app/controllers/controllerBase.js")
                     .Include("~/app/controllers/addToCartController.js")
                     .Include("~/app/controllers/productBaseController.js")
                     .Include("~/app/controllers/productController.js")
                     .Include("~/app/controllers/productTypeController.js")
                     .Include("~/app/controllers/productTypeListController.js")
                     .Include("~/app/controllers/searchController.js")
                     .Include("~/app/controllers/shoppingController.js")
                     .Include("~/app/controllers/storeController.js")
                     .Include("~/app/controllers/payOrderController.js")
                     .Include("~/app/controllers/customerAddressController.js")
                     .Include("~/app/controllers/orderTransportController.js")
                     .Include("~/app/controllers/productViewController.js")
                     .Include("~/app/controllers/mainPageController.js");
            bundles.Add(angularjsAppBundle);
        }
    }
}

