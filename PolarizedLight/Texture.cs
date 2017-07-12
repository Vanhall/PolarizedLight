using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;
using System.IO;
using Tao.Platform.Windows;
using Tao.FreeGlut;
using System.Drawing;
using Tao.DevIl;

namespace PolarizedLight
{
    class Texture
    {
        // ID текстуры
        public int textureID = 0;
        // идетификатор текстуры в памяти openGL
        public uint mGlTextureObject = 0;

        public int width, height;
        /*
        // загрузка текстуры
        public void LoadTextureForModel(string FileName)
        {
            // создаем изображение с индификатором imageId 
            Il.ilGenImages(1, out textureID);
            // делаем изображение текущим 
            Il.ilBindImage(textureID);

            // если загрузка удалась
            if (Il.ilLoadImage(FileName))
            {
                // если загрузка прошла успешно 
                // сохраняем размеры изображения 
                width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);

                // определяем число бит на пиксель 
                int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);

                switch (bitspp)// в зависимости оп полученного результата 
                {
                    // создаем текстуру используя режим GL_RGB или GL_RGBA 
                    case 24:
                        mGlTextureObject = MakeGlTexture(OpenGL.GL_RGB, Il.ilGetData(), width, height);
                        break;
                    case 32:
                        mGlTextureObject = MakeGlTexture(OpenGL.GL_RGBA, Il.ilGetData(), width, height);
                        break;
                }
                // очищаем память 
                Il.ilDeleteImages(1, ref textureID);
            }
        }
        */
        // создание текстуры в OpenGL 
        public static uint MakeGlTexture(uint Format, IntPtr pixels, int w, int h)
        {
            // идентификатор текстурного объекта 
            uint[] texObject = new uint[1];

            // генерируем текстурный объект 
            Gl.glGenTextures(1, texObject);

            // устанавливаем режим упаковки пикселей 
            Gl.glPixelStoref(Gl.GL_UNPACK_ALIGNMENT, 1);

            // создаем привязку к только что созданной текстуре 
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject[0]);

            // устанавливаем режим фильтрации и повторения текстуры 
            Gl.glTexParameterf(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameterf(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameterf(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameterf(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);

            // создаем RGB или RGBA текстуру 
            switch (Format)
            {
                case Gl.GL_RGB:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

                case Gl.GL_RGBA:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;
            }
            // возвращаем идентификатор текстурного объекта 
            return texObject[0];
        }
        public static byte[] ToByte(Bitmap bmp)
        {
            int size = bmp.Height * bmp.Width * 4;
            byte[] pArray = new byte[size];

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int k = (j * bmp.Width + i) * 4;

                    pArray[k] = bmp.GetPixel(i, j).R;
                    pArray[k + 1] = bmp.GetPixel(i, j).G;
                    pArray[k + 2] = bmp.GetPixel(i, j).B;
                    pArray[k + 3] = bmp.GetPixel(i, j).A;
                }
            }

            return pArray;
        }
    }
}
