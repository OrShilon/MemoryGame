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


        public MemoryGame(int i_NumOfRows, int i_NumOfColumns, bool i_IsAgainstHuman, string i_FirstPlayerName, string i_SecondPlayerName) 
        {
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

            this.InitBoard(4, 5);

        }

        public void InitBoard(int i_Columns, int i_Rows)
        {
            m_BoardGame = new BoardGame(i_Columns, i_Rows);

            for (int i = 0; i < i_Rows; i++)
            {
                for (int j = 0; j < i_Columns; j++)
                {
                    global::MemoryGame.Square currentSquare = m_BoardGame.BoardGameWithSquares.m_SuqaresValue[i, j];
                    m_BoardGame.BoardGameWithButtons[i, j] = new MemoryGameButton(currentSquare);
                    m_BoardGame.BoardGameWithButtons[i, j].Click += new EventHandler(ButtonClicked);
                    m_BoardGame.BoardGameWithButtons[i, j].Size = new System.Drawing.Size(114, 92);
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
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            MemoryGameButton thisButton = sender as MemoryGameButton;
            thisButton.Text = thisButton.Square.letter.ToString();
        }
    }
}
