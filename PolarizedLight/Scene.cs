using Tao.OpenGl;
using Tao.Platform.Windows;

namespace PolarizedLight
{
    class Scene
    {
        private SimpleOpenGlControl GLVP;
        public Camera cam;
        private const double FOV = 50, zNear = 1.0, zFar = 200.0;

        private Model Table, Laser, ControlUnit, Supports, Connector, Platforms, Crystal, OSD;
        private Outline CrystalOutline;
        public Wave wave1, wave2, wave3;
        private float[] light0Pos = { 0.0f, 3.0f, 15.0f, 1.0f };
        private float[] light1Pos = { -15.8f, 0.0f, 0.0f, 1.0f };
        private float[] light1spotdir = { 1.0f, 0.0f, -0.15f };
        public bool ExpIsPaused = false;
        private bool _ExpIsRunning = false;
        public bool ExpIsRunning
        {
            get { return _ExpIsRunning; }
            set
            {
                if (value) Gl.glEnable(Gl.GL_LIGHT1);
                else Gl.glDisable(Gl.GL_LIGHT1);
                _ExpIsRunning = value;
            }
        }
        public bool DrawOSD = true, DrawCrystal = true;
        public float Scale = 1.0f;

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
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            
            Table = new Model("PolarizedLight.Models.Table");
            Table.SetShininess(100.0f);

            Laser = new Model("PolarizedLight.Models.Laser");
            Laser.SetShininess(30.0f);
            Laser.SetSpecular(new float[] { 0.45f, 0.45f, 0.45f, 1.0f });

            ControlUnit = new Model("PolarizedLight.Models.ControlUnit");
            ControlUnit.SetShininess(30.0f);
            ControlUnit.SetSpecular(new float[] { 0.45f, 0.45f, 0.45f, 1.0f });

            Supports = new Model("PolarizedLight.Models.Supports");
            Supports.SetShininess(50.0f);

            Connector = new Model("PolarizedLight.Models.Connector");
            Connector.SetShininess(30.0f);

            Platforms = new Model("PolarizedLight.Models.Platforms");
            Platforms.SetShininess(30.0f);

            Crystal = new Model("PolarizedLight.Models.Crystal");
            Crystal.UseAlpha = true;
            Crystal.SetShininess(60.0f);
            Crystal.SetDiffuse(new float[] { 1.0f, 1.0f, 1.0f, 0.6f });
            Crystal.SetEmission(new float[] { 0.2f, 0.2f, 0.2f, 1.0f });

            OSD = new Model("PolarizedLight.Models.OSD");
            OSD.UseAlpha = true;
            OSD.SetEmission(new float[] { 1.0f, 1.0f, 1.0f, 1.0f });

            CrystalOutline = new Outline();

            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light0Pos);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, new float[] { 0.3f, 0.3f, 0.3f, 1.0f });
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, new float[] { 0.45f, 0.45f, 0.45f, 1.0f });
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, new float[] { 0.5f, 0.5f, 0.5f, 1.0f });
            
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, light1Pos);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, new float[] { 1.0f, 0.0f, 1.0f, 1.0f });
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPECULAR, new float[] { 1.0f, 0.0f, 1.0f, 1.0f });
            Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_SPOT_CUTOFF, 80.0f);
            Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_SPOT_EXPONENT, 13.0f);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPOT_DIRECTION, light1spotdir);
            Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_QUADRATIC_ATTENUATION, 0.0015f);
        }
        
        public void UpdateColor(double Lambda)
        {
            float[] color = new float[4];
            float l = (float)(Lambda);

            // Red
            if (l >= 5.8f) color[0] = 1.0f;
            else if (l >= 5.4f) color[0] = 1.0f * (l - 5.4f) / 0.2f;
            else if (l >= 4.65f) color[0] = 0.0f;
            else color[0] = 0.65f - 0.65f * ((l - 3.8f) / 0.85f);

            // Green
            if (l >= 6.1f) color[1] = 0.5f - 0.5f * (l - 6.1f) / 1.7f;
            else if (l >= 5.8) color[1] = 1.0f - 0.5f * (l - 5.8f) / 0.3f;
            else if (l >= 4.95f) color[1] = 1.0f;
            else if (l >= 4.65f) color[1] = 1.0f * (l - 4.65f) / 0.3f;
            else color[1] = 0.0f;

            // Blue
            if (l >= 5.4f) color[2] = 0.0f;
            else if (l >= 4.95f) color[2] = 1.0f - 1.0f * (l - 4.95f) / 0.45f;
            else color[2] = 1.0f;

            // Alpha
            color[3] = 1.0f;

            wave1.Color = color;
            wave2.Color = color;
            wave3.Color = color;
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPECULAR, color);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, color);
        }

        public void SetCrystalColor(int ID)
        {
            switch(ID)
            {
                case 0:
                    {
                        Crystal.SetDiffuse(new float[] { 1.0f, 1.0f, 1.0f, 0.6f });
                        Crystal.SetEmission(new float[] { 0.2f, 0.2f, 0.2f, 1.0f });
                    } break;
                case 1:
                    {
                        Crystal.SetDiffuse(new float[] { 0.7f, 0.8f, 1.0f, 0.6f });
                        Crystal.SetEmission(new float[] { 0.0f, 0.1f, 0.2f, 1.0f });
                    }
                    break;
                case 2:
                    {
                        Crystal.SetDiffuse(new float[] { 0.8f, 0.8f, 0.8f, 0.6f });
                        Crystal.SetEmission(new float[] { 0.3f, 0.3f, 0.3f, 1.0f });
                    }
                    break;
                case 3:
                    {
                        Crystal.SetDiffuse(new float[] { 0.8f, 0.0f, 0.0f, 0.6f });
                        Crystal.SetEmission(new float[] { 0.2f, 0.0f, 0.0f, 1.0f });
                    }
                    break;
                case 4:
                    {
                        Crystal.SetDiffuse(new float[] { 0.4f, 1.0f, 1.0f, 0.6f });
                        Crystal.SetEmission(new float[] { 0.5f, 0.1f, 0.2f, 1.0f });
                    }
                    break;
            }
        }

        public void render()
        {
            // очищаем экран и z-буфер
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            // ставим свет (нужно вызывать каждый раз, иначе будет двигаться вместе с камерой)
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light0Pos);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, light1Pos);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPOT_DIRECTION, light1spotdir);

            if (ExpIsRunning)
            {
                wave1.render();
                wave2.render();
                wave3.render();
            }
            
            Table.render();
            Laser.render();
            ControlUnit.render();
            Supports.render();
            Connector.render();
            Platforms.render();
            if (DrawOSD) OSD.render();

            Gl.glPushMatrix();
            Gl.glScalef(Scale, 1.0f, 1.0f);
            if (DrawCrystal) Crystal.render();
            else CrystalOutline.render();
            Gl.glPopMatrix();

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
