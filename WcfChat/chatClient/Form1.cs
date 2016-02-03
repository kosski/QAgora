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
    public partial class Form1 : Form
    {
        ChatServiceClient proxy;
        List<string> messages = new List<string>();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Sender_Click(object sender, EventArgs e)
        {
            if (Message.TextLength > 0)
                proxy.Post(Message.Text);
            Message.Text = "";
        }

        private void ConnectButt_Click(object sender, EventArgs e)
        {
            ChatCallback callback = new ChatCallback();
            proxy = new ChatServiceClient(new InstanceContext(callback));
            callback.Change += callback_Change;

            if (!proxy.Connect(nicktext.Text))
                MessageBox.Show("Imie już zarezerwowane");
            else
            {
                chatBox.Enabled = true;
                Message.Enabled = true;
                Sender.Enabled = true;
                nicktext.Enabled = false;
            }
            KeyPreview = true;
        }

        void callback_Change(object sender, MessageSent ca)
        {
            messages.Add(ca.Message());
            chatBox.DataSource = null;
            chatBox.DataSource = messages;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            proxy.Disconnect();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if(e.KeyCode== Keys.Enter)
              if(Message.TextLength>0)
              {
                proxy.Post(Message.Text);
                Message.Text = "";
              }
        }


    }


}
