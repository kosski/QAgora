using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QAgoraForum.Models;
using Microsoft.AspNet.Identity;

namespace QAgoraForum.Controllers
{
    public class MessagesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private readonly Respository respository=new Respository();
        // GET: Messages
        [Authorize]
        public ActionResult Index(bool success=false)
        {
            string userID = User.Identity.GetUserId();

            List<Message> incomingMessages = respository.GetIncomingMessages(userID);
            List<Message> outgoingMessages = respository.GetSendedMessages(userID);
            ViewBag.incoming = incomingMessages;
            ViewBag.outgoing = outgoingMessages;
            if (success != null) return View(success);
            return View();
        }

        // GET: Messages/Create
        [Authorize]
        public ActionResult Create([Bind(Prefix="Id")] string userID)
        {
            var newOne = new Message { SendDate = DateTime.Now,
                From = User.Identity.GetUserId(), 
                content = "Type text here",
                readed = false,
                Reciver= respository.getUser(userID) };
            return View(newOne);
        }

        // POST: Messages/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Message message)
        {
            bool success = respository.AddMessage(message);
            return RedirectToAction("Index",new {success= success });
 
        }

        [Authorize]
        public PartialViewResult Read(int id)
        {
            respository.ReadMessage(id);
            return PartialView();
        }

    }
}
