using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using ConsoleApp1.Components.Dialog;

namespace ConsoleApp1
{
    public class InputDialog : Dialog
    {
        FileService FileService = new FileService();
        private int FileOperation { get; set; }
        private string selectedFileName;
        public TextBox TextBox { get; set; } 

        public InputDialog(string label, string selectedFileName, int fileOperation)
        {
            this.Width = label.Length;
            TextBox = new TextBox();
            TextBox.Width = this.Width;

            this.selectedFileName = selectedFileName;

            FileOperation = fileOperation;

            Label = label;

            this.X = (Console.WindowWidth / 2) - (this.Width / 2);
            this.Y = (Console.WindowHeight / 2) - (this.Height / 2);
        }

        public override void HandleKey(ConsoleKeyInfo handle)
        {
            if(handle.Key != ConsoleKey.Enter)
            TextBox.HandleKey(handle);
        }

        public override void EnterReaction(string path, string path1)
        {
            if (FileOperation == (int)FileOperations.CreateDir)
                FileService.CreateDirectory(Path.Combine(path, TextBox.Value.Trim()));

            else if (FileOperation == (int)FileOperations.Rename)
            {
                FileService.Move(Path.Combine(path, selectedFileName), Path.Combine(path, TextBox.Value.Trim()));
            }
        }

        public override void Draw()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;

            int y = this.Y;
            Console.SetCursorPosition(X, y++);
            this.DrawLine("┌", "┐");
            Console.SetCursorPosition(X, y++);
            Console.Write("│ " + Label.PadRight(Width + 1) + "│");
            Console.SetCursorPosition(X, y++);
            TextBox.Draw();
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
