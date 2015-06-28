using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using QAgoraForum.Models;
using QAgoraForum.App_Start;
using Microsoft.AspNet.Identity.Owin;

namespace QAgoraForum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private Respository respository= new Respository();
        // GET: AdminPanel

        private ApplicationRoleManager _AppRoleManager = null;

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }
        public ActionResult Index()
        {
            return View();
        }


        public PartialViewResult ExistingRoles()
        {
            return PartialView(respository.GetRoles());
        }


        [HttpGet]
        public PartialViewResult CreateRole()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult CreateRole(IdentityRole newRole)
        {
            return PartialView(respository.AddRole(newRole) ? "_Success" : "_Error");
        }

        [HttpPost]
        public PartialViewResult RemoveRole(IdentityRole removeRole)
        {
            return PartialView(respository.RemoveRole(removeRole.Id) ? "_Success" : "_Error");
        }
    }
}