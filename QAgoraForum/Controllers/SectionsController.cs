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
using Microsoft.AspNet.Identity.EntityFramework;

namespace QAgoraForum.Controllers
{
    public class SectionsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        Respository respository= new Respository();
        // GET: Sections
        public ActionResult Index()
        {
            List<SectionPanel> sectionPanels = respository.GetSectionPanels();
            List<SectionPanel> result;
            if(User.Identity.IsAuthenticated)
            {
                //string userRoleName=UM.GetRoles(User.Identity.GetUserId()).FirstOrDefault();
                //string userRole = db.Roles.FirstOrDefault(r=>r.Name.Equals(userRoleName)).Id;
                int userRole = respository.GetUserRole(User.Identity.GetUserId());
                result= sectionPanels.Where(panel => userRole<=panel.Visibility).ToList();
            }
            else
            {
                result=sectionPanels.Where(p => p.Visibility == 4).ToList();
            }
            return View(result);
        }


        // GET: Sections/Create
        public ActionResult Create()
        {
            ViewBag.roles = respository.GetRoles();
            ViewBag.sections = respository.GetSectionPanels();
            return View();
        }

        // POST: Sections/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Section section)
        {
            //string userId = User.Identity.GetUserId();
            //section.Owner = respository.getUser(User.Identity.GetUserId());
            //section.Panel = db.SectionPanels.FirstOrDefault(s=>s.Id==section.Panel.Id);
            if (ModelState.IsValid)
            {
                //db.Sections.Add(section);
                //db.SaveChanges();
                respository.AddPanel(section, User.Identity.GetUserId());
                return RedirectToAction("Index");
            }

            return View(section);
        }

        // GET: Sections/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.roles = respository.GetRoles();
            ViewBag.sections = respository.GetSectionPanels();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = respository.GetSection(id.Value);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Sections/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Section section)
        {
            //string userId = User.Identity.GetUserId();
            //section.Owner = db.Users.FirstOrDefault(u => u.Id.Equals(userId));
            //section.Panel = db.SectionPanels.FirstOrDefault(s => s.Id == section.Panel.Id);
            if (!ModelState.IsValid) return View(section);
            respository.EditSection(section, User.Identity.GetUserId());
            return RedirectToAction("Index");
        }

        // GET: Sections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = respository.GetSection(id.Value);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            respository.DeleteSection(id);
            return RedirectToAction("Index");
        }


        public PartialViewResult AdminPanel()
        {
            if (User.IsInRole("Admin"))
                return PartialView(true);
            return PartialView(false);
        }


        public ActionResult CreateSectionPanel()
        {
            ViewBag.roles = respository.GetRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSectionPanel(SectionPanel section)
        {
            if (ModelState.IsValid)
            {
                respository.AddSectionPanel(section);
                return RedirectToAction("Index");
            }

            return View(section);
        }


        public ActionResult Details(int Id)
        {
            List<Topic> topics = respository.GetSectionTopics(Id);
            ViewBag.Title = respository.GetSection(Id);
            ViewBag.SectionID = Id;
            return View(topics);
        }

    }
}
