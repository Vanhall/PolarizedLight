using System.Windows.Forms;

namespace PolarizedLight
{
    public partial class SplashScreen : Form
    {
        MainForm Main;
        public SplashScreen()
        {
            InitializeComponent();
            Main = new MainForm();
            SplashTimer.Start();
        }
        
        private void SplashTimer_Tick(object sender, System.EventArgs e)
        {
            SplashTimer.Stop();
            Main.Show();
            Hide();
        }
        
        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
            SplashTimer.Stop();
            Main.Show();
            Hide();
        }

        private void SplashScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            SplashTimer.Stop();
            Main.Show();
            Hide();
        }
    }
}
