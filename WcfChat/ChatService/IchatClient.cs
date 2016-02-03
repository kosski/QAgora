using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace chatClient
{
    public interface IchatClient
    {
        [OperationContract]
        void messagePosted(string from, string post);
    }
}
