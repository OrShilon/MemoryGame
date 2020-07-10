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
    class MemoryGame : Form
    {
        private BoardGame m_BoardGame;
        private int m_NumOfColums;
        private int m_NumOfRows;
        private bool m_IsAgainstHuman;
        private string m_FirstPlayerName;
        private string m_SecondPlayerName;
        private Label m_FirstPlayerScore;
        private Label m_SecondPlayerScore;
        private Label m_CurrentPlayersTurn;


        public MemoryGame(int i_NumOfColumns, int i_NumOfRows, bool i_IsAgainstHuman, string i_FirstPlayerName, string i_SecondPlayerName) 
        {
            m_NumOfColums = i_NumOfColumns;
            m_NumOfRows = i_NumOfRows;
            m_IsAgainstHuman = i_IsAgainstHuman;
            m_FirstPlayerName = i_FirstPlayerName;
            m_SecondPlayerName = i_SecondPlayerName;
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MemoryGame
            // 
            this.ClientSize = new System.Drawing.Size(747, 451);
            this.Name = "MemoryGame";
            this.Text = "Memory Game";
            this.ResumeLayout(false);
            this.m_FirstPlayerScore = new System.Windows.Forms.Label();
            this.m_SecondPlayerScore = new System.Windows.Forms.Label();
            this.m_CurrentPlayersTurn = new System.Windows.Forms.Label();

            InitBoard();
        }

        public void InitBoard()
        {
            m_BoardGame = new BoardGame(m_NumOfColums, m_NumOfRows);

            for (int i = 0; i < m_NumOfRows; i++)
            {
                for (int j = 0; j < m_NumOfColums; j++)
                {
                    global::MemoryGame.Square currentSquare = m_BoardGame.BoardGameWithSquares.m_SuqaresValue[i, j];
                    m_BoardGame.BoardGameWithButtons[i, j] = new MemoryGameButton(currentSquare);
                    m_BoardGame.BoardGameWithButtons[i, j].Click += new EventHandler(ButtonClicked);
                    m_BoardGame.BoardGameWithButtons[i, j].Size = new System.Drawing.Size(100, 90);
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
                            int ButtonXLocation = m_BoardGame.BoardGameWithButtons[i, j - 1].Location.X + m_BoardGame.BoardGameWithButtons[i, j].Right + 24;
                            int ButtonYLocation = m_BoardGame.BoardGameWithButtons[i, j - 1].Location.Y;
                            m_BoardGame.BoardGameWithButtons[i, j].Location = new System.Drawing.Point(ButtonXLocation, ButtonYLocation);
                        }
                        else
                        {
                            // need to change 0 const
                            if (j == 0)
                            {
                                int ButtonXLocation = m_BoardGame.BoardGameWithButtons[i - 1, j].Location.X;
                                int ButtonYLocation = m_BoardGame.BoardGameWithButtons[i - 1, j].Location.Y + m_BoardGame.BoardGameWithButtons[i, j].Bottom + 24;
                                m_BoardGame.BoardGameWithButtons[i, j].Location = new System.Drawing.Point(ButtonXLocation, ButtonYLocation);
                            }
                            else
                            {
                                int ButtonXLocation = m_BoardGame.BoardGameWithButtons[i, j - 1].Location.X + m_BoardGame.BoardGameWithButtons[i, j].Right + 24;
                                int ButtonYLocation = m_BoardGame.BoardGameWithButtons[i - 1, j].Location.Y + m_BoardGame.BoardGameWithButtons[i, j].Bottom + 24;
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
            /*
            System.Drawing.Point temp = m_BoardGame.BoardGameWithButtons[0, 3].Location;
            this.m_CurrentPlayersTurn.AutoSize = true;
            int XLocation = m_BoardGame.BoardGameWithButtons[0, 0].Location.X;
            int YLocation = m_BoardGame.BoardGameWithButtons[3, 0].Location.Y + 20;
            this.m_CurrentPlayersTurn.Location = new System.Drawing.Point(XLocation, YLocation);
            this.m_CurrentPlayersTurn.Name = "m_CurrentPlayersTurn";
            this.m_CurrentPlayersTurn.Size = new System.Drawing.Size(137, 20);
            this.m_CurrentPlayersTurn.TabIndex = 0;
            this.m_CurrentPlayersTurn.Text = "Current Player:{0}";


            // 
            // m_FirstPlayerScore
            // 
            this.m_FirstPlayerScore.AutoSize = true;
            this.m_FirstPlayerScore.Location = new System.Drawing.Point(m_CurrentPlayersTurn.Location.X, m_CurrentPlayersTurn.Location.Y + 20);
            this.m_FirstPlayerScore.Name = "m_FirstPlayerScore";
            this.m_FirstPlayerScore.Size = new System.Drawing.Size(137, 20);
            this.m_FirstPlayerScore.TabIndex = 0;
            this.m_FirstPlayerScore.Text = "First Player: {Score}";

            // 
            // m_SecondPlayerScore
            // 
            this.m_SecondPlayerScore.AutoSize = true;
            this.m_SecondPlayerScore.Location = new System.Drawing.Point(m_FirstPlayerScore.Location.X, m_FirstPlayerScore.Location.Y + 20);
            this.m_SecondPlayerScore.Name = "m_SecondPlayerScore";
            this.m_SecondPlayerScore.Size = new System.Drawing.Size(137, 20);
            this.m_SecondPlayerScore.TabIndex = 0;
            this.m_SecondPlayerScore.Text = "Second Player {Score}";

            this.Controls.Add(m_CurrentPlayersTurn);
            this.Controls.Add(m_FirstPlayerScore);
            this.Controls.Add(m_SecondPlayerScore);
            */
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            MemoryGameButton thisButton = sender as MemoryGameButton;
            thisButton.Text = thisButton.Square.letter.ToString();
        }
    }
}
