using Persistence;

namespace robot_fight.View
{
    public partial class ViewerForm : Form
    {
        #region Variables

        // Close the form variables
        private bool _clicked = true;
        private bool _remember = false;

        // Timer
        private System.Windows.Forms.Timer _timer = null!;

        // Check if Form is already generated
        private bool firstGenerated = true;
        #endregion

        #region Initialize ViewerForm
        public ViewerForm()
        {
            InitializeComponent();

            _timer = new System.Windows.Forms.Timer();
            _timer.Tick += new EventHandler(DrawTable);
            _timer.Interval = 3000;
            _timer.Start();
            
            DrawTable(null, new EventArgs());
        }
        #endregion

        #region Draw Viewer Table from Persistence table.txt

        // If firstGenerated is true, we need to add the Controls to Panel
        // And set the Panels width and height; otherwise, we can get the pictures
        // By its names, don't have to render much
        private void DrawTable(object? sender, EventArgs e)
        {
            (bool, string) _readed = SaveFile.ReadFromTable();
            if (_readed.Item1 == false && !firstGenerated) return;

            string getTableString = _readed.Item2;

            int width = getTableString.Split("%").Length-1;
            int height = getTableString.Split("%")[0].Split("!").Length-1;

            string[] sorok = getTableString.Split("%");

            _tableLayoutGrid.RowCount = width;
            _tableLayoutGrid.ColumnCount = height;

            if (firstGenerated == true)
            {
                _tableLayoutGrid.Controls.Clear();
                _tableLayoutGrid.RowStyles.Clear();
                _tableLayoutGrid.ColumnStyles.Clear();

                _tableLayoutGrid.RowCount = width;
                _tableLayoutGrid.ColumnCount = height;
            }

            string[,] table_ = new string[width, height];

            for (int i = 0; i < width; i++)
            {
                string[] _line = sorok[i].Split("!");
                for (int j = 0; j < height; j++)
                {
                    table_[i, j] = _line[j];
                }
            }

            for (int row = 0; row < width; row++)
            {
                for (int col = 0; col < height; col++)
                {
                    string[] field = table_[row, col].Split(":");

                    if (firstGenerated == false)
                    {
                        TableLayoutPanel tlp = _tableLayoutGrid;

                        foreach (PictureBox image in tlp.Controls)
                        {
                            if (image.Name == ("ptr_" + row + "_" + col))
                            {
                                switch (field[0])
                                {
                                    case "ND":
                                        image.BackgroundImage = resources.black;
                                        break;
                                    case "W":
                                        image.BackgroundImage = resources.water01;
                                        break;
                                    case "E":
                                        image.BackgroundImage = resources.floor;
                                        break;
                                    case "D":
                                        image.BackgroundImage = resources.door;
                                        break;
                                    case "L":
                                        image.BackgroundImage = resources.wall;
                                        break;
                                    case "F":
                                        image.BackgroundImage = resources.freeze_field;
                                        break;
                                    case "DP":
                                        image.BackgroundImage = resources.floor;
                                        break;
                                    default:
                                        image.BackgroundImage = resources.floor;
                                        break;
                                }


                                // Is there a player in this coordinate
                                if (field[1] != "NP" && field[0] != "ND")
                                {
                                    int teamColor = Convert.ToInt32(field[2]);
                                    image.BackgroundImage = GetMatchingTeam(teamColor + 1);
                                    switch (field[3])
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

                                // Is there a Box in this coordinate
                                if ((field[0] == "E" || field[0] == "D" || field[0] == "DP") && field[1] != "Player" && field[2] != "NoBox" && GetMatchingColor(field[2], 1) != resources.black) //Doboz
                                {
                                    string color = field[2].Split("-")[0];
                                    int size = Convert.ToInt32(field[2].Split("-")[1]);

                                    image.BackgroundImage = GetMatchingColor(color, size);
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

                        switch (field[0])
                        {
                            case "ND":
                                picture.BackgroundImage = resources.black;
                                break;
                            case "W":
                                picture.BackgroundImage = resources.water01;
                                break;
                            case "E":
                                picture.BackgroundImage = resources.floor;
                                break;
                            case "D":
                                picture.BackgroundImage = resources.door;
                                break;
                            case "L":
                                picture.BackgroundImage = resources.wall;
                                break;
                            case "F":
                                picture.BackgroundImage = resources.freeze_field;
                                break;
                            default:
                                picture.BackgroundImage = resources.floor;
                                break;
                        }

                        // Is there a Player in this coordinate
                        if (field[1] != "NP" && field[0] != "ND")
                        {
                            int teamColor = Convert.ToInt32(field[2]);
                            picture.BackgroundImage = GetMatchingTeam(teamColor + 1);
                            switch (field[3])
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

                        // Is there a Box on this coordinate
                        if ((field[0] == "E" || field[0] == "D" || field[0] == "DP") && field[1] != "Player" && field[2] != "NoBox" && GetMatchingColor(field[2], 1) != resources.black) //Doboz
                        {
                            string color = field[2].Split("-")[0];
                            int size = Convert.ToInt32(field[2].Split("-")[1]);

                            picture.BackgroundImage = GetMatchingColor(color, size);
                        }

                        picture.BackgroundImageLayout = ImageLayout.Stretch;
                        picture.Dock = DockStyle.Fill;
                        picture.Margin = new Padding(0);

                        _tableLayoutGrid.Controls.Add(picture, col, row);
                    }
                }
            }

            if (firstGenerated)
            {
                for (Int32 i = 0; i < width; i++)
                {
                    _tableLayoutGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 1 / Convert.ToSingle(width)));
                }
                for (Int32 j = 0; j < height; j++)
                {
                    _tableLayoutGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1 / Convert.ToSingle(height)));
                }
                firstGenerated = false;
            }

            string getLogText = SaveFile.ReadFromLog();
            _globalLog.Text = "";
            _globalLog.AppendText(getLogText);
        }
        #endregion

        #region Get pictures for View

        public Bitmap GetMatchingColor(string color, int size)
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

        public Bitmap GetMatchingTeam(int teamNumber)
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
        #endregion

        #region Hide Button Event
        private void exitButton_Click(object sender, EventArgs e)
        { 
            this.Hide();
        }
        #endregion

        #region Close ViewerForm Event
        private void ViewerForm_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
