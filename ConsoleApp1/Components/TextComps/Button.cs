using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Button : TextComp
    {
        public bool Bool { get; set; }
        //public override string Value { get; set; }
        //Action a;

        public Button(string value, bool boolean)
        {
            Value = value;
            Bool = boolean;
        }

        public override void Draw()
        {
            Console.Write(Value);
        }
        public override void HandleKey(ConsoleKeyInfo handle)
        {
            //if (handle.Key == ConsoleKey.Enter)
            //    a();
        }
    }
}
