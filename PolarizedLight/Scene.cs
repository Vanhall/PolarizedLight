using Tao.OpenGl;
using System;
using Tao.Platform.Windows;

namespace PolarizedLight
{
    class Scene
    {
        private SimpleOpenGlControl GLVP;
        public Camera cam;
        private const double FOV = 50, zNear = 1.0, zFar = 200.0;

        private Model Table, Laser, ControlUnit, Supports, Connector, Platforms;
        private Crystal Crystal_model;
        public Wave wave1, wave2, wave3;
        public Axies axies;
        private float[] light0Pos = { 0.0f, 3.0f, 15.0f, 1.0f };
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
            Supports = new Model("Models/Supports");
            Connector = new Model("Models/Connector");
            Platforms = new Model("Models/Platforms");
            Crystal_model = new Crystal("Models/Crystal");

            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light0Pos);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, new float[] { 0.25f, 0.25f, 0.25f, 1.0f });
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, new float[] { 0.5f, 0.5f, 0.5f, 1.0f });
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
            Supports.render();
            Connector.render();
            Platforms.render();

            Crystal_model.render();

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
