using FireSys.DB;
using FireSys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireSys.Controllers
{
    public class HomeController : BaseController
    {
        private Repository db = new Repository();

        // GET: Index
        public ActionResult Index()
        {
            Order orderModel = new Order();
            orderModel.LocationsList = db.Locations.ToList();
            return View(orderModel);
        }

        public ActionResult How()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}