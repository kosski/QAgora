using chatServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(chatService)))
            {
                host.Open();
                Console.WriteLine("Działa");
                Console.ReadKey();
            }
        
        }
    }
}
