using OpenTK.Windowing.GraphicsLibraryFramework;
using static OpenTK.Windowing.GraphicsLibraryFramework.GLFW;
using static OpenTK.Graphics.OpenGL.GL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk3D.src
{
    public unsafe class WindowManager
    {
        public Window* Window;

        public WindowManager(int width, int height, string title)
        {
            Init();

            WindowHint(WindowHintInt.ContextVersionMinor, 3);
            WindowHint(WindowHintInt.ContextVersionMajor, 3);
            WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);

            WindowHint(WindowHintBool.Focused, false);
            WindowHint(WindowHintBool.Maximized, false);
            WindowHint(WindowHintBool.Resizable, true);

            Window = CreateWindow(width, height, title, null, null);

            MakeContextCurrent(Window);

            GLFWBindingsContext bindingcontext = new GLFWBindingsContext();
            LoadBindings(bindingcontext);
            SwapInterval(1);
        }
    }
}
