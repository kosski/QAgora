using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chatControls
{
    public partial class chatPost : UserControl
    {
        public chatPost()
        {
            InitializeComponent();
        }
        public chatPost(string name,string message)
        {
            PostBox.Text = name;
            Message.Text = message;
            InitializeComponent();
        }

    }
}
