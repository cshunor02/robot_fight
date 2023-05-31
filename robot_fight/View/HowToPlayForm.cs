namespace robot_fight.View
{
    public partial class HowToPlayForm : Form
    {
        #region Initialize HowToPlayForm
        public HowToPlayForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Close HowToPlay form
        private void HowToPlayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }
        #endregion
    }
}
