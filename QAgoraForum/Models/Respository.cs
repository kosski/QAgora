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

        public ApplicationUser getUser(string userId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.Users.FirstOrDefault(u => u.Id.Equals(userId));
            }
        }

        public List<ApplicationUser> SearchUsers(string some)
        {
            using (var dbContext= new ApplicationDbContext())
            {
                return dbContext.Users.Where(u => u.UserName.Contains(some)).ToList();
            }
        }

        public string GetRole(int roleId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var id = roleId.ToString();
                return dbContext.Roles.FirstOrDefault(r => r.Id.Equals(id)).Name;
            }
        }

        public int GetUserRole(string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                AccountController account = new AccountController();
                var role=account.UserManager.GetRoles(userId).FirstOrDefault();
                return Convert.ToInt32(dbContext.Roles.FirstOrDefault(r => r.Name.Equals(role)).Id);
            }

        }

        public List<IdentityRole> GetRoles()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Roles.ToList();
            }
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
                var result = dbContext
                    .Users
                    .FirstOrDefault(u => u.Id.Equals(userId))
                    .Messages
                    .OrderByDescending(d => d.SendDate)
                    .ToList() ?? new List<Message>();
                return SecureMessages(result);
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
                var result = dbContext
                    .Messages
                    .Where(m => m.From.Equals(userId))
                    .Include(m => m.Reciver)
                    .OrderByDescending(d => d.SendDate)
                    .ToList();
                return SecureMessages(result);
            }
        }

        /// <summary>
        /// Making messages secure
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        private List<Message> SecureMessages(List<Message> messages)
        {
                foreach (var item in messages)
                {
                    item.From = getUser(item.From).UserName;
                }        
            return messages;
        }

        public bool AddMessage(Message newMessage)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
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
        }

        public bool ReadMessage(int messageId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                try
                {
                    dbContext
                        .Messages
                        .FirstOrDefault(m => m.Id.Equals(messageId))
                        .readed = true;
                    dbContext.SaveChanges();
                    return true;
                }                
                catch (DbEntityValidationException dbEx)
                {
                    return false;
                }                
            }
        }
        #endregion

        #region Sections

        public List<Section> GetSections()
        {
            using (var dbContext= new ApplicationDbContext())
            {
                return dbContext.Sections.Include(s=>s.Topics).Include(s=>s.Panel).ToList();
            }
        }

        public Section GetSection(int id)
        {
            return GetSections().Find(s=>s.Id==id);
        }

        public bool AddPanel(Section section, string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                section.Owner = dbContext.Users.FirstOrDefault(u => u.Id.Equals(userId));
                section.Panel = dbContext.SectionPanels.FirstOrDefault(s => s.Id == section.Panel.Id);
                dbContext.Sections.Add(section);
                dbContext.SaveChanges();
                return true;
            }
        }

        public bool EditSection(Section section, string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                section.Owner = dbContext.Users.FirstOrDefault(u => u.Id.Equals(userId));
                section.Panel = dbContext.SectionPanels.FirstOrDefault(s => s.Id == section.Panel.Id);
                dbContext.Entry(section).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
        }

        public bool DeleteSection(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                Section section = dbContext.Sections.Find(id);
                dbContext.Sections.Remove(section);
                dbContext.SaveChanges();
                return true;
            }
        }

        public List<Topic> GetSectionTopics(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Topics.Where(t => t.SectionId.Id == id).Include(t=>t.SectionId).Include(t=>t.Owner).ToList();
            }
        }

        #endregion

        #region SectionPanel

        public List<SectionPanel> GetSectionPanels()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var sectionPanels = dbContext.SectionPanels.Include(p => p.Sections.Select(s=>s.Topics));
                
                return sectionPanels.ToList();
            }
        }

        public bool AddSectionPanel(SectionPanel panel)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.SectionPanels.Add(panel);
                dbContext.SaveChanges();
                return true;
            }
        }


        #endregion

        #region Topics

        public List<XmlPost> GetPosts(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var topicInfo = dbContext.Topics.FirstOrDefault(t => t.Id == id);
                return XmlPost.getPosts(id);
            }
        }

        public bool createNewTopic(Topic topic, int sectionId,string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                try
                {
                    topic.Owner = dbContext.Users.Find(userId);
                    topic.IsOpen = true;
                    topic.SectionId = this.GetSection(sectionId);
                    topic.Date = DateTime.Now;
                    dbContext.Topics.Add(topic);
                    XmlPost newTopic = new XmlPost { id = topic.Id, Date = DateTime.Now, Owner = userId, content = topic.PrimaryPost };
                    dbContext.SaveChanges();
                    XmlPost.addPost(newTopic, topic.Id);

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
            }
            return false;
        }

        public Topic GetTopic(int Id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Topics.Find(Id);
            }
        }

        public bool EditTopic(Topic topic)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Entry(topic).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
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
    }
}