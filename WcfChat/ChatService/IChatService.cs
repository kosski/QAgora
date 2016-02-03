using chatClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChatService" in both code and config file together.
    [ServiceContract(
        CallbackContract=typeof(IchatClient), SessionMode=SessionMode.Required)]
    public interface IChatService
    {
        [OperationContract]
        bool Connect(string name);

        [OperationContract]
        void Disconnect();

        [OperationContract]
        void Post(string message);
    }

    public interface IchatClient
    {
        [OperationContract]
        void messagePosted(string from, string post);
    }
}
