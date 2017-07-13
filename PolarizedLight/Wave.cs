using System;
using Tao.OpenGl;

namespace PolarizedLight
{
    class Wave
    {
        private int[] VBOPtr = new int[1];
        private int VertexCount;
        private float[] col = { 1.0f, 1.0f, 0.0f, 1.0f };

        public Wave()
        {
            float[] VBO = new float[101 * 3];
            VertexCount = 101;
            for(int i = 0; i<101; i++)
            {
                VBO[i * 3] = -10 + i * 0.2f;
                VBO[i * 3 + 1] = 0.0f;
                VBO[i * 3 + 2] = 2.0f * (float)Math.Cos(-10 + i * 0.2);
            }
            Gl.glGenBuffers(1, VBOPtr);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(VBO.Length * sizeof(float)), VBO, Gl.GL_STATIC_DRAW);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, 0);
            Gl.glLineWidth(2.0f);
        }

        public void render()
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glColor3f(1.0f, 1.0f, 0.0f);

            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);

            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);

            // Рисуем модель
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, VertexCount);

            // Отключаем режим отрисовки VBO
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
        }
    }
}
