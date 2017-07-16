using Tao.OpenGl;
using System;
using Tao.Platform.Windows;

namespace PolarizedLight
{
    class Scene
    {
        private SimpleOpenGlControl GLVP;
        public Camera cam;
        private const double FOV = 45, zNear = 0.1, zFar = 200;

        private Model testModel, testModel2;
        public Wave wave1, wave2, wave3;
        private Axies axies;
        private readonly float[] light0Pos = { 5.0f, 10.0f, 20.0f, 0.0f };
        public float r = 0.0f;

        public Scene(SimpleOpenGlControl _GLVP)
        {
            GLVP = _GLVP;
            Gl.glViewport(0, 0, GLVP.Width, GLVP.Height);
            Gl.glClearColor(0.7f, 0.7f, 0.8f, 1.0f);

            cam = new Camera(GLVP);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(FOV, (double)GLVP.Width / (double)GLVP.Height, zNear, zFar);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_NORMALIZE);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glEnable(Gl.GL_CULL_FACE);
            Gl.glEnable(Gl.GL_TEXTURE_2D);

            axies = new Axies();
            testModel = new Model("Models/testcube");
            testModel2 = new Model("Models/testcyl");
            wave1 = new Wave(7.8, 0.0, 2.0f, 2.0f, 1.0, 1.0, -15.0, 10.0);
            wave2 = new Wave(wave1, 1.658, 1.486, 10.0);
            wave3 = new Wave(wave2, 1.0, 1.0, 10.0);
            
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light0Pos);
            float[] light0Amb = new float[] { 0.3f, 0.3f, 0.3f, 1.0f };
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, light0Amb);
            float[] light0Dif = new float[] { 1.0f, 1.0f, 1.0f, 0.5f };
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, light0Dif);
            float[] light0Spec = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, light0Spec);


        }

        public void render()
        {
            // очищаем экран и z-буфер
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            // ставим свет (нужно вызывать каждый раз, иначе будет двигаться вместе с камерой)
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light0Pos);


            wave1.render();
            wave2.render();
            wave3.render();

            axies.render();

            //Gl.glPushMatrix();
            //Gl.glRotatef(r, 0.0f, 0.0f, 1.0f);
            //testModel.render();
            //testModel2.render();
            //Gl.glPopMatrix();
            
            // сообщаем OpenGL что закончили все дела и можно рисовать кадр
            Gl.glFlush();
            GLVP.Invalidate();
        }

        public void resize()
        {
            Gl.glViewport(0, 0, GLVP.Width, GLVP.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(FOV, (double)GLVP.Width / (double)GLVP.Height, zNear, zFar);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            render();
        }
    }
}
