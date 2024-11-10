using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using static OpenTK.Graphics.OpenGL.GL;

namespace OpenTk3D.src
{
    public class Shader
    {
        int id;

        public Shader(string vertexFilePath, string fragmentFilePath)
        {
            id = CreateProgram();

            int verShader = CreateShader(OpenTK.Graphics.OpenGL.ShaderType.VertexShader);
            int fragShader = CreateShader(OpenTK.Graphics.OpenGL.ShaderType.FragmentShader);
            ShaderSource(verShader, LoadShaderFromFile(vertexFilePath));
            ShaderSource(fragShader, LoadShaderFromFile(fragmentFilePath));

            AttachShader(id, verShader);
            AttachShader(id, fragShader);

            LinkProgram(id);
            ValidateProgram(id);
            Use();
        }

        public void Use()
        {
            UseProgram(id);
        }
        public void Unuse()
        {
            UseProgram(0);
        }

        public void SetMatrixUniform(string name, Matrix4 matrix)
        {
            int location = GetUniformLocation(id, name);
            if (location != -1)
            {
                UniformMatrix4(location, true, ref matrix);
            }
        }

        string LoadShaderFromFile(string filepath)
        {
            StringBuilder sb = new StringBuilder();
            StreamReader sr = new StreamReader(filepath);

            while (!sr.EndOfStream)
            {
                sb.Append(sr.ReadLine() + '\n');
            }
            return sb.ToString();
        }
    }
}
