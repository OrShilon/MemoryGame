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
        private Button button1;
        private BoardGame m_BoardGame;
        private int m_NumOfColums;
        private int m_NumOfRows;
        private bool m_IsAgainstHuman;
        private string m_FirstPlayerName;
        private string m_SecondPlayerName;


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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MemoryGame
            // 
            this.ClientSize = new System.Drawing.Size(747, 451);
            this.Controls.Add(this.button1);
            this.Name = "MemoryGame";
            this.Text = "Memory Game";
            this.ResumeLayout(false);

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
                    m_BoardGame.BoardGameWithButtons[i, j].Size = new System.Drawing.Size(101, 90);
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
