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

        [HttpPost]
        public PartialViewResult FindUser(string fraze="")
        {
            return PartialView(respository.SearchUsers(fraze));
        }
    }
}