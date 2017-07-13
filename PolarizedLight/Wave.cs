using System;
using Tao.OpenGl;

namespace PolarizedLight
{
    class Wave
    {
        private int[] VBOPtr = new int[3];
        private int VertexCount;
        private double X0 = -15.0, Xf = 15.0;
        private double Lambda = 3.0, DeltaPhi = Math.PI/4.0, Step = 0.2;

        public Wave()
        {
            int steps = (int)((Xf - X0)/Step);
            float[] SumVBO = new float[steps * 3];
            float[] YVBO = new float[steps * 3];
            float[] ZVBO = new float[steps * 3];
            VertexCount = steps;
            for(int i = 0; i<steps; i++)
            {
                SumVBO[i * 3] = (float)(X0 + i  *Step);
                YVBO[i * 3] = (float)(X0 + i * Step);
                ZVBO[i * 3] = (float)(X0 + i * Step);

                SumVBO[i * 3 + 1] = 2.0f * (float)Math.Cos(X0 + i * Step);
                YVBO[i * 3 + 1] = 2.0f * (float)Math.Cos(X0 + i * Step);
                ZVBO[i * 3 + 1] = 0.0f;

                SumVBO[i * 3 + 2] = 2.0f * (float)Math.Cos(X0 + i * Step + DeltaPhi);
                YVBO[i * 3 + 2] = 0.0f;
                ZVBO[i * 3 + 2] = 2.0f * (float)Math.Cos(X0 + i * Step + DeltaPhi);
            }
            Gl.glGenBuffers(3, VBOPtr);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(SumVBO.Length * sizeof(float)), SumVBO, Gl.GL_STATIC_DRAW);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[1]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(YVBO.Length * sizeof(float)), YVBO, Gl.GL_STATIC_DRAW);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[2]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(ZVBO.Length * sizeof(float)), ZVBO, Gl.GL_STATIC_DRAW);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, 0);
            Gl.glLineWidth(2.0f);
        }

        public void render()
        {
            Gl.glDisable(Gl.GL_LIGHTING);
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);

            Gl.glColor3f(1.0f, 1.0f, 0.0f);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, VertexCount);

            Gl.glColor3f(1.0f, 0.0f, 1.0f);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[1]);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, VertexCount);

            Gl.glColor3f(0.0f, 1.0f, 1.0f);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[2]);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, VertexCount);

            // Отключаем режим отрисовки VBO
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
        }
    }
}
