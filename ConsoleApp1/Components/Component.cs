using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Components
{
    public abstract class Component
    {
        public abstract void Draw();

        public abstract void HandleKey(ConsoleKeyInfo handle);
    }
}
