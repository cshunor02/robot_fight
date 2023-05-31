using Persistence;
using robot_fight.Model;
using System.Media;
using System.Windows.Forms;

namespace robot_fight.View
{
    public partial class GamerForm : Form
    {
        #region Variables

        //Set the Theme: Default, Picnic or Christmas
        private int _defaultResource = 0;

        //Time and Timer
        private bool stoppedTimer = false;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private DateTime startpicnic = new DateTime(DateTime.Now.Year, 6, 18, 0, 0, 0); //0 o'clock
        private DateTime endpicnic = new DateTime(DateTime.Now.Year, 6, 19, 0, 0, 0);
        private DateTime startdecember = new DateTime(DateTime.Now.Year, 12, 1, 0, 0, 0); //0 o'clock
        private DateTime enddecember = new DateTime(DateTime.Now.Year, 12, 31, 0, 0, 0);

        //GameModel and Random
        private GameModel gm;
        private Random rnd = new Random();
        private string? name;
        int _shuffleCounter;

        //First time to fill up the Table
        private bool firstGenerated = true;
        private int _additionalOption = 0;

        //Store the steps in a round
        private Dictionary<(int, int), string> _dictionary = new Dictionary<(int, int), string>();
        private int round = 0;
        private int num = 0;
        private int attachDetach = 0;

        //Set the actual robot and team to play
        private int currentTeam = 0;
        private int currentPlayer = 0;
        private int teamNumberLocal = 1;

        //Variables that was passed by the GameMaster
        private int _gamerView;
        private int _numOfTeams;
        private int _numOfTicks;
        private int _numOfSteps;
        private int _maxPlayerCount;
        private int rowCol = 7;
        private int _fromNum;
        private int _toNum;
        private int _numOfLife;

        //Tasks
        private Model.Task task1;
        private Model.Task task2;

        //Team messages
        private string save_coordinate = "";
        private List<List<string>> old_messages = new List<List<string>>();
        private List<List<string>> new_messages = new List<List<string>>();
        private List<string> commands = new List<string>();
        private int tempMessage = 0;
        private string[] temps = new string[2];
        private List<List<List<string>>> playerLocalMessages = new List<List<List<string>>>();

        //Close Form
        private bool _clicked = true;
        private bool _remember = false;
        private bool innerclose = false;

        //Sound for cleaning
        private string projectDirectory;
        private string subfolderPath;
        private string subfolderPath2;
        private string path;

        // This dictionary will be passed to the Gamemodel
        // requests contains the players commands
        private Dictionary<string, string> requests = new Dictionary<string, string>
        {
            { "É_Step", "Move North" },
            { "K_Step", "Move East" },
            { "D_Step", "Move South" },
            { "NY_Step", "Move West" },

            { "É_PickUp", "BoxPickUp North" },
            { "K_PickUp", "BoxPickUp East" },
            { "D_PickUp", "BoxPickUp South" },
            { "NY_PickUp", "BoxPickUp West" },

            { "É_Drop", "BoxPutDown North" },
            { "K_Drop", "BoxPutDown East" },
            { "D_Drop", "BoxPutDown South" },
            { "NY_Drop", "BoxPutDown West" },

            { "É_Clean", "ClearField North" },
            { "K_Clean", "ClearField East" },
            { "D_Clean", "ClearField South" },
            { "NY_Clean", "ClearField West" },

            { "É_É_Attach", "AttachBoxes North_North" },
            { "É_K_Attach", "AttachBoxes North_East" },
            { "É_D_Attach", "Wait" }, // unsuccessful
            { "É_NY_Attach", "AttachBoxes North_West" },

            { "D_É_Attach", "Wait" }, // unsuccessful
            { "D_K_Attach", "AttachBoxes South_East" },
            { "D_D_Attach", "AttachBoxes South_South" },
            { "D_NY_Attach", "AttachBoxes South_West" },

            { "K_É_Attach", "AttachBoxes East_North" },
            { "K_K_Attach", "AttachBoxes East_East" },
            { "K_D_Attach", "AttachBoxes East_South" },
            { "K_NY_Attach", "Wait" }, // unsuccessful

            { "NY_É_Attach", "AttachBoxes West_North" },
            { "NY_K_Attach", "Wait" }, // unsuccessful
            { "NY_D_Attach", "AttachBoxes West_South" },
            { "NY_NY_Attach", "AttachBoxes West_West" },

            { "É_É_Detach", "DetachBoxes North_North" },
            { "É_K_Detach", "DetachBoxes North_East" },
            { "É_D_Detach", "DetachBoxes North_South" },
            { "É_NY_Detach", "DetachBoxes North_West" },

            { "D_É_Detach", "DetachBoxes South_North" },
            { "D_K_Detach", "DetachBoxes South_East" },
            { "D_D_Detach", "DetachBoxes South_South" },
            { "D_NY_Detach", "DetachBoxes South_West" },

            { "K_É_Detach", "DetachBoxes East_North" },
            { "K_K_Detach", "DetachBoxes East_East" },
            { "K_D_Detach", "DetachBoxes East_South" },
            { "K_NY_Detach", "DetachBoxes East_West" },

            { "NY_É_Detach", "DetachBoxes West_North" },
            { "NY_K_Detach", "DetachBoxes West_East" },
            { "NY_D_Detach", "DetachBoxes West_South" },
            { "NY_NY_Detach", "DetachBoxes West_West" },

            { "R_Rotate", "ChangeFaceDirection East" },
            { "L_Rotate", "ChangeFaceDirection West" },

            { "Freeze", "Freeze" },
            { "", "Wait" } // successful
        };

        #endregion

        #region Initialize Game

