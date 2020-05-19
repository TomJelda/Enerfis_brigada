using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using ConsoleApp1.MyCMD;

using ConsoleApp1.Components;
using ConsoleApp1.Components.WindowComps;

namespace ConsoleApp1
{
    public class Application
    {
        public List<WindowComp> components = new List<WindowComp>();
        public Dialog Dialog;
        FileService FileService = new FileService();
        
        string Path = @"\\";
        int ActiveTable = 0;

        string[] Hint;

        public Application()
        {
            int Width = 0;
            List<int> widths = new List<int>() {16, 15, 12 };
            foreach (int item in widths)
                Width += item + 2;

            Console.CursorVisible = false;
            Console.WindowWidth = (Width + 4) * 2;
            Console.WindowHeight = 41;

            for (int i = 0; i < 2; i++)
            {
                int x = (Width + 4) * i;
                components.Add(new Window(new Table(Path, widths, Console.WindowHeight - 8, x, 0, "Name", "Size", "Change Time")));
            }

            components[ActiveTable].ChangeActive();
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Hint = new string[10] {"Help", "Menu", "View", "Move", "Copy", "Rename", "MkDir", "Delete", "Quit", "PullDn" };
            //CMD cmd = new CMD(this);
        }



        public void Draw()
        {
            foreach (Component component in components)
            {
                component.Draw();
            }

            if (Dialog != null)
                Dialog.Draw();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            this.DrawHint();

            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void HandleKey(ConsoleKeyInfo handle)
        {
            if (Dialog != null)
                DialogHandle(handle);

            else if (handle.Key == ConsoleKey.F4)
            {
                Dialog = new QuestionDialog("Move file", components[ActiveTable].GetSelectedFileOrDefault(), (int)FileOperations.Move);
            }

            else if (handle.Key == ConsoleKey.F5)
            {
                Dialog = new QuestionDialog("Copy file", components[ActiveTable].GetSelectedFileOrDefault(), (int)FileOperations.Copy);
            }

            else if (handle.Key == ConsoleKey.F6)
            {
                Dialog = new InputDialog("Rename file", components[ActiveTable].GetSelectedFileOrDefault(), (int)FileOperations.Rename);
            }

            else if (handle.Key == ConsoleKey.F7)
            {
                Dialog = new InputDialog("Enter directory name:", components[ActiveTable].GetSelectedFileOrDefault(), (int)FileOperations.CreateDir);
            }

            else if (handle.Key == ConsoleKey.F8)
            {
                Dialog = new QuestionDialog("Delete file", components[ActiveTable].GetSelectedFileOrDefault(), (int)FileOperations.DeleteFalse);
            }

            else if (handle.Key == ConsoleKey.F9)
            {
                System.Environ­ment.Exit(0);
            }

            else if (handle.Key == ConsoleKey.Tab)
            {
                components[ActiveTable].ChangeActive();
                ActiveTable++;
                ActiveTable = ActiveTable % components.Count;
                components[ActiveTable].ChangeActive();
            }

            else
            {
                try
                {
                    components[ActiveTable].HandleKey(handle);
                }

                catch (UnauthorizedAccessException)
                {
                    Dialog = new WarningDialog("Unauthorized access");
                }

                catch (Exception)
                {
                    Dialog = new WarningDialog("Unexpected error");
                }
            }
        }

        private void DialogHandle(ConsoleKeyInfo handle)
        {
            if (handle.Key == ConsoleKey.Escape)
                Dialog = null;
            else
                Dialog.HandleKey(handle);

            if (handle.Key == ConsoleKey.Enter)
            {
                try
                {
                    Dialog.EnterReaction(components[ActiveTable].GetPathOrDefault(), components[(ActiveTable + 1) % components.Count].GetPathOrDefault());
                    Dialog = null;

                    foreach (WindowComp component in components)
                    {
                        if (component.GetSelectedFileOrDefault().Substring(1,1) != ":")
                        {
                            component.ChangeData();
                        }
                    }
                }

                catch (MyExceptions.FileExistsException)
                {
                    Dialog = new WarningDialog("File already Exists");
                }

                //catch (MyExceptions.UnsupportedSymbolException)
                //{
                //    Dialog = new WarningDialog("Unsupported symbols in name");
                //}

                catch (UnauthorizedAccessException)
                {
                    Dialog = new WarningDialog("Unathorized access");
                }

                catch (MyExceptions.DeleteFullDirException)
                {
                    Dialog = new QuestionDialog("Delete with subdirectory", components[ActiveTable].GetSelectedFileOrDefault(), (int)FileOperations.DeleteTrue);
                    //Dialog = new WarningDialog("Directory is not empty");
                }
            }
        }

        private void DrawHint()
        {
            Console.SetCursorPosition(2, Console.WindowHeight - 2);
            Console.Write("FN keys:");
            for (int i = 3; i < 9; i++)
                HintD(i);
        }

        private void HintD(int index)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ".PadRight(7) + (index + 1));

            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(this.Hint[index].PadRight(6));
        }
    }
}
