namespace robot_fight.View
{
    public partial class MainMenuForm : Form
    {
        #region Variables
        private bool _clicked = true;
        private bool _remember = false;
        #endregion

        #region Initialize MainMenuForm
        public MainMenuForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Close the game

        private void MainMenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_remember && _clicked)
            {
                var secure = MessageBox.Show("Biztosan ki szeretne lépni a játékból?",
                                    "Kilépés",
                                     MessageBoxButtons.YesNo);
                if (secure == DialogResult.Yes)
                {
                    _remember = true;
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region Start the Game Button Event
        public void GameMasterButtonClick(object sender, EventArgs e)
        {
            _clicked = false;
            this.Close();
            GameMasterForm gmf = new GameMasterForm();
            gmf.Show();
        }
        #endregion

        #region Viewer mode Button Event
        public void ViewerButton(object sender, EventArgs e)
        {
            _clicked = false;
            ViewerForm vf = new ViewerForm();
            vf.Show();
        }
        #endregion

        #region How to Play Button Event
        public void HowToPlayButton(object sender, EventArgs e)
        {
            _clicked = false;
            HowToPlayForm htpf = new HowToPlayForm();
            htpf.Show();
        }
        #endregion

        #region Button hover effect Events

        private void OnHover(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button.FlatAppearance.BorderSize = 8;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        private void OnLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.FlatAppearance.BorderSize = 0;
        }
        #endregion
    }

}
