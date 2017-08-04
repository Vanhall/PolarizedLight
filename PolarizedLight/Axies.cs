﻿using System;
using Tao.OpenGl;

namespace PolarizedLight
{
    class Axies
    {
        private int[] VBOPtr = new int[1];
        private float[] CrysTemplateVBO;
        public float scale = 0.1f;

        public Axies()
        {
            CrysTemplateVBO = new float[] {
                0.5f, 2.5f, 2.5f,   -0.5f, 2.5f, 2.5f,
                0.5f, -2.5f, 2.5f,  -0.5f, -2.5f, 2.5f,
                0.5f, 2.5f, -2.5f,  -0.5f, 2.5f, -2.5f,
                0.5f, -2.5f, -2.5f, -0.5f, -2.5f, -2.5f,

                0.5f, 2.5f, 2.5f,   0.5f, 2.5f, -2.5f,
                0.5f, -2.5f, 2.5f,  0.5f, -2.5f, -2.5f,
                -0.5f, 2.5f, 2.5f,  -0.5f, 2.5f, -2.5f,
                -0.5f, -2.5f, 2.5f, -0.5f, -2.5f, -2.5f,

                0.5f, 2.5f, 2.5f,   0.5f, -2.5f, 2.5f,
                0.5f, 2.5f, -2.5f,  0.5f, -2.5f, -2.5f,
                -0.5f, 2.5f, 2.5f,  -0.5f, -2.5f, 2.5f,
                -0.5f, 2.5f, -2.5f, -0.5f, -2.5f, -2.5f,
            };

            Gl.glGenBuffers(1, VBOPtr);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(CrysTemplateVBO.Length * sizeof(float)), CrysTemplateVBO, Gl.GL_STATIC_DRAW);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, 0);
        }

        public void SetScale(float new_scale)
        {
            scale = new_scale;
        }

        public void render()
        {
            Gl.glDisable(Gl.GL_LIGHTING);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            
            Gl.glColor3f(0.3f, 0.8f, 1.0f);
            Gl.glLineWidth(1.5f);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);

            Gl.glPushMatrix();
            Gl.glScalef(scale, 1.0f, 1.0f);
            Gl.glDrawArrays(Gl.GL_LINES, 0, 24);
            Gl.glPopMatrix();

            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
        }
    }
}
