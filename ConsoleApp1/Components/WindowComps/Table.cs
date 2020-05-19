using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1.Components.WindowComps
{
    public class Table : WindowComp
    {
        FileService FileService = new FileService();

        private Row Header = new Row();
        private List<Row> Data = new List<Row>();

        private int X;
        private int Y;
        private List<int> Widths = new List<int>();
        private int Height;
        private int Width;

        public string Path { get; protected set; } = string.Empty;

        //GetOrDefault
        public override string GetPathOrDefault()
        {
            return Path;
        }
        public override string GetSelectedFileOrDefault()
        {
            return Data[SelectedIndex].CellValue(0);
        }

        public override bool SameFileInDirectoryOrDefault(string selectedFile)
        {
            bool boolean = false;

            for (int row = 0; row < this.Data.Count; row++)
            {
                if (Data[row].CellValue(0) == selectedFile)
                    continue;

                boolean = true;
            }

            return boolean;
        }

        

        public bool Active { get; set; }
        public int SelectedIndex = 0;
        public int StartIndex = 0;
        public int PathStartIndex = 0;

        public Table(string path, List<int> widths, int height, int x, int y, params string[] headers)
        {
            this.Path = path;
            this.Widths = widths;
            this.Height = height;
            this.X = x;
            this.Y = y;

            foreach (string item in headers)
            {
                this.Header.Add(new Cell() { Value = item });
                this.Header.FileInterface = " ";
            }

            foreach (int width in widths)
            {
                this.Width += width + 2;
            }

            ChangeDrive();
        }


        //-------------------------------------------
        //HandleKey
        public override void HandleKey(ConsoleKeyInfo handle)
        {
            if (handle.Key == ConsoleKey.DownArrow || handle.Key == ConsoleKey.UpArrow)
                ChangeIndex(handle);

            else if (handle.Key == ConsoleKey.Enter)
            {
                if (Data[SelectedIndex].FileType == (int)FileType.BackDir)
                    EnterParent();

                else if (Data[SelectedIndex].FileType == (int)FileType.Directory)
                    EnterDirectory();

                else if (Data[SelectedIndex].FileType == (int)FileType.Drive)
                    EnterDrive();
            }
        }


        //-------------------------------------------
        //Metody pro HandleKey
        public void ChangeIndex(ConsoleKeyInfo handle)
        {
            if (handle.Key == ConsoleKey.DownArrow && SelectedIndex < Data.Count - 1)
            {
                SelectedIndex++;

                if (SelectedIndex >= Height)
                    StartIndex = SelectedIndex - Height + 1;
            }

            else if (handle.Key == ConsoleKey.UpArrow && SelectedIndex > 0)
            {
                SelectedIndex--;

                if (SelectedIndex < StartIndex)
                    StartIndex = SelectedIndex;
            }
        }

        public void EnterDirectory()
        {
            string oldPath = Path;
            Path = System.IO.Path.Combine(Path, Data[SelectedIndex].CellValue(0));
            try
            {
                ChangeData();
            }
            catch (Exception)
            {
                Path = oldPath;
                ChangeData();
                throw;
            }
        }

        public void EnterParent()
        {
            if (System.IO.Directory.GetParent(Path) == null)
                ChangeDrive();
            else
            {
                Path = System.IO.Directory.GetParent(Path).ToString();
                ChangeData();
            }
        }

        public override void ChangeData()
        {
            this.Data = FileService.ReturnData(Path);

            this.SelectedIndex = 0;
            this.StartIndex = 0;
            this.ReinitializeWidths();
        }

        private void ChangeDrive()
        {
            Path = @"\\";
            this.Data.Clear();
            this.Data = FileService.ReturnDrives();

            this.SelectedIndex = 0;
            this.ReinitializeWidths();
        }

        private void EnterDrive()
        {
            Path = Data[SelectedIndex].CellValue(0);
            ChangeData();
        }

        //-------------------------------------------
        //Draw
        public override void Draw()
        {
            int y = Y;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(X, y++);
            this.DrawLine("┌", "┐");
            DrawPath();
            Console.SetCursorPosition(X, y++);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            this.Header.Draw();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(X, y++);


            //if (SelectedIndex > Height)
            //    StartIndex = SelectedIndex - Height + 1;

            for (int i = StartIndex; i < Height + StartIndex; i++)
            {
                if (i >= Data.Count)
                    break;

                if (i == SelectedIndex && Active)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                }

                Data[i].Draw();
                Console.SetCursorPosition(X, y++);
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (Data.Count < Height)
            {
                y = FillRest(y);
            }

            this.DrawLine("├", "┤");
            Console.SetCursorPosition(X, y++);
            this.DrawInfo();
            Console.SetCursorPosition(X, y++);
            this.DrawLine("└", "┘");
            //this.DrawPath();
        }

        private void DrawPath()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(X + 5, Y);

            int width = Width - 5;
            if (this.Path.Length >= width)
            {
                PathStartIndex = this.Path.Length - width + 5;
                Console.Write(this.Path.Substring(0, 3) + ".." + this.Path.Substring(PathStartIndex, width - 5));
            }
            
            else
                Console.Write(this.Path);

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void DrawLine(string first, string second)
        {
            Console.Write(first.PadRight(Width + 3, '─'));
            Console.Write(second);
        }

        private void DrawInfo()
        {
            int width = 1;
            string info = "";
            foreach (int item in Widths)
                width += item + 1;
            
            info = (Data[SelectedIndex].FileInterface + Data[SelectedIndex].CellValue(0)).Trim();

            Console.Write("│ " + info.PadRight(width + 2) + " │");
        }

        private int FillRest(int y)
        {
            for (int row = Data.Count; row < Height; row++)
            {
                foreach (int width in Widths)
                {
                    Console.Write("│ ".PadRight(width + 3));
                }
                Console.Write("│");

                Console.SetCursorPosition(X, y++);
            }
            return y;
        }

        private void ReinitializeWidths()
        {
            this.Header.SetWidths(Widths);
            foreach (Row item in this.Data)
            {
                item.SetWidths(Widths);
            }
        }

        public override void ChangeActive()
        {
            this.Active = !this.Active;
        }
    }
}
