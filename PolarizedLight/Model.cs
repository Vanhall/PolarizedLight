using System;
//using System.Reflection;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using Tao.OpenGl;

namespace PolarizedLight
{
    class Model
    {
        // Указатели на буферы вершин и нормалей
        private int[] VBOPtr = new int[1];
        private int[] TexPtr = new int[1];
        private Bitmap bmp;
        private byte[] b;
        private int VertexCount = 0;    // счетчик вершин
        
        // Конструктор
        public Model(string name)
        {
            bmp = new Bitmap(name + ".bmp");
            b = ToByte(bmp);
            bmp.Dispose();

            #region парсер файлов .obj
            // Списки для записи информации из файла
            List<float> VertexCoords = new List<float>();
            List<float> NormalCoords = new List<float>();
            List<float> UVCoords = new List<float>();
            List<int> VIndices = new List<int>();
            List<int> NIndices = new List<int>();
            List<int> UVIndices = new List<int>();

            // Читаем файл
            //var assembly = Assembly.GetExecutingAssembly();
            //Stream stream = assembly.GetManifestResourceStream(name);
            StreamReader reader = new StreamReader(name + ".obj");

            string content;
            var fltFormat = System.Globalization.CultureInfo.InvariantCulture;

            while ((content = reader.ReadLine()) != null)
            {
                string[] line = content.Split(' ', '/');
                switch (line[0])
                {
                    case "v":
                        {
                            for (int j = 1; j < 4; j++)
                                VertexCoords.Add(float.Parse(line[j], fltFormat));
                        }
                        break;
                    case "vn":
                        {
                            for (int j = 1; j < 4; j++)
                                NormalCoords.Add(float.Parse(line[j], fltFormat));
                        }
                        break;
                    case "vt":
                        {
                            for (int j = 1; j < 3; j++)
                                UVCoords.Add(float.Parse(line[j], fltFormat));
                        }
                        break;
                    case "f":
                        {
                            for (int j = 1; j < 9; j += 3)
                            {
                                VIndices.Add(int.Parse(line[j]));
                                UVIndices.Add(int.Parse(line[j + 1]));
                                NIndices.Add(int.Parse(line[j + 2]));
                            }
                        }
                        break;
                }
            }

            VertexCount = VIndices.Count;
            // Выделяем массивы для вершин и нормалей
            float[] VBO = new float[VIndices.Count * 8];

            // Заполняем массивы
            for (int i = 0; i < VertexCount; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    VBO[i * 8 + j] = VertexCoords[(VIndices[i] - 1) * 3 + j];
                    VBO[i * 8 + j + 3] = NormalCoords[(NIndices[i] - 1) * 3 + j];
                }
                for (int j = 0; j < 2; j++)
                    VBO[i * 8 + j + 6] = UVCoords[(UVIndices[i] - 1) * 2 + j];
            }

            VertexCoords.Clear(); NormalCoords.Clear(); UVCoords.Clear();
            VIndices.Clear(); NIndices.Clear(); UVIndices.Clear();
            #endregion

            // Создаем VBO (Vertex Buffer Object)-------------------------------------
            Gl.glGenBuffers(1, VBOPtr);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(VBO.Length * sizeof(float)), VBO, Gl.GL_STATIC_DRAW);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, 0);

            Gl.glGenTextures(1, TexPtr);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, TexPtr[0]);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, 3, 512, 512, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, b);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);
        }

        // Функция отрисовки модели
        public void render()
        {
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, TexPtr[0]);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 8 * sizeof(float), IntPtr.Zero);
            Gl.glNormalPointer(Gl.GL_FLOAT, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glTexCoordPointer(2, Gl.GL_FLOAT, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));

            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnableClientState(Gl.GL_NORMAL_ARRAY);
            Gl.glEnableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
            
            // Рисуем модель
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, VertexCount);

            // Отключаем режим отрисовки VBO
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glDisableClientState(Gl.GL_NORMAL_ARRAY);
            Gl.glDisableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
        }

        public byte[] ToByte(Bitmap bmp)
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
