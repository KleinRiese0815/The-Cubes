using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using static OpenTK.Graphics.OpenGL.GL;

namespace OpenTk3D.src
{
    public class IndexBuffer
    {
        int id;
        public IndexBuffer(uint[] data)
        {
            id = GenBuffer();
            Bind();
            BufferData(OpenTK.Graphics.OpenGL.BufferTarget.ElementArrayBuffer, data.Length * sizeof(uint), data, OpenTK.Graphics.OpenGL.BufferUsageHint.StaticDraw);
        }
        public void Bind()
        {
            BindBuffer(OpenTK.Graphics.OpenGL.BufferTarget.ElementArrayBuffer, id);
        }
        public void Unbind()
        {
            BindBuffer(OpenTK.Graphics.OpenGL.BufferTarget.ElementArrayBuffer, 0);
        }
    }
}
