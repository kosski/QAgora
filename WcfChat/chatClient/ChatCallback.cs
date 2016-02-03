using chatClient.chatService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chatClient
{
    [CallbackBehavior(ConcurrencyMode=ConcurrencyMode.Reentrant)]
    class ChatCallback: IChatServiceCallback
    {
        // deklaracja delegata
        public delegate void ChangingHandler(object sender, MessageSent ca);
        //deklaracja zdarzenia
        public event ChangingHandler Change;
        public void messagePosted(string from, string post)
        {
            //MessageBox.Show(from + " : " + post);
            Change(this, new MessageSent(from+" : "+post));
        }
    }
}
