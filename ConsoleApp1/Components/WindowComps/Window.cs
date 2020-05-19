using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1.Components
{
    public class Window : WindowComp
    {
        WindowComp component;

        public override string GetPathOrDefault()
        {
            return component.GetPathOrDefault();
        }

        public Window(WindowComp component)
        {
            this.component = component;
        }
        

        public override void Draw()
        {
            component.Draw();
        }

        public override void HandleKey(ConsoleKeyInfo handle)
        {
            component.HandleKey(handle);
        }

        public override void ChangeActive()
        {
            component.ChangeActive();
        }

        public override void ChangeData()
        {
            component.ChangeData();
        }

        public override string GetSelectedFileOrDefault()
        {
            return component.GetSelectedFileOrDefault();
        }

        public override bool SameFileInDirectoryOrDefault(string selectedFile)
        {
            return component.SameFileInDirectoryOrDefault(selectedFile);
        }
    }
}
