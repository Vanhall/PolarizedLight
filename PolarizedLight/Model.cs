using System;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
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
        private int VertexCount = 0;    // кол-во вершин

        // Свойства материала
        private float[] Diffuse;
        private float[] Specular;
        private float Shininess;
        private float[] Emission;

        // Использовать прозрачность?
        public bool UseAlpha = false;

        #region Конструктор
        public Model(string name)
        {
            Diffuse = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
            Specular = new float[] { 0.7f, 0.7f, 0.7f, 1.0f };
            Shininess = 80.0f;
            Emission = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
            
            #region Парсер OBJ файлов

            // Массив атрибутов вершин:
            // |          Vertex 1         |    Vertex 2    | ...
            // |X|Y|Z|normX|normY|normZ|U|V|      ...       | ...
            float[] VBO;

            // Списки для чтения атрибутов вершин из файла
            List<float> VertexCoords = new List<float>();   // координаты
            List<float> NormalCoords = new List<float>();   // нормали
            List<float> UVCoords = new List<float>();       // текстурные координаты
            List<int> VIndices = new List<int>();           // индексы координат
            List<int> NIndices = new List<int>();           // индексы нормалей
            List<int> UVIndices = new List<int>();          // индексы текс. координат

            // Читаем файл
            var assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(name + ".obj");
            StreamReader reader = new StreamReader(stream);

            string content;
            var fltFormat = System.Globalization.CultureInfo.InvariantCulture;

            // Читаем файл построчно
            while ((content = reader.ReadLine()) != null)
            {
                string[] line = content.Split(' ', '/');    // разделяем строку
                // какой атрибут содержала строка?
                switch (line[0])
                {
                    case "v":
                        // координаты вершины
                        for (int j = 1; j < 4; j++)
                            VertexCoords.Add(float.Parse(line[j], fltFormat));
                        break;
                    case "vn":
                        // координаты нормали
                        for (int j = 1; j < 4; j++)
                            NormalCoords.Add(float.Parse(line[j], fltFormat));
                        break;
                    case "vt":
                        // текстурные координаты
                        for (int j = 1; j < 3; j++)
                            UVCoords.Add(float.Parse(line[j], fltFormat));
                        break;
                    case "f":
                        // поверхность (полигон)
                        // три группы по три числа вида A1/B1/C1 A2/B2/C2 A3/B3/C3
                        // где каждая группа - одна вершина полигона (в данном случае треугольника)
                        // а Ai, Bi, Ci - индекс атрибута вершины в соответствующем массиве
                        // A - координаты, B - нормали, C - текстурные координаты
                        for (int j = 1; j < 9; j += 3)
                        {
                            VIndices.Add(int.Parse(line[j]));
                            UVIndices.Add(int.Parse(line[j + 1]));
                            NIndices.Add(int.Parse(line[j + 2]));
                        }
                        break;
                }
            }

            VertexCount = VIndices.Count;
            VBO = new float[VertexCount * 8];

            // Пакуем все атрибуты вершин в один массив
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

            // Очищаем списки
            VertexCoords.Clear(); NormalCoords.Clear(); UVCoords.Clear();
            VIndices.Clear(); NIndices.Clear(); UVIndices.Clear();
            #endregion

            // Создаем VBO (Vertex Buffer Object)
            Gl.glGenBuffers(1, VBOPtr);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(VBO.Length * sizeof(float)), VBO, Gl.GL_STATIC_DRAW);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, 0);

            // Грузим текстуру
            stream = assembly.GetManifestResourceStream(name + ".png");
            Bitmap Texture = new Bitmap(stream);
            Texture.RotateFlip(RotateFlipType.RotateNoneFlipY);
            var TexData = Texture.LockBits(new Rectangle(0, 0, Texture.Width, Texture.Height),
                    ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);

            Gl.glGenTextures(1, TexPtr);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, TexPtr[0]);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, Texture.Width, Texture.Height, 0, Gl.GL_BGRA, Gl.GL_UNSIGNED_BYTE, TexData.Scan0);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);

            Texture.UnlockBits(TexData);
            Texture.Dispose();
        }
        #endregion
        
        #region Задание свойств материала
        public void SetDiffuse(float[] rgba)
        {
            if (rgba.Length == 4) rgba.CopyTo(Diffuse, 0);
        }

        public void SetSpecular(float[] rgba)
        {
            if (rgba.Length == 4) rgba.CopyTo(Specular, 0);
        }

        public void SetShininess(float s)
        {
            Shininess = s;
        }

        public void SetEmission(float[] rgba)
        {
            if (rgba.Length == 4) rgba.CopyTo(Emission, 0);
        }
        #endregion

        #region Функция отрисовки модели
        public void render()
        {
            // задаем материал
            if (UseAlpha) Gl.glEnable(Gl.GL_BLEND);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, Diffuse);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, Specular);
            Gl.glMaterialf(Gl.GL_FRONT, Gl.GL_SHININESS, Shininess);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_EMISSION, Emission);

            // подключаем VBO и текстуру
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, TexPtr[0]);

            // настраиваем указатели на атрибуты
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 8 * sizeof(float), IntPtr.Zero);
            Gl.glNormalPointer(Gl.GL_FLOAT, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glTexCoordPointer(2, Gl.GL_FLOAT, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));

            // включаем нужные параметры отрисовки VBO
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnableClientState(Gl.GL_NORMAL_ARRAY);
            Gl.glEnableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
            
            // Рисуем модель
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, VertexCount);

            // Отключаем параметры отрисовки VBO
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glDisableClientState(Gl.GL_NORMAL_ARRAY);
            Gl.glDisableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
            if (UseAlpha) Gl.glDisable(Gl.GL_BLEND);
        }
        #endregion
    }
}
