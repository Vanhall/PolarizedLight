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

        private Model Table, Laser, ControlUnit, Support, Screen;
        public Wave wave1, wave2, wave3;
        public Axies axies;
        private readonly float[] light0Pos = { 0.0f, 5.0f, 10.0f, 0.0f };
        public bool ExpIsRunning = false, ExpIsPaused = false;

        public Scene(SimpleOpenGlControl _GLVP)
        {
            GLVP = _GLVP;
            Gl.glViewport(0, 0, GLVP.Width, GLVP.Height);
            Gl.glClearColor(0.7f, 0.7f, 0.8f, 1.0f);

            cam = new Camera();
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
            Gl.glLightModeli(Gl.GL_LIGHT_MODEL_COLOR_CONTROL, Gl.GL_SEPARATE_SPECULAR_COLOR);

            axies = new Axies();
            Table = new Model("Models/Table");
            Laser = new Model("Models/Laser");
            ControlUnit = new Model("Models/ControlUnit");
            Support = new Model("Models/Support");
            Screen = new Model("Models/Screen");

            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light0Pos);
            float[] light0Amb = new float[] { 0.3f, 0.3f, 0.3f, 1.0f };
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, light0Amb);
            float[] light0Dif = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, light0Dif);
            float[] light0Spec = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, light0Spec);
        }

        public void render()
        {
            // очищаем экран и z-буфер
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            // ставим свет (нужно вызывать каждый раз, иначе будет двигаться вместе с камерой)
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light0Pos);

            if (ExpIsRunning)
            {
                wave1.render();
                wave2.render();
                wave3.render();
            }

            axies.render();
            
            Table.render();
            Laser.render();
            ControlUnit.render();
            Support.render();
            Screen.render();
            
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
