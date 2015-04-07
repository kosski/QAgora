using QAgoraForum.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace QAgoraForum.Engine
{
    public static class Helpers
    {
        public static void DownloadFile(string url,string username)
        {
            string strRealname = Path.GetFileName(url);
            string exts = Path.GetExtension(url);

            WebClient webClient = new WebClient();
            webClient.DownloadFile(url, System.Web.HttpContext.Current.Server.MapPath("~/DefaultLayout/Avatars/") + username + "-avatar.jpg");
        }

        public static void giveDefaultMessage(ApplicationUser user)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var newOne = new Message { 
                SendDate = DateTime.Now,
                From = "f3ebab07-7341-444d-8739-1c3f218708a8", 
                content = "First Message", 
                readed = false, 
                Reciver = db.Users.FirstOrDefault(m => m.Id.Equals(user.Id)) };  
         
            var users = db.Users;
            var you = users.FirstOrDefault(m => m.Id.Equals(user.Id));
            db.Messages.Add(newOne);
            db.SaveChanges();
        }

        public static List<Message> getSendedMessages(ApplicationDbContext db, string user)
        {
            var messages = db.Messages;
            var yourMessages = messages.Where(m => m.From.Equals(user));
            return yourMessages.ToList();
        }


    }
}