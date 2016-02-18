using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Hosting.Self;

namespace Api
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:1234"), new MyNancyBootstrapper()))
            {
                host.Start();
                Console.WriteLine("NB: You don't need to have the API running as an actual end point for the tests to work");
                Console.ReadLine();
            }
        }
    }
}
