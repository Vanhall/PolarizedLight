using System;
using System.Windows.Forms;

namespace PolarizedLight
{
    public partial class MainForm : Form
    {
        Scene scene;

        public MainForm()
        {
            InitializeComponent();
            GLViewPort.InitializeContexts();
            GLViewPort.MouseWheel += new MouseEventHandler(GLViewPort_MouseWheel);

            CrystalChoice_dropdown.SelectedIndex = 0;
            scene = new Scene(GLViewPort);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            scene.render();
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            scene.render();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            AnimTimer.Start();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            AnimTimer.Stop();
            scene.render();
        }

        private void GLViewPort_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Middle))
            {
                scene.cam.reset();
                scene.render();
            }
            else scene.cam.moving = true;
        }

        private void GLViewPort_MouseUp(object sender, MouseEventArgs e)
        {
            scene.cam.moving = false;
            scene.cam.mouseDX = e.X;
            scene.cam.mouseDY = e.Y;
        }

        private void GLViewPort_MouseMove(object sender, MouseEventArgs e)
        {
            if (scene.cam.moving)
            {
                // Если левая кнопка - вращаем камеру
                if (e.Button.Equals(MouseButtons.Left))
                {
                    scene.cam.rotate(
                            scene.cam.phi - (e.X - scene.cam.mouseDX) / 3.0,
                            scene.cam.psi - (e.Y - scene.cam.mouseDY) / 3.0);
                    scene.render();
                }
                // Если правая - перемещаем вдоль Z
                else if (e.Button.Equals(MouseButtons.Right))
                {
                    scene.cam.translate(scene.cam.height + (e.Y - scene.cam.mouseDY) / 6.0);
                    scene.render();
                }
            }
            scene.cam.mouseDX = e.X;
            scene.cam.mouseDY = e.Y;
        }

        private void GLViewPort_MouseWheel(object sender, MouseEventArgs e)
        {
            scene.cam.zoom(scene.cam.R - e.Delta / 120);
            scene.render();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            scene.resize();
        }
    }
}