        // The properties was set by the GameMaster
        // First, we clear the previous table in Persistene (table.txt) and set the properties
        // We check if today is the National Picnic Day or it is December => use different resource
        // Create the GameModel and set the Robots, then Start the game
        public GamerForm(string robotname, int _gamerView, int _tableHeight, int _tableWidth, int _numOfTeams, int _fromNum, int _toNum, int _numOfBarriers, int _numOfLife, int _numOfTicks, int _numOfSteps, int _numOfExits, int _maxPlayerCount, int defaultResource)
        {
            InitializeComponent();

            projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            subfolderPath = Path.Combine(projectDirectory, "View");
            subfolderPath2 = Path.Combine(subfolderPath, "textures");
            path = Path.Combine(subfolderPath2, "clean.wav");

            SaveFile.ClearFile();

            this._gamerView = _gamerView;
            this._numOfTeams = _numOfTeams;
            this._numOfTicks = _numOfTicks;
            this._numOfSteps = _numOfSteps;
            this._shuffleCounter = 2;
            this._fromNum = _fromNum;
            this._toNum = _toNum;
            this._numOfLife = _numOfLife;
            rowCol = this._gamerView * 2 + 1;

            DateTime now = DateTime.Now;
            if ((now > startpicnic) && (now < endpicnic))
            {
                this._defaultResource = 1;
            }
            else if ((now > startdecember) && (now < enddecember))
            {
                this._defaultResource = 2;
            }
            else
            {
                this._defaultResource = defaultResource;
            }

            timer.Interval = 1000;
            timer.Tick += new EventHandler(timeToDecide);

            name = robotname == null ? "Robi" : robotname;

            gm = new GameModel("server", _tableHeight, _tableWidth, _fromNum, _toNum);
            gm.Ground.GenerateGround(_tableWidth, _tableHeight, _numOfBarriers, _numOfLife, _numOfExits);
            gm.Robots = new List<List<Robot>>();
            gm.createRandomBoxes(gm.Tasks[0], _numOfLife);
            gm.createRandomBoxes(gm.Tasks[1], _numOfLife);
            gm.createRandomBoxes(gm.Tasks[0], _numOfLife);
            gm.createRandomBoxes(gm.Tasks[1], _numOfLife);

            string[] robotnames = robotname.Split(',');
            this._maxPlayerCount = robotnames.Length;
            temps = new string[_maxPlayerCount];

            for (int i = 0; i < robotnames.Length; i++)
            {
                old_messages.Add(new List<string>());
                new_messages.Add(new List<string>());
                playerLocalMessages.Add(new List<List<string>>());
            }

            gm.fillRobotsList(robotname, _numOfTeams, _gamerView);

            for (int i = 0; i < gm.Robots.Count; i++)
            {
                for (int j = 0; j < gm.Robots[i].Count; j++)
                {
                    playerLocalMessages[i].Add(new List<string>());
                }
            }

            currentTeam = 0;

            //Initialize RobotsStepped in Gamemodel
            foreach (var group in gm.Robots)
            {
                List<bool> groupBools = new List<bool>();
                foreach (Robot r in group)
                {
                    groupBools.Add(false);
                }
                gm.RobotsStepped.Add(groupBools);
            }

            //Get Tasks from Gamemodel
            task1 = gm.Tasks[0];
            task2 = gm.Tasks[1];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    PictureBox picture = new PictureBox
                    {
                        Name = "ptr_" + i + "_" + j,
                        BorderStyle = BorderStyle.None,
                        Size = new Size(80, 80),
                        Visible = true
                    };
                    picture.BackgroundImage = GetMatchingColor(task1.Boxes[i * 3 + j]);

                    picture.BackgroundImageLayout = ImageLayout.Stretch;
                    picture.Dock = DockStyle.Fill;
                    picture.Margin = new Padding(0);

                    _task1Display.Controls.Add(picture, j, i);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    PictureBox picture = new PictureBox
                    {
                        Name = "ptr_" + i + "_" + j,
                        BorderStyle = BorderStyle.None,
                        Size = new Size(80, 80),
                        Visible = true
                    };
                    picture.BackgroundImage = GetMatchingColor(task2.Boxes[i * 3 + j]);

                    picture.BackgroundImageLayout = ImageLayout.Stretch;
                    picture.Dock = DockStyle.Fill;
                    picture.Margin = new Padding(0);

                    _task2Display.Controls.Add(picture, j, i);
                }
            }

            RefreshState();

            SaveFile.WriteTable(gm.Stringify());

