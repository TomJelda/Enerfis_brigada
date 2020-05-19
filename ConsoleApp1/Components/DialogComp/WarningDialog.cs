using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class WarningDialog : Dialog
    {
        public WarningDialog(string label)
        {
            Label = label;

            Width = label.Length;
            this.X = (Console.WindowWidth / 2) - (this.Width / 2);
            this.Y = (Console.WindowHeight / 2) - (this.Height / 2);
        }

        public override void HandleKey(ConsoleKeyInfo handle)
        {
        }

        public override void EnterReaction(string path, string path1)
        {
        }

        public override void Draw()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            int y = this.Y;

            Console.SetCursorPosition(X, y++);
            this.DrawLine("┌", "┐");
            Console.SetCursorPosition(X, y++);
            Console.Write("│ " + Label.PadRight(Width + 1) + "│");
            Console.SetCursorPosition(X, y++);
            this.DrawLine("└", "┘");

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void DrawLine(string first, string second)
        {
            Console.Write(first.PadRight(Width + 3, '─'));
            Console.Write(second);
        }
    }
}
