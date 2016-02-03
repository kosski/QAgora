using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using chatClient;
using System.Threading;

namespace ChatService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ChatService" in both code and config file together.
[ServiceBehavior(InstanceContextMode=InstanceContextMode.Single, ConcurrencyMode=ConcurrencyMode.Reentrant)]
    public class ChatService : IChatService
    {
    private Dictionary<IchatClient, int> clientsAndNames = new Dictionary<IchatClient, int>();
    private List<string> clientsNames = new List<string>();
    public bool Connect(string name)
    {
        if (!clientsNames.Contains(name)) clientsNames.Add(name);
        if (clientsAndNames.ContainsValue(clientsNames.IndexOf(name)))
            return false;
        IchatClient clientCallback = OperationContext.Current.GetCallbackChannel<IchatClient>();
        clientsAndNames.Add(clientCallback, clientsNames.IndexOf(name));
        Post(clientsNames[clientsAndNames[clientCallback]] + " dolaczyl do rozmowy.");
        return true;
    }
        public void Post(string message)
        {
            IchatClient clientCallback = OperationContext.Current.GetCallbackChannel<IchatClient>();
            string name = clientsNames[clientsAndNames[clientCallback]];
            Console.WriteLine(name + " : " + message);
            foreach(KeyValuePair<IchatClient,int> client in clientsAndNames)
            {
                Console.WriteLine(client.Key);
                try
                {
                    Thread callback = new Thread(callbackThread);
                    callback.Start(new info(clientCallback, name, message));
                   // client.Key.messagePosted(name, message);
                }
                catch(Exception e)
                {
                    Disconnect(client.Key);

                }
            }
        }
        private void Disconnect(IchatClient client)
        {
         Post(clientsNames[clientsAndNames[client]]+" rozlaczyl sie...");
         clientsAndNames.Remove(client);
        }
        public void Disconnect()
        {
            IchatClient clientCallback = OperationContext.Current.GetCallbackChannel<IchatClient>();
            Disconnect(clientCallback);
        }
        private static void callbackThread(object o)
        {
            info i = (info)o;
            i.client.messagePosted(i.name, i.message);
        }

    }
    public struct info
    {
        public IchatClient client;
        public string name, message;
        public info(IchatClient c,string n,string m)
        {
            client = c;
            name = n;
            message = m;
        }

    }
}
