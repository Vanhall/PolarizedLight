using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PolarizedLight
{
    public partial class MainForm : Form
    {
        private double Lambda = 3.8;
        private double ny = 1.0;
        private double nz = 1.0;
        private double d = 0.95;
        private float Ey = 1.5f;
        private float Ez = 1.5f;
        private double DeltaPhase = 0.0;
        private double c = 6.0;

        Scene scene;
        Stopwatch ExpTimer = new Stopwatch();
        private const double TotalLength = 30.0;

        public MainForm()
        {
            InitializeComponent();
            GLViewPort.InitializeContexts();
            GLViewPort.MouseWheel += new MouseEventHandler(GLViewPort_MouseWheel);
            scene = new Scene(GLViewPort);
            scene.Scale = 0.95f/ 4.0f;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            #region Инициализация интерфейса
            Lambda_label.Text = (Lambda * 100.0).ToString("F0") + " нм";
            nz_label.Text = nz.ToString("F3");
            ny_label.Text = ny.ToString("F3");
            Width_label.Text = (d * 100.0).ToString("F0") + " нм";
            Ey_label.Text = Ey.ToString("F1") + " В/м";
            Ez_label.Text = Ez.ToString("F1") + " В/м";
            DeltaPhase_label.Text = "0°";
            CrystalChoice_dropdown.SelectedIndex = 0;
            Speed_label.Text = "Средняя";
            #endregion
            scene.render();
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            scene.render();
            if (!scene.ExpIsPaused)
            {
                double time = ExpTimer.ElapsedMilliseconds / 1000.0;
                scene.wave1.t = time;
                scene.wave2.t = time;
                scene.wave3.t = time;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            scene.resize();
        }

        #region Кнопки
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            AnimTimer.Start();
            ExpTimer.Start();
            scene.wave1 = new Wave(Lambda, DeltaPhase, Ey, Ez, 1.0, 1.0, -15.0, (TotalLength - d) / 2.0, c);
            scene.wave2 = new Wave(scene.wave1, ny, nz, d);
            scene.wave3 = new Wave(scene.wave2, 1.0, 1.0, (TotalLength - d) / 2.0);

            #region Задание опций отрисовки
            if (DrawBoth_radio.Checked)
           {
               scene.wave1.Draw.OutLine = true;
               scene.wave2.Draw.OutLine = true;
               scene.wave3.Draw.OutLine = true;

               scene.wave1.Draw.Vectors = true;
               scene.wave2.Draw.Vectors = true;
               scene.wave3.Draw.Vectors = true;
           }
           if (DrawVec_radio.Checked)
           {
               scene.wave1.Draw.OutLine = false;
               scene.wave2.Draw.OutLine = false;
               scene.wave3.Draw.OutLine = false;

               scene.wave1.Draw.Vectors = true;
               scene.wave2.Draw.Vectors = true;
               scene.wave3.Draw.Vectors = true;
           }
           if (DrawOutline_radio.Checked)
           {
               scene.wave1.Draw.OutLine = true;
               scene.wave2.Draw.OutLine = true;
               scene.wave3.Draw.OutLine = true;

               scene.wave1.Draw.Vectors = false;
               scene.wave2.Draw.Vectors = false;
               scene.wave3.Draw.Vectors = false;
           }
           if (DrawSumm_chbox.Checked)
           {
               scene.wave1.Draw.Sum = true;
               scene.wave2.Draw.Sum = true;
               scene.wave3.Draw.Sum = true;
           }
           if (DrawY_chbox.Checked)
           {
               scene.wave1.Draw.Y = true;
               scene.wave2.Draw.Y = true;
               scene.wave3.Draw.Y = true;
           }
           if (DrawZ_chbox.Checked)
           {
               scene.wave1.Draw.Z = true;
               scene.wave2.Draw.Z = true;
               scene.wave3.Draw.Z = true;
           }
            #endregion

            scene.UpdateColor(Lambda);
            scene.ExpIsRunning = true;
            LockInterface();
        }
        private void ButtonPause_Click(object sender, EventArgs e)
        {
            if (scene.ExpIsPaused)
            {
                AnimTimer.Start();
                ExpTimer.Start();
                scene.ExpIsPaused = false;
                ButtonPause.Text = "ПАУЗА";
            }
            else
            {
                AnimTimer.Stop();
                ExpTimer.Stop();
                double time = ExpTimer.ElapsedMilliseconds / 1000.0;
                scene.wave1.t = time;
                scene.wave2.t = time;
                scene.wave3.t = time;
                scene.render();
                scene.ExpIsPaused = true;
                ButtonPause.Text = "ПРОД.";
            }
        }
        private void ButtonStop_Click(object sender, EventArgs e)
        {
            AnimTimer.Stop();
            scene.ExpIsRunning = false;
            scene.ExpIsPaused = false;
            scene.render();
            ExpTimer.Reset();
            //timer_label.Text = "0.00 с";
            ButtonPause.Text = "ПАУЗА";
            UnlockInterface();
        }
        #endregion

        #region Обработка мыши
        private void GLViewPort_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Middle))
            {
                scene.cam.reset();
                if (!scene.ExpIsRunning || scene.ExpIsPaused) scene.render();
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
                    if (!scene.ExpIsRunning || scene.ExpIsPaused) scene.render();
                }
                // Если правая - перемещаем вдоль Z
                else if (e.Button.Equals(MouseButtons.Right))
                {
                    if (scene.cam.phi > 180.0)
                        scene.cam.translate(scene.cam.shift - (e.X - scene.cam.mouseDX) / 6.0);
                    else
                        scene.cam.translate(scene.cam.shift + (e.X - scene.cam.mouseDX) / 6.0);
                    if (!scene.ExpIsRunning || scene.ExpIsPaused) scene.render();
                }
            }
            scene.cam.mouseDX = e.X;
            scene.cam.mouseDY = e.Y;
        }

        private void GLViewPort_MouseWheel(object sender, MouseEventArgs e)
        {
            scene.cam.zoom(scene.cam.R - e.Delta / 120);
            if (!scene.ExpIsRunning || scene.ExpIsPaused) scene.render();
        }
        #endregion
        
        #region Вкладка "Кристалл"
        private void CrystalChoice_dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CrystalChoice_dropdown.SelectedIndex)
            {
                case 0:
                    {
                        scene.SetCrystalColor(0);
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
                        scene.SetCrystalColor(1);
                        if (scene.ExpIsRunning)
                        {
                            scene.wave2.ny_update(ny);
                            scene.wave2.nz_update(nz);
                            scene.wave2.Phases_update(scene.wave1);
                            scene.wave3.Phases_update(scene.wave2);
                        }
                        scene.render();
                    }
                    break;
                case 2:
                    {
                        ny = 1.658;
                        nz = 1.486;
                        ny_slider.Value = 1685;
                        ny_label.Text = ny.ToString("F3");
                        nz_slider.Value = 1486;
                        nz_label.Text = nz.ToString("F3");
                        scene.SetCrystalColor(2);
                        if (scene.ExpIsRunning)
                        {
                            scene.wave2.ny_update(ny);
                            scene.wave2.nz_update(nz);
                            scene.wave2.Phases_update(scene.wave1);
                            scene.wave3.Phases_update(scene.wave2);
                        }
                        scene.render();
                    }
                    break;
                case 3:
                    {
                        ny = 1.770;
                        nz = 1.762;
                        ny_slider.Value = 1770;
                        ny_label.Text = ny.ToString("F3");
                        nz_slider.Value = 1762;
                        nz_label.Text = nz.ToString("F3");
                        scene.SetCrystalColor(3);
                        if (scene.ExpIsRunning)
                        {
                            scene.wave2.ny_update(ny);
                            scene.wave2.nz_update(nz);
                            scene.wave2.Phases_update(scene.wave1);
                            scene.wave3.Phases_update(scene.wave2);
                        }
                        scene.render();
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
                        scene.SetCrystalColor(4);
                        if (scene.ExpIsRunning)
                        {
                            scene.wave2.ny_update(ny);
                            scene.wave2.nz_update(nz);
                            scene.wave2.Phases_update(scene.wave1);
                            scene.wave3.Phases_update(scene.wave2);
                        }
                        scene.render();
                    }
                    break;
            }
        }

        private void ny_slider_Scroll(object sender, EventArgs e)
        {
            ny = ny_slider.Value / 1000.0;
            ny_label.Text = ny.ToString("F3");
            CrystalChoice_dropdown.SelectedIndex = 0;
            if (scene.ExpIsRunning)
            {
                scene.wave2.ny_update(ny);
                scene.wave2.Phases_update(scene.wave1);
                scene.wave3.Phases_update(scene.wave2);
            }
            scene.render();
        }

        private void nz_slider_Scroll(object sender, EventArgs e)
        {
            nz = nz_slider.Value / 1000.0;
            nz_label.Text = nz.ToString("F3");
            CrystalChoice_dropdown.SelectedIndex = 0;
            if (scene.ExpIsRunning)
            {
                scene.wave2.nz_update(nz);
                scene.wave2.Phases_update(scene.wave1);
                scene.wave3.Phases_update(scene.wave2);
            }
            scene.render();
        }

        private void Width_slider_Scroll(object sender, EventArgs e)
        {
            d = 0.95 + Width_slider.Value * 0.05;
            Width_label.Text = (d * 100.0).ToString("F0") + " нм";
            scene.Scale = (float)d / 4.0f;
            scene.render();
        }
        #endregion

        #region Вкладка "Источник света"
        private void Ey_slider_Scroll(object sender, EventArgs e)
        {
            Ey = Ey_slider.Value/10.0f;
            Ey_label.Text = Ey.ToString("F1") + " В/м";

            if (scene.ExpIsRunning)
            {
                scene.wave1.Ey_update(Ey);
                scene.wave2.Ey_update(Ey);
                scene.wave3.Ey_update(Ey);
            }
            scene.render();
        }

        private void Ez_slider_Scroll(object sender, EventArgs e)
        {
            Ez = Ez_slider.Value/10.0f;
            Ez_label.Text = Ez.ToString("F1") + " В/м";
            if (scene.ExpIsRunning)
            {
                scene.wave1.Ez_update(Ez);
                scene.wave2.Ez_update(Ez);
                scene.wave3.Ez_update(Ez);
            }
            scene.render();
        }

        private void Lambda_slider_MouseDown(object sender, MouseEventArgs e)
        {
            if (scene.ExpIsRunning)
            {
                ExpTimer.Stop();
                double time = ExpTimer.ElapsedMilliseconds / 1000.0;
                scene.wave1.t = time;
                scene.wave1.FixCurrentPhase();
                scene.wave2.t = time;
                scene.wave3.t = time;
            }
        }

        private void Lambda_slider_MouseUp(object sender, MouseEventArgs e)
        {
            if (!scene.ExpIsPaused)
                ExpTimer.Start();
        }

        private void Lambda_slider_Scroll(object sender, EventArgs e)
        {
            Lambda = 3.8 + Lambda_slider.Value * 0.05;
            Lambda_label.Text = (Lambda * 100.0).ToString("F0") + " нм";
            if (scene.ExpIsRunning)
            {
                scene.wave1.Lambda_update(Lambda);
                scene.wave2.Lambda_update(scene.wave1);
                scene.wave3.Lambda_update(scene.wave2);
                scene.UpdateColor(Lambda);
            }
            scene.render();
        }

        private void DeltaPhase_slider_MouseDown(object sender, MouseEventArgs e)
        {
            if (scene.ExpIsRunning)
            {
                ExpTimer.Stop();
                double time = ExpTimer.ElapsedMilliseconds / 1000.0;
                scene.wave1.t = time;
                scene.wave2.t = time;
                scene.wave3.t = time;
            }
        }

        private void DeltaPhase_slider_MouseUp(object sender, MouseEventArgs e)
        {
            if (!scene.ExpIsPaused)
                ExpTimer.Start();
        }
        private void DeltaPhase_slider_Scroll(object sender, EventArgs e)
        {
            DeltaPhase = DeltaPhase_slider.Value * 5.0;
            DeltaPhase_label.Text = DeltaPhase.ToString("F0") + "°";
            DeltaPhase *= Math.PI / 180.0;

            if (scene.ExpIsRunning)
            {
                scene.wave1.Phases_update(DeltaPhase);
                scene.wave2.Phases_update(scene.wave1);
                scene.wave3.Phases_update(scene.wave2);
            }
            scene.render();
        }
        #endregion

        #region Вкладка "Отображение"
        private void DrawSumm_chbox_CheckedChanged(object sender, EventArgs e)
        {
            if (scene.ExpIsRunning)
            {
                scene.wave1.Draw.Sum = DrawSumm_chbox.Checked;
                scene.wave2.Draw.Sum = DrawSumm_chbox.Checked;
                scene.wave3.Draw.Sum = DrawSumm_chbox.Checked;

                scene.render();
            }
        }

        private void DrawZ_chbox_CheckedChanged(object sender, EventArgs e)
        {
            if (scene.ExpIsRunning)
            {
                scene.wave1.Draw.Z = DrawZ_chbox.Checked;
                scene.wave2.Draw.Z = DrawZ_chbox.Checked;
                scene.wave3.Draw.Z = DrawZ_chbox.Checked;

                scene.render();
            }
        }

        private void DrawY_chbox_CheckedChanged(object sender, EventArgs e)
        {
            if (scene.ExpIsRunning)
            {
                scene.wave1.Draw.Y = DrawY_chbox.Checked;
                scene.wave2.Draw.Y = DrawY_chbox.Checked;
                scene.wave3.Draw.Y = DrawY_chbox.Checked;

                scene.render();
            }
        }

        private void DrawBoth_radio_CheckedChanged(object sender, EventArgs e)
        {
            if (DrawBoth_radio.Checked && scene.ExpIsRunning)
            {
                scene.wave1.Draw.OutLine = true;
                scene.wave2.Draw.OutLine = true;
                scene.wave3.Draw.OutLine = true;

                scene.wave1.Draw.Vectors = true;
                scene.wave2.Draw.Vectors = true;
                scene.wave3.Draw.Vectors = true;

                scene.render();
            }
        }

        private void DrawOutline_radio_CheckedChanged(object sender, EventArgs e)
        {
            if (DrawOutline_radio.Checked && scene.ExpIsRunning)
            {
                scene.wave1.Draw.OutLine = true;
                scene.wave2.Draw.OutLine = true;
                scene.wave3.Draw.OutLine = true;

                scene.wave1.Draw.Vectors = false;
                scene.wave2.Draw.Vectors = false;
                scene.wave3.Draw.Vectors = false;

                scene.render();
            }
        }

        private void DrawVec_radio_CheckedChanged(object sender, EventArgs e)
        {
            if (DrawVec_radio.Checked && scene.ExpIsRunning)
            {
                scene.wave1.Draw.OutLine = false;
                scene.wave2.Draw.OutLine = false;
                scene.wave3.Draw.OutLine = false;

                scene.wave1.Draw.Vectors = true;
                scene.wave2.Draw.Vectors = true;
                scene.wave3.Draw.Vectors = true;

                scene.render();
            }
        }


        private void Speed_slider_Scroll(object sender, EventArgs e)
        {
            switch(Speed_slider.Value)
            {
                case 0:
                    {
                        c = 3.0;
                        Speed_label.Text = "Низкая";
                        if (scene.ExpIsRunning)
                        {
                            scene.wave1.c_update(c);
                            scene.wave2.c_update(scene.wave1);
                            scene.wave3.c_update(scene.wave2);
                        }
                    } break;
                case 1:
                    {
                        c = 6.0;
                        Speed_label.Text = "Средняя";
                        if (scene.ExpIsRunning)
                        {
                            scene.wave1.c_update(c);
                            scene.wave2.c_update(scene.wave1);
                            scene.wave3.c_update(scene.wave2);
                        }
                    }
                    break;
                case 2:
                    {
                        c = 9.0;
                        Speed_label.Text = "Высокая";
                        if (scene.ExpIsRunning)
                        {
                            scene.wave1.c_update(c);
                            scene.wave2.c_update(scene.wave1);
                            scene.wave3.c_update(scene.wave2);
                        }
                    }
                    break;
            }
        }

        private void DrawAxies_chbox_CheckedChanged(object sender, EventArgs e)
        {
            if (DrawAxies_chbox.Checked)
            {
                scene.DrawOSD = false;
                if (!scene.ExpIsRunning || scene.ExpIsPaused) scene.render();
            }
            else
            {
                scene.DrawOSD = true;
                if (!scene.ExpIsRunning || scene.ExpIsPaused) scene.render();
            }
        }

        private void DrawCrystal_chbox_CheckedChanged(object sender, EventArgs e)
        {
            if (DrawCrystal_chbox.Checked)
            {
                scene.DrawCrystal = false;
                if (!scene.ExpIsRunning || scene.ExpIsPaused) scene.render();
            }
            else
            {
                scene.DrawCrystal = true;
                if (!scene.ExpIsRunning || scene.ExpIsPaused) scene.render();
            }
        }
        #endregion

        #region Меню
        private void ExitMenuStripItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion
        
        private void LockInterface()
        {
            ButtonStart.Enabled = false;
            ButtonPause.Enabled = true;
            ButtonStop.Enabled = true;

            Width_slider.Enabled = false;
        }

        private void UnlockInterface()
        {
            ButtonStart.Enabled = true;
            ButtonPause.Enabled = false;
            ButtonStop.Enabled = false;

            Width_slider.Enabled = true;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        
    }
}
