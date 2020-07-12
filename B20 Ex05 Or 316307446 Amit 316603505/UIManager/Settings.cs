using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIManager
{
    public partial class Settings : Form
    {
        private Label m_FirstPlayerNameLabel;
        private Label m_SecondPlayerNameLabel;
        private TextBox m_TextBoxFirstPlayer;
        private TextBox m_TextBoxSecondPlayer;
        private Button m_Against;
        private Label m_BoardSizeLabel;
        private Button m_BoardSizeButton;
        private Button m_StartButton;

        public Settings()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void InitializeComponent()
        {
            this.m_FirstPlayerNameLabel = new System.Windows.Forms.Label();
            this.m_SecondPlayerNameLabel = new System.Windows.Forms.Label();
            this.m_TextBoxFirstPlayer = new System.Windows.Forms.TextBox();
            this.m_TextBoxSecondPlayer = new System.Windows.Forms.TextBox();
            this.m_Against = new System.Windows.Forms.Button();
            this.m_BoardSizeLabel = new System.Windows.Forms.Label();
            this.m_BoardSizeButton = new System.Windows.Forms.Button();
            this.m_StartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_FirstPlayerNameLabel
            // 
            this.m_FirstPlayerNameLabel.AutoSize = true;
            this.m_FirstPlayerNameLabel.Location = new System.Drawing.Point(31, 33);
            this.m_FirstPlayerNameLabel.Name = "m_FirstPlayerNameLabel";
            this.m_FirstPlayerNameLabel.Size = new System.Drawing.Size(137, 20);
            this.m_FirstPlayerNameLabel.TabIndex = 0;
            this.m_FirstPlayerNameLabel.Text = "First Player Name:";
            // 
            // m_SecondPlayerNameLabel
            // 
            this.m_SecondPlayerNameLabel.AutoSize = true;
            this.m_SecondPlayerNameLabel.Location = new System.Drawing.Point(31, 70);
            this.m_SecondPlayerNameLabel.Name = "m_SecondPlayerNameLabel";
            this.m_SecondPlayerNameLabel.Size = new System.Drawing.Size(161, 20);
            this.m_SecondPlayerNameLabel.TabIndex = 1;
            this.m_SecondPlayerNameLabel.Text = "Second Player Name:";
            // 
            // m_TextBoxFirstPlayer
            // 
            this.m_TextBoxFirstPlayer.Location = new System.Drawing.Point(206, 33);
            this.m_TextBoxFirstPlayer.Name = "m_TextBoxFirstPlayer";
            this.m_TextBoxFirstPlayer.Size = new System.Drawing.Size(204, 26);
            this.m_TextBoxFirstPlayer.TabIndex = 2;
            // 
            // m_TextBoxSecondPlayer
            // 
            this.m_TextBoxSecondPlayer.Enabled = false;
            this.m_TextBoxSecondPlayer.Location = new System.Drawing.Point(206, 70);
            this.m_TextBoxSecondPlayer.Name = "m_TextBoxSecondPlayer";
            this.m_TextBoxSecondPlayer.Size = new System.Drawing.Size(204, 26);
            this.m_TextBoxSecondPlayer.TabIndex = 3;
            this.m_TextBoxSecondPlayer.Text = "-computer-";
            // 
            // m_Against
            // 
            this.m_Against.Location = new System.Drawing.Point(432, 70);
            this.m_Against.Name = "m_Against";
            this.m_Against.Size = new System.Drawing.Size(140, 26);
            this.m_Against.TabIndex = 4;
            this.m_Against.Text = "Against a Friend";
            this.m_Against.UseVisualStyleBackColor = true;
            this.m_Against.Click += new System.EventHandler(this.m_Against_Click);
            // 
            // m_BoardSizeLabel
            // 
            this.m_BoardSizeLabel.AutoSize = true;
            this.m_BoardSizeLabel.Location = new System.Drawing.Point(31, 151);
            this.m_BoardSizeLabel.Name = "m_BoardSizeLabel";
            this.m_BoardSizeLabel.Size = new System.Drawing.Size(91, 20);
            this.m_BoardSizeLabel.TabIndex = 5;
            this.m_BoardSizeLabel.Text = "Board Size:";
            // 
            // m_BoardSizeButton
            // 
            this.m_BoardSizeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_BoardSizeButton.Location = new System.Drawing.Point(35, 184);
            this.m_BoardSizeButton.Name = "m_BoardSizeButton";
            this.m_BoardSizeButton.Size = new System.Drawing.Size(179, 124);
            this.m_BoardSizeButton.TabIndex = 6;
            this.m_BoardSizeButton.Text = "4 x 4";
            this.m_BoardSizeButton.UseVisualStyleBackColor = false;
            this.m_BoardSizeButton.Click += new System.EventHandler(this.m_BoardSizeButton_Click);
            // 
            // m_StartButton
            // 
            this.m_StartButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.m_StartButton.Location = new System.Drawing.Point(421, 273);
            this.m_StartButton.Name = "m_StartButton";
            this.m_StartButton.Size = new System.Drawing.Size(151, 35);
            this.m_StartButton.TabIndex = 7;
            this.m_StartButton.Text = "Start!";
            this.m_StartButton.UseVisualStyleBackColor = false;
            this.m_StartButton.Click += new System.EventHandler(this.m_StartButton_Click);
            // 
            // Settings
            // 
            this.ClientSize = new System.Drawing.Size(590, 325);
            this.Controls.Add(this.m_StartButton);
            this.Controls.Add(this.m_BoardSizeButton);
            this.Controls.Add(this.m_BoardSizeLabel);
            this.Controls.Add(this.m_Against);
            this.Controls.Add(this.m_TextBoxSecondPlayer);
            this.Controls.Add(this.m_TextBoxFirstPlayer);
            this.Controls.Add(this.m_SecondPlayerNameLabel);
            this.Controls.Add(this.m_FirstPlayerNameLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory game - Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}