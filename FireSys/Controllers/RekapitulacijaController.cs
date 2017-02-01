using FireSys.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireSys.Controllers
{
    [AuthorizeRoles("user")]
    public class RekapitulacijaController : BaseController
    {
        // GET: Rekapitulacija
        public ActionResult Index()
        {
            return View();
        }
    }
}