using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QAgora.Controllers
{
    public class AgoraController : Controller
    {
        //
        // GET: /Agora/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewTopic()
        {
            return View();
        }
	}
}