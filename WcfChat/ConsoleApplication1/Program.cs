using ChatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(ChatService.ChatService)))
            {
                host.Open();
                Console.WriteLine("Działa");
                Console.ReadKey();
            }
        }
    }
}
