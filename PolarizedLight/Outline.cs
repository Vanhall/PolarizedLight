using System;
using Tao.OpenGl;

namespace PolarizedLight
{
    class Outline
    {
        private int[] VBOPtr = new int[1];
        private float[] VBO;

        public Outline()
        {
            VBO = new float[] {
                2.0f, 2.5f, 2.5f,   -2.0f, 2.5f, 2.5f,
                2.0f, -2.5f, 2.5f,  -2.0f, -2.5f, 2.5f,
                2.0f, 2.5f, -2.5f,  -2.0f, 2.5f, -2.5f,
                2.0f, -2.5f, -2.5f, -2.0f, -2.5f, -2.5f,

                2.0f, 2.5f, 2.5f,   2.0f, 2.5f, -2.5f,
                2.0f, -2.5f, 2.5f,  2.0f, -2.5f, -2.5f,
                -2.0f, 2.5f, 2.5f,  -2.0f, 2.5f, -2.5f,
                -2.0f, -2.5f, 2.5f, -2.0f, -2.5f, -2.5f,

                2.0f, 2.5f, 2.5f,   2.0f, -2.5f, 2.5f,
                2.0f, 2.5f, -2.5f,  2.0f, -2.5f, -2.5f,
                -2.0f, 2.5f, 2.5f,  -2.0f, -2.5f, 2.5f,
                -2.0f, 2.5f, -2.5f, -2.0f, -2.5f, -2.5f,
            };

            Gl.glGenBuffers(1, VBOPtr);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(VBO.Length * sizeof(float)), VBO, Gl.GL_STATIC_DRAW);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, 0);
        }

        public void render()
        {
            Gl.glDisable(Gl.GL_LIGHTING);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);

            Gl.glColor3f(1.0f, 1.0f, 1.0f);
            Gl.glLineWidth(1.5f);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
            
            Gl.glDrawArrays(Gl.GL_LINES, 0, 24);

            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
        }
    }
}
