using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class Dialog : Component
    {
        public string Label { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width = 25;
        public int Height = 7;

        public abstract void EnterReaction(string path, string pastePath);

        private void DrawLine(string first, string second)
        {
            Console.Write(first.PadRight(Width + 3, '─'));
            Console.Write(second);
        }
    }
}
