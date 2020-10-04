namespace UIManager
{
    internal partial class Settings
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
            this.m_FirstPlayerNameLabel = new System.Windows.Forms.Label();
            this.m_SecondPlayerNameLabel = new System.Windows.Forms.Label();
            this.m_TextBoxFirstPlayer = new System.Windows.Forms.TextBox();
            this.m_TextBoxSecondPlayer = new System.Windows.Forms.TextBox();
            this.m_BoardSizeButton = new System.Windows.Forms.Button();
            this.m_StartButton = new System.Windows.Forms.Button();
            this.m_AgainstFriendOrComputer = new System.Windows.Forms.Button();
            this.m_BoardSizeLabel = new System.Windows.Forms.Label();
            this.m_ComputerLevel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_FirstPlayerNameLabel
            // 
            this.m_FirstPlayerNameLabel.AutoSize = true;
            this.m_FirstPlayerNameLabel.Location = new System.Drawing.Point(22, 42);
            this.m_FirstPlayerNameLabel.Name = "m_FirstPlayerNameLabel";
            this.m_FirstPlayerNameLabel.Size = new System.Drawing.Size(130, 20);
            this.m_FirstPlayerNameLabel.TabIndex = 0;
            this.m_FirstPlayerNameLabel.Text = "First player name";
            // 
            // m_SecondPlayerNameLabel
            // 
            this.m_SecondPlayerNameLabel.AutoSize = true;
            this.m_SecondPlayerNameLabel.Location = new System.Drawing.Point(22, 88);
            this.m_SecondPlayerNameLabel.Name = "m_SecondPlayerNameLabel";
            this.m_SecondPlayerNameLabel.Size = new System.Drawing.Size(154, 20);
            this.m_SecondPlayerNameLabel.TabIndex = 1;
            this.m_SecondPlayerNameLabel.Text = "Second player name";
            // 
            // m_TextBoxFirstPlayer
            // 
            this.m_TextBoxFirstPlayer.Location = new System.Drawing.Point(198, 36);
            this.m_TextBoxFirstPlayer.Name = "m_TextBoxFirstPlayer";
            this.m_TextBoxFirstPlayer.Size = new System.Drawing.Size(204, 26);
            this.m_TextBoxFirstPlayer.TabIndex = 2;
            // 
            // m_TextBoxSecondPlayer
            // 
            this.m_TextBoxSecondPlayer.Enabled = false;
            this.m_TextBoxSecondPlayer.Location = new System.Drawing.Point(198, 82);
            this.m_TextBoxSecondPlayer.Name = "m_TextBoxSecondPlayer";
            this.m_TextBoxSecondPlayer.Size = new System.Drawing.Size(204, 26);
            this.m_TextBoxSecondPlayer.TabIndex = 7;
            this.m_TextBoxSecondPlayer.Text = "-computer-";
            // 
            // m_BoardSizeButton
            // 
            this.m_BoardSizeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_BoardSizeButton.Location = new System.Drawing.Point(39, 189);
            this.m_BoardSizeButton.Name = "m_BoardSizeButton";
            this.m_BoardSizeButton.Size = new System.Drawing.Size(195, 124);
            this.m_BoardSizeButton.TabIndex = 4;
            this.m_BoardSizeButton.Text = "4 x 4";
            this.m_BoardSizeButton.UseVisualStyleBackColor = false;
            this.m_BoardSizeButton.Click += new System.EventHandler(this.m_BoardSizeButton_Click);
            // 
            // m_StartButton
            // 
            this.m_StartButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.m_StartButton.Location = new System.Drawing.Point(421, 267);
            this.m_StartButton.Name = "m_StartButton";
            this.m_StartButton.Size = new System.Drawing.Size(180, 46);
            this.m_StartButton.TabIndex = 5;
            this.m_StartButton.Text = "Start!";
            this.m_StartButton.UseVisualStyleBackColor = false;
            this.m_StartButton.Click += new System.EventHandler(this.m_StartButton_Click);
            // 
            // m_AgainstFriendOrComputer
            // 
            this.m_AgainstFriendOrComputer.Location = new System.Drawing.Point(421, 82);
            this.m_AgainstFriendOrComputer.Name = "m_AgainstFriendOrComputer";
            this.m_AgainstFriendOrComputer.Size = new System.Drawing.Size(180, 32);
            this.m_AgainstFriendOrComputer.TabIndex = 6;
            this.m_AgainstFriendOrComputer.Text = "Against a friend";
            this.m_AgainstFriendOrComputer.UseVisualStyleBackColor = true;
            this.m_AgainstFriendOrComputer.Click += new System.EventHandler(this.m_AgainstFriendOrComputer_Click);
            // 
            // m_BoardSizeLabel
            // 
            this.m_BoardSizeLabel.AutoSize = true;
            this.m_BoardSizeLabel.Location = new System.Drawing.Point(35, 162);
            this.m_BoardSizeLabel.Name = "m_BoardSizeLabel";
            this.m_BoardSizeLabel.Size = new System.Drawing.Size(91, 20);
            this.m_BoardSizeLabel.TabIndex = 7;
            this.m_BoardSizeLabel.Text = "Board Size:";
            // 
            // m_ComputerLevel
            // 
            this.m_ComputerLevel.BackColor = System.Drawing.Color.DodgerBlue;
            this.m_ComputerLevel.Location = new System.Drawing.Point(421, 120);
            this.m_ComputerLevel.Name = "m_ComputerLevel";
            this.m_ComputerLevel.Size = new System.Drawing.Size(180, 40);
            this.m_ComputerLevel.TabIndex = 8;
            this.m_ComputerLevel.Text = "Easy";
            this.m_ComputerLevel.UseVisualStyleBackColor = false;
            this.m_ComputerLevel.Click += new System.EventHandler(this.m_ComputerLevel_Click);
            // 
            // Settings
            // 
            this.AcceptButton = this.m_StartButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 341);
            this.Controls.Add(this.m_ComputerLevel);
            this.Controls.Add(this.m_BoardSizeLabel);
            this.Controls.Add(this.m_AgainstFriendOrComputer);
            this.Controls.Add(this.m_StartButton);
            this.Controls.Add(this.m_BoardSizeButton);
            this.Controls.Add(this.m_TextBoxSecondPlayer);
            this.Controls.Add(this.m_TextBoxFirstPlayer);
            this.Controls.Add(this.m_SecondPlayerNameLabel);
            this.Controls.Add(this.m_FirstPlayerNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory Game - Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_FirstPlayerNameLabel;
        private System.Windows.Forms.Label m_SecondPlayerNameLabel;
        private System.Windows.Forms.TextBox m_TextBoxFirstPlayer;
        private System.Windows.Forms.TextBox m_TextBoxSecondPlayer;
        private System.Windows.Forms.Button m_BoardSizeButton;
        private System.Windows.Forms.Button m_StartButton;
        private System.Windows.Forms.Button m_AgainstFriendOrComputer;
        private System.Windows.Forms.Label m_BoardSizeLabel;
        private System.Windows.Forms.Button m_ComputerLevel;
    }
}