namespace robot_fight.View
{
    partial class GameMasterForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._numOfSteps = new System.Windows.Forms.NumericUpDown();
            this._numOfTicks = new System.Windows.Forms.NumericUpDown();
            this._numOfLife = new System.Windows.Forms.NumericUpDown();
            this._numOfTeams = new System.Windows.Forms.NumericUpDown();
            this._tableWidth = new System.Windows.Forms.NumericUpDown();
            this._tableHeight = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._gamerView = new System.Windows.Forms.NumericUpDown();
            this._numOfBarriers = new System.Windows.Forms.NumericUpDown();
            this._fromNum = new System.Windows.Forms.NumericUpDown();
            this._toNum = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this._numOfExits = new System.Windows.Forms.NumericUpDown();
            this._consoleInput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this._maxPlayerCount = new System.Windows.Forms.NumericUpDown();
            this._normalTheme = new System.Windows.Forms.RadioButton();
            this._picnicTheme = new System.Windows.Forms.RadioButton();
            this._christmasTheme = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numOfSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numOfTicks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numOfLife)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numOfTeams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._tableWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._tableHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gamerView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numOfBarriers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._fromNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._toNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numOfExits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._maxPlayerCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Eras Medium ITC", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1196, 70);
            this.label1.TabIndex = 0;
            this.label1.Text = "Robot Fight";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "Játékos látótere:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(3, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tábla szélessége:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this._numOfSteps, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this._numOfTicks, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this._numOfLife, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this._numOfTeams, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this._tableWidth, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this._tableHeight, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._gamerView, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._numOfBarriers, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this._fromNum, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 127);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(574, 629);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // _numOfSteps
            // 
            this._numOfSteps.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._numOfSteps.Location = new System.Drawing.Point(359, 555);
            this._numOfSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numOfSteps.Name = "_numOfSteps";
            this._numOfSteps.Size = new System.Drawing.Size(215, 31);
            this._numOfSteps.TabIndex = 19;
            this._numOfSteps.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // _numOfTicks
            // 
            this._numOfTicks.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._numOfTicks.Location = new System.Drawing.Point(359, 486);
            this._numOfTicks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numOfTicks.Name = "_numOfTicks";
            this._numOfTicks.Size = new System.Drawing.Size(215, 31);
            this._numOfTicks.TabIndex = 18;
            this._numOfTicks.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _numOfLife
            // 
            this._numOfLife.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._numOfLife.Location = new System.Drawing.Point(359, 417);
            this._numOfLife.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numOfLife.Name = "_numOfLife";
            this._numOfLife.Size = new System.Drawing.Size(215, 31);
            this._numOfLife.TabIndex = 17;
            this._numOfLife.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // _numOfTeams
            // 
            this._numOfTeams.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._numOfTeams.Location = new System.Drawing.Point(359, 210);
            this._numOfTeams.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this._numOfTeams.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numOfTeams.Name = "_numOfTeams";
            this._numOfTeams.Size = new System.Drawing.Size(215, 31);
            this._numOfTeams.TabIndex = 14;
            this._numOfTeams.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // _tableWidth
            // 
            this._tableWidth.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._tableWidth.Location = new System.Drawing.Point(359, 141);
            this._tableWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._tableWidth.Name = "_tableWidth";
            this._tableWidth.Size = new System.Drawing.Size(215, 31);
            this._tableWidth.TabIndex = 13;
            this._tableWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _tableHeight
            // 
            this._tableHeight.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._tableHeight.Location = new System.Drawing.Point(359, 72);
            this._tableHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._tableHeight.Name = "_tableHeight";
            this._tableHeight.Size = new System.Drawing.Size(215, 31);
            this._tableHeight.TabIndex = 12;
            this._tableHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(3, 552);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(285, 28);
            this.label10.TabIndex = 10;
            this.label10.Text = "Játék hossza (lépés):";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(3, 483);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(311, 28);
            this.label9.TabIndex = 9;
            this.label9.Text = "Kör hossza (másodperc):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(3, 414);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(350, 28);
            this.label8.TabIndex = 8;
            this.label8.Text = "Elemek takarítása (akció):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(3, 345);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(220, 28);
            this.label7.TabIndex = 7;
            this.label7.Text = "Akadályok száma:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(3, 276);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(233, 28);
            this.label6.TabIndex = 6;
            this.label6.Text = "Feladatok értéke:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(3, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(207, 28);
            this.label5.TabIndex = 5;
            this.label5.Text = "Csapatok száma:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(3, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(220, 28);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tábla magassága:";
            // 
            // _gamerView
            // 
            this._gamerView.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._gamerView.Location = new System.Drawing.Point(359, 3);
            this._gamerView.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._gamerView.Name = "_gamerView";
            this._gamerView.Size = new System.Drawing.Size(215, 31);
            this._gamerView.TabIndex = 11;
            this._gamerView.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // _numOfBarriers
            // 
            this._numOfBarriers.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._numOfBarriers.Location = new System.Drawing.Point(359, 348);
            this._numOfBarriers.Name = "_numOfBarriers";
            this._numOfBarriers.Size = new System.Drawing.Size(215, 31);
            this._numOfBarriers.TabIndex = 15;
            this._numOfBarriers.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _fromNum
            // 
            this._fromNum.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._fromNum.Location = new System.Drawing.Point(359, 279);
            this._fromNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._fromNum.Name = "_fromNum";
            this._fromNum.Size = new System.Drawing.Size(65, 31);
            this._fromNum.TabIndex = 16;
            this._fromNum.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // _toNum
            // 
            this._toNum.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._toNum.Location = new System.Drawing.Point(485, 406);
            this._toNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._toNum.Name = "_toNum";
            this._toNum.Size = new System.Drawing.Size(65, 31);
            this._toNum.TabIndex = 17;
            this._toNum.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(440, 412);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 19);
            this.label11.TabIndex = 18;
            this.label11.Text = "-tól        -ig";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(637, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(220, 28);
            this.label12.TabIndex = 19;
            this.label12.Text = "Kijáratok száma:";
            // 
            // _numOfExits
            // 
            this._numOfExits.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._numOfExits.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._numOfExits.Location = new System.Drawing.Point(876, 127);
            this._numOfExits.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numOfExits.Name = "_numOfExits";
            this._numOfExits.Size = new System.Drawing.Size(332, 31);
            this._numOfExits.TabIndex = 21;
            this._numOfExits.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // _consoleInput
            // 
            this._consoleInput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._consoleInput.BackColor = System.Drawing.Color.GhostWhite;
            this._consoleInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._consoleInput.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._consoleInput.ForeColor = System.Drawing.Color.Black;
            this._consoleInput.Location = new System.Drawing.Point(634, 326);
            this._consoleInput.Multiline = true;
            this._consoleInput.Name = "_consoleInput";
            this._consoleInput.PlaceholderText = "Hibaüzenetek...";
            this._consoleInput.ReadOnly = true;
            this._consoleInput.Size = new System.Drawing.Size(574, 347);
            this._consoleInput.TabIndex = 0;
            this._consoleInput.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button1.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(631, 679);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(577, 72);
            this.button1.TabIndex = 24;
            this.button1.Text = "Játék indítása";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.StartGameButton);
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(637, 199);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(220, 28);
            this.label13.TabIndex = 25;
            this.label13.Text = "Játékosok száma:";
            // 
            // _maxPlayerCount
            // 
            this._maxPlayerCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._maxPlayerCount.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._maxPlayerCount.Location = new System.Drawing.Point(876, 196);
            this._maxPlayerCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._maxPlayerCount.Name = "_maxPlayerCount";
            this._maxPlayerCount.Size = new System.Drawing.Size(332, 31);
            this._maxPlayerCount.TabIndex = 26;
            this._maxPlayerCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // _normalTheme
            // 
            this._normalTheme.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._normalTheme.Checked = true;
            this._normalTheme.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._normalTheme.Location = new System.Drawing.Point(634, 243);
            this._normalTheme.Name = "_normalTheme";
            this._normalTheme.Size = new System.Drawing.Size(177, 77);
            this._normalTheme.TabIndex = 30;
            this._normalTheme.TabStop = true;
            this._normalTheme.Text = "Normál téma";
            this._normalTheme.UseVisualStyleBackColor = true;
            // 
            // _picnicTheme
            // 
            this._picnicTheme.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._picnicTheme.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._picnicTheme.Location = new System.Drawing.Point(817, 241);
            this._picnicTheme.Name = "_picnicTheme";
            this._picnicTheme.Size = new System.Drawing.Size(177, 77);
            this._picnicTheme.TabIndex = 31;
            this._picnicTheme.TabStop = true;
            this._picnicTheme.Text = "Nemzetközi Piknik nap";
            this._picnicTheme.UseVisualStyleBackColor = true;
            // 
            // _christmasTheme
            // 
            this._christmasTheme.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._christmasTheme.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._christmasTheme.Location = new System.Drawing.Point(1031, 243);
            this._christmasTheme.Name = "_christmasTheme";
            this._christmasTheme.Size = new System.Drawing.Size(177, 77);
            this._christmasTheme.TabIndex = 32;
            this._christmasTheme.TabStop = true;
            this._christmasTheme.Text = "Karácsonyi téma";
            this._christmasTheme.UseVisualStyleBackColor = true;
            // 
            // GameMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1220, 768);
            this.Controls.Add(this._christmasTheme);
            this.Controls.Add(this._picnicTheme);
            this.Controls.Add(this._normalTheme);
            this.Controls.Add(this._maxPlayerCount);
            this.Controls.Add(this.label13);
            this.Controls.Add(this._consoleInput);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._numOfExits);
            this.Controls.Add(this.label12);
            this.Controls.Add(this._toNum);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Name = "GameMasterForm";
            this.Text = "Robot Fight - Beállítások";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameMasterForm_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numOfSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numOfTicks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numOfLife)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numOfTeams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._tableWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._tableHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gamerView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numOfBarriers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._fromNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._toNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numOfExits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._maxPlayerCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label4;
        private Label label5;
        private NumericUpDown _numOfSteps;
        private NumericUpDown _numOfTicks;
        private NumericUpDown _numOfLife;
        private NumericUpDown _numOfTeams;
        private NumericUpDown _tableWidth;
        private NumericUpDown _tableHeight;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private NumericUpDown _gamerView;
        private NumericUpDown _numOfBarriers;
        private NumericUpDown _fromNum;
        private NumericUpDown _toNum;
        private Label label11;
        private Label label12;
        private NumericUpDown _numOfExits;
        private TextBox _consoleInput;
        private Button button1;
        private Label label13;
        private NumericUpDown _maxPlayerCount;
        private RadioButton _normalTheme;
        private RadioButton _picnicTheme;
        private RadioButton _christmasTheme;
    }
}