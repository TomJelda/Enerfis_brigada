using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Components
{
    public class Cell
    {
        public string Value { get; set; }

        public int Width { get; set; }

        public void Draw()
        {
            if (Value.Length > Width)
                Console.Write(this.Value.Substring(0,Width - 2) + "..");
            else
            Console.Write(this.Value.PadRight(this.Width, ' '));
        }
    }
}
