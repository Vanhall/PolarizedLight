using System;
using System.Windows.Forms;

namespace PolarizedLight
{
    public partial class MainForm : Form
    {
        Scene scene;
        public const float Pi = 3.1415926f;
        private int Lambda = 380;
        private float ny = 1.0f;
        private float nz = 1.0f;
        private int d = 1;
        private int Ey = 1;
        private int Ez = 1;
        private float DeltaPhase = 0.0f;

        public MainForm()
        {
            InitializeComponent();
            GLViewPort.InitializeContexts();
            GLViewPort.MouseWheel += new MouseEventHandler(GLViewPort_MouseWheel);

            Lambda_label.Text = Lambda.ToString("D") + " нм";
            nz_label.Text = nz.ToString("F3");
            ny_label.Text = ny.ToString("F3");
            Width_label.Text = d.ToString("D") + " мм";
            Ey_label.Text = Ey.ToString("D") + " В/м";
            Ez_label.Text = Ez.ToString("D") + " В/м";
            DeltaPhase_label.Text = DeltaPhase.ToString("F2") + "π";

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
            scene.r += 1.0f;
        }

        #region Кнопки
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            AnimTimer.Start();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            AnimTimer.Stop();
            scene.r = 0.0f;
            scene.render();
        }
        #endregion

        #region Обработка мыши
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
        #endregion

        private void MainForm_Resize(object sender, EventArgs e)
        {
            scene.resize();
        }

        private void CrystalChoice_dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CrystalChoice_dropdown.SelectedIndex)
            {
                case 0:
                    {

                    }
                    break;
                case 1:
                    {
                        ny = 2.417f;
                        nz = 2.417f;
                        nz_slider.Value = 2417;
                        ny_label.Text = ny.ToString("F3");
                        nz_slider.Value = 2417;
                        nz_label.Text = nz.ToString("F3");
                    }
                    break;
                case 2:
                    {
                        ny = 1.76f;
                        nz = 1.76f;
                        nz_slider.Value = 1760;
                        ny_label.Text = ny.ToString("F3");
                        nz_slider.Value = 1760;
                        nz_label.Text = nz.ToString("F3");
                    }
                    break;
                case 3:
                    {
                        ny = 1.63f;
                        nz = 1.63f;
                        nz_slider.Value = 1630;
                        ny_label.Text = ny.ToString("F3");
                        nz_slider.Value = 1630;
                        nz_label.Text = nz.ToString("F3");
                    }
                    break;
                case 4:
                    {
                        ny = 1.532f;
                        nz = 1.532f;
                        nz_slider.Value = 1532;
                        ny_label.Text = ny.ToString("F3");
                        nz_slider.Value = 1532;
                        nz_label.Text = nz.ToString("F3");
                    }
                    break;
            }
        }

        private void ny_slider_Scroll(object sender, EventArgs e)
        {
            ny = ny_slider.Value / 1000.0f;
            ny_label.Text = ny.ToString("F3");
            CrystalChoice_dropdown.SelectedIndex = 0;
        }

        private void nz_slider_Scroll(object sender, EventArgs e)
        {
            nz = nz_slider.Value / 1000.0f;
            nz_label.Text = nz.ToString("F3");
            CrystalChoice_dropdown.SelectedIndex = 0;
        }

        private void Width_slider_Scroll(object sender, EventArgs e)
        {
            d = Width_slider.Value;
            Width_label.Text = d.ToString("D") + " мм";
        }

        private void Ey_slider_Scroll(object sender, EventArgs e)
        {
            Ey = Ey_slider.Value;
            Ey_label.Text = Ey.ToString("D") + " В/м";
        }

        private void Ez_slider_Scroll(object sender, EventArgs e)
        {
            Ez = Ez_slider.Value;
            Ez_label.Text = Ez.ToString("D") + " В/м";
        }

        private void ExitMenuStripItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Lambda_slider_Scroll(object sender, EventArgs e)
        {
            Lambda = Lambda_slider.Value;
            Lambda_label.Text = Lambda.ToString("D") + " нм";
        }

        private void DeltaPhase_slider_Scroll(object sender, EventArgs e)
        {
            DeltaPhase = DeltaPhase_slider.Value / 100.0f;
            DeltaPhase_label.Text = DeltaPhase.ToString("F2") + "π";
            DeltaPhase *= Pi;
        }
    }
}
