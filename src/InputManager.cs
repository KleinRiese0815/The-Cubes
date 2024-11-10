using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk3D.src
{
    public static class InputManager
    {
        static Vector2 lastMousePosition = new Vector2();

        public unsafe static void SaveLasteMousePosition()
        {
            double x;
            double y;
            GLFW.GetCursorPos(Program.windowManager.Window, out x, out y);
            lastMousePosition = new Vector2((float)x, (float)y);
        }

        public static unsafe Vector2 GetMouseAcceleration()
        {
            double currentX;
            double currentY;
            GLFW.GetCursorPos(Program.windowManager.Window, out currentX, out currentY);

            return lastMousePosition - new Vector2((float)currentX, (float)currentY);
        }
    }
}
