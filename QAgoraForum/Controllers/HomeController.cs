using QAgoraForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QAgoraForum.Engine;

namespace QAgoraForum.Controllers
{
    public class HomeController : Controller
    {
        private Respository respository=new Respository();

        public PartialViewResult mail_notification()
        {          
                List<Message> messages = respository.GetIncomingMessages(User.Identity.GetUserId()).Where(m=>m.readed==false).ToList();
                foreach (var item in messages)
                    {
                     item.content = item.content.Length > 10 ? item.content.Substring(0, 10) : item.content;
                    }
                return PartialView("_mail_notification", messages);         
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Sections", null);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string name = "")
        {
            IEnumerable<ApplicationUser> users = respository.SearchUsers(name);
            return View(users);
        }
    }
}