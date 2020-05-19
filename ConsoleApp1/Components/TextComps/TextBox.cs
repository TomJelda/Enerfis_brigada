using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TextBox : TextComp
    {
        public int Width { get; set; }
        private int startIndex = 0;

        public override void Draw()
        {
            if (Value.Length == 0)
                Console.Write("│ " + "_".PadRight(Width) + " │");

            else if (Value.Length >= Width)
            {
                startIndex = Value.Length - Width + 2;
                Console.Write("│ " + ".." + this.Value.Substring(startIndex, Width - 2) + " │");
            }
            else
                Console.Write("│ " + Value.PadRight(Width) + " │");
            //Console.Write("│ " + Value.PadRight(Width + 1) + "│");
        }
        public override void HandleKey(ConsoleKeyInfo handle)
        {
            if (handle.Key == ConsoleKey.Backspace)
                Value = Value.Substring(0, Math.Max(0, Value.Length - 1));
            
            else if (handle.Key == ConsoleKey.Delete)
                Value = string.Empty;

            else if (Value.Length < 55)
                Value += handle.KeyChar;
        }
    }
}
