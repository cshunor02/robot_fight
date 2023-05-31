using System.Media;

namespace robot_fight.View
{
    public partial class MainForm : Form
    {
        #region Variables
        private string path;
        #endregion

        #region Initialize MainForm
        public MainForm()
        {
            InitializeComponent();

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string subfolderPath = Path.Combine(projectDirectory, "View");
            string subfolderPath2 = Path.Combine(subfolderPath, "textures");
            path = Path.Combine(subfolderPath2, "welcome.wav");
            SoundPlayer simpleSound = new SoundPlayer(path);
            simpleSound.Play();
        }
        #endregion

        #region Load Menu

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.Hide();

            //Open MainMenu
            MainMenuForm mmf = new MainMenuForm();

            mmf.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion
    }
}
