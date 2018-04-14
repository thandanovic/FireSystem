using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FireSys
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DevExpress.XtraReports.Web.WebDocumentViewer.Native.WebDocumentViewerBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.Disabled;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.DefaultBinder = new DevExpress.Web.Mvc.DevExpressEditorsBinder();
            DevExpress.Utils.UrlAccessSecurityLevelSetting.SecurityLevel = DevExpress.Utils.UrlAccessSecurityLevel.Unrestricted;

            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var serverError = Server.GetLastError() as HttpException;

            if (serverError != null && serverError.GetHttpCode() == 404)
            {
                Server.ClearError();
                HttpContext.Current.Response.Redirect("~/404.html");
            }       
        }
    }
}
