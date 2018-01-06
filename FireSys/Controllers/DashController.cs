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
            model.HidrantiCount = db.Hidrants.Count();
            model.RadniNaloziCount = db.RadniNalogs.Count();
            model.FaktureCount = db.RadniNalogs.Count();
            model.KlijentiCount = db.Klijents.Count();
            model.VatrigasniAparatiCount = db.VatrogasniAparats.Count();
            model.LokacijeCount = db.Lokacijas.Count();
            model.ZapisniciCount = db.Zapisniks.Count();
            model.EvidencijskeCount = db.EvidencijskaKarticas.Count();
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