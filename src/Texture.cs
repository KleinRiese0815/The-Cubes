using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using StbImageSharp;
using static OpenTK.Graphics.OpenGL.GL;

namespace OpenTk3D.src
{
    public class Texture
    {
        int id;
        public Texture(string ImagePath) 
        {
            id = GenTexture();

            Bind(TextureUnit.Texture0);

            TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            //load image
            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult textureImage = ImageResult.FromStream(File.OpenRead(ImagePath), ColorComponents.RedGreenBlueAlpha);

            int width, height;
            width = textureImage.Width; height = textureImage.Height;

            TexImage2D(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, 0, OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgba, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, textureImage.Data);
            Unbind(TextureUnit.Texture0);
        }

        public void Bind(TextureUnit unit)
        {
            ActiveTexture(unit);
            BindTexture(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, id);
        }

        public void Unbind(TextureUnit unit)
        {
            ActiveTexture(unit);
            BindTexture(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, 0);
        }
    }
}
