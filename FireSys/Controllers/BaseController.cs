using FireSys.Attributes;
using FireSys.Service;
using FireSys.UI.Manager;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireSys.Controllers
{
    [PreventCaching]
    public class BaseController : Controller
    {

        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        private CaWeService caweService;

        private CaWeService CaWeServiceInstance
        {
            get { return caweService ?? (caweService = new CaWeService()); }
        }


        private AuthManager authHelper;

        private AccountManager accountManager;

        public AccountManager AccountManager
        {
            get
            {
                if (accountManager == null) accountManager = new AccountManager();
                return accountManager;
            }
            set { }
        }

        protected AuthManager AuthHelper
        {
            get { return authHelper ?? (authHelper = new AuthManager()); }
        }

       

    }
}