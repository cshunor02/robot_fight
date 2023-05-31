namespace robot_fight.View
{
    partial class ViewerForm
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
            this._tableLayoutGrid = new System.Windows.Forms.TableLayoutPanel();
            this._globalLog = new System.Windows.Forms.TextBox();
            this.exitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _tableLayoutGrid
            // 
            this._tableLayoutGrid.ColumnCount = 2;
            this._tableLayoutGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutGrid.Location = new System.Drawing.Point(12, 12);
            this._tableLayoutGrid.Name = "_tableLayoutGrid";
            this._tableLayoutGrid.RowCount = 2;
            this._tableLayoutGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutGrid.Size = new System.Drawing.Size(727, 745);
            this._tableLayoutGrid.TabIndex = 0;
            // 
            // _globalLog
            // 
            this._globalLog.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._globalLog.Location = new System.Drawing.Point(745, 12);
            this._globalLog.Multiline = true;
            this._globalLog.Name = "_globalLog";
            this._globalLog.ReadOnly = true;
            this._globalLog.Size = new System.Drawing.Size(350, 657);
            this._globalLog.TabIndex = 1;
            this._globalLog.TabStop = false;
            // 
            // exitButton
            // 
            this.exitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.exitButton.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.exitButton.Location = new System.Drawing.Point(745, 675);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(350, 82);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Elrejtés";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // ViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 769);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this._globalLog);
            this.Controls.Add(this._tableLayoutGrid);
            this.Name = "ViewerForm";
            this.Text = "Robot Fight - Nézői mód";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewerForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel _tableLayoutGrid;
        private TextBox _globalLog;
        private Button exitButton;
    }
}