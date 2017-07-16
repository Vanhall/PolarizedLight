using System;
using Tao.OpenGl;

namespace PolarizedLight
{
    class Axies
    {
        private int[] VBOPtr = new int[1];
        private float[] VBO;

        public Axies()
        {
            VBO = new float[] {
                0.0f, 0.0f, 0.0f,   1.0f, 0.0f, 0.0f,
                10.0f, 0.0f, 0.0f,   1.0f, 0.0f, 0.0f,

                0.0f, 0.0f, 0.0f,   0.0f, 1.0f, 0.0f,
                0.0f, 10.0f, 0.0f,   0.0f, 1.0f, 0.0f,

                0.0f, 0.0f, 0.0f,   0.0f, 0.0f, 1.0f,
                0.0f, 0.0f, 10.0f,   0.0f, 0.0f, 1.0f,

                0.0f, 0.0f, 0.0f,   0.5f, 0.0f, 0.0f,
                -10.0f, 0.0f, 0.0f,   0.5f, 0.0f, 0.0f,

                0.0f, 0.0f, 0.0f,   0.0f, 0.5f, 0.0f,
                0.0f, -10.0f, 0.0f,   0.0f, 0.5f, 0.0f,

                0.0f, 0.0f, 0.0f,   0.0f, 0.0f, 0.5f,
                0.0f, 0.0f, -10.0f,   0.0f, 0.0f, 0.5f,
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

            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnableClientState(Gl.GL_COLOR_ARRAY);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 6 * sizeof(float), IntPtr.Zero);
            Gl.glColorPointer(3, Gl.GL_FLOAT, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glDrawArrays(Gl.GL_LINES, 0, 12);
            
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glDisableClientState(Gl.GL_COLOR_ARRAY);

            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
        }
    }
}
