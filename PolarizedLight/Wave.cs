using System;
using Tao.OpenGl;

namespace PolarizedLight
{
    class Wave
    {
        private int[] VBOPtr = new int[3];
        private int VertexCount;
        private double X0 = -15.0, Length = 30.0;
        private float Ey=2.0f, Ez=2.0f;
        private double Lambda = 3.0, DeltaPhi = Math.PI/2.0, Step = 0.2;
        private float[] SumVBO, YVBO, ZVBO;
        int steps;
        public double t = 0.0;

        public Wave()
        {
            steps = (int)(Length/Step);
            SumVBO = new float[steps * 3];
            YVBO = new float[steps * 3];
            ZVBO = new float[steps * 3];
            VertexCount = steps;
            Gl.glGenBuffers(3, VBOPtr);
            Gl.glLineWidth(2.0f);
        }

        public void render()
        {
            for (int i = 0; i < steps; i++)
            {
                int i_offset = i * 3;
                float X = (float)(X0 + i * Step);
                SumVBO[i_offset] = X;
                YVBO[i_offset] = X;
                ZVBO[i_offset] = X;

                float Y = (float)Math.Cos(X0 + i * Step + t);
                SumVBO[i_offset + 1] = Ey * Y;
                YVBO[i_offset + 1] = Ey * Y;
                ZVBO[i_offset + 1] = 0.0f;

                float Z = (float)Math.Cos(X0 + i * Step + DeltaPhi + t);
                SumVBO[i_offset + 2] = Ez * Z;
                YVBO[i_offset + 2] = 0.0f;
                ZVBO[i_offset + 2] = Ez * Z;
            }

            Gl.glDisable(Gl.GL_LIGHTING);
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);

            Gl.glColor3f(1.0f, 1.0f, 0.0f);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(SumVBO.Length * sizeof(float)), SumVBO, Gl.GL_DYNAMIC_DRAW);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, VertexCount);

            Gl.glColor3f(1.0f, 0.0f, 1.0f);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[1]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(YVBO.Length * sizeof(float)), YVBO, Gl.GL_DYNAMIC_DRAW);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, VertexCount);

            Gl.glColor3f(0.0f, 1.0f, 1.0f);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[2]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(ZVBO.Length * sizeof(float)), ZVBO, Gl.GL_DYNAMIC_DRAW);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, VertexCount);

            // Отключаем режим отрисовки VBO
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
        }
    }
}
