using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace QAgoraForum.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime? SendDate { get; set; }
        public string From { get; set; }
        public string content { get; set; }
        public bool readed { get; set; }
        public virtual ApplicationUser Reciver { get; set; }
    }

    public class Section
    {
        public int Id { get; set; }
        public int Permision { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual List<Topic> Topics { get; set; }
        public virtual SectionPanel Panel { get; set; }
    }

    public class SectionPanel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Visibility { get; set; }
        public virtual List<Section> Sections { get; set; }
    }
    public class Topic
    {
        public int Id { get; set; }
        public int Permision { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public bool IsOpen { get; set; }
        [AllowHtml]
        public string PrimaryPost { get; set; }
        public virtual Section SectionId { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public int Permision { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        [AllowHtml]
        public string content { get; set; }
        public virtual List<Post> Answers { get; set; }
        public virtual int AnswerFor { get; set; }
        public virtual Section SectionId { get; set; }
    }

    [Serializable]
    public class XmlPost
    {
        [XmlAttribute("id")]
        public int id { get; set; }
        [XmlAttribute("OwnerId")]
        public string Owner { get; set; }
        [XmlText, XmlElement("content")]
        public string content { get; set; }
        [XmlAttribute("Date")]
        public DateTime Date { get; set; }

        public XmlPost() { }

        public XmlPost(SerializationInfo info, StreamingContext ctxt)
        {
            this.id = (int)info.GetValue("id", typeof(int));
            this.Owner = (string)info.GetValue("OwnerId", typeof(string));
            this.Date = (DateTime)info.GetValue("Make", typeof(DateTime));
            this.content = (string)info.GetValue("Make", typeof(HtmlString));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("id", this.id);
            info.AddValue("OwnerId", this.Owner);
            info.AddValue("Date", this.Date);
            info.AddValue("content", this.content);
        }

        public static void addPost(XmlPost post, int topicId)
        {
            List<XmlPost> posts = getPosts(topicId);
            posts.Add(post);
            XmlRootAttribute oRootAttr = new XmlRootAttribute() { ElementName = "Posts", IsNullable = true };
            XmlSerializer oSerializer = new XmlSerializer(typeof(List<XmlPost>), oRootAttr);
            StreamWriter oStreamWriter = null;
            try
            {
                oStreamWriter = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/TopicsStorage/")+ topicId + ".xml");
                oSerializer.Serialize(oStreamWriter, posts);
            }
            catch (Exception oException)
            {
                Console.WriteLine("Aplikacja wygenerowała następujący wyjątek: " + oException.Message);
            }
            finally
            {
                if (null != oStreamWriter)
                {
                    oStreamWriter.Dispose();
                }
            }

        }

        public static List<XmlPost> getPosts(int id)
        {
            try
            {
                StreamReader r = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/TopicsStorage/") + id + ".xml");
                XmlSerializer serializer = new XmlSerializer(typeof(List<XmlPost>));
                List<XmlPost> result = (List<XmlPost>)serializer.Deserialize(r);
                return (List<XmlPost>)result.ToList();
            }
            catch (Exception)
            {
                return new List<XmlPost>();
            }
        }
    }

}