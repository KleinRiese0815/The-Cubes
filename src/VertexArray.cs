using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using static OpenTK.Graphics.OpenGL.GL;
using  OpenTK.Graphics.OpenGL;

namespace OpenTk3D.src
{
    public struct AttribPointer
    {
        public int count;
        public int size;
        public VertexAttribPointerType type;
        public bool normalized;
        
        public AttribPointer(int count, int size, VertexAttribPointerType type, bool normalized)
        {
            this.count = count;
            this.size = size;
            this.type = type;
            this.normalized = normalized;
        }
    }

    public class VertexArray
    {
        int id;
        int stride = 0;
        List<AttribPointer> Attributes = new List<AttribPointer>();
        bool wasBound = false;
        public VertexArray()
        {
            id = GenVertexArray();
        }

        public void PushFloatToAttributes(int count)
        {
            Attributes.Add(new AttribPointer(count, count * sizeof(float), VertexAttribPointerType.Float, false));
            stride += count * sizeof(float);
        }

        public void BindBuffer(VertexBuffer buffer)
        {
            Bind();
            buffer.Bind();
            int offset = 0;
            int index = 0;
            foreach (var attr in Attributes)
            {
                VertexAttribPointer(index, attr.count, attr.type, attr.normalized, stride, offset);
                EnableVertexAttribArray(index);
                offset += attr.size;
                index++;
            }
            buffer.Unbind();
            Unbind();
        }
        
        public void Bind()
        {
            BindVertexArray(id);
        }
        public void Unbind()
        {
            BindVertexArray(0);
        }
    }
}
