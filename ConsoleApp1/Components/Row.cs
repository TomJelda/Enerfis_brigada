using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Components
{
    public class Row
    {
        private List<Cell> data = new List<Cell>();
        public string this [int i] => data[i].Value;
        public int FileType { get; set; }
        public string FileInterface { get; set; }

        public int Count
        {
            get
            {
                return this.data.Count;
            }
        }

        public string CellValue(int index)
        {
            return data[index].Value;
        }

        public int MinWidth(int index)
        {
            return this.data[index].Value.Length;
        }

        public List<int> GetWidths()
        {
            List<int> widths = new List<int>();

            foreach (Cell item in this.data)
            {
                widths.Add(item.Width);
            }

            return widths;
        }

        public void SetWidths(List<int> widths)
        {
            if (widths.Count != this.data.Count)
                throw new ArgumentOutOfRangeException("Columns count missmatch");

            for (int i = 0; i < widths.Count; i++)
            {
                this.data[i].Width = widths[i];
            }
        }

        public void Add(Cell cell)
        {
            this.data.Add(cell);
        }

        public void Draw()
        {
            for (int i = 0; i < data.Count(); i++)
            {
                //Console.Write("│ ");
                if (i == 0)
                    Console.Write("│" + FileInterface);
                else
                    Console.Write("│ ");
                data[i].Draw();
                Console.Write(" ");
            }

            Console.Write("│");
        }

    }
}
