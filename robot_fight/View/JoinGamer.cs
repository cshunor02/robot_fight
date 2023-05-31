namespace robot_fight.View
{
    public partial class JoinGamer : Form
    {
        #region Variables

        // Variables that are passed to GamerForm
        private int _gamerView;
        private int _tableHeight;
        private int _tableWidth;
        private int _numOfTeams;
        private int _fromNum;
        private int _toNum;
        private int _numOfBarriers;
        private int _numOfLife;
        private int _numOfTicks;
        private int _numOfSteps;
        private int _numOfExits;
        private int _maxPlayerCount;
        private int _defaultResource;

        // Check if user can Start or Close the game
        private bool _clicked = true;
        private bool _remember = false;
        private bool _canClick = false;
        #endregion

        #region Initialize JoinGamerForm
        public JoinGamer(int _gamerView, int _tableHeight, int _tableWidth, int _numOfTeams, int _fromNum, int _toNum, int _numOfBarriers, int _numOfLife, int _numOfTicks, int _numOfSteps, int _numOfExits, int _maxPlayerCount, int _defaultResource)
        {
            InitializeComponent();

            this._gamerView = _gamerView;
            this._tableHeight = _tableHeight;
            this._tableWidth = _tableWidth;
            this._numOfTeams = _numOfTeams;
            this._fromNum = _fromNum;
            this._toNum = _toNum;
            this._numOfBarriers = _numOfBarriers;
            this._numOfLife = _numOfLife;
            this._numOfTicks = _numOfTicks;
            this._numOfSteps = _numOfSteps;
            this._numOfExits = _numOfExits;
            this._maxPlayerCount = _maxPlayerCount;
            this._defaultResource = _defaultResource;
        }
        #endregion

        #region Start Game Button Event
        private void button1_Click(object sender, EventArgs e)
        {
            if (_canClick == true)
            {
                GamerForm gf = new GamerForm(_robotName.Text, _gamerView, _tableHeight, _tableWidth, _numOfTeams, _fromNum, _toNum, _numOfBarriers, _numOfLife, _numOfTicks, _numOfSteps, _numOfExits, _maxPlayerCount, _defaultResource);
                this.Hide();
                gf.Show();
            }
        }
        #endregion

        #region Close the Form
        private void JoinGamer_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Check if there are enough amount of Players
        private void _robotName_TextChanged(object sender, EventArgs e)
        {
            if(_robotName != null)
            {
                string[] szoveg = _robotName.Text.Split(',');
                bool uresnev = false;
                foreach(string nevek in szoveg)
                {
                    if (nevek == "") uresnev = true;
                }
                if(szoveg.Length == _maxPlayerCount && !uresnev)
                {
                    _canClick = true;
                    textBox1.Text = "";
                    textBox1.ForeColor = Color.Green;
                    textBox1.AppendText("> MEGFELELŐ SZÁMÚ JÁTÉKOS!\r\n");
                    button1.Enabled = true;
                } else if (szoveg.Length < _maxPlayerCount)
                {
                    _canClick = false;
                    textBox1.Text = "";
                    textBox1.ForeColor = Color.Red;
                    textBox1.AppendText("> Nincs elég játékos megadva!\r\n");
                    button1.Enabled = false;
                } else if (szoveg.Length > _maxPlayerCount)
                {
                    _canClick = false;
                    textBox1.Text = "";
                    textBox1.ForeColor = Color.Red;
                    textBox1.AppendText("> Túl sok játékos megadva!\r\n");
                    button1.Enabled = false;
                } else
                {
                    _canClick = false;
                    textBox1.Text = "";
                    textBox1.ForeColor = Color.Red;
                    textBox1.AppendText("> Nem megfelelő adatok!\r\n");
                    button1.Enabled = false;
                }
            } else
            {
                _canClick = false;
            }
        }
        #endregion
    }
}
