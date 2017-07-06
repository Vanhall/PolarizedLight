using System;
using System.Windows.Forms;

namespace PolarizedLight
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            GLViewPort.InitializeContexts();

            CrystalChoice_dropdown.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
