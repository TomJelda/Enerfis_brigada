using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application();

            while (true)
            {
                app.Draw();
                app.HandleKey(Console.ReadKey());
            }
        }
    }
}
