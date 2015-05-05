using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QAgoraForum.Controllers;
using WebGrease.Css.Extensions;

namespace QAgoraForum.Models
{
    interface IInterface
    {

    }
    public class Respository
    {

        #region UserInfo
        //
        public ApplicationUser getUser(string userId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return getUser(dbContext, userId);
            }
        }


        //
        public List<ApplicationUser> SearchUsers(string some)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return SearchUsers(dbContext, some);
            }
        }

        //
        public string GetRole(int roleId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetRole(dbContext, roleId);
            }
        }
        //
        public int GetUserRole(string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetUserRole(dbContext, userId);
            }

        }
        //
        public List<IdentityRole> GetRoles()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetRoles(dbContext);
            }
        }
        #endregion

        #region CXUser
        private ApplicationUser getUser(ApplicationDbContext context, string userId)
        {
            return context.Users.Find(userId);
        }

        private List<ApplicationUser> SearchUsers(ApplicationDbContext context, string some)
        {
            return context.Users.Where(u => u.UserName.Contains(some)).ToList();
        }

        public string GetRole(ApplicationDbContext context, int roleId)
        {
            var id = roleId.ToString();
            return context.Roles.Find(id).Name;
        }

        public int GetUserRole(ApplicationDbContext context, string userId)
        {
            AccountController account = new AccountController();
            var role = account.UserManager.GetRoles(userId).FirstOrDefault();
            return Convert.ToInt32(context.Roles.FirstOrDefault(r => r.Name.Equals(role)).Id);
        }

        public List<IdentityRole> GetRoles(ApplicationDbContext context)
        {
            return context.Roles.ToList();
        }

        #endregion

        #region Messages
        /// <summary>
        /// Get User incoming messages 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Message> GetIncomingMessages(string userId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return GetIncomingMessages(dbContext, userId);
            }
        }

        /// <summary>
        /// Get User outgoing messages
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Message> GetSendedMessages(string userId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return GetSendedMessages(dbContext, userId);
            }
        }

        public bool AddMessage(Message newMessage)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return AddMessage(dbContext, newMessage);
            }
        }

        public bool ReadMessage(int messageId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return ReadMessage(dbContext, messageId);
            }
        }
        #endregion

        #region CXMessages
        /// <summary>
        /// Get User incoming messages 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private List<Message> GetIncomingMessages(ApplicationDbContext dbContext, string userId)
        {

            var result = dbContext
                .Users
                .Find(userId)
                .Messages
                .OrderByDescending(d => d.SendDate)
                .ToList() ?? new List<Message>();
            return SecureMessages(dbContext, result);
        }

        /// <summary>
        /// Get User outgoing messages
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private List<Message> GetSendedMessages(ApplicationDbContext dbContext, string userId)
        {
            var result = dbContext
                .Messages
                .Where(m => m.From.Equals(userId))
                .Include(m => m.Reciver)
                .OrderByDescending(d => d.SendDate)
                .ToList();
            return SecureMessages(dbContext, result);
        }

        /// <summary>
        /// Making messages secure
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        private List<Message> SecureMessages(ApplicationDbContext dbContext, List<Message> messages)
        {
            foreach (var item in messages)
            {
                item.From = getUser(dbContext, item.From).UserName;
            }
            return messages;
        }

        private bool AddMessage(ApplicationDbContext dbContext, Message newMessage)
        {
            try
            {
                newMessage.Reciver = dbContext
                    .Users
                    .FirstOrDefault(u => u.Id.Equals(newMessage.Reciver.Id));
                dbContext.Messages.Add(newMessage);
                dbContext.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                return false;
            }
        }

        private bool ReadMessage(ApplicationDbContext dbContext, int messageId)
        {
            try
            {
                dbContext
                    .Messages
                    .Find(messageId)
                    .readed = true;
                dbContext.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                return false;
            }

        }
        #endregion

        #region Sections

        public List<Section> GetSections()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetSections(dbContext);
            }
        }

        public Section GetSection(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetSection(dbContext, id);
            }
        }

        public bool AddPanel(Section section, string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return AddPanel(dbContext, section, userId);
            }
        }

        public bool EditSection(Section section, string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return EditSection(dbContext, section, userId);
            }
        }

        public bool DeleteSection(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return DeleteSection(dbContext, id);
            }
        }

        public List<Topic> GetSectionTopics(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetSectionTopics(dbContext, id);
            }
        }

        #endregion

        #region CXSections

        private List<Section> GetSections(ApplicationDbContext dbContext)
        {
            return dbContext.Sections.Include(s => s.Topics).Include(s => s.Panel).ToList();
        }

        private Section GetSection(ApplicationDbContext dbContext, int id)
        {
            return GetSections(dbContext).Find(s => s.Id == id);
        }

        private bool AddPanel(ApplicationDbContext dbContext, Section section, string userId)
        {
            section.Owner = dbContext.Users.Find(userId);
            section.Panel = dbContext.SectionPanels.Find(section.Id);
            dbContext.Sections.Add(section);
            dbContext.SaveChanges();
            return true;

        }

        private bool EditSection(ApplicationDbContext dbContext, Section section, string userId)
        {

            section.Owner = dbContext.Users.Find(userId);
            section.Panel = dbContext.SectionPanels.Find(section.Id);
            dbContext.Sections.Add(section);
            dbContext.SaveChanges();
            return true;

        }

        private bool DeleteSection(ApplicationDbContext dbContext, int id)
        {

            Section section = dbContext.Sections.Find(id);
            dbContext.Sections.Remove(section);
            dbContext.SaveChanges();
            return true;

        }

        private List<Topic> GetSectionTopics(ApplicationDbContext dbContext, int id)
        {
            return dbContext.Topics.Where(t => t.SectionId.Id == id).Include(t => t.SectionId).Include(t => t.Owner).ToList();
        }

        #endregion

        #region SectionPanel

        public List<SectionPanel> GetSectionPanels()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetSectionPanels(dbContext);
            }
        }

        public bool AddSectionPanel(SectionPanel panel)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return AddSectionPanel(dbContext, panel);
            }
        }


        #endregion

        #region CXSectionPanel

        private List<SectionPanel> GetSectionPanels(ApplicationDbContext dbContext)
        {
            var sectionPanels = dbContext.SectionPanels.Include(p => p.Sections.Select(s => s.Topics));
            return sectionPanels.ToList();
        }

        private bool AddSectionPanel(ApplicationDbContext dbContext, SectionPanel panel)
        {
            dbContext.SectionPanels.Add(panel);
            dbContext.SaveChanges();
            return true;
        }


        #endregion



        #region Topics

        public List<XmlPost> GetPosts(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetPosts(dbContext, id);
            }
        }

        public bool createNewTopic(Topic topic, int sectionId, string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return createNewTopic(dbContext, topic, sectionId, userId);
            }

        }

        public Topic GetTopic(int Id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetTopic(dbContext, Id);
            }
        }

        public bool EditTopic(Topic topic)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return EditTopic(dbContext, topic);
            }

        }

        public async Task<bool> DeleteTopic(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                Topic topic = await dbContext.Topics.FindAsync(id);
                dbContext.Topics.Remove(topic);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }
        #endregion


        #region CXTopics

        private List<XmlPost> GetPosts(ApplicationDbContext dbContext, int id)
        {
            var topicInfo = dbContext.Topics.Find(id);
            return XmlPost.getPosts(id);
        }

        private bool createNewTopic(ApplicationDbContext dbContext, Topic topic, int sectionId, string userId)
        {
            try
            {
                topic.Owner = dbContext.Users.Find(userId);
                topic.IsOpen = true;
                topic.SectionId = this.GetSection(dbContext, sectionId);
                topic.Date = DateTime.Now;
                dbContext.Topics.Add(topic);
                XmlPost newTopic = new XmlPost { id = topic.Id, Date = DateTime.Now, Owner = userId, content = topic.PrimaryPost };
                XmlPost.addPost(newTopic, topic.Id);
                dbContext.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

            return false;
        }

        private Topic GetTopic(ApplicationDbContext dbContext, int Id)
        {
            return dbContext.Topics.Find(Id);
        }

        private bool EditTopic(ApplicationDbContext dbContext, Topic topic)
        {

            dbContext.Entry(topic).State = EntityState.Modified;
            dbContext.SaveChanges();
            return true;

        }
        #endregion
    }
}