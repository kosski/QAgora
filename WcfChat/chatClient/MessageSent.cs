using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatClient
{
    class MessageSent: System.EventArgs
    {
        string message;
        public MessageSent(string message) { this.message = message; }
        public string Message() { return message; }
    }
}
