using FireSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireSys.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            LoginViewModel model = new LoginViewModel();
             return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            if (base.AuthHelper.SignIn(model))
                return RedirectToAction("Index", "Dash");
            else
            {
                return View("Index", model);
            }
             
        }

        public ActionResult Logout()
        {
            base.AuthHelper.SignOut();
            return RedirectToAction("Index");
        }
    }
}