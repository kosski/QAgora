using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QAgoraForum.Models;

namespace QAgoraForum.Controllers
{
    public class SearchController : Controller
    {
        private readonly Respository respository = new Respository();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FindUser()
        {
            return View();

        }

        [HttpPost]
        public ActionResult FindUser(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return View();
            }
            return View(respository.FindUser(userName));
        }
    }
}