            timer.Start();
        }
        #endregion

        #region Timer Tick Events

        // The event is called in every second
        // If the Progress bar reaches zero, the View will go to the next player's view
        private void timeToDecide(object? sender, EventArgs e)
        {
            _progressBar.Value--;
            if (_progressBar.Value <= 0)
            {
                // The syntax of the message which will be passed to the Gamemodel is:
                // @master_TeamNumber_NameOfRobot_Direction_Event
                // @master is neccessary at the beginning to decide whether it is a command
                // or just a message between the robots
                if (round == 0)
                {
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_");
                }
                round = 0;
                _communicationBox.Text = "";

                currentPlayer++;
                if (gm.Robots[currentTeam].Count == currentPlayer)
                {
                    currentPlayer = 0;
                    currentTeam++;
                }
                timer.Stop();

                // A new round begins
                // We set the current Player and team to the first player
                if (currentTeam >= _numOfTeams)
                {
                    gm.RobotsStepped.Clear();
                    //fill robotsStepped list with "false" data, initialize it to the next round
                    foreach (var group in gm.Robots)
                    {
                        List<bool> groupBools = new List<bool>();
                        foreach (Robot r in group)
                        {
                            groupBools.Add(false);
                        }
                        gm.RobotsStepped.Add(groupBools);
                    }

                    //the cubes on the board will shuffle sometimes
                    
                    if (_numOfSteps >= 6)
                    {
                        _shuffleCounter--;
                        if (_shuffleCounter <= 0)
                        {
                            gm.Shuffle();
                            gm.createRandomWaters(_numOfLife);
                        
                            _shuffleCounter = rnd.Next(3, (_numOfSteps / 2) + 6);
                        
                        }
                    }

                    bool[] success = gm.Step(_dictionary);

                    int u = 0;
                    int o = 0;
                    tempMessage = 0;

                    // The GameModel's Step method gives back a bool array
                    // Here we can sort the messages and write out the details
                    // If it is true, the step was Successful, otherwise it was Unsuccessful
                    for (int t = 0; t < _dictionary.Count; t++)
                    {
                        if (success[t] == true)
                        {
                            string add = (temps[tempMessage] == "" || temps[tempMessage] == null) ? "> Várakozás..." : temps[tempMessage];
                            playerLocalMessages[u][o++].Add(add + "Sikeres!\r\n");
                            string text = commands[t].Remove(0, commands[t].IndexOf("_") + 1);
                            SaveFile.WriteInFile(text);
                            tempMessage++;
                        }
                        else
                        {
                            string add = (temps[tempMessage] == "" || temps[tempMessage] == null) ? "> Várakozás..." : temps[tempMessage];
                            playerLocalMessages[u][o++].Add(add + "Sikertelen!\r\n");
                            string text = commands[t].Remove(0, commands[t].IndexOf("_") + 1);
                            SaveFile.WriteInFile(text);
                            tempMessage++;
                        }

                        if (o >= gm.Robots[u].Count)
                        {
                            o = 0;
                            u++;
                        }
                        if (u >= gm.Robots.Count)
                        {
                            u = 0;
                            o = 0;
                        }
                    }

                    _dictionary.Clear();
                    commands.Clear();
                    currentTeam = 0;
                    currentPlayer = 0;

                    // Players can communicate with each other through the communicatior
                    // This cycles will collect the messages and save it
                    for (int k = 0; k < new_messages.Count; k++)
                    {
                        old_messages[k].Clear();
                        foreach (string uzenetek in new_messages[k])
                        {
                            old_messages[k].Add(uzenetek);
                        }
                        new_messages[k].Clear();
                    }

                    Robot.circleTime++;
                    gm.Tasks[0].StepCount--;
                    gm.Tasks[1].StepCount--;

                    //check tasks expire, if yes, we refresh it
                    if (gm.Tasks[0].StepCount == 0)
                    {
                        RefreshTask(0);
                        //the more the merrier
                        gm.createRandomBoxes(gm.Tasks[0], _numOfLife);
                        gm.createRandomBoxes(gm.Tasks[0], _numOfLife);
                    }
                    if (gm.Tasks[1].StepCount == 0)
                    {
                        RefreshTask(1);
                        //it is intentional.
                        gm.createRandomBoxes(gm.Tasks[1], _numOfLife);
                        gm.createRandomBoxes(gm.Tasks[1], _numOfLife);
                    }

                    SaveFile.WriteTable(gm.Stringify());

                    tempMessage = 0;
                    temps = new string[_maxPlayerCount];

                    _numOfSteps--;
                    if (_numOfSteps <= 0)
                    {
                        GameOverEvent();
                        return;
                    }
                }

                GetPrevRoundMessages(currentTeam);
                RefreshState();
                EnableControls();

                round = 0;
                save_coordinate = "";

                _localLog.Text = "";
                foreach (string messages in playerLocalMessages[currentTeam][currentPlayer])
                {
                    _localLog.AppendText(messages);
                }

                timer.Start();
            }
        }

        //refresh the given task
        private void RefreshTask(int whichTask)
        {
            if (whichTask == 0)
            {
                //generate new task
                gm.Tasks[0] = new Model.Task(rnd.Next(100, 200), (_fromNum, _toNum), "1. Feladat");
                task1 = gm.Tasks[0];
                //refresh the task on the display
                _task1Display.Controls.Clear();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        PictureBox picture = new PictureBox
                        {
                            Name = "ptr_" + i + "_" + j,
                            BorderStyle = BorderStyle.None,
                            Size = new Size(80, 80),
                            Visible = true
                        };
                        picture.BackgroundImage = GetMatchingColor(task1.Boxes[i * 3 + j]);

                        picture.BackgroundImageLayout = ImageLayout.Stretch;
                        picture.Dock = DockStyle.Fill;
                        picture.Margin = new Padding(0);

                        _task1Display.Controls.Add(picture, j, i);

                    }
                }
            }
            else
            {
                //generate new task
                gm.Tasks[1] = new Model.Task(rnd.Next(100, 200), (_fromNum, _toNum), "2. Feladat");
                task2 = gm.Tasks[1];
                //refresh the task on the display
                _task2Display.Controls.Clear();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        PictureBox picture = new PictureBox
                        {
                            Name = "ptr_" + i + "_" + j,
                            BorderStyle = BorderStyle.None,
                            Size = new Size(80, 80),
                            Visible = true
                        };
                        picture.BackgroundImage = GetMatchingColor(task2.Boxes[i * 3 + j]);

                        picture.BackgroundImageLayout = ImageLayout.Stretch;
                        picture.Dock = DockStyle.Fill;
                        picture.Margin = new Padding(0);

                        _task2Display.Controls.Add(picture, j, i);
                    }
                }
            }
        }

        // Check whether it is Game Over
        // and ask the Players if they would like to start a new game
        private void GameOverEvent()
        {
            timer.Stop();
            int teamWon = gm.GameEnd();

            if (teamWon == -1)
            {
                var secure = MessageBox.Show("Egyik csapat sem szerzett pontot, nincs győztes!\r\nSzeretne új játékot indítani?",
                                    "Játék vége!", MessageBoxButtons.YesNo);

                if (secure == DialogResult.Yes)
                {
                    innerclose = true;
                    this.Close();
                    GameMasterForm gmf = new GameMasterForm();
                    gmf.Show();
                }
                else
                {
                    innerclose = false;
                    Environment.Exit(0);
                }
            }
            else
            {
                int points = gm.Scores[teamWon];

                var secure = MessageBox.Show("A(z) " + teamWon + " csapatt győzőtt " + points + " ponttal!\r\nSzeretne új játékot indítani?",
                                    "Játék vége!", MessageBoxButtons.YesNo);

                if (secure == DialogResult.Yes)
                {
                    innerclose = true;
                    this.Close();
                    GameMasterForm gmf = new GameMasterForm();
                    gmf.Show();
                }
                else
                {
                    innerclose = false;
                    Environment.Exit(0);
                }
            }
        }

        // Set the actual round's state and change the labels
        private void RefreshState()
        {
            _teamPoints.Text = gm.Scores[currentTeam].ToString();
            _remainingSteps.Text = _numOfSteps.ToString();
            name = gm.Robots[currentTeam][currentPlayer].Name;
            _robotName.Text = gm.Robots[currentTeam][currentPlayer].Name;
            teamNumberLocal = gm.Robots[currentTeam][currentPlayer].TeamNumber + 1;
            GenerateTable(gm.RobotStringify(gm.Robots[currentTeam][currentPlayer]));

            _progressBar.Maximum = _numOfTicks;
            _progressBar.Minimum = 0;
            _progressBar.Value = _numOfTicks;

            _task1Name.Text = task1.Name;
            _task1StepCount.Text = "Teljesítési idő: " + task1.StepCount.ToString() + " lépés";
            _task1Ertek.Text = "Érték: " + task1.Score.ToString();
            _task2Name.Text = task2.Name;
            _task2StepCount.Text = "Teljesítési idő: " + task2.StepCount.ToString() + " lépés";
            _task2Ertek.Text = "Érték: " + task2.Score.ToString();
        }

        private void GetPrevRoundMessages(int currentTeam)
        {
            foreach (string message in old_messages[currentTeam])
            {
                _communicationBox.AppendText(message + "\r\n");
                _communicationBox.SelectionStart = _communicationBox.TextLength;
                _communicationBox.ScrollToCaret();
            }
        }

        #endregion

        #region Minimap

        // If _defaultResource = 1 => National Picnic Day Theme
        // If _defaultResource = 2 => Christmas Theme
        // In each round, we redraw the Minimap
        // In the property, we get the robot's minimap-view in stringified format
        public void RefreshMinimap(string data)
        {
            _minimapPanel.Controls.Clear();
            _minimapPanel.RowStyles.Clear();
            _minimapPanel.ColumnStyles.Clear();

            _minimapPanel.RowCount = data.Split("=").Length - 1;
            _minimapPanel.ColumnCount = data.Split("=")[0].Split("$").Length - 1;

            int rows = data.Split("=").Length - 1;
            int columns = data.Split("=")[0].Split("$").Length - 1;

            for (int row = 0; row < rows; row++)
            {
                string line = data.Split("=")[row];
                for (int col = 0; col < columns; col++)
                {
                    string[] field = line.Split("$")[col].Split("/");

                    PictureBox picture = new PictureBox
                    {
                        Name = "pictureBox" + (row + col),
                        BorderStyle = BorderStyle.None,
                        Size = new Size(80, 80),
                        Visible = true
                    };

                    switch (field[0])
                    {
                        case "ND":
                            picture.BackgroundImage = resources.black;
                            break;
                        case "W":
                            if (_defaultResource == 1)
                            {
                                picture.BackgroundImage = picnic.water;
                            }
                            else
                            {
                                picture.BackgroundImage = resources.water01;
                            }
                            break;
                        case "E":
                            if (_defaultResource == 1)
                            {
                                picture.BackgroundImage = picnic.floor;
                            }
                            else if (_defaultResource == 2)
                            {
                                picture.BackgroundImage = resources.floor;
                            }
                            else
                            {
                                picture.BackgroundImage = resources.floor;
                            }
                            break;
                        case "L":
                            picture.BackgroundImage = resources.wall;
                            break;
                        case "D":
                            if (_defaultResource == 1)
                            {
                                picture.BackgroundImage = resources.door;
                            }
                            else
                            {
                                picture.BackgroundImage = resources.door;
                            }
                            break;
                        case "F":
                            if (_defaultResource == 1)
                            {
                                picture.BackgroundImage = picnic.freeze_field;
                            }
                            else if (_defaultResource == 2)
                            {
                                picture.BackgroundImage = resources.freeze_field;
                            }
                            else
                            {
                                picture.BackgroundImage = resources.freeze_field;
                            }
                            break;
                        default:
                            if (_defaultResource == 1)
                            {
                                picture.BackgroundImage = picnic.door;
                            }
                            else
                            {
                                picture.BackgroundImage = resources.door;
                            }
                            break;
                    }

                    if (field[1] == "ROBOT")
                    {
                        if (_defaultResource == 1)
                        {
                            picture.BackgroundImage = picnic.minimapLoc;
                        }
                        else if (_defaultResource == 2)
                        {
                            picture.BackgroundImage = christmas.minimapLoc;
                        }
                        else
                        {
                            picture.BackgroundImage = resources.relative_bot;
                        }
                    }

                    if (field[2] != "NoBox") //BOX*Purple
                    {
                        picture.BackgroundImage = GetMatchingColor(field[2].Split("*")[1], 1);
                    }

                    picture.BackgroundImageLayout = ImageLayout.Stretch;
                    picture.Dock = DockStyle.Fill;
                    picture.Margin = new Padding(0);

                    _minimapPanel.Controls.Add(picture, col, row);
                }
            }

            for (Int32 i = 0; i < rows; i++)
            {
                _minimapPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 1 / Convert.ToSingle(rows)));
            }
            for (Int32 j = 0; j < columns; j++)
            {
                _minimapPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1 / Convert.ToSingle(columns)));
            }
        }

        #endregion

        #region Table

        public void GenerateTable(string data)
        {
            int R = _gamerView;

            _tableLayoutGrid.RowCount = rowCol;
            _tableLayoutGrid.ColumnCount = rowCol;

            int rows = rowCol;
            int columns = rowCol;

            if (firstGenerated == true)
            {
                _tableLayoutGrid.Controls.Clear();
                _tableLayoutGrid.RowStyles.Clear();
                _tableLayoutGrid.ColumnStyles.Clear();

                _tableLayoutGrid.RowCount = rowCol;
                _tableLayoutGrid.ColumnCount = rowCol;
            }


            string[] jatekos = data.Split('+');

            string playerDir = jatekos[0].Split('_')[3];

            int delete = data.IndexOf('+');

            data = data.Remove(0, delete + 1);

            string[] sorok = data.Split('%');

            string[,] table_ = new string[rows, columns];

            for (int i = 0; i < columns; i++)
            {
                string[] _line = sorok[i].Split("!");
                for (int j = 0; j < rows; j++)
                {
                    table_[i, j] = _line[j];
                }
            }

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    string[] elem = table_[row, col].Split(":");

                    if (firstGenerated == false)
                    {
                        TableLayoutPanel tlp = _tableLayoutGrid;

                        foreach (PictureBox image in tlp.Controls)
                        {
                            if (image.Name == ("ptr_" + row + "_" + col))
                            {
                                switch (elem[0])
                                {
                                    case "ND":
                                        image.BackgroundImage = resources.black;
                                        break;
                                    case "W":
                                        if (_defaultResource == 1)
                                        {
                                            image.BackgroundImage = picnic.water;
                                        }
                                        else
                                        {
                                            image.BackgroundImage = resources.water01;
                                        }
                                        break;
                                    case "E":
                                        if (_defaultResource == 1)
                                        {
                                            image.BackgroundImage = picnic.floor;
                                        }
                                        else if (_defaultResource == 2)
                                        {
                                            image.BackgroundImage = resources.floor;
                                        }
                                        else
                                        {
                                            image.BackgroundImage = resources.floor;
                                        }
                                        break;
                                    case "D":
                                        if (_defaultResource == 1)
                                        {
                                            image.BackgroundImage = picnic.door;
                                        }
                                        else
                                        {
                                            image.BackgroundImage = resources.door;
                                        }
                                        break;
                                    case "L":
                                        image.BackgroundImage = resources.wall;
                                        break;
                                    case "F":
                                        if (_defaultResource == 1)
                                        {
                                            image.BackgroundImage = picnic.freeze_field;
                                        }
                                        else if (_defaultResource == 2)
                                        {
                                            image.BackgroundImage = resources.freeze_field;
                                        }
                                        else
                                        {
                                            image.BackgroundImage = resources.freeze_field;
                                        }
                                        break;
                                    default:
                                        if (_defaultResource == 1)
                                        {
                                            image.BackgroundImage = picnic.floor;
                                        }
                                        else
                                        {
                                            image.BackgroundImage = resources.floor;
                                        }
                                        break;
                                }

                                if (elem[1] != "NP" && elem[0] != "ND")
                                {
                                    int teamColor = Convert.ToInt32(elem[2]);
                                    switch (elem[3])
                                    {
                                        case "Up":
                                            image.BackgroundImage.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                                            break;
                                        case "Down":
                                            image.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                            break;
                                        case "Left":
                                            image.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                            break;
                                        case "Right":
                                            image.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                            break;
                                    }
                                    image.BackgroundImage = GetMatchingTeam(teamColor + 1);
                                }

                                if (row == R && col == R)
                                {
                                    image.BackgroundImage = GetMatchingTeam(teamNumberLocal);

                                    switch (playerDir)
                                    {
                                        case "Up":
                                            image.BackgroundImage.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                                            break;
                                        case "Down":
                                            image.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                            break;
                                        case "Left":
                                            image.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                            break;
                                        case "Right":
                                            image.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                            break;
                                    }
                                }
                                else if ((elem[0] == "E" || elem[0] == "DP" || elem[0] == "D" || elem[0] == "F") && elem[1] != "Player" && elem[2] != "NoBox" && GetMatchingColor(elem[2], 1) != resources.black) //Doboz
                                {
                                    string color = elem[2].Split("-")[0];
                                    image.BackgroundImage = GetMatchingColor(color, Convert.ToInt32(elem[2].Split("-")[1]));
                                }
                            }
                        }
                    }
                    else
                    {
                        PictureBox picture = new PictureBox
                        {
                            Name = "ptr_" + row + "_" + col,
                            BorderStyle = BorderStyle.None,
                            Size = new Size(80, 80),
                            Visible = true
                        };

                        switch (elem[0])
                        {
                            case "ND":
                                picture.BackgroundImage = resources.black;
                                break;
                            case "W":
                                if (_defaultResource == 1)
                                {
                                    picture.BackgroundImage = picnic.water;
                                }
                                else
                                {
                                    picture.BackgroundImage = resources.water01;
                                }
                                break;
                            case "E":
                                if (_defaultResource == 1)
                                {
                                    picture.BackgroundImage = picnic.floor;
                                }
                                else if (_defaultResource == 2)
                                {
                                    picture.BackgroundImage = resources.floor;
                                }
                                else
                                {
                                    picture.BackgroundImage = resources.floor;
                                }
                                break;
                            case "D":
                                if (_defaultResource == 1)
                                {
                                    picture.BackgroundImage = picnic.door;
                                }
                                else
                                {
                                    picture.BackgroundImage = resources.door;
                                }
                                break;
                            case "L":
                                picture.BackgroundImage = resources.wall;
                                break;
                            case "F":
                                if (_defaultResource == 1)
                                {
                                    picture.BackgroundImage = picnic.freeze_field;
                                }
                                else if (_defaultResource == 2)
                                {
                                    picture.BackgroundImage = resources.freeze_field;
                                }
                                else
                                {
                                    picture.BackgroundImage = resources.freeze_field;
                                }
                                break;
                            default:
                                if (_defaultResource == 1)
                                {
                                    picture.BackgroundImage = picnic.floor; //DropPlace
                                }
                                else
                                {
                                    picture.BackgroundImage = resources.floor; //DropPlace
                                }
                                break;
                        }

                        if (elem[1] != "NP" && elem[0] != "ND")
                        {
                            int teamColor = Convert.ToInt32(elem[2]);
                            switch (elem[3])
                            {
                                case "Up":
                                    picture.BackgroundImage.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                                    break;
                                case "Down":
                                    picture.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                    break;
                                case "Left":
                                    picture.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    break;
                                case "Right":
                                    picture.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                    break;
                            }
                            picture.BackgroundImage = GetMatchingTeam(teamColor + 1);
                        }

                        if (row == R && col == R)
                        {
                            picture.BackgroundImage = GetMatchingTeam(teamNumberLocal);

                            switch (playerDir)
                            {
                                case "Up":
                                    picture.BackgroundImage.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                                    break;
                                case "Down":
                                    picture.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                    break;
                                case "Left":
                                    picture.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    break;
                                case "Right":
                                    picture.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                    break;
                            }
                        }
                        else if ((elem[0] == "E" || elem[0] == "DP" || elem[0] == "D" || elem[0] == "F") && elem[1] != "Player" && elem[2] != "NoBox" && GetMatchingColor(elem[2], 1) != resources.black) //Doboz
                        {
                            string color = elem[2].Split("-")[0];
                            picture.BackgroundImage = GetMatchingColor(color, Convert.ToInt32(elem[2].Split("-")[1]));
                        }

                        picture.BackgroundImageLayout = ImageLayout.Stretch;
                        picture.Dock = DockStyle.Fill;
                        picture.Margin = new Padding(0);

                        _tableLayoutGrid.Controls.Add(picture, col, row);

                    }
                }
            }

            if (firstGenerated == true)
            {
                for (Int32 i = 0; i < rows; i++)
                {
                    _tableLayoutGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 1 / Convert.ToSingle(rows)));
                }
                for (Int32 j = 0; j < columns; j++)
                {
                    _tableLayoutGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1 / Convert.ToSingle(columns)));
                }
                firstGenerated = false;
            }

            RefreshMinimap(data.Split("#")[1]);
        }

        #endregion

        #region Get colors for Pictures

        // If _defaultResource = 1 => National Picnic Day Theme
        // If _defaultResource = 2 => Christmas Theme
        public Bitmap GetMatchingColor(TaskElement box)
        {
            if (box == null)
            {
                return resources.black;
            }
            if (_defaultResource == 1)
            {
                switch (box.Color)
                {
                    case Colors.Red: return picnic.red;
                    case Colors.Yellow: return picnic.yellow;
                    case Colors.Purple: return picnic.purple;
                    case Colors.Blue: return picnic.blue;
                    case Colors.Brown: return picnic.brown;
                    case Colors.Green: return picnic.green;
                    default: return resources.black;
                }
            }
            else if (_defaultResource == 2)
            {
                switch (box.Color)
                {
                    case Colors.Red: return christmas.red;
                    case Colors.Yellow: return christmas.yellow;
                    case Colors.Purple: return christmas.purple;
                    case Colors.Blue: return christmas.blue;
                    case Colors.Brown: return christmas.brown;
                    case Colors.Green: return christmas.green;
                    default: return resources.black;
                }
            }
            else
            {
                switch (box.Color)
                {
                    case Colors.Red: return resources.red;
                    case Colors.Yellow: return resources.yellow;
                    case Colors.Purple: return resources.purple;
                    case Colors.Blue: return resources.blue;
                    case Colors.Brown: return resources.brown;
                    case Colors.Green: return resources.green;
                    default: return resources.black;
                }
            }
        }

        // Size will decide if the box is being picked
        // Or it is in the tasks view
        public Bitmap GetMatchingColor(string color, int size)
        {
            if (_defaultResource == 1)
            {
                if (size == 1)
                {
                    switch (color)
                    {
                        case "Red": return picnic.red_picked;
                        case "Yellow": return picnic.yellow_picked;
                        case "Purple": return picnic.purple_picked;
                        case "Blue": return picnic.blue_picked;
                        case "Brown": return picnic.brown_picked;
                        case "Green": return picnic.green_picked;
                        default: return resources.black;
                    }
                }

                switch (color)
                {
                    case "Red": return picnic.red;
                    case "Yellow": return picnic.yellow;
                    case "Purple": return picnic.purple;
                    case "Blue": return picnic.blue;
                    case "Brown": return picnic.brown;
                    case "Green": return picnic.green;
                    default: return resources.black;
                }
            }
            else if (_defaultResource == 2)
            {
                if (size == 1)
                {
                    switch (color)
                    {
                        case "Red": return christmas.red_picked;
                        case "Yellow": return christmas.yellow_picked;
                        case "Purple": return christmas.purple_picked;
                        case "Blue": return christmas.blue_picked;
                        case "Brown": return christmas.brown_picked;
                        case "Green": return christmas.green_picked;
                        default: return resources.black;
                    }
                }

                switch (color)
                {
                    case "Red": return christmas.red;
                    case "Yellow": return christmas.yellow;
                    case "Purple": return christmas.purple;
                    case "Blue": return christmas.blue;
                    case "Brown": return christmas.brown;
                    case "Green": return christmas.green;
                    default: return resources.black;
                }
            }
            else
            {
                if (size == 1)
                {
                    switch (color)
                    {
                        case "Red": return resources.red;
                        case "Yellow": return resources.yellow;
                        case "Purple": return resources.purple;
                        case "Blue": return resources.blue;
                        case "Brown": return resources.brown;
                        case "Green": return resources.green;
                        default: return resources.black;
                    }
                }

                switch (color)
                {
                    case "Red": return resources.box_red;
                    case "Yellow": return resources.box_yellow;
                    case "Purple": return resources.box_purple;
                    case "Blue": return resources.box_blue;
                    case "Brown": return resources.box_brown;
                    case "Green": return resources.box_green;
                    default: return resources.black;
                }
            }
        }

        public Bitmap GetMatchingTeam(int teamNumber)
        {
            if (_defaultResource == 1) // Picnic
            {
                switch (teamNumber)
                {
                    case 1: return picnic.team01;
                    case 2: return picnic.team02;
                    case 3: return picnic.team03;
                    case 4: return picnic.team04;
                    case 5: return picnic.team06;
                    case 6: return picnic.team06;
                    default: return picnic.team06;
                }
            }
            else if (_defaultResource == 2) // Christmas
            {
                switch (teamNumber)
                {
                    case 1: return christmas.team01;
                    case 2: return christmas.team02;
                    case 3: return christmas.team03;
                    case 4: return christmas.team04;
                    case 5: return christmas.team05;
                    case 6: return christmas.team06;
                    default: return christmas.team06;
                }
            }
            else
            {
                switch (teamNumber)
                {
                    case 1: return resources.team01;
                    case 2: return resources.team02;
                    case 3: return resources.team03;
                    case 4: return resources.team04;
                    case 5: return resources.team05;
                    case 6: return resources.team06;
                    default: return resources.team06;
                }
            }
        }

        #endregion

        #region Process commands and Communicator messages

        public void SendMessage(string message)
        {
            if (message.StartsWith("@master")) // This is the user's commnand
            {
                commands.Add(message);
                string[] temp = message.Split("_");
                int team = currentTeam;
                int robotN = 0;
                foreach (var robots in gm.Robots[team])
                {
                    if (robots.Name == temp[2])
                    {
                        robotN = gm.Robots[team].IndexOf(robots);
                    }
                }

                string merged = "";
                if (temp.Length == 6) // Attach / Detach
                {
                    merged = temp[3] + "_" + temp[4] + "_" + temp[5];
                }
                else if (temp.Length == 5) // Regular command
                {
                    merged = temp[3] + "_" + temp[4];
                }
                else //Wait and Freeze
                {
                    merged = temp[3];
                }

                if (merged == "") tempMessage++;

                _dictionary.Add((team, robotN), requests[merged]);
            }
            else // Player sent a message to Communicator
            {
                _communicationBox.AppendText(message + "\r\n");
                _communicationBox.SelectionStart = _communicationBox.TextLength;
                _communicationBox.ScrollToCaret();

                new_messages[currentTeam].Add(message);
            }
        }

        #endregion

        #region Communicator
        private void communicatorSend_Click(object sender, EventArgs e)
        {
            string finalMessage = "> " + name + ": ";

            if (_blueCheck.Checked)
            {
                finalMessage += "Viszem a Kék dobozt; ";
                _blueCheck.Checked = false;
            }
            if (_yellowCheck.Checked)
            {
                finalMessage += "Viszem a Sárga dobozt; ";
                _yellowCheck.Checked = false;
            }
            if (_redCheck.Checked)
            {
                finalMessage += "Viszem a Piros dobozt; ";
                _redCheck.Checked = false;
            }
            if (_greenCheck.Checked)
            {
                finalMessage += "Viszem a Zöld dobozt; ";
                _greenCheck.Checked = false;
            }
            if (_purpleCheck.Checked)
            {
                finalMessage += "Viszem a Lila dobozt; ";
                _purpleCheck.Checked = false;
            }
            if (_brownCheck.Checked)
            {
                finalMessage += "Viszem a Barna dobozt; ";
                _brownCheck.Checked = false;
            }
            if (_upCheck.Checked)
            {
                finalMessage += "A Fenti kijárathoz megyek; ";
                _upCheck.Checked = false;
            }
            if (_downCheck.Checked)
            {
                finalMessage += "A Lenti kijárathoz megyek; ";
                _downCheck.Checked = false;
            }
            if (_leftCheck.Checked)
            {
                finalMessage += "A Bal oldali kijárathoz megyek; ";
                _leftCheck.Checked = false;
            }
            if (_rightCheck.Checked)
            {
                finalMessage += "A Jobb oldali kijárathoz megyek; ";
                _rightCheck.Checked = false;
            }
            finalMessage += "";
            SendMessage(finalMessage);
        }

        private void _taskOneButton_Click(object sender, EventArgs e)
        {
            SendMessage("> " + name + ": Az Első feladatot oldom!");
        }

        private void _tasktwoButton_Click(object sender, EventArgs e)
        {
            SendMessage("> " + name + ": A Második feladatot oldom!");
        }
        #endregion

        #region Player Events
        private void upButton_Click(object sender, EventArgs e)
        {
            round = 1;
            switch (_additionalOption + num)
            {
                // Clear the Field
                case 1:
                    EnableControls();
                    temps[tempMessage++] = "> Északi mező tisztítása...";
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_É_Clean");
                    round = 1;
                    SoundPlayer simpleSound = new SoundPlayer(path);
                    simpleSound.PlaySync();
                    break;

                // Pick up the Box
                case 2:
                    EnableControls();
                    temps[tempMessage++] = "> Északi doboz felvétele...";
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_É_PickUp");
                    break;

                // Put down the Box
                case 3:
                    EnableControls();
                    temps[tempMessage++] = "> Északi doboz lerakása...";
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_É_Drop");
                    break;

                // We need two coordinates to Connect and Disconnect two boxes
                // case 4 gets the first coordinate
                case 4:
                    save_coordinate = "@master_" + teamNumberLocal + "_" + name + "_É";
                    if (attachDetach == 1)
                    {
                        num = 1;
                    }
                    else if (attachDetach == 2)
                    {
                        num = 2;
                    }
                    break;

                // Attach boxes
                case 5:
                    EnableControls();
                    temps[tempMessage++] = "> Dobozok összekapcsolása...";
                    save_coordinate += "_É_Attach";
                    SendMessage(save_coordinate);
                    attachDetach = 0;
                    num = 0;
                    save_coordinate = "";
                    break;

                // Detach boxes
                case 6:
                    EnableControls();
                    temps[tempMessage++] = "> Dobozok szétválasztása...";
                    save_coordinate += "_É_Detach";
                    SendMessage(save_coordinate);
                    attachDetach = 0;
                    num = 0;
                    save_coordinate = "";
                    break;

                // Step
                default:
                    EnableControls();
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_É_Step");
                    temps[tempMessage++] = "> Lépés Észak irányba...";
                    break;
            }
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            round = 1;
            switch (_additionalOption + num)
            {
                // Clear the Field
                case 1:
                    EnableControls();
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_NY_Clean");
                    temps[tempMessage++] = "> Nyugati mező tisztítása...";
                    SoundPlayer simpleSound = new SoundPlayer(path);
                    simpleSound.PlaySync(); 
                    break;

                // Pick up the Box
                case 2:
                    EnableControls();
                    temps[tempMessage++] = "> Nyugati doboz felvétele...";
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_NY_PickUp");
                    break;

                // Put down the Box
                case 3:
                    EnableControls();
                    temps[tempMessage++] = "> Nyugati doboz lerakása...";
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_NY_Drop");
                    break;

                // We need two coordinates to Connect and Disconnect two boxes
                // case 4 gets the first coordinate
                case 4:
                    save_coordinate = "@master_" + teamNumberLocal + "_" + name + "_NY";
                    if (attachDetach == 1)
                    {
                        num = 1;
                    }
                    else if (attachDetach == 2)
                    {
                        num = 2;
                    }
                    break;

                // Attach boxes
                case 5:
                    EnableControls();
                    temps[tempMessage++] = "> Dobozok összekapcsolása...";
                    save_coordinate += "_NY_Attach";
                    SendMessage(save_coordinate);
                    attachDetach = 0;
                    num = 0;
                    save_coordinate = "";
                    break;

                // Detach boxes
                case 6:
                    EnableControls();
                    temps[tempMessage++] = "> Dobozok szétválasztása...";
                    save_coordinate += "_NY_Detach";
                    SendMessage(save_coordinate);
                    attachDetach = 0;
                    num = 0;
                    save_coordinate = "";
                    break;

                // Step
                default:
                    EnableControls();
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_NY_Step");
                    temps[tempMessage++] = "> Lépés Nyugat irányba...";
                    break;
            }
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            round = 1;
            switch (_additionalOption + num)
            {
                // Clear the Field
                case 1:
                    EnableControls();
                    temps[tempMessage++] = "> Déli mező tisztítása...";
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_D_Clean");
                    round = 1;
                    SoundPlayer simpleSound = new SoundPlayer(path);
                    simpleSound.PlaySync(); 
                    break;

                // Pick up the Box
                case 2:
                    EnableControls();
                    temps[tempMessage++] = "> Déli doboz felvétele...";
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_D_PickUp");
                    break;

                // Put down the Box
                case 3:
                    EnableControls();
                    temps[tempMessage++] = "> Déli doboz lerakása...";
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_D_Drop");
                    break;

                // We need two coordinates to Connect and Disconnect two boxes
                // case 4 gets the first coordinate
                case 4:
                    save_coordinate = "@master_" + teamNumberLocal + "_" + name + "_D";
                    if (attachDetach == 1)
                    {
                        num = 1;
                    }
                    else if (attachDetach == 2)
                    {
                        num = 2;
                    }
                    break;

                // Attach Boxes
                case 5:
                    temps[tempMessage++] = "> Dobozok összekapcsolása...";
                    EnableControls();
                    save_coordinate += "_D_Attach";
                    SendMessage(save_coordinate);
                    attachDetach = 0;
                    num = 0;
                    save_coordinate = "";
                    break;

                // Detach Boxes
                case 6:
                    temps[tempMessage++] = "> Dobozok szétválasztása...";
                    EnableControls();
                    save_coordinate += "_D_Detach";
                    SendMessage(save_coordinate);
                    attachDetach = 0;
                    num = 0;
                    save_coordinate = "";
                    break;

                // Step
                default:
                    EnableControls();
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_D_Step");
                    temps[tempMessage++] = "> Lépés Dél irányba...";
                    break;
            }
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            round = 1;
            switch (_additionalOption + num)
            {
                // Clear the Field
                case 1:
                    EnableControls();
                    temps[tempMessage++] = "> Keleti mező tisztítása...";
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_K_Clean");
                    round = 1;
                    SoundPlayer simpleSound = new SoundPlayer(path);
                    simpleSound.PlaySync(); 
                    break;

                // Pick up the Box
                case 2:
                    EnableControls();
                    temps[tempMessage++] = "> Keleti doboz felvétele...";
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_K_PickUp");
                    break;

                // Put down the Box
                case 3:
                    EnableControls();
                    temps[tempMessage++] = "> Keleti doboz lerakása...";
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_K_Drop");
                    break;

                // We need two coordinates to Connect and Disconnect two boxes
                // case 4 gets the first coordinate
                case 4:
                    save_coordinate = "@master_" + teamNumberLocal + "_" + name + "_K";
                    if (attachDetach == 1)
                    {
                        num = 1;
                    }
                    else if (attachDetach == 2)
                    {
                        num = 2;
                    }
                    break;

                // Attach Boxes
                case 5:
                    EnableControls();
                    temps[tempMessage++] = "> Dobozok összekapcsolása...";
                    save_coordinate += "_K_Attach";
                    SendMessage(save_coordinate);
                    attachDetach = 0;
                    num = 0;
                    save_coordinate = "";
                    break;

                // Detach Boxes
                case 6:
                    EnableControls();
                    temps[tempMessage++] = "> Dobozok szétválasztása...";
                    save_coordinate += "_K_Detach";
                    SendMessage(save_coordinate);
                    attachDetach = 0;
                    num = 0;
                    save_coordinate = "";
                    break;

                // Step
                default:
                    EnableControls();
                    SendMessage("@master_" + teamNumberLocal + "_" + name + "_K_Step");
                    temps[tempMessage++] = "> Lépés Kelet irányba...";
                    break;
            }
        }

        // Rotate to Left
        private void turnLeftButton_Click(object sender, EventArgs e)
        {
            round = 1;
            temps[tempMessage++] = "> Forgás Balra...";
            SendMessage("@master_" + teamNumberLocal + "_" + name + "_L_Rotate");
            EnableControls();
        }

        // Rotate to Right
        private void turnRightButton_Click(object sender, EventArgs e)
        {
            round = 1;
            temps[tempMessage++] = "> Forgás Jobbra...";
            SendMessage("@master_" + teamNumberLocal + "_" + name + "_R_Rotate");
            EnableControls();
        }

        // Freeze the enemy
        private void FreezeButton_Click(object sender, EventArgs e)
        {
            round = 1;
            temps[tempMessage++] = "> Fagysztás!...";
            SendMessage("@master_" + teamNumberLocal + "_" + name + "_Freeze");
            EnableControls();
        }

        // If we want to clean a Field, we set the _additionalOption to 1
        // And with the arrow buttons, we can set the proper coordinate where to clean
        private void cleanButton_Click(object sender, EventArgs e)
        {
            _additionalOption = 1;
            DisableControls();
        }

        // If we want to pick up a Box, we set the _additionalOption to 1
        // And with the arrow buttons, we can set the proper coordinate where to pick up the Box
        private void pickUpButton_Click(object sender, EventArgs e)
        {
            _additionalOption = 2;
            DisableControls();
        }

        // If we want to put down a Box, we set the _additionalOption to 2
        // And with the arrow buttons, we can set the proper coordinate where to put down the Box
        private void dropButton_Click(object sender, EventArgs e)
        {
            _additionalOption = 3;
            DisableControls();
        }

        // If we want to attach two Boxes, we set the _additionalOption to 4
        // and the attachDetach variable to 1
        // With the arrow buttons, we can set the proper coordinates
        private void attachButton_Click(object sender, EventArgs e)
        {
            attachDetach = 1;
            _additionalOption = 4;
            DisableControls();
        }

        // If we want to detach two Boxes, we set the _additionalOption to 4
        // and the attachDetach variable to 2
        // With the arrow buttons, we can set the proper coordinates
        private void detachButton_Click(object sender, EventArgs e)
        {
            attachDetach = 2;
            _additionalOption = 4;
            DisableControls();
        }

        // Disable all buttons except Up, Down, Left and Right
        private void DisableControls()
        {
            turnLeftButton.Enabled = false;
            turnRightButton.Enabled = false;
            attachButton.Enabled = false;
            pickUpButton.Enabled = false;
            throwButton.Enabled = false;
            detachButton.Enabled = false;
            freezeButton.Enabled = false;
            cleanButton.Enabled = false;
            turnLeftButton.BackgroundImage = resources.button;
            turnRightButton.BackgroundImage = resources.button;
            attachButton.BackgroundImage = resources.button;
            pickUpButton.BackgroundImage = resources.button;
            throwButton.BackgroundImage = resources.button;
            detachButton.BackgroundImage = resources.button;
            freezeButton.BackgroundImage = resources.button;
            cleanButton.BackgroundImage = resources.button;
        }

        // Round variable helps to decide whether we choose Attach or Detach as a command
        // If round is 0, we can re-enable all the buttons
        // Otherwise this will not reenable the buttons
        private void EnableControls()
        {
            if (round == 0)
            {
                _additionalOption = 0;
                turnLeftButton.Enabled = true;
                turnRightButton.Enabled = true;
                attachButton.Enabled = true;
                pickUpButton.Enabled = true;
                throwButton.Enabled = true;
                detachButton.Enabled = true;
                freezeButton.Enabled = true;
                cleanButton.Enabled = true;
                upButton.Enabled = true;
                downButton.Enabled = true;
                leftButton.Enabled = true;
                rightButton.Enabled = true;
                turnLeftButton.BackgroundImage = resources.rotateleft;
                turnRightButton.BackgroundImage = resources.rotateright;
                attachButton.BackgroundImage = resources.connect;
                pickUpButton.BackgroundImage = resources.attach;
                throwButton.BackgroundImage = resources.detach;
                detachButton.BackgroundImage = resources.disconnect;
                freezeButton.BackgroundImage = resources.freeze;
                cleanButton.BackgroundImage = resources.sweep;
                upButton.BackgroundImage = resources.up;
                leftButton.BackgroundImage = resources.left;
                downButton.BackgroundImage = resources.down;
                rightButton.BackgroundImage = resources.right;
            }
            else
            {
                _additionalOption = 0;
                turnLeftButton.Enabled = false;
                turnRightButton.Enabled = false;
                attachButton.Enabled = false;
                pickUpButton.Enabled = false;
                throwButton.Enabled = false;
                detachButton.Enabled = false;
                freezeButton.Enabled = false;
                cleanButton.Enabled = false;
                upButton.Enabled = false;
                downButton.Enabled = false;
                leftButton.Enabled = false;
                rightButton.Enabled = false;
                turnLeftButton.BackgroundImage = resources.button;
                turnRightButton.BackgroundImage = resources.button;
                attachButton.BackgroundImage = resources.button;
                pickUpButton.BackgroundImage = resources.button;
                throwButton.BackgroundImage = resources.button;
                detachButton.BackgroundImage = resources.button;
                freezeButton.BackgroundImage = resources.button;
                cleanButton.BackgroundImage = resources.button;
                upButton.BackgroundImage = resources.button;
                leftButton.BackgroundImage = resources.button;
                downButton.BackgroundImage = resources.button;
                rightButton.BackgroundImage = resources.button;
            }
        }

        #endregion

        #region Help Buttons

        // Hide view while the game is paused
        public void PauseResumeButton(object sender, EventArgs e)
        {
            if (!stoppedTimer)
            {
                _tableLayoutGrid.RowCount = rowCol;
                _tableLayoutGrid.ColumnCount = rowCol;

                for (int row = 0; row < rowCol; row++)
                {
                    for (int col = 0; col < rowCol; col++)
                    {
                        TableLayoutPanel tlp = _tableLayoutGrid;

                        foreach (PictureBox image in tlp.Controls)
                        {
                            if (image.Name == ("ptr_" + row + "_" + col))
                            {
                                image.BackgroundImage = resources.black;
                            }
                        }
                    }
                }
                foreach (PictureBox elem in _minimapPanel.Controls)
                {
                    elem.BackgroundImage = resources.black;
                }

                timer.Stop();
                stoppedTimer = true;
            }
            else
            {
                GenerateTable(gm.RobotStringify(gm.Robots[currentTeam][currentPlayer]));
                timer.Start();
                stoppedTimer = false;
            }
        }

        // See the full map
        public void ViewerButton(object sender, EventArgs e)
        {
            ViewerForm vf = new ViewerForm();
            vf.Show();
        }

        // How to Play menu
        public void HowToPlayButton(object sender, EventArgs e)
        {
            HowToPlayForm htpf = new HowToPlayForm();
            htpf.Show();
        }
        #endregion

        #region Hover Button effect
        private void OnHover(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button.FlatAppearance.BorderSize = 3;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        private void OnLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.FlatAppearance.BorderSize = 0;
        }
        #endregion

        #region Close Form
        private void GamerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (innerclose == false)
            {
                if (!_remember && _clicked)
                {
                    var secure = MessageBox.Show("Biztosan ki szeretne lépni a játékból?",
                                        "Kilépés",
                                         MessageBoxButtons.YesNo);
                    if (secure == DialogResult.Yes)
                    {
                        _remember = true;
                        SaveFile.ClearFile();
                        Environment.Exit(0);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
        #endregion
    }
}
