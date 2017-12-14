using FireSys.Attributes;
using FireSys.DB;
using FireSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireSys.Controllers
{
    public class DashController : BaseController
    {
        private FireSysModel db = new FireSysModel();
        // GET: Dashboard
        /// <summary>
        /// Test only
        /// </summary>
        /// <returns></returns>
        [AuthorizeRoles("user")]
        public ActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel();
            model.Hidranti = db.Hidrants.ToList();
           // model.Orders = db.Orders.Include("UserInfo").Include("Location").ToList();
           // model.Clients = db.UserInfos.Include("User").Where(x=>x.Email.StartsWith("user")).ToList();

            return View(model);
        }

        // GET: Dashboard-Template
        public ActionResult Template()
        {
            return View();
        }
    }
}