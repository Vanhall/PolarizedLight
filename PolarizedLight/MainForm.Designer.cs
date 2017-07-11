namespace PolarizedLight
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainPanel = new System.Windows.Forms.Panel();
            this.Credits_panel = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label20 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.ExpSetupTabs = new System.Windows.Forms.TabControl();
            this.CrystalTab = new System.Windows.Forms.TabPage();
            this.Width_slider = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ny_slider = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.nx_slider = new System.Windows.Forms.TrackBar();
            this.CrystalChoice_dropdown = new System.Windows.Forms.ComboBox();
            this.LightTab = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.DeltaPhase_slider = new System.Windows.Forms.TrackBar();
            this.WaveLen_slider = new System.Windows.Forms.TrackBar();
            this.label9 = new System.Windows.Forms.Label();
            this.Ey_slider = new System.Windows.Forms.TrackBar();
            this.Ex_slider = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.DisplayTab = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.DrawAxies_chbox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DrawY_chbox = new System.Windows.Forms.CheckBox();
            this.DrawX_chbox = new System.Windows.Forms.CheckBox();
            this.DrawSumm_chbox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DrawVec_radio = new System.Windows.Forms.RadioButton();
            this.DrawOutline_radio = new System.Windows.Forms.RadioButton();
            this.DrawBoth_radio = new System.Windows.Forms.RadioButton();
            this.label21 = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExpHelpMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManualMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GLViewPort = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.AnimTimer = new System.Windows.Forms.Timer(this.components);
            this.MainPanel.SuspendLayout();
            this.Credits_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.ExpSetupTabs.SuspendLayout();
            this.CrystalTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Width_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ny_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nx_slider)).BeginInit();
            this.LightTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeltaPhase_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaveLen_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ey_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ex_slider)).BeginInit();
            this.DisplayTab.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.Credits_panel);
            this.MainPanel.Controls.Add(this.groupBox1);
            this.MainPanel.Controls.Add(this.label21);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.MainPanel.Location = new System.Drawing.Point(728, 24);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(256, 637);
            this.MainPanel.TabIndex = 0;
            // 
            // Credits_panel
            // 
            this.Credits_panel.Controls.Add(this.linkLabel1);
            this.Credits_panel.Controls.Add(this.label20);
            this.Credits_panel.Controls.Add(this.label14);
            this.Credits_panel.Controls.Add(this.label13);
            this.Credits_panel.Controls.Add(this.label12);
            this.Credits_panel.Controls.Add(this.label18);
            this.Credits_panel.Controls.Add(this.label17);
            this.Credits_panel.Controls.Add(this.label16);
            this.Credits_panel.Controls.Add(this.label15);
            this.Credits_panel.Controls.Add(this.label11);
            this.Credits_panel.Controls.Add(this.label19);
            this.Credits_panel.Controls.Add(this.label10);
            this.Credits_panel.Controls.Add(this.pictureBox1);
            this.Credits_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Credits_panel.Location = new System.Drawing.Point(0, 309);
            this.Credits_panel.Name = "Credits_panel";
            this.Credits_panel.Size = new System.Drawing.Size(256, 275);
            this.Credits_panel.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(144, 107);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(98, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "baranovav@ngs.ru";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(15, 108);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(76, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "Баранов А. В.";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 71);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Шарашкина А. В.";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 53);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Тябин Е. А.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Малахов И. С.";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(141, 53);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "ПМ-53";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(141, 35);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "ПМ-53";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(141, 71);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "ПМ-53";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(141, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(39, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "ПМ-53";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Алексашин А. С.";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.Location = new System.Drawing.Point(15, 95);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(81, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "Руководитель:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(15, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Разработчики:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PolarizedLight.Properties.Resources.NSTU_Logo_blue;
            this.pictureBox1.Location = new System.Drawing.Point(0, 124);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 149);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ButtonStart);
            this.groupBox1.Controls.Add(this.ButtonStop);
            this.groupBox1.Controls.Add(this.ExpSetupTabs);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 309);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Управление экспериментом";
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(44, 272);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(75, 23);
            this.ButtonStart.TabIndex = 1;
            this.ButtonStart.Text = "ПУСК";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // ButtonStop
            // 
            this.ButtonStop.Location = new System.Drawing.Point(144, 272);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.Size = new System.Drawing.Size(75, 23);
            this.ButtonStop.TabIndex = 1;
            this.ButtonStop.Text = "СТОП";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // ExpSetupTabs
            // 
            this.ExpSetupTabs.Controls.Add(this.CrystalTab);
            this.ExpSetupTabs.Controls.Add(this.LightTab);
            this.ExpSetupTabs.Controls.Add(this.DisplayTab);
            this.ExpSetupTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.ExpSetupTabs.Location = new System.Drawing.Point(3, 16);
            this.ExpSetupTabs.Name = "ExpSetupTabs";
            this.ExpSetupTabs.SelectedIndex = 0;
            this.ExpSetupTabs.Size = new System.Drawing.Size(250, 250);
            this.ExpSetupTabs.TabIndex = 0;
            // 
            // CrystalTab
            // 
            this.CrystalTab.Controls.Add(this.Width_slider);
            this.CrystalTab.Controls.Add(this.label3);
            this.CrystalTab.Controls.Add(this.label2);
            this.CrystalTab.Controls.Add(this.label4);
            this.CrystalTab.Controls.Add(this.ny_slider);
            this.CrystalTab.Controls.Add(this.label1);
            this.CrystalTab.Controls.Add(this.nx_slider);
            this.CrystalTab.Controls.Add(this.CrystalChoice_dropdown);
            this.CrystalTab.Location = new System.Drawing.Point(4, 22);
            this.CrystalTab.Name = "CrystalTab";
            this.CrystalTab.Padding = new System.Windows.Forms.Padding(3);
            this.CrystalTab.Size = new System.Drawing.Size(242, 224);
            this.CrystalTab.TabIndex = 0;
            this.CrystalTab.Text = "Кристалл";
            this.CrystalTab.UseVisualStyleBackColor = true;
            // 
            // Width_slider
            // 
            this.Width_slider.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Width_slider.Location = new System.Drawing.Point(22, 43);
            this.Width_slider.Name = "Width_slider";
            this.Width_slider.Size = new System.Drawing.Size(172, 45);
            this.Width_slider.TabIndex = 1;
            this.Width_slider.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Толщина:";
            // 
            // ny_slider
            // 
            this.ny_slider.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ny_slider.Location = new System.Drawing.Point(23, 168);
            this.ny_slider.Name = "ny_slider";
            this.ny_slider.Size = new System.Drawing.Size(171, 45);
            this.ny_slider.TabIndex = 1;
            this.ny_slider.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Коэффициенты преломления по осям:";
            // 
            // nx_slider
            // 
            this.nx_slider.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.nx_slider.Location = new System.Drawing.Point(23, 119);
            this.nx_slider.Name = "nx_slider";
            this.nx_slider.Size = new System.Drawing.Size(171, 45);
            this.nx_slider.TabIndex = 1;
            this.nx_slider.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // CrystalChoice_dropdown
            // 
            this.CrystalChoice_dropdown.Dock = System.Windows.Forms.DockStyle.Top;
            this.CrystalChoice_dropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CrystalChoice_dropdown.FormattingEnabled = true;
            this.CrystalChoice_dropdown.Items.AddRange(new object[] {
            "Свой кристалл",
            "Кристалл 1",
            "Кристалл 2",
            "Кристалл 3"});
            this.CrystalChoice_dropdown.Location = new System.Drawing.Point(3, 3);
            this.CrystalChoice_dropdown.Name = "CrystalChoice_dropdown";
            this.CrystalChoice_dropdown.Size = new System.Drawing.Size(236, 21);
            this.CrystalChoice_dropdown.TabIndex = 0;
            // 
            // LightTab
            // 
            this.LightTab.Controls.Add(this.pictureBox2);
            this.LightTab.Controls.Add(this.DeltaPhase_slider);
            this.LightTab.Controls.Add(this.WaveLen_slider);
            this.LightTab.Controls.Add(this.label9);
            this.LightTab.Controls.Add(this.Ey_slider);
            this.LightTab.Controls.Add(this.Ex_slider);
            this.LightTab.Controls.Add(this.label5);
            this.LightTab.Controls.Add(this.label6);
            this.LightTab.Controls.Add(this.label7);
            this.LightTab.Controls.Add(this.label8);
            this.LightTab.Location = new System.Drawing.Point(4, 22);
            this.LightTab.Name = "LightTab";
            this.LightTab.Padding = new System.Windows.Forms.Padding(3);
            this.LightTab.Size = new System.Drawing.Size(242, 224);
            this.LightTab.TabIndex = 1;
            this.LightTab.Text = "Источник света";
            this.LightTab.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Image = global::PolarizedLight.Properties.Resources.Palette;
            this.pictureBox2.Location = new System.Drawing.Point(27, 44);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(22, 134);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // DeltaPhase_slider
            // 
            this.DeltaPhase_slider.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DeltaPhase_slider.Location = new System.Drawing.Point(6, 32);
            this.DeltaPhase_slider.Maximum = 100;
            this.DeltaPhase_slider.Name = "DeltaPhase_slider";
            this.DeltaPhase_slider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.DeltaPhase_slider.Size = new System.Drawing.Size(45, 159);
            this.DeltaPhase_slider.TabIndex = 11;
            // 
            // WaveLen_slider
            // 
            this.WaveLen_slider.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.WaveLen_slider.Location = new System.Drawing.Point(189, 32);
            this.WaveLen_slider.Name = "WaveLen_slider";
            this.WaveLen_slider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.WaveLen_slider.Size = new System.Drawing.Size(45, 159);
            this.WaveLen_slider.TabIndex = 4;
            this.WaveLen_slider.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(184, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 26);
            this.label9.TabIndex = 12;
            this.label9.Text = "Разность\r\nфаз:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Ey_slider
            // 
            this.Ey_slider.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Ey_slider.Location = new System.Drawing.Point(128, 32);
            this.Ey_slider.Name = "Ey_slider";
            this.Ey_slider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.Ey_slider.Size = new System.Drawing.Size(45, 159);
            this.Ey_slider.TabIndex = 5;
            this.Ey_slider.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // Ex_slider
            // 
            this.Ex_slider.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Ex_slider.Location = new System.Drawing.Point(67, 32);
            this.Ex_slider.Name = "Ex_slider";
            this.Ex_slider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.Ex_slider.Size = new System.Drawing.Size(45, 159);
            this.Ex_slider.TabIndex = 6;
            this.Ex_slider.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(143, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(82, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 26);
            this.label7.TabIndex = 7;
            this.label7.Text = "Длина\r\nволны:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(85, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Амплитуда:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DisplayTab
            // 
            this.DisplayTab.Controls.Add(this.groupBox4);
            this.DisplayTab.Controls.Add(this.groupBox3);
            this.DisplayTab.Controls.Add(this.groupBox2);
            this.DisplayTab.Location = new System.Drawing.Point(4, 22);
            this.DisplayTab.Name = "DisplayTab";
            this.DisplayTab.Padding = new System.Windows.Forms.Padding(3);
            this.DisplayTab.Size = new System.Drawing.Size(242, 224);
            this.DisplayTab.TabIndex = 2;
            this.DisplayTab.Text = "Отображение";
            this.DisplayTab.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.DrawAxies_chbox);
            this.groupBox4.Location = new System.Drawing.Point(7, 120);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(229, 98);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Общие";
            // 
            // DrawAxies_chbox
            // 
            this.DrawAxies_chbox.AutoSize = true;
            this.DrawAxies_chbox.Location = new System.Drawing.Point(7, 20);
            this.DrawAxies_chbox.Name = "DrawAxies_chbox";
            this.DrawAxies_chbox.Size = new System.Drawing.Size(109, 17);
            this.DrawAxies_chbox.TabIndex = 0;
            this.DrawAxies_chbox.Text = "Отображать оси";
            this.DrawAxies_chbox.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DrawY_chbox);
            this.groupBox3.Controls.Add(this.DrawX_chbox);
            this.groupBox3.Controls.Add(this.DrawSumm_chbox);
            this.groupBox3.Location = new System.Drawing.Point(108, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(128, 107);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Компоненты волны";
            // 
            // DrawY_chbox
            // 
            this.DrawY_chbox.AutoSize = true;
            this.DrawY_chbox.Location = new System.Drawing.Point(6, 66);
            this.DrawY_chbox.Name = "DrawY_chbox";
            this.DrawY_chbox.Size = new System.Drawing.Size(33, 17);
            this.DrawY_chbox.TabIndex = 0;
            this.DrawY_chbox.Text = "Y";
            this.DrawY_chbox.UseVisualStyleBackColor = true;
            // 
            // DrawX_chbox
            // 
            this.DrawX_chbox.AutoSize = true;
            this.DrawX_chbox.Location = new System.Drawing.Point(6, 43);
            this.DrawX_chbox.Name = "DrawX_chbox";
            this.DrawX_chbox.Size = new System.Drawing.Size(33, 17);
            this.DrawX_chbox.TabIndex = 0;
            this.DrawX_chbox.Text = "X";
            this.DrawX_chbox.UseVisualStyleBackColor = true;
            // 
            // DrawSumm_chbox
            // 
            this.DrawSumm_chbox.AutoSize = true;
            this.DrawSumm_chbox.Location = new System.Drawing.Point(6, 20);
            this.DrawSumm_chbox.Name = "DrawSumm_chbox";
            this.DrawSumm_chbox.Size = new System.Drawing.Size(113, 17);
            this.DrawSumm_chbox.TabIndex = 0;
            this.DrawSumm_chbox.Text = "Результирующая";
            this.DrawSumm_chbox.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DrawVec_radio);
            this.groupBox2.Controls.Add(this.DrawOutline_radio);
            this.groupBox2.Controls.Add(this.DrawBoth_radio);
            this.groupBox2.Location = new System.Drawing.Point(7, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(95, 107);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Отрисовка";
            // 
            // DrawVec_radio
            // 
            this.DrawVec_radio.AutoSize = true;
            this.DrawVec_radio.Location = new System.Drawing.Point(7, 79);
            this.DrawVec_radio.Name = "DrawVec_radio";
            this.DrawVec_radio.Size = new System.Drawing.Size(69, 17);
            this.DrawVec_radio.TabIndex = 0;
            this.DrawVec_radio.TabStop = true;
            this.DrawVec_radio.Text = "Векторы";
            this.DrawVec_radio.UseVisualStyleBackColor = true;
            // 
            // DrawOutline_radio
            // 
            this.DrawOutline_radio.AutoSize = true;
            this.DrawOutline_radio.Location = new System.Drawing.Point(7, 56);
            this.DrawOutline_radio.Name = "DrawOutline_radio";
            this.DrawOutline_radio.Size = new System.Drawing.Size(85, 17);
            this.DrawOutline_radio.TabIndex = 0;
            this.DrawOutline_radio.TabStop = true;
            this.DrawOutline_radio.Text = "Огибающие";
            this.DrawOutline_radio.UseVisualStyleBackColor = true;
            // 
            // DrawBoth_radio
            // 
            this.DrawBoth_radio.AutoSize = true;
            this.DrawBoth_radio.Location = new System.Drawing.Point(7, 20);
            this.DrawBoth_radio.Name = "DrawBoth_radio";
            this.DrawBoth_radio.Size = new System.Drawing.Size(85, 30);
            this.DrawBoth_radio.TabIndex = 0;
            this.DrawBoth_radio.TabStop = true;
            this.DrawBoth_radio.Text = "Огибающие\r\nи векторы";
            this.DrawBoth_radio.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Calibri Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label21.Location = new System.Drawing.Point(70, 587);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(116, 46);
            this.label21.TabIndex = 1;
            this.label21.Text = "Новосибирск\r\n2017";
            this.label21.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuStripItem,
            this.HelpMenuStripItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(984, 24);
            this.MenuStrip.TabIndex = 1;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // FileMenuStripItem
            // 
            this.FileMenuStripItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsMenuStripItem,
            this.ExitMenuStripItem});
            this.FileMenuStripItem.Name = "FileMenuStripItem";
            this.FileMenuStripItem.Size = new System.Drawing.Size(48, 20);
            this.FileMenuStripItem.Text = "Файл";
            // 
            // SettingsMenuStripItem
            // 
            this.SettingsMenuStripItem.Name = "SettingsMenuStripItem";
            this.SettingsMenuStripItem.Size = new System.Drawing.Size(134, 22);
            this.SettingsMenuStripItem.Text = "Настройки";
            // 
            // ExitMenuStripItem
            // 
            this.ExitMenuStripItem.Name = "ExitMenuStripItem";
            this.ExitMenuStripItem.Size = new System.Drawing.Size(134, 22);
            this.ExitMenuStripItem.Text = "Выход";
            // 
            // HelpMenuStripItem
            // 
            this.HelpMenuStripItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExpHelpMenuStripItem,
            this.ManualMenuStripItem});
            this.HelpMenuStripItem.Name = "HelpMenuStripItem";
            this.HelpMenuStripItem.Size = new System.Drawing.Size(65, 20);
            this.HelpMenuStripItem.Text = "Справка";
            // 
            // ExpHelpMenuStripItem
            // 
            this.ExpHelpMenuStripItem.Name = "ExpHelpMenuStripItem";
            this.ExpHelpMenuStripItem.Size = new System.Drawing.Size(221, 22);
            this.ExpHelpMenuStripItem.Text = "Справка по эксперименту";
            // 
            // ManualMenuStripItem
            // 
            this.ManualMenuStripItem.Name = "ManualMenuStripItem";
            this.ManualMenuStripItem.Size = new System.Drawing.Size(221, 22);
            this.ManualMenuStripItem.Text = "Руководство пользователя";
            // 
            // GLViewPort
            // 
            this.GLViewPort.AccumBits = ((byte)(0));
            this.GLViewPort.AutoCheckErrors = false;
            this.GLViewPort.AutoFinish = false;
            this.GLViewPort.AutoMakeCurrent = true;
            this.GLViewPort.AutoSwapBuffers = true;
            this.GLViewPort.BackColor = System.Drawing.Color.Black;
            this.GLViewPort.ColorBits = ((byte)(32));
            this.GLViewPort.DepthBits = ((byte)(16));
            this.GLViewPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLViewPort.Location = new System.Drawing.Point(0, 24);
            this.GLViewPort.Name = "GLViewPort";
            this.GLViewPort.Size = new System.Drawing.Size(728, 637);
            this.GLViewPort.StencilBits = ((byte)(0));
            this.GLViewPort.TabIndex = 2;
            this.GLViewPort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GLViewPort_MouseDown);
            this.GLViewPort.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GLViewPort_MouseMove);
            this.GLViewPort.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GLViewPort_MouseUp);
            // 
            // AnimTimer
            // 
            this.AnimTimer.Interval = 16;
            this.AnimTimer.Tick += new System.EventHandler(this.AnimTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.GLViewPort);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поляризованный свет в анизотромном кристалле";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.Credits_panel.ResumeLayout(false);
            this.Credits_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ExpSetupTabs.ResumeLayout(false);
            this.CrystalTab.ResumeLayout(false);
            this.CrystalTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Width_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ny_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nx_slider)).EndInit();
            this.LightTab.ResumeLayout(false);
            this.LightTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeltaPhase_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaveLen_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ey_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ex_slider)).EndInit();
            this.DisplayTab.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private Tao.Platform.Windows.SimpleOpenGlControl GLViewPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.Button ButtonStop;
        private System.Windows.Forms.TabControl ExpSetupTabs;
        private System.Windows.Forms.TabPage CrystalTab;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar ny_slider;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar nx_slider;
        private System.Windows.Forms.ComboBox CrystalChoice_dropdown;
        private System.Windows.Forms.TabPage LightTab;
        private System.Windows.Forms.TrackBar Width_slider;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar DeltaPhase_slider;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar WaveLen_slider;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar Ey_slider;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar Ex_slider;
        private System.Windows.Forms.TabPage DisplayTab;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox DrawY_chbox;
        private System.Windows.Forms.CheckBox DrawX_chbox;
        private System.Windows.Forms.CheckBox DrawSumm_chbox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton DrawVec_radio;
        private System.Windows.Forms.RadioButton DrawOutline_radio;
        private System.Windows.Forms.RadioButton DrawBoth_radio;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox DrawAxies_chbox;
        private System.Windows.Forms.ToolStripMenuItem FileMenuStripItem;
        private System.Windows.Forms.ToolStripMenuItem SettingsMenuStripItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuStripItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuStripItem;
        private System.Windows.Forms.ToolStripMenuItem ExpHelpMenuStripItem;
        private System.Windows.Forms.ToolStripMenuItem ManualMenuStripItem;
        private System.Windows.Forms.Panel Credits_panel;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer AnimTimer;
    }
}

