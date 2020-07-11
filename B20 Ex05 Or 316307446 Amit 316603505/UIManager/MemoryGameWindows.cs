using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIManager
{
    class MemoryGameWindows : Form
    {
        private BoardGame m_BoardGame;
        private Label m_FirstPlayerScore;
        private Label m_SecondPlayerScore;
        private Label m_CurrentPlayersTurn;
        private int m_NumOfColums;
        private int m_NumOfRows;
        private bool m_IsAgainstHuman;
        private bool m_IsFirstPlayerTurn = true; // True means first player's turn, false means second player's turn.
        private const bool k_HumanPlayer = true; // Use to create first player, that is always human
        MemoryGame.Player m_FirstPlayer;
        MemoryGame.Player m_SecondPlayer;


        public MemoryGameWindows(int i_NumOfColumns, int i_NumOfRows, bool i_IsAgainstHuman, string i_FirstPlayerName, string i_SecondPlayerName) 
        {
            m_NumOfColums = i_NumOfColumns;
            m_NumOfRows = i_NumOfRows;
            m_IsAgainstHuman = i_IsAgainstHuman;
            m_FirstPlayer = new MemoryGame.Player(i_FirstPlayerName, k_HumanPlayer);
            m_SecondPlayer = new MemoryGame.Player(i_SecondPlayerName, i_IsAgainstHuman);
            InitializeComponent();
            CreateBoard();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MemoryGame
            // 
            //this.ClientSize = new System.Drawing.Size(747, 451);
            this.Name = "MemoryGame";
            this.Text = "Memory Game";
            this.ResumeLayout(false);
            this.m_CurrentPlayersTurn = new System.Windows.Forms.Label();
            this.m_FirstPlayerScore = new System.Windows.Forms.Label();
            this.m_SecondPlayerScore = new System.Windows.Forms.Label();
        }

        public void CreateBoard()
        {
            m_BoardGame = new BoardGame(m_NumOfColums, m_NumOfRows);

            for (int i = 0; i < m_NumOfRows; i++)
            {
                for (int j = 0; j < m_NumOfColums; j++)
                {
                    MemoryGame.Square currentSquare = m_BoardGame.BoardGameWithSquares.m_SuqaresValue[i, j];
                    m_BoardGame.BoardGameWithButtons[i, j] = new MemoryGameButton(currentSquare);
                    m_BoardGame.BoardGameWithButtons[i, j].Click += new EventHandler(ButtonClicked);
                    m_BoardGame.BoardGameWithButtons[i, j].Size = new System.Drawing.Size(80, 80);
                    m_BoardGame.BoardGameWithButtons[i, j].Name = "Card" + i + j;
                    m_BoardGame.BoardGameWithButtons[i, j].Text = string.Empty;
                    m_BoardGame.BoardGameWithButtons[i, j].UseVisualStyleBackColor = true;

                    // need to change 0 const
                    if (i == 0 && j == 0)
                    {
                        m_BoardGame.BoardGameWithButtons[i, j].Location = new System.Drawing.Point(12, 12);
                    }
                    else
                    {
                        // if true, we are in the first row
                        // need to change 0 const
                        if (i == 0)
                        {
                            int ButtonXLocation = m_BoardGame.BoardGameWithButtons[i, j - 1].Location.X + m_BoardGame.BoardGameWithButtons[i, j].Right + 12;
                            int ButtonYLocation = m_BoardGame.BoardGameWithButtons[i, j - 1].Location.Y;
                            m_BoardGame.BoardGameWithButtons[i, j].Location = new System.Drawing.Point(ButtonXLocation, ButtonYLocation);
                        }
                        else
                        {
                            // need to change 0 const
                            if (j == 0)
                            {
                                int ButtonXLocation = m_BoardGame.BoardGameWithButtons[i - 1, j].Location.X;
                                int ButtonYLocation = m_BoardGame.BoardGameWithButtons[i - 1, j].Location.Y + m_BoardGame.BoardGameWithButtons[i, j].Bottom + 12;
                                m_BoardGame.BoardGameWithButtons[i, j].Location = new System.Drawing.Point(ButtonXLocation, ButtonYLocation);
                            }
                            else
                            {
                                int ButtonXLocation = m_BoardGame.BoardGameWithButtons[i, j - 1].Location.X + m_BoardGame.BoardGameWithButtons[i, j].Right + 12;
                                int ButtonYLocation = m_BoardGame.BoardGameWithButtons[i - 1, j].Location.Y + m_BoardGame.BoardGameWithButtons[i, j].Bottom + 12;
                                m_BoardGame.BoardGameWithButtons[i, j].Location = new System.Drawing.Point(ButtonXLocation, ButtonYLocation);
                            }
                        }
                    }

                    this.Controls.Add(m_BoardGame.BoardGameWithButtons[i, j]);
                }
            }

            // 
            // m_CurrentPlayersTurn
            //
            this.m_CurrentPlayersTurn.AutoSize = true;
            int XLocationCurrent = m_BoardGame.BoardGameWithButtons[0, 0].Location.X;
            int YLocationCurrent = m_BoardGame.BoardGameWithButtons[m_NumOfRows - 1, 0].Bottom + 10;
            this.m_CurrentPlayersTurn.Location = new System.Drawing.Point(XLocationCurrent, YLocationCurrent);
            this.m_CurrentPlayersTurn.Name = "m_CurrentPlayersTurn";
            this.m_CurrentPlayersTurn.Size = new System.Drawing.Size(137, 20);
            this.m_CurrentPlayersTurn.TabIndex = 0;
            this.m_CurrentPlayersTurn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_CurrentPlayersTurn.Text = "Current Player: " + this.m_FirstPlayer.Name;


            // 
            // m_FirstPlayerScore
            // 
            this.m_FirstPlayerScore.AutoSize = true;
            int XLocationFirst = m_CurrentPlayersTurn.Location.X;
            int YLocationFirst = m_CurrentPlayersTurn.Bottom + 10;
            this.m_FirstPlayerScore.Location = new System.Drawing.Point(XLocationFirst, YLocationFirst);
            this.m_FirstPlayerScore.Name = "m_FirstPlayerScore";
            this.m_FirstPlayerScore.Size = new System.Drawing.Size(137, 20);
            this.m_FirstPlayerScore.TabIndex = 0;
            this.m_FirstPlayerScore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_FirstPlayerScore.Text = this.m_FirstPlayer.Name + ": " + this.m_FirstPlayer.Score + (this.m_FirstPlayer.Score < 2 ? " Pair(s)" : " Pairs");

            // 
            // m_SecondPlayerScore
            //
            this.m_SecondPlayerScore.AutoSize = true;
            int XLocationSecond = m_FirstPlayerScore.Location.X;
            int YLocationSecond = m_FirstPlayerScore.Bottom + 10;
            this.m_SecondPlayerScore.Location = new System.Drawing.Point(XLocationSecond, YLocationSecond);
            this.m_SecondPlayerScore.Name = "m_SecondPlayerScore";
            this.m_SecondPlayerScore.Size = new System.Drawing.Size(137, 20);
            this.m_SecondPlayerScore.TabIndex = 0;
            this.m_SecondPlayerScore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_SecondPlayerScore.Text = this.m_SecondPlayer.Name + ": " + this.m_SecondPlayer.Score + (this.m_SecondPlayer.Score < 2 ? " Pair(s)" : " Pairs");

            this.Controls.Add(m_CurrentPlayersTurn);
            this.Controls.Add(m_FirstPlayerScore);
            this.Controls.Add(m_SecondPlayerScore);

            // Set windows size
            // Need to change to const
            int XWindowSize = m_BoardGame.BoardGameWithButtons[m_NumOfRows - 1, m_NumOfColums - 1].Right + 12;
            int YWindowSize = m_SecondPlayerScore.Bottom + 12;
            this.ClientSize = new System.Drawing.Size(XWindowSize, YWindowSize);
        }

        private void manageGame()
        {

        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            MemoryGameButton thisButton = sender as MemoryGameButton;
            thisButton.Text = thisButton.Square.letter.ToString();
        }
    }
}
