using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ConsoleApp1
{
    public class QuestionDialog : Dialog
    {
        FileService FileService = new FileService();
        private int FileOperation { get; set; }
        private string selectedFileName;
        private int SelectedButton { get; set; }
        List<Button> buttons = new List<Button>();

        public QuestionDialog(string label, string selectedFileName, int fileOperation)
        {

            ButtonAdd("[Yes]", true);
            ButtonAdd("[No]", false);
            
            Label = label + " '" + selectedFileName + "'";
            this.selectedFileName = selectedFileName;

            FileOperation = fileOperation;

            this.Width = Label.Length;
            this.X = (Console.WindowWidth / 2) - (this.Width / 2);
            this.Y = (Console.WindowHeight / 2) - (this.Height / 2);
        }

        public override void HandleKey(ConsoleKeyInfo handle)
        {
            if (handle.Key == ConsoleKey.Tab)
                SelectedButton = (SelectedButton + 1) % buttons.Count;
        }

        public override void EnterReaction(string path, string pastePath)
        {
            if (!buttons[SelectedButton].Bool)
                return;

            if (FileOperation == (int)FileOperations.DeleteFalse)
                FileService.Delete(Path.Combine(path, selectedFileName), false);

            else if (FileOperation == (int)FileOperations.DeleteTrue)
                FileService.Delete(Path.Combine(path, selectedFileName), true);

            else if (FileOperation == (int)FileOperations.Copy)
            {
                FileService.CopyFile(Path.Combine(path, selectedFileName), Path.Combine(pastePath, selectedFileName));
            }

            else if (FileOperation == (int)FileOperations.Move)
            {
                FileService.Move(Path.Combine(path, selectedFileName), Path.Combine(pastePath, selectedFileName));
            }
        }

        public override void Draw()
        {
            int y = this.Y;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(X, y++);
            this.DrawLine("┌", "┐");

            Console.SetCursorPosition(X, y++);
            Console.Write("│ " + Label.PadRight(Width + 1) + "│");

            Console.SetCursorPosition(X, y++);
            this.DrawLine("├", "┤");

            Console.SetCursorPosition(X, y++);
            DrawButtons(y);

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

        private void DrawButtons(int y)
        {
            Console.Write("│ " + "".PadRight(Width + 1) + "│");
            Console.SetCursorPosition(X, y--);

            for (int i = 0; i < buttons.Count(); i++)
            {
                if (i == SelectedButton)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                if (i == 0)
                    Console.SetCursorPosition(X + 2, y);

                if (i == 1)
                    Console.SetCursorPosition(X + Width + 2 - buttons[i].Value.Length, y);

                Console.Write(buttons[i].Value);
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void ButtonAdd(string value, bool boolean)
        {
            Button button = new Button(value, boolean);
            buttons.Add(button);
        }
    }
}
