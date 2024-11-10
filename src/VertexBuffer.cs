using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.ES30;
using static OpenTK.Graphics.OpenGL.GL;

namespace OpenTk3D.src
{
    public class VertexBuffer
    {
        int id;
        public VertexBuffer(float[] vertecies)
        {
            id = GenBuffer();
            Bind();
            BufferData(OpenTK.Graphics.OpenGL.BufferTarget.ArrayBuffer, vertecies.Length * sizeof(float), vertecies, OpenTK.Graphics.OpenGL.BufferUsageHint.StaticDraw);
            Unbind();
        }
        public void Bind()
        {
            BindBuffer(OpenTK.Graphics.OpenGL.BufferTarget.ArrayBuffer, id);
        }
        public void Unbind()
        {
            BindBuffer(OpenTK.Graphics.OpenGL.BufferTarget.ArrayBuffer, 0);
        }
    }
}
