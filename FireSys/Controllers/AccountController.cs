using FireSys.Attributes;
using FireSys.Entities;
using FireSys.Manager;
using FireSys.Models;
using FireSys.UI.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireSys.Controllers
{
    public class AccountController : BaseController
    {

        /// <summary>
        /// Test only
        /// </summary>
        /// <returns></returns>
        [AuthorizeRoles("user")]
        public ActionResult Index()
        {
            UserViewModel model = AccountManager.GetModel(User.Identity.Name);
            
            return View(model);
        }

        /// <summary>
        /// Test only
        /// </summary>
        /// <returns></returns>
        [AuthorizeRoles("user")]
        public ActionResult Edit()
        {
            UserViewModel model = AccountManager.GetModel(User.Identity.Name);
            return View(model);
        }

        /// <summary>
        /// Test only
        /// </summary>
        /// <returns></returns>
        [AuthorizeRoles("user")]
        public ActionResult SaveUserInfo(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            UserManager userManager = new UserManager();
            User updateUser = userManager.Find(u => u.Email == User.Identity.Name).FirstOrDefault();
            updateUser.FirstName = model.UserInfo.FirstName;
            updateUser.LastName = model.UserInfo.LastName;
            userManager.Update(updateUser);
            

            return View("Index");
        }


        /// <summary>
        /// Test only
        /// </summary>
        /// <returns></returns>
        [AuthorizeRoles("user")]
        public ActionResult ChangePassword(UserViewModel model)
        {
            return View();
        }
    }
}