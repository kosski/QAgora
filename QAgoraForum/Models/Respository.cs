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

        #endregion

        #region CXUser
        private ApplicationUser getUser(ApplicationDbContext context, string userId)
        {
            return context.Users.Include(u => u.TopicsOwner).First(u => u.Id.Equals(userId));
        }

        private List<ApplicationUser> SearchUsers(ApplicationDbContext context, string some)
        {
            return context.Users.Where(u => u.UserName.Contains(some)).ToList();
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


        public Message getMessage(int messageId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return this.getMessage(dbContext, messageId);
            }
        }

        public bool AddMessage(Message newMessage)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return AddMessage(dbContext, newMessage);
            }
        }

        public bool AddMessage(Message newMessage, int answerFor)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return AddMessage(dbContext, newMessage, answerFor);
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

        private Message getMessage(ApplicationDbContext dbContext, int messageId)
        {
            var result = dbContext.Messages.Find(messageId);
            result.From = dbContext.Users.Find(result.From).UserName;
            return result;
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

        private bool AddMessage(ApplicationDbContext dbContext, Message newMessage, int answerFor)
        {
            try
            {

                newMessage.AnswerFor = dbContext.Messages.Find(answerFor);
                newMessage.Reciver = dbContext.Users.Find(newMessage.AnswerFor.From);
                newMessage.Title = "RE: " + newMessage.AnswerFor.Title;
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

        public List<Post> GetPosts(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetPosts(dbContext, id);
            }
        }

        public bool createNewTopic(Post topic, int sectionId, string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return createNewTopic(dbContext, topic, sectionId, userId);
            }

        }

        public Post GetTopic(int Id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetTopic(dbContext, Id);
            }
        }

        public bool EditTopic(Post topic)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return EditTopic(dbContext, topic);
            }

        }
        //TODO
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

        public bool CreateAnswer(Post answer, string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return this.CreateAnswer(dbContext, answer, userId);
            }
        }
        #endregion


        #region CXTopics

        private List<Post> GetPosts(ApplicationDbContext dbContext, int id)
        {
            var firstPost = dbContext.Posts.Include(p => p.Owner).First(p => p.Id == id);
            List<Post> result = new List<Post> { firstPost };
            result.AddRange(firstPost.Answers.OrderByDescending(p => p.Date));
            return result;
        }

        private bool createNewTopic(ApplicationDbContext dbContext, Post topic, int sectionId, string userId)
        {
            try
            {
                topic.Answers = null;
                topic.Date = DateTime.Now;
                topic.Owner = dbContext.Users.Find(userId);
                topic.SectionId = dbContext.Sections.Find(sectionId);
                dbContext.Posts.Add(topic);
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

        private Post GetTopic(ApplicationDbContext dbContext, int Id)
        {
            return dbContext.Posts.Find(Id);
        }

        private bool EditTopic(ApplicationDbContext dbContext, Post topic)
        {

            dbContext.Entry(topic).State = EntityState.Modified;
            dbContext.SaveChanges();
            return true;

        }

        private bool CreateAnswer(ApplicationDbContext dbContext, Post answer, string userId)
        {
            answer.Owner = this.getUser(dbContext, userId);
            dbContext.Posts.Find(answer.AnswerFor).Answers.Add(answer);
            dbContext.SaveChanges();
            return true;
        }

        #endregion

        #region Roles


        public bool AddRole(IdentityRole newRole)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return AddRole(dbContext, newRole);
            }
        }

        public bool RemoveRole(string roleId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return RemoveRole(dbContext, roleId);
            }
        }

        public string GetRole(int roleId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetRole(dbContext, roleId);
            }
        }

        public int GetUserRole(string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetUserRole(dbContext, userId);
            }
        }

        public List<IdentityRole> GetUserRoles(string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetUserRoles(dbContext, userId);
            }
        }

        public List<IdentityRole> GetRoles()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return GetRoles(dbContext);
            }
        }

        #endregion

        #region CXRoles

        private bool AddRole(ApplicationDbContext dbContext, IdentityRole newRole)
        {
            dbContext.Roles.Add(newRole);
            dbContext.SaveChanges();
            return true;
        }

        private bool RemoveRole(ApplicationDbContext dbContext, string roleId)
        {
            dbContext.Roles.Remove(dbContext.Roles.Find(roleId));
            dbContext.SaveChanges();
            return true;
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

        private List<IdentityRole> GetUserRoles(ApplicationDbContext context, string userId)
        {
            var userRolesId = getUser(context, userId).Roles.Select(r => r.RoleId).ToList();
            var roles = GetRoles(context);
            return roles.Where(role => userRolesId.Any(r => r.Equals(role.Id))).ToList();
        }

        public List<IdentityRole> GetRoles(ApplicationDbContext context)
        {
            return context.Roles.ToList();
        }



        #endregion
    }
}