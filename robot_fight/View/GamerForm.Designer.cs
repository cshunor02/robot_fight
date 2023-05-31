namespace robot_fight.View
{
    partial class GamerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._communicationBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._tasktwoButton = new System.Windows.Forms.Button();
            this._taskOneButton = new System.Windows.Forms.Button();
            this._sendMessage = new System.Windows.Forms.Button();
            this._rightCheck = new System.Windows.Forms.CheckBox();
            this._leftCheck = new System.Windows.Forms.CheckBox();
            this._downCheck = new System.Windows.Forms.CheckBox();
            this._upCheck = new System.Windows.Forms.CheckBox();
            this._purpleCheck = new System.Windows.Forms.CheckBox();
            this._brownCheck = new System.Windows.Forms.CheckBox();
            this._redCheck = new System.Windows.Forms.CheckBox();
            this._yellowCheck = new System.Windows.Forms.CheckBox();
            this._blueCheck = new System.Windows.Forms.CheckBox();
            this._greenCheck = new System.Windows.Forms.CheckBox();
            this._localLogBox = new System.Windows.Forms.GroupBox();
            this._localLog = new System.Windows.Forms.TextBox();
            this._minimapPanel = new robot_fight.View.MyTableLayoutPanel();
            this._taskBox = new System.Windows.Forms.GroupBox();
            this._tasks = new System.Windows.Forms.TableLayoutPanel();
            this._task1Display = new robot_fight.View.MyTableLayoutPanel();
            this._task2Display = new robot_fight.View.MyTableLayoutPanel();
            this._task1Description = new robot_fight.View.MyTableLayoutPanel();
            this._task1Name = new System.Windows.Forms.Label();
            this._task1StepCount = new System.Windows.Forms.Label();
            this._task1Ertek = new System.Windows.Forms.Label();
            this._task2Description = new robot_fight.View.MyTableLayoutPanel();
            this._task2Ertek = new System.Windows.Forms.Label();
            this._task2StepCount = new System.Windows.Forms.Label();
            this._task2Name = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._minimap = new System.Windows.Forms.GroupBox();
            this._robotName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._remainingSteps = new System.Windows.Forms.Label();
            this._teamPoints = new System.Windows.Forms.Label();
            this._controlPanel = new System.Windows.Forms.GroupBox();
            this.detachButton = new System.Windows.Forms.Button();
            this.attachButton = new System.Windows.Forms.Button();
            this.throwButton = new System.Windows.Forms.Button();
            this.freezeButton = new System.Windows.Forms.Button();
            this.pickUpButton = new System.Windows.Forms.Button();
            this.cleanButton = new System.Windows.Forms.Button();
            this.turnRightButton = new System.Windows.Forms.Button();
            this.turnLeftButton = new System.Windows.Forms.Button();
            this.rightButton = new System.Windows.Forms.Button();
            this.leftButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this._pauseButton = new System.Windows.Forms.Button();
            this._helpButton = new System.Windows.Forms.Button();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._tableLayoutGrid = new robot_fight.View.MyTableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this._localLogBox.SuspendLayout();
            this._taskBox.SuspendLayout();
            this._tasks.SuspendLayout();
            this._task1Description.SuspendLayout();
            this._task2Description.SuspendLayout();
            this._minimap.SuspendLayout();
            this._controlPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::robot_fight.View.resources.communicator;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(6, 30);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(308, 400);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // _communicationBox
            // 
            this._communicationBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(135)))), ((int)(((byte)(20)))));
            this._communicationBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._communicationBox.Font = new System.Drawing.Font("Consolas", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._communicationBox.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this._communicationBox.Location = new System.Drawing.Point(45, 68);
            this._communicationBox.Margin = new System.Windows.Forms.Padding(2);
            this._communicationBox.Multiline = true;
            this._communicationBox.Name = "_communicationBox";
            this._communicationBox.ReadOnly = true;
            this._communicationBox.Size = new System.Drawing.Size(222, 96);
            this._communicationBox.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this._tasktwoButton);
            this.groupBox1.Controls.Add(this._taskOneButton);
            this.groupBox1.Controls.Add(this._sendMessage);
            this.groupBox1.Controls.Add(this._rightCheck);
            this.groupBox1.Controls.Add(this._leftCheck);
            this.groupBox1.Controls.Add(this._downCheck);
            this.groupBox1.Controls.Add(this._upCheck);
            this.groupBox1.Controls.Add(this._purpleCheck);
            this.groupBox1.Controls.Add(this._brownCheck);
            this.groupBox1.Controls.Add(this._redCheck);
            this.groupBox1.Controls.Add(this._yellowCheck);
            this.groupBox1.Controls.Add(this._blueCheck);
            this.groupBox1.Controls.Add(this._greenCheck);
            this.groupBox1.Controls.Add(this._communicationBox);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.ForeColor = System.Drawing.Color.Wheat;
            this.groupBox1.Location = new System.Drawing.Point(657, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(324, 439);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kommunikátor";
            // 
            // _tasktwoButton
            // 
            this._tasktwoButton.BackColor = System.Drawing.Color.SeaGreen;
            this._tasktwoButton.BackgroundImage = global::robot_fight.View.resources.task2;
            this._tasktwoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._tasktwoButton.FlatAppearance.BorderSize = 0;
            this._tasktwoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this._tasktwoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._tasktwoButton.Location = new System.Drawing.Point(206, 188);
            this._tasktwoButton.Margin = new System.Windows.Forms.Padding(2);
            this._tasktwoButton.Name = "_tasktwoButton";
            this._tasktwoButton.Size = new System.Drawing.Size(79, 80);
            this._tasktwoButton.TabIndex = 15;
            this._tasktwoButton.UseVisualStyleBackColor = false;
            this._tasktwoButton.Click += new System.EventHandler(this._tasktwoButton_Click);
            this._tasktwoButton.MouseEnter += new System.EventHandler(this.OnHover);
            this._tasktwoButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // _taskOneButton
            // 
            this._taskOneButton.BackColor = System.Drawing.Color.SeaGreen;
            this._taskOneButton.BackgroundImage = global::robot_fight.View.resources.task1;
            this._taskOneButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._taskOneButton.FlatAppearance.BorderSize = 0;
            this._taskOneButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this._taskOneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._taskOneButton.Location = new System.Drawing.Point(26, 188);
            this._taskOneButton.Margin = new System.Windows.Forms.Padding(2);
            this._taskOneButton.Name = "_taskOneButton";
            this._taskOneButton.Size = new System.Drawing.Size(79, 80);
            this._taskOneButton.TabIndex = 14;
            this._taskOneButton.UseVisualStyleBackColor = false;
            this._taskOneButton.Click += new System.EventHandler(this._taskOneButton_Click);
            this._taskOneButton.MouseEnter += new System.EventHandler(this.OnHover);
            this._taskOneButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // _sendMessage
            // 
            this._sendMessage.BackColor = System.Drawing.Color.SeaGreen;
            this._sendMessage.BackgroundImage = global::robot_fight.View.resources.send;
            this._sendMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._sendMessage.FlatAppearance.BorderSize = 0;
            this._sendMessage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this._sendMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._sendMessage.Location = new System.Drawing.Point(111, 184);
            this._sendMessage.Margin = new System.Windows.Forms.Padding(2);
            this._sendMessage.Name = "_sendMessage";
            this._sendMessage.Size = new System.Drawing.Size(89, 88);
            this._sendMessage.TabIndex = 13;
            this._sendMessage.UseVisualStyleBackColor = false;
            this._sendMessage.Click += new System.EventHandler(this.communicatorSend_Click);
            this._sendMessage.MouseEnter += new System.EventHandler(this.OnHover);
            this._sendMessage.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // _rightCheck
            // 
            this._rightCheck.AutoSize = true;
            this._rightCheck.BackColor = System.Drawing.Color.Transparent;
            this._rightCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rightCheck.ForeColor = System.Drawing.Color.Black;
            this._rightCheck.Location = new System.Drawing.Point(288, 342);
            this._rightCheck.Margin = new System.Windows.Forms.Padding(2);
            this._rightCheck.Name = "_rightCheck";
            this._rightCheck.Size = new System.Drawing.Size(17, 16);
            this._rightCheck.TabIndex = 12;
            this._rightCheck.UseVisualStyleBackColor = false;
            // 
            // _leftCheck
            // 
            this._leftCheck.AutoSize = true;
            this._leftCheck.BackColor = System.Drawing.Color.Transparent;
            this._leftCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._leftCheck.ForeColor = System.Drawing.Color.Black;
            this._leftCheck.Location = new System.Drawing.Point(229, 342);
            this._leftCheck.Margin = new System.Windows.Forms.Padding(2);
            this._leftCheck.Name = "_leftCheck";
            this._leftCheck.Size = new System.Drawing.Size(17, 16);
            this._leftCheck.TabIndex = 11;
            this._leftCheck.UseVisualStyleBackColor = false;
            // 
            // _downCheck
            // 
            this._downCheck.AutoSize = true;
            this._downCheck.BackColor = System.Drawing.Color.Transparent;
            this._downCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._downCheck.ForeColor = System.Drawing.Color.Black;
            this._downCheck.Location = new System.Drawing.Point(170, 342);
            this._downCheck.Margin = new System.Windows.Forms.Padding(2);
            this._downCheck.Name = "_downCheck";
            this._downCheck.Size = new System.Drawing.Size(17, 16);
            this._downCheck.TabIndex = 10;
            this._downCheck.UseVisualStyleBackColor = false;
            // 
            // _upCheck
            // 
            this._upCheck.AutoSize = true;
            this._upCheck.BackColor = System.Drawing.Color.Transparent;
            this._upCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._upCheck.ForeColor = System.Drawing.Color.Black;
            this._upCheck.Location = new System.Drawing.Point(110, 342);
            this._upCheck.Margin = new System.Windows.Forms.Padding(2);
            this._upCheck.Name = "_upCheck";
            this._upCheck.Size = new System.Drawing.Size(17, 16);
            this._upCheck.TabIndex = 9;
            this._upCheck.UseVisualStyleBackColor = false;
            // 
            // _purpleCheck
            // 
            this._purpleCheck.AutoSize = true;
            this._purpleCheck.BackColor = System.Drawing.Color.Transparent;
            this._purpleCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._purpleCheck.ForeColor = System.Drawing.Color.Black;
            this._purpleCheck.Location = new System.Drawing.Point(50, 344);
            this._purpleCheck.Margin = new System.Windows.Forms.Padding(2);
            this._purpleCheck.Name = "_purpleCheck";
            this._purpleCheck.Size = new System.Drawing.Size(17, 16);
            this._purpleCheck.TabIndex = 8;
            this._purpleCheck.UseVisualStyleBackColor = false;
            // 
            // _brownCheck
            // 
            this._brownCheck.AutoSize = true;
            this._brownCheck.BackColor = System.Drawing.Color.Transparent;
            this._brownCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._brownCheck.ForeColor = System.Drawing.Color.Black;
            this._brownCheck.Location = new System.Drawing.Point(284, 278);
            this._brownCheck.Margin = new System.Windows.Forms.Padding(2);
            this._brownCheck.Name = "_brownCheck";
            this._brownCheck.Size = new System.Drawing.Size(17, 16);
            this._brownCheck.TabIndex = 7;
            this._brownCheck.UseVisualStyleBackColor = false;
            // 
            // _redCheck
            // 
            this._redCheck.AutoSize = true;
            this._redCheck.BackColor = System.Drawing.Color.Transparent;
            this._redCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._redCheck.ForeColor = System.Drawing.Color.Black;
            this._redCheck.Location = new System.Drawing.Point(229, 278);
            this._redCheck.Margin = new System.Windows.Forms.Padding(2);
            this._redCheck.Name = "_redCheck";
            this._redCheck.Size = new System.Drawing.Size(17, 16);
            this._redCheck.TabIndex = 6;
            this._redCheck.UseVisualStyleBackColor = false;
            // 
            // _yellowCheck
            // 
            this._yellowCheck.AutoSize = true;
            this._yellowCheck.BackColor = System.Drawing.Color.Transparent;
            this._yellowCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._yellowCheck.ForeColor = System.Drawing.Color.Black;
            this._yellowCheck.Location = new System.Drawing.Point(170, 278);
            this._yellowCheck.Margin = new System.Windows.Forms.Padding(2);
            this._yellowCheck.Name = "_yellowCheck";
            this._yellowCheck.Size = new System.Drawing.Size(17, 16);
            this._yellowCheck.TabIndex = 5;
            this._yellowCheck.UseVisualStyleBackColor = false;
            // 
            // _blueCheck
            // 
            this._blueCheck.AutoSize = true;
            this._blueCheck.BackColor = System.Drawing.Color.Transparent;
            this._blueCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._blueCheck.ForeColor = System.Drawing.Color.Black;
            this._blueCheck.Location = new System.Drawing.Point(110, 278);
            this._blueCheck.Margin = new System.Windows.Forms.Padding(2);
            this._blueCheck.Name = "_blueCheck";
            this._blueCheck.Size = new System.Drawing.Size(17, 16);
            this._blueCheck.TabIndex = 4;
            this._blueCheck.UseVisualStyleBackColor = false;
            // 
            // _greenCheck
            // 
            this._greenCheck.AutoSize = true;
            this._greenCheck.BackColor = System.Drawing.Color.Transparent;
            this._greenCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._greenCheck.ForeColor = System.Drawing.Color.Black;
            this._greenCheck.Location = new System.Drawing.Point(50, 279);
            this._greenCheck.Margin = new System.Windows.Forms.Padding(2);
            this._greenCheck.Name = "_greenCheck";
            this._greenCheck.Size = new System.Drawing.Size(17, 16);
            this._greenCheck.TabIndex = 3;
            this._greenCheck.UseVisualStyleBackColor = false;
            // 
            // _localLogBox
            // 
            this._localLogBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._localLogBox.BackColor = System.Drawing.Color.Transparent;
            this._localLogBox.Controls.Add(this._localLog);
            this._localLogBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._localLogBox.ForeColor = System.Drawing.Color.Wheat;
            this._localLogBox.Location = new System.Drawing.Point(1006, 12);
            this._localLogBox.Margin = new System.Windows.Forms.Padding(2);
            this._localLogBox.Name = "_localLogBox";
            this._localLogBox.Padding = new System.Windows.Forms.Padding(2);
            this._localLogBox.Size = new System.Drawing.Size(522, 430);
            this._localLogBox.TabIndex = 4;
            this._localLogBox.TabStop = false;
            this._localLogBox.Text = "Előzmények";
            // 
            // _localLog
            // 
            this._localLog.BackColor = System.Drawing.Color.LightGreen;
            this._localLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this._localLog.Location = new System.Drawing.Point(2, 26);
            this._localLog.Margin = new System.Windows.Forms.Padding(2);
            this._localLog.Multiline = true;
            this._localLog.Name = "_localLog";
            this._localLog.ReadOnly = true;
            this._localLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._localLog.Size = new System.Drawing.Size(518, 402);
            this._localLog.TabIndex = 0;
            // 
            // _minimapPanel
            // 
            this._minimapPanel.ColumnCount = 2;
            this._minimapPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._minimapPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._minimapPanel.Location = new System.Drawing.Point(6, 30);
            this._minimapPanel.Margin = new System.Windows.Forms.Padding(2);
            this._minimapPanel.Name = "_minimapPanel";
            this._minimapPanel.RowCount = 2;
            this._minimapPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._minimapPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._minimapPanel.Size = new System.Drawing.Size(500, 478);
            this._minimapPanel.TabIndex = 1;
            // 
            // _taskBox
            // 
            this._taskBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._taskBox.BackColor = System.Drawing.Color.Transparent;
            this._taskBox.Controls.Add(this._tasks);
            this._taskBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._taskBox.ForeColor = System.Drawing.Color.Wheat;
            this._taskBox.Location = new System.Drawing.Point(114, 5);
            this._taskBox.Margin = new System.Windows.Forms.Padding(2);
            this._taskBox.Name = "_taskBox";
            this._taskBox.Padding = new System.Windows.Forms.Padding(2);
            this._taskBox.Size = new System.Drawing.Size(522, 510);
            this._taskBox.TabIndex = 5;
            this._taskBox.TabStop = false;
            this._taskBox.Text = "Feladatok";
            // 
            // _tasks
            // 
            this._tasks.ColumnCount = 2;
            this._tasks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tasks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tasks.Controls.Add(this._task1Display, 0, 0);
            this._tasks.Controls.Add(this._task2Display, 0, 1);
            this._tasks.Controls.Add(this._task1Description, 1, 0);
            this._tasks.Controls.Add(this._task2Description, 1, 1);
            this._tasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tasks.Location = new System.Drawing.Point(2, 26);
            this._tasks.Margin = new System.Windows.Forms.Padding(2);
            this._tasks.Name = "_tasks";
            this._tasks.Padding = new System.Windows.Forms.Padding(12);
            this._tasks.RowCount = 2;
            this._tasks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tasks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tasks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._tasks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._tasks.Size = new System.Drawing.Size(518, 482);
            this._tasks.TabIndex = 6;
            // 
            // _task1Display
            // 
            this._task1Display.ColumnCount = 3;
            this._task1Display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task1Display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this._task1Display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this._task1Display.Dock = System.Windows.Forms.DockStyle.Fill;
            this._task1Display.Location = new System.Drawing.Point(14, 14);
            this._task1Display.Margin = new System.Windows.Forms.Padding(2);
            this._task1Display.Name = "_task1Display";
            this._task1Display.Padding = new System.Windows.Forms.Padding(4);
            this._task1Display.RowCount = 3;
            this._task1Display.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task1Display.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task1Display.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task1Display.Size = new System.Drawing.Size(243, 225);
            this._task1Display.TabIndex = 0;
            // 
            // _task2Display
            // 
            this._task2Display.ColumnCount = 3;
            this._task2Display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task2Display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task2Display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task2Display.Dock = System.Windows.Forms.DockStyle.Fill;
            this._task2Display.Location = new System.Drawing.Point(14, 243);
            this._task2Display.Margin = new System.Windows.Forms.Padding(2);
            this._task2Display.Name = "_task2Display";
            this._task2Display.Padding = new System.Windows.Forms.Padding(4);
            this._task2Display.RowCount = 3;
            this._task2Display.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task2Display.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task2Display.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task2Display.Size = new System.Drawing.Size(243, 225);
            this._task2Display.TabIndex = 1;
            // 
            // _task1Description
            // 
            this._task1Description.ColumnCount = 1;
            this._task1Description.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._task1Description.Controls.Add(this._task1Name, 0, 0);
            this._task1Description.Controls.Add(this._task1StepCount, 0, 1);
            this._task1Description.Controls.Add(this._task1Ertek, 0, 2);
            this._task1Description.Dock = System.Windows.Forms.DockStyle.Fill;
            this._task1Description.Location = new System.Drawing.Point(261, 14);
            this._task1Description.Margin = new System.Windows.Forms.Padding(2);
            this._task1Description.Name = "_task1Description";
            this._task1Description.RowCount = 3;
            this._task1Description.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task1Description.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task1Description.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task1Description.Size = new System.Drawing.Size(243, 225);
            this._task1Description.TabIndex = 2;
            // 
            // _task1Name
            // 
            this._task1Name.AutoSize = true;
            this._task1Name.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._task1Name.ForeColor = System.Drawing.Color.Wheat;
            this._task1Name.Location = new System.Drawing.Point(2, 0);
            this._task1Name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._task1Name.Name = "_task1Name";
            this._task1Name.Size = new System.Drawing.Size(142, 28);
            this._task1Name.TabIndex = 0;
            this._task1Name.Text = "1. feladat";
            // 
            // _task1StepCount
            // 
            this._task1StepCount.AutoSize = true;
            this._task1StepCount.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._task1StepCount.ForeColor = System.Drawing.Color.Wheat;
            this._task1StepCount.Location = new System.Drawing.Point(2, 75);
            this._task1StepCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._task1StepCount.Name = "_task1StepCount";
            this._task1StepCount.Size = new System.Drawing.Size(233, 56);
            this._task1StepCount.TabIndex = 1;
            this._task1StepCount.Text = "Teljesítési idő: 300 lépés";
            // 
            // _task1Ertek
            // 
            this._task1Ertek.AutoSize = true;
            this._task1Ertek.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._task1Ertek.ForeColor = System.Drawing.Color.Wheat;
            this._task1Ertek.Location = new System.Drawing.Point(2, 150);
            this._task1Ertek.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._task1Ertek.Name = "_task1Ertek";
            this._task1Ertek.Size = new System.Drawing.Size(142, 28);
            this._task1Ertek.TabIndex = 2;
            this._task1Ertek.Text = "Érték: 300";
            // 
            // _task2Description
            // 
            this._task2Description.ColumnCount = 1;
            this._task2Description.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._task2Description.Controls.Add(this._task2Ertek, 0, 2);
            this._task2Description.Controls.Add(this._task2StepCount, 0, 1);
            this._task2Description.Controls.Add(this._task2Name, 0, 0);
            this._task2Description.Dock = System.Windows.Forms.DockStyle.Fill;
            this._task2Description.Location = new System.Drawing.Point(261, 243);
            this._task2Description.Margin = new System.Windows.Forms.Padding(2);
            this._task2Description.Name = "_task2Description";
            this._task2Description.RowCount = 3;
            this._task2Description.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task2Description.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task2Description.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._task2Description.Size = new System.Drawing.Size(243, 225);
            this._task2Description.TabIndex = 3;
            // 
            // _task2Ertek
            // 
            this._task2Ertek.AutoSize = true;
            this._task2Ertek.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._task2Ertek.ForeColor = System.Drawing.Color.Wheat;
            this._task2Ertek.Location = new System.Drawing.Point(2, 150);
            this._task2Ertek.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._task2Ertek.Name = "_task2Ertek";
            this._task2Ertek.Size = new System.Drawing.Size(142, 28);
            this._task2Ertek.TabIndex = 3;
            this._task2Ertek.Text = "Érték: 300";
            // 
            // _task2StepCount
            // 
            this._task2StepCount.AutoSize = true;
            this._task2StepCount.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._task2StepCount.ForeColor = System.Drawing.Color.Wheat;
            this._task2StepCount.Location = new System.Drawing.Point(2, 75);
            this._task2StepCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._task2StepCount.Name = "_task2StepCount";
            this._task2StepCount.Size = new System.Drawing.Size(233, 56);
            this._task2StepCount.TabIndex = 2;
            this._task2StepCount.Text = "Teljesítési idő: 300 lépés";
            // 
            // _task2Name
            // 
            this._task2Name.AutoSize = true;
            this._task2Name.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._task2Name.ForeColor = System.Drawing.Color.Wheat;
            this._task2Name.Location = new System.Drawing.Point(2, 0);
            this._task2Name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._task2Name.Name = "_task2Name";
            this._task2Name.Size = new System.Drawing.Size(142, 28);
            this._task2Name.TabIndex = 0;
            this._task2Name.Text = "2. feladat";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Wheat;
            this.label1.Location = new System.Drawing.Point(1006, 445);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "Hátralévő idő a következő körig:";
            // 
            // _minimap
            // 
            this._minimap.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._minimap.BackColor = System.Drawing.Color.Transparent;
            this._minimap.Controls.Add(this._minimapPanel);
            this._minimap.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._minimap.ForeColor = System.Drawing.Color.Wheat;
            this._minimap.Location = new System.Drawing.Point(114, 526);
            this._minimap.Margin = new System.Windows.Forms.Padding(2);
            this._minimap.Name = "_minimap";
            this._minimap.Padding = new System.Windows.Forms.Padding(2);
            this._minimap.Size = new System.Drawing.Size(522, 524);
            this._minimap.TabIndex = 8;
            this._minimap.TabStop = false;
            this._minimap.Text = "Minimap";
            // 
            // _robotName
            // 
            this._robotName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._robotName.BackColor = System.Drawing.Color.OldLace;
            this._robotName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._robotName.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._robotName.Location = new System.Drawing.Point(647, 901);
            this._robotName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._robotName.Name = "_robotName";
            this._robotName.Size = new System.Drawing.Size(350, 52);
            this._robotName.TabIndex = 10;
            this._robotName.Text = "<Robot neve>";
            this._robotName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(14, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 22);
            this.label3.TabIndex = 11;
            this.label3.Text = "Csapat pontjai:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(14, 43);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 22);
            this.label4.TabIndex = 12;
            this.label4.Text = "Hátralévő lépések:";
            // 
            // _remainingSteps
            // 
            this._remainingSteps.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._remainingSteps.AutoSize = true;
            this._remainingSteps.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._remainingSteps.Location = new System.Drawing.Point(292, 43);
            this._remainingSteps.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._remainingSteps.Name = "_remainingSteps";
            this._remainingSteps.Size = new System.Drawing.Size(40, 22);
            this._remainingSteps.TabIndex = 14;
            this._remainingSteps.Text = "200";
            this._remainingSteps.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _teamPoints
            // 
            this._teamPoints.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._teamPoints.AutoSize = true;
            this._teamPoints.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._teamPoints.Location = new System.Drawing.Point(292, 12);
            this._teamPoints.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._teamPoints.Name = "_teamPoints";
            this._teamPoints.Size = new System.Drawing.Size(40, 22);
            this._teamPoints.TabIndex = 13;
            this._teamPoints.Text = "300";
            this._teamPoints.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _controlPanel
            // 
            this._controlPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._controlPanel.BackColor = System.Drawing.Color.Linen;
            this._controlPanel.Controls.Add(this.detachButton);
            this._controlPanel.Controls.Add(this.attachButton);
            this._controlPanel.Controls.Add(this.throwButton);
            this._controlPanel.Controls.Add(this.freezeButton);
            this._controlPanel.Controls.Add(this.pickUpButton);
            this._controlPanel.Controls.Add(this.cleanButton);
            this._controlPanel.Controls.Add(this.turnRightButton);
            this._controlPanel.Controls.Add(this.turnLeftButton);
            this._controlPanel.Controls.Add(this.rightButton);
            this._controlPanel.Controls.Add(this.leftButton);
            this._controlPanel.Controls.Add(this.downButton);
            this._controlPanel.Controls.Add(this.upButton);
            this._controlPanel.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._controlPanel.ForeColor = System.Drawing.Color.Black;
            this._controlPanel.Location = new System.Drawing.Point(1006, 522);
            this._controlPanel.Margin = new System.Windows.Forms.Padding(2);
            this._controlPanel.Name = "_controlPanel";
            this._controlPanel.Padding = new System.Windows.Forms.Padding(2);
            this._controlPanel.Size = new System.Drawing.Size(522, 524);
            this._controlPanel.TabIndex = 15;
            this._controlPanel.TabStop = false;
            this._controlPanel.Text = "Irányítópanel";
            // 
            // detachButton
            // 
            this.detachButton.BackColor = System.Drawing.Color.Transparent;
            this.detachButton.BackgroundImage = global::robot_fight.View.resources.disconnect;
            this.detachButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.detachButton.FlatAppearance.BorderSize = 0;
            this.detachButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.detachButton.Location = new System.Drawing.Point(358, 272);
            this.detachButton.Margin = new System.Windows.Forms.Padding(2);
            this.detachButton.Name = "detachButton";
            this.detachButton.Size = new System.Drawing.Size(130, 130);
            this.detachButton.TabIndex = 11;
            this.detachButton.UseVisualStyleBackColor = false;
            this.detachButton.Click += new System.EventHandler(this.detachButton_Click);
            this.detachButton.MouseEnter += new System.EventHandler(this.OnHover);
            this.detachButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // attachButton
            // 
            this.attachButton.BackColor = System.Drawing.Color.Transparent;
            this.attachButton.BackgroundImage = global::robot_fight.View.resources.connect;
            this.attachButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.attachButton.FlatAppearance.BorderSize = 0;
            this.attachButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.attachButton.Location = new System.Drawing.Point(28, 272);
            this.attachButton.Margin = new System.Windows.Forms.Padding(2);
            this.attachButton.Name = "attachButton";
            this.attachButton.Size = new System.Drawing.Size(130, 130);
            this.attachButton.TabIndex = 10;
            this.attachButton.UseVisualStyleBackColor = false;
            this.attachButton.Click += new System.EventHandler(this.attachButton_Click);
            this.attachButton.MouseEnter += new System.EventHandler(this.OnHover);
            this.attachButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // throwButton
            // 
            this.throwButton.BackColor = System.Drawing.Color.Transparent;
            this.throwButton.BackgroundImage = global::robot_fight.View.resources.detach;
            this.throwButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.throwButton.FlatAppearance.BorderSize = 0;
            this.throwButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.throwButton.Location = new System.Drawing.Point(260, 392);
            this.throwButton.Margin = new System.Windows.Forms.Padding(2);
            this.throwButton.Name = "throwButton";
            this.throwButton.Size = new System.Drawing.Size(130, 130);
            this.throwButton.TabIndex = 9;
            this.throwButton.UseVisualStyleBackColor = false;
            this.throwButton.Click += new System.EventHandler(this.dropButton_Click);
            this.throwButton.MouseEnter += new System.EventHandler(this.OnHover);
            this.throwButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // freezeButton
            // 
            this.freezeButton.BackColor = System.Drawing.Color.Transparent;
            this.freezeButton.BackgroundImage = global::robot_fight.View.resources.freeze;
            this.freezeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.freezeButton.Enabled = false;
            this.freezeButton.FlatAppearance.BorderSize = 0;
            this.freezeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.freezeButton.Location = new System.Drawing.Point(396, 398);
            this.freezeButton.Margin = new System.Windows.Forms.Padding(2);
            this.freezeButton.Name = "freezeButton";
            this.freezeButton.Size = new System.Drawing.Size(120, 120);
            this.freezeButton.TabIndex = 8;
            this.freezeButton.UseVisualStyleBackColor = false;
            this.freezeButton.Click += new System.EventHandler(this.FreezeButton_Click);
            this.freezeButton.MouseEnter += new System.EventHandler(this.OnHover);
            this.freezeButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // pickUpButton
            // 
            this.pickUpButton.BackColor = System.Drawing.Color.Transparent;
            this.pickUpButton.BackgroundImage = global::robot_fight.View.resources.attach;
            this.pickUpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pickUpButton.FlatAppearance.BorderSize = 0;
            this.pickUpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pickUpButton.Location = new System.Drawing.Point(132, 392);
            this.pickUpButton.Margin = new System.Windows.Forms.Padding(2);
            this.pickUpButton.Name = "pickUpButton";
            this.pickUpButton.Size = new System.Drawing.Size(130, 130);
            this.pickUpButton.TabIndex = 7;
            this.pickUpButton.UseVisualStyleBackColor = false;
            this.pickUpButton.Click += new System.EventHandler(this.pickUpButton_Click);
            this.pickUpButton.MouseEnter += new System.EventHandler(this.OnHover);
            this.pickUpButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // cleanButton
            // 
            this.cleanButton.BackColor = System.Drawing.Color.Transparent;
            this.cleanButton.BackgroundImage = global::robot_fight.View.resources.sweep;
            this.cleanButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cleanButton.FlatAppearance.BorderSize = 0;
            this.cleanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cleanButton.Location = new System.Drawing.Point(6, 398);
            this.cleanButton.Margin = new System.Windows.Forms.Padding(2);
            this.cleanButton.Name = "cleanButton";
            this.cleanButton.Size = new System.Drawing.Size(120, 120);
            this.cleanButton.TabIndex = 6;
            this.cleanButton.UseVisualStyleBackColor = false;
            this.cleanButton.Click += new System.EventHandler(this.cleanButton_Click);
            this.cleanButton.MouseEnter += new System.EventHandler(this.OnHover);
            this.cleanButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // turnRightButton
            // 
            this.turnRightButton.BackColor = System.Drawing.Color.Transparent;
            this.turnRightButton.BackgroundImage = global::robot_fight.View.resources.rotateright;
            this.turnRightButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.turnRightButton.FlatAppearance.BorderSize = 0;
            this.turnRightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.turnRightButton.Location = new System.Drawing.Point(358, 28);
            this.turnRightButton.Margin = new System.Windows.Forms.Padding(2);
            this.turnRightButton.Name = "turnRightButton";
            this.turnRightButton.Size = new System.Drawing.Size(130, 130);
            this.turnRightButton.TabIndex = 5;
            this.turnRightButton.UseVisualStyleBackColor = false;
            this.turnRightButton.Click += new System.EventHandler(this.turnRightButton_Click);
            this.turnRightButton.MouseEnter += new System.EventHandler(this.OnHover);
            this.turnRightButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // turnLeftButton
            // 
            this.turnLeftButton.BackColor = System.Drawing.Color.Transparent;
            this.turnLeftButton.BackgroundImage = global::robot_fight.View.resources.rotateleft;
            this.turnLeftButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.turnLeftButton.FlatAppearance.BorderSize = 0;
            this.turnLeftButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.turnLeftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.turnLeftButton.Location = new System.Drawing.Point(28, 28);
            this.turnLeftButton.Margin = new System.Windows.Forms.Padding(2);
            this.turnLeftButton.Name = "turnLeftButton";
            this.turnLeftButton.Size = new System.Drawing.Size(130, 130);
            this.turnLeftButton.TabIndex = 4;
            this.turnLeftButton.UseVisualStyleBackColor = false;
            this.turnLeftButton.Click += new System.EventHandler(this.turnLeftButton_Click);
            this.turnLeftButton.MouseEnter += new System.EventHandler(this.OnHover);
            this.turnLeftButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // rightButton
            // 
            this.rightButton.BackColor = System.Drawing.Color.Transparent;
            this.rightButton.BackgroundImage = global::robot_fight.View.resources.right;
            this.rightButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rightButton.FlatAppearance.BorderSize = 0;
            this.rightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rightButton.Location = new System.Drawing.Point(301, 152);
            this.rightButton.Margin = new System.Windows.Forms.Padding(2);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(130, 130);
            this.rightButton.TabIndex = 3;
            this.rightButton.UseVisualStyleBackColor = false;
            this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
            this.rightButton.MouseEnter += new System.EventHandler(this.OnHover);
            this.rightButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // leftButton
            // 
            this.leftButton.BackColor = System.Drawing.Color.Transparent;
            this.leftButton.BackgroundImage = global::robot_fight.View.resources.left;
            this.leftButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.leftButton.FlatAppearance.BorderSize = 0;
            this.leftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leftButton.Location = new System.Drawing.Point(86, 152);
            this.leftButton.Margin = new System.Windows.Forms.Padding(2);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(130, 130);
            this.leftButton.TabIndex = 2;
            this.leftButton.UseVisualStyleBackColor = false;
            this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
            this.leftButton.MouseEnter += new System.EventHandler(this.OnHover);
            this.leftButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // downButton
            // 
            this.downButton.BackColor = System.Drawing.Color.Transparent;
            this.downButton.BackgroundImage = global::robot_fight.View.resources.down;
            this.downButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.downButton.FlatAppearance.BorderSize = 0;
            this.downButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downButton.Location = new System.Drawing.Point(192, 258);
            this.downButton.Margin = new System.Windows.Forms.Padding(2);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(130, 130);
            this.downButton.TabIndex = 1;
            this.downButton.UseVisualStyleBackColor = false;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            this.downButton.MouseEnter += new System.EventHandler(this.OnHover);
            this.downButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // upButton
            // 
            this.upButton.BackColor = System.Drawing.Color.Transparent;
            this.upButton.BackgroundImage = global::robot_fight.View.resources.up;
            this.upButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.upButton.FlatAppearance.BorderSize = 0;
            this.upButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.upButton.Location = new System.Drawing.Point(192, 51);
            this.upButton.Margin = new System.Windows.Forms.Padding(2);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(130, 130);
            this.upButton.TabIndex = 0;
            this.upButton.UseVisualStyleBackColor = false;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            this.upButton.MouseEnter += new System.EventHandler(this.OnHover);
            this.upButton.MouseLeave += new System.EventHandler(this.OnLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::robot_fight.View.resources.blurry_bg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this._pauseButton);
            this.panel1.Controls.Add(this._helpButton);
            this.panel1.Controls.Add(this._progressBar);
            this.panel1.Controls.Add(this._tableLayoutGrid);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this._controlPanel);
            this.panel1.Controls.Add(this._taskBox);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this._localLogBox);
            this.panel1.Controls.Add(this._robotName);
            this.panel1.Controls.Add(this._minimap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1632, 1050);
            this.panel1.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.Color.PapayaWhip;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this._teamPoints);
            this.panel2.Controls.Add(this._remainingSteps);
            this.panel2.Location = new System.Drawing.Point(647, 956);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(350, 78);
            this.panel2.TabIndex = 21;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Font = new System.Drawing.Font("Consolas", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(1544, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 70);
            this.button1.TabIndex = 20;
            this.button1.Text = "👀";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ViewerButton);
            // 
            // _pauseButton
            // 
            this._pauseButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._pauseButton.Font = new System.Drawing.Font("Consolas", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._pauseButton.Location = new System.Drawing.Point(1544, 106);
            this._pauseButton.Name = "_pauseButton";
            this._pauseButton.Size = new System.Drawing.Size(70, 70);
            this._pauseButton.TabIndex = 19;
            this._pauseButton.Text = "⏯️";
            this._pauseButton.UseVisualStyleBackColor = true;
            this._pauseButton.Click += new System.EventHandler(this.PauseResumeButton);
            // 
            // _helpButton
            // 
            this._helpButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._helpButton.Font = new System.Drawing.Font("Consolas", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._helpButton.Location = new System.Drawing.Point(1544, 23);
            this._helpButton.Name = "_helpButton";
            this._helpButton.Size = new System.Drawing.Size(70, 70);
            this._helpButton.TabIndex = 18;
            this._helpButton.Text = "❔";
            this._helpButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._helpButton.UseVisualStyleBackColor = true;
            this._helpButton.Click += new System.EventHandler(this.HowToPlayButton);
            // 
            // _progressBar
            // 
            this._progressBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._progressBar.Location = new System.Drawing.Point(1010, 470);
            this._progressBar.Margin = new System.Windows.Forms.Padding(2);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(510, 42);
            this._progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this._progressBar.TabIndex = 17;
            this._progressBar.Value = 60;
            // 
            // _tableLayoutGrid
            // 
            this._tableLayoutGrid.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._tableLayoutGrid.ColumnCount = 2;
            this._tableLayoutGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutGrid.Location = new System.Drawing.Point(647, 526);
            this._tableLayoutGrid.Margin = new System.Windows.Forms.Padding(2);
            this._tableLayoutGrid.Name = "_tableLayoutGrid";
            this._tableLayoutGrid.RowCount = 2;
            this._tableLayoutGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutGrid.Size = new System.Drawing.Size(350, 350);
            this._tableLayoutGrid.TabIndex = 16;
            // 
            // GamerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1632, 1050);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GamerForm";
            this.Text = "Robot Fight";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GamerForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this._localLogBox.ResumeLayout(false);
            this._localLogBox.PerformLayout();
            this._taskBox.ResumeLayout(false);
            this._tasks.ResumeLayout(false);
            this._task1Description.ResumeLayout(false);
            this._task1Description.PerformLayout();
            this._task2Description.ResumeLayout(false);
            this._task2Description.PerformLayout();
            this._minimap.ResumeLayout(false);
            this._controlPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox _communicationBox;
        private GroupBox groupBox1;
        private GroupBox _localLogBox;
        private GroupBox _taskBox;
        private TableLayoutPanel _tasks;
        private Label label1;
        private GroupBox _minimap;
        private Label _robotName;
        private Label label3;
        private Label label4;
        private Label _remainingSteps;
        private Label _teamPoints;
        private GroupBox _controlPanel;
        private TextBox _localLog;
        private ColorDialog colorDialog1;
        private Panel panel1;
        private Button upButton;
        private Button rightButton;
        private Button leftButton;
        private Button downButton;
        private Button turnRightButton;
        private Button turnLeftButton;
        private Button throwButton;
        private Button freezeButton;
        private Button pickUpButton;
        private Button cleanButton;
        private Button detachButton;
        private Button attachButton;
        private CheckBox _rightCheck;
        private CheckBox _leftCheck;
        private CheckBox _downCheck;
        private CheckBox _upCheck;
        private CheckBox _purpleCheck;
        private CheckBox _brownCheck;
        private CheckBox _redCheck;
        private CheckBox _yellowCheck;
        private CheckBox _blueCheck;
        private CheckBox _greenCheck;
        private Button _sendMessage;
        private Button _tasktwoButton;
        private Button _taskOneButton;
        private MyTableLayoutPanel _minimapPanel;
        private MyTableLayoutPanel _tableLayoutGrid;
        private ProgressBar _progressBar;
        private MyTableLayoutPanel _task1Display;
        private MyTableLayoutPanel _task2Display;
        private MyTableLayoutPanel _task1Description;
        private MyTableLayoutPanel _task2Description;
        private Label _task1Name;
        private Label _task1StepCount;
        private Label _task1Ertek;
        private Label _task2Ertek;
        private Label _task2StepCount;
        private Label _task2Name;
        private Button _helpButton;
        private Button _pauseButton;
        private Button button1;
        private Panel panel2;
    }
}