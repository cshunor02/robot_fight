namespace robot_fight.View
{
    public partial class GameMasterForm : Form
    {
        #region Variables
        private bool _clicked = true;
        private bool _remember = false;
        #endregion

        #region Initialize GameMasterForm
        public GameMasterForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Start game Button
        private void StartGameButton(object sender, EventArgs e)
        {
            if (CheckIfCorrect())
            {
                _consoleInput.AppendText("\r\nJáték létrehozása...\r\n");

                int themePicker;
                if (_picnicTheme.Checked == true)
                {
                    themePicker = 1;
                }
                else if (_christmasTheme.Checked == true)
                {
                    themePicker = 2;
                }
                else
                {
                    themePicker = 0;
                }

                JoinGamer jg = new JoinGamer(Convert.ToInt32(_gamerView.Value), Convert.ToInt32(_tableHeight.Value), Convert.ToInt32(_tableWidth.Value), Convert.ToInt32(_numOfTeams.Value), Convert.ToInt32(_fromNum.Value) , Convert.ToInt32(_toNum.Value), Convert.ToInt32(_numOfBarriers.Value), Convert.ToInt32(_numOfLife.Value), Convert.ToInt32(_numOfTicks.Value), Convert.ToInt32(_numOfSteps.Value), Convert.ToInt32(_numOfExits.Value), Convert.ToInt32(_maxPlayerCount.Value), themePicker);
               
                this.Hide();
                jg.Show();

            } else
            {
                _consoleInput.Text = "";
                _consoleInput.AppendText("NEM MEGFELELŐ ADATOK! Kérem ne feledje:\r\n\r\n");
                _consoleInput.AppendText(" > A játékos látótere nem lehet kisebb, mint a pálya mérete!\r\n\r\n");
                _consoleInput.AppendText(" > Az akadályok száma nem lehet több, mint a pálya mérete!\r\n\r\n");
                _consoleInput.AppendText(" > A feladatok értéke: A -TÓL paraméter értékének kisebb egyenlőnek kell lennie, mint az -IG mező értékének!");
            } 
        }
        #endregion

        #region Check if Input data was valid
        private bool CheckIfCorrect()
        {
            bool _correct = false;

            if (Convert.ToInt32(_gamerView.Value) < Convert.ToInt32(_tableHeight.Value) &&
                Convert.ToInt32(_gamerView.Value) < Convert.ToInt32(_tableWidth.Value) &&
                Convert.ToInt32(_numOfBarriers.Value) < Convert.ToInt32(_tableWidth.Value)* Convert.ToInt32(_tableHeight.Value) &&
                Convert.ToInt32(_fromNum.Value) <= Convert.ToInt32(_toNum.Value)) _correct = true;

            return _correct;
        }
        #endregion

        #region Close Game

        private void GameMasterForm_FormClosed(object sender, FormClosingEventArgs e)
        {
            if (!_remember && _clicked)
            {
                var secure = MessageBox.Show("Biztosan ki szeretne lépni a játékból?","Kilépés",MessageBoxButtons.YesNo);
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
    }
}
