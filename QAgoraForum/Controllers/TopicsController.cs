using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QAgoraForum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QAgoraForum.Controllers
{
    [Authorize]//lolo
    public class TopicsController : Controller
    {
        Respository respository= new Respository();
        [AllowAnonymous]
        public async Task<ActionResult> Index(int id)
        {
            return View(respository.GetPosts(id));

        }


        // GET: Topics/Details/5
        
        // GET: Topics/Create
        public ActionResult Create(int sectionId)
        {
            Section section = respository.GetSection(sectionId);
            return View();
        }

        // POST: Topics/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Topic topic, int sectionId)
        {            
            string owner = User.Identity.GetUserId();
            respository.createNewTopic(topic, sectionId, owner);
            return RedirectToAction("Index", new { id=topic.Id });   
        }

        // GET: Topics/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = respository.GetTopic(id.Value);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Permision,Date,IsOpen,PrimaryPost")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                respository.EditTopic(topic);
                return RedirectToAction("Index");
            }
            return View(topic);
        }

        // GET: Topics/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = respository.GetTopic(id.Value);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await respository.DeleteTopic(id);
            return RedirectToAction("Index");
        }

        public PartialViewResult userInfo(string userId)
        {
            return PartialView(respository.getUser(userId));
        }
    }
}
