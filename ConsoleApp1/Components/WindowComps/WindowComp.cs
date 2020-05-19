using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Components
{
    public abstract class WindowComp : Component
    {
        public virtual string GetPathOrDefault() => null;
        public virtual string GetSelectedFileOrDefault() => null;
        public virtual bool SameFileInDirectoryOrDefault(string selectedFile) => false;

        public abstract void ChangeActive();
        public abstract void ChangeData();
    }
}
