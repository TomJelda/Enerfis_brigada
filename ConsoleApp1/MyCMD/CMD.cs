using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.MyCMD
{
    public class CMD
    {
        public Application App;
        public CMDchar[,] charArray;

        public CMD(Application app)
        {
            App = app;
            GenerateCMD();
        }
        private void GenerateCMD()
        {
            charArray = new CMDchar[Console.WindowWidth, Console.WindowHeight];

            for (int y = 0; y < charArray.GetLength(1); y++)
            {
                for (int x = 0; x < charArray.GetLength(0); x++)
                {
                    charArray[x, y] = new CMDchar();
                }
            }
        }
    }
}
