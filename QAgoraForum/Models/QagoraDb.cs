using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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

}