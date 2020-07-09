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
        private const string k_AgainstPlayer = "Against a Player";
        private const string k_AgainstComputer = "Against a Computer";
        private const string k_Computer = "-computer-";
        private readonly List<string> r_BoardSize = new List<string> {"4 x 4" , "4 x 5", "4 x 6", "5 x 4", "5 x 6", "6 x 4", "6 x 5", "6 x 6"};
        private int m_BoardSizePositionInList = 1;
        private Label m_FirstPlayerName;
        private TextBox m_TextBoxPlayer;
        private TextBox m_TextBoxFriend;
        private Button m_Against;
        private Label m_BoardSizeLabel;
        private Button m_BoardSizeButton;
        private Button m_StartButton;
        private Label m_SecondPlayerName;

        public Settings()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.m_FirstPlayerName = new System.Windows.Forms.Label();
            this.m_SecondPlayerName = new System.Windows.Forms.Label();
            this.m_TextBoxPlayer = new System.Windows.Forms.TextBox();
            this.m_TextBoxFriend = new System.Windows.Forms.TextBox();
            this.m_Against = new System.Windows.Forms.Button();
            this.m_BoardSizeLabel = new System.Windows.Forms.Label();
            this.m_BoardSizeButton = new System.Windows.Forms.Button();
            this.m_StartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_FirstPlayerName
            // 
            this.m_FirstPlayerName.AutoSize = true;
            this.m_FirstPlayerName.Location = new System.Drawing.Point(31, 33);
            this.m_FirstPlayerName.Name = "m_FirstPlayerName";
            this.m_FirstPlayerName.Size = new System.Drawing.Size(137, 20);
            this.m_FirstPlayerName.TabIndex = 0;
            this.m_FirstPlayerName.Text = "First Player Name:";
            this.m_FirstPlayerName.Click += new System.EventHandler(this.label1_Click);
            // 
            // m_SecondPlayerName
            // 
            this.m_SecondPlayerName.AutoSize = true;
            this.m_SecondPlayerName.Location = new System.Drawing.Point(31, 70);
            this.m_SecondPlayerName.Name = "m_SecondPlayerName";
            this.m_SecondPlayerName.Size = new System.Drawing.Size(161, 20);
            this.m_SecondPlayerName.TabIndex = 1;
            this.m_SecondPlayerName.Text = "Second Player Name:";
            this.m_SecondPlayerName.Click += new System.EventHandler(this.label2_Click);
            // 
            // m_TextBoxPlayer
            // 
            this.m_TextBoxPlayer.Location = new System.Drawing.Point(206, 33);
            this.m_TextBoxPlayer.Name = "m_TextBoxPlayer";
            this.m_TextBoxPlayer.Size = new System.Drawing.Size(204, 26);
            this.m_TextBoxPlayer.TabIndex = 2;
            // 
            // m_TextBoxFriend
            // 
            this.m_TextBoxFriend.Enabled = false;
            this.m_TextBoxFriend.Location = new System.Drawing.Point(206, 70);
            this.m_TextBoxFriend.Name = "m_TextBoxFriend";
            this.m_TextBoxFriend.Size = new System.Drawing.Size(204, 26);
            this.m_TextBoxFriend.TabIndex = 3;
            this.m_TextBoxFriend.Text = "-computer-";
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
            this.m_StartButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Settings
            // 
            this.ClientSize = new System.Drawing.Size(590, 325);
            this.Controls.Add(this.m_StartButton);
            this.Controls.Add(this.m_BoardSizeButton);
            this.Controls.Add(this.m_BoardSizeLabel);
            this.Controls.Add(this.m_Against);
            this.Controls.Add(this.m_TextBoxFriend);
            this.Controls.Add(this.m_TextBoxPlayer);
            this.Controls.Add(this.m_SecondPlayerName);
            this.Controls.Add(this.m_FirstPlayerName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Memory game - Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void m_BoardSizeButton_Click(object sender, EventArgs e)
        {
            m_BoardSizeButton.Text = r_BoardSize[m_BoardSizePositionInList];

            //need to change 1 to const
            if(m_BoardSizePositionInList == (r_BoardSize.Count - 1))
            {
                m_BoardSizePositionInList = 0;
            }
            else
            {
                m_BoardSizePositionInList++;
            }
        }

        private void m_Against_Click(object sender, EventArgs e)
        {
            m_TextBoxFriend.Enabled = !m_TextBoxFriend.Enabled;

            if (m_Against.Text.Equals(k_AgainstPlayer))
            {
                m_Against.Text = k_AgainstComputer;
                m_TextBoxFriend.Text = k_Computer;
            }
            else
            {
                m_Against.Text = k_AgainstPlayer;
                m_TextBoxFriend.Text = String.Empty;
            }

        }
    }
}