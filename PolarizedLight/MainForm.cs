using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PolarizedLight
{
    public partial class MainForm : Form
    {
        Scene scene;
        public const float Pi = 3.1415926f;
        private double Lambda = 380.0;
        private double ny = 1.0;
        private double nz = 1.0;
        private double d = 1.0;
        private float Ey = 0.5f;
        private float Ez = 0.5f;
        private double DeltaPhase = 0.0;
        Stopwatch Timer = new Stopwatch();

        public MainForm()
        {
            InitializeComponent();
            GLViewPort.InitializeContexts();
            GLViewPort.MouseWheel += new MouseEventHandler(GLViewPort_MouseWheel);
            scene = new Scene(GLViewPort);

            #region Инициализация интерфейса
            Lambda_label.Text = Lambda.ToString("F0") + " нм";
            nz_label.Text = nz.ToString("F3");
            ny_label.Text = ny.ToString("F3");
            Width_label.Text = d.ToString("F0") + " мм";
            Ey_label.Text = Ey.ToString("F2") + " В/м";
            Ez_label.Text = Ez.ToString("F2") + " В/м";
            DeltaPhase_label.Text = DeltaPhase.ToString("F2") + "π";
            CrystalChoice_dropdown.SelectedIndex = 0;
            #endregion
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            scene.render();
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            scene.render();
            scene.r += 1.0f;
            double time = Timer.ElapsedMilliseconds / 1000.0;
            scene.wave1.t = time;
            scene.wave2.t = time;
            scene.wave3.t = time;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            scene.resize();
        }

        #region Кнопки
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            AnimTimer.Start();
            Timer.Start();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            AnimTimer.Stop();
            scene.r = 0.0f;
            scene.render();
            Timer.Stop();
            Timer.Reset();
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
        
        #region Вкладка "Кристалл"
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
                        ny = 1.6776;
                        nz = 1.5534;
                        ny_slider.Value = 1678;
                        ny_label.Text = ny.ToString("F3");
                        nz_slider.Value = 1553;
                        nz_label.Text = nz.ToString("F3");
                    }
                    break;
                case 2:
                    {
                        ny = 1.770;
                        nz = 1.762;
                        ny_slider.Value = 1770;
                        ny_label.Text = ny.ToString("F3");
                        nz_slider.Value = 1762;
                        nz_label.Text = nz.ToString("F3");
                    }
                    break;
                case 3:
                    {
                        ny = 1.768;
                        nz = 1.760;
                        ny_slider.Value = 1768;
                        ny_label.Text = ny.ToString("F3");
                        nz_slider.Value = 1760;
                        nz_label.Text = nz.ToString("F3");
                    }
                    break;
                case 4:
                    {
                        ny = 1.669;
                        nz = 1.638;
                        ny_slider.Value = 1669;
                        ny_label.Text = ny.ToString("F3");
                        nz_slider.Value = 1638;
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
            scene.wave2.ny = ny;
            scene.wave2.link(scene.wave1);
            scene.wave3.link(scene.wave2);
            scene.render();
        }

        private void nz_slider_Scroll(object sender, EventArgs e)
        {
            nz = nz_slider.Value / 1000.0f;
            nz_label.Text = nz.ToString("F3");
            CrystalChoice_dropdown.SelectedIndex = 0;
            scene.wave2.nz = nz;
            scene.wave2.link(scene.wave1);
            scene.wave3.link(scene.wave2);
            scene.render();
        }

        private void Width_slider_Scroll(object sender, EventArgs e)
        {
            d = Width_slider.Value;
            Width_label.Text = d.ToString("F0") + " мм";
        }
        #endregion

        #region Вкладка"Источник света"
        private void Ey_slider_Scroll(object sender, EventArgs e)
        {
            Ey = Ey_slider.Value/100.0f;
            Ey_label.Text = Ey.ToString("F2") + " В/м";
            scene.wave1.Ey = Ey;
            scene.wave2.Ey = Ey;
            scene.wave3.Ey = Ey;
            scene.render();
        }

        private void Ez_slider_Scroll(object sender, EventArgs e)
        {
            Ez = Ez_slider.Value/100.0f;
            Ez_label.Text = Ez.ToString("F2") + " В/м";
            scene.wave1.Ez = Ez;
            scene.wave2.Ez = Ez;
            scene.wave3.Ez = Ez;
            scene.render();
        }

        private void Lambda_slider_Scroll(object sender, EventArgs e)
        {
            Lambda = Lambda_slider.Value;
            Lambda_label.Text = Lambda.ToString("F0") + " нм";
        }

        private void DeltaPhase_slider_Scroll(object sender, EventArgs e)
        {
            DeltaPhase = DeltaPhase_slider.Value / 100.0;
            DeltaPhase_label.Text = DeltaPhase.ToString("F2") + "π";
            DeltaPhase *= Pi;
            scene.wave1.DPhi = DeltaPhase;
            scene.wave2.link(scene.wave1);
            scene.wave3.link(scene.wave2);
            scene.render();
        }
        #endregion

        #region Меню
        private void ExitMenuStripItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        private void GLViewPort_Load(object sender, EventArgs e)
        {

        }
    }
}
