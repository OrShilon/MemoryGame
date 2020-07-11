using MemoryGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIManager
{
    class MemoryGameWindows : Form
    {
        private const int k_ColumsOffset = 17; // When choosing (for example) square A1, then this offset will make the coice of columns to be A insted of '0'
        private const int k_RowOffset = 1; // Same as column offset, but for the rows
        private BoardGameWindows m_BoardGame;
        private Label m_FirstPlayerScore;
        private Label m_SecondPlayerScore;
        private Label m_CurrentPlayersTurn;
        private int m_NumOfColums;
        private int m_NumOfRows;
        private bool m_IsAgainstHuman;
        private bool m_IsFirstPlayerTurn = true; // True means first player's turn, false means second player's turn.
        private const bool k_HumanPlayer = true; // Use to create first player, that is always human
        public const int k_LettersInPair = 2; // Each letter has 2 appearances in the board game.
        private static int s_PointsLeftUntilFinish;
        MemoryGame.Player m_FirstPlayer;
        MemoryGame.Player m_SecondPlayer;
        private MemoryGameButton m_FirstButtonGeuss;
        private MemoryGameButton m_SecondButtonGeuss;
        private bool m_IsGuessNumberOne = true;
        private bool m_IsCorrectGuess;


        public MemoryGameWindows(int i_NumOfColumns, int i_NumOfRows, bool i_IsAgainstHuman, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            m_NumOfColums = i_NumOfColumns;
            m_NumOfRows = i_NumOfRows;
            m_IsAgainstHuman = i_IsAgainstHuman;
            m_FirstPlayer = new MemoryGame.Player(i_FirstPlayerName, k_HumanPlayer);
            m_SecondPlayer = new MemoryGame.Player(i_SecondPlayerName, i_IsAgainstHuman);
            s_PointsLeftUntilFinish = (m_NumOfColums * m_NumOfRows) / k_LettersInPair;
            InitializeComponent();
            CreateBoard();
            GameManager.StartGame(m_NumOfRows, m_NumOfColums);
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
            m_BoardGame = new BoardGameWindows(m_NumOfColums, m_NumOfRows);
            int columnIndex;
            int rowIndex;

            for (int i = 0; i < m_NumOfRows; i++)
            {
                for (int j = 0; j < m_NumOfColums; j++)
                {
                    MemoryGame.Square currentSquare = m_BoardGame.BoardGameWithSquares.m_SuqaresValue[i, j];
                    m_BoardGame.BoardGameWithButtons[i, j] = new MemoryGameButton(currentSquare);
                    m_BoardGame.BoardGameWithButtons[i, j].Click += new EventHandler(ButtonClicked);
                    m_BoardGame.BoardGameWithButtons[i, j].Size = new System.Drawing.Size(80, 80);
                    columnIndex = (int)(j + '0' + k_ColumsOffset);
                    rowIndex = (int)(i + '0' + k_RowOffset);
                    m_BoardGame.BoardGameWithButtons[i, j].Name = Convert.ToChar(columnIndex).ToString() + Convert.ToChar(rowIndex).ToString();
                    Console.WriteLine(m_BoardGame.BoardGameWithButtons[i, j].Name);
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

            // add labels to form
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
            const int k_EndOfTheGame = 0;
            string firstSquareGuess = m_FirstButtonGeuss.Text;
            string secondSqureGuess;

           /* if (m_IsFirstPlayerTurn)
            {
                //first player turn
                    
                m_FirstButtonGeuss = m_BoardGame.BoardGameWithButtons[m_FirstSquareGuessed[0] - MemoryGame.gameManager.k_BottomLetersBound, m_FirstSquareGuessed[1] - MemoryGame.gameManager.k_BottomnumbersBound];
                m_SecondButtonGeuss = m_BoardGame.BoardGameWithButtons[m_SecundSquareGuessed[0] - MemoryGame.gameManager.k_BottomLetersBound, m_SecundSquareGuessed[1] - MemoryGame.gameManager.k_BottomnumbersBound];
            }
            else if (m_IsAgainstHuman)
            {
                //second player turn and he is human
                m_FirstButtonGeuss = m_BoardGame.BoardGameWithButtons[m_FirstSquareGuessed[0] - MemoryGame.gameManager.k_BottomLetersBound, m_FirstSquareGuessed[1] - MemoryGame.gameManager.k_BottomnumbersBound];
                m_SecondButtonGeuss = m_BoardGame.BoardGameWithButtons[m_SecundSquareGuessed[0] - MemoryGame.gameManager.k_BottomLetersBound, m_SecundSquareGuessed[1] - MemoryGame.gameManager.k_BottomnumbersBound];
            }*/



                /* makeTurn(out firstSquareGuessed, out secundSquareGuessed, isFirstPlayerTurn, isGuessNumberOne);
                 if (isCorrectGuess(firstSquareGuessed, secundSquareGuessed))
                 {
                     Thread.Sleep(1000);
                     increaseScore(isFirstPlayerTurn);
                     s_PointsLeftUntilFinish--;
                     s_AvailbleMoves.Remove(firstSquareGuessed);
                     s_AvailbleMoves.Remove(secundSquareGuessed);
                 }
                 else
                 {
                     Thread.Sleep(1000);
                     s_AvailbleMoves.Add(firstSquareGuessed);
                     s_BoardGame.m_SuqaresValue[firstSquareGuessed[1] - k_BottomnumbersBound, firstSquareGuessed[0] - k_BottomLetersBound].visible = false;
                     s_BoardGame.m_SuqaresValue[secundSquareGuessed[1] - k_BottomnumbersBound, secundSquareGuessed[0] - k_BottomLetersBound].visible = false;
                     isFirstPlayerTurn = !isFirstPlayerTurn;
                 }*/
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            MemoryGameButton thisButton = sender as MemoryGameButton;

            thisButton.Text = thisButton.Square.letter.ToString();
            thisButton.Enabled = false;
            changeSeenLetters(thisButton);
            thisButton.Refresh();
            if (m_IsGuessNumberOne)
            {
                m_FirstButtonGeuss = thisButton;
                m_IsGuessNumberOne = !m_IsGuessNumberOne;
            }
            else
            {
                m_SecondButtonGeuss = thisButton;
                if(GameManager.isCorrectGuess(m_BoardGame.BoardGameWithSquares, m_FirstButtonGeuss.Name, m_SecondButtonGeuss.Name, m_IsFirstPlayerTurn ? m_FirstPlayer : m_SecondPlayer))
                {
                    m_FirstButtonGeuss.BackColor = m_IsFirstPlayerTurn ? m_FirstPlayerScore.BackColor : m_SecondPlayerScore.BackColor;
                    m_SecondButtonGeuss.BackColor = m_IsFirstPlayerTurn ? m_FirstPlayerScore.BackColor : m_SecondPlayerScore.BackColor;
                    changeScoreText();
                }
                else
                {
                    Thread.Sleep(1500);
                    m_FirstButtonGeuss.Text = string.Empty;
                    m_SecondButtonGeuss.Text = string.Empty;
                    m_IsFirstPlayerTurn = !m_IsFirstPlayerTurn;
                    changeCurrentPlayerLabel();
                    doWhenIncorrectGuess();
                }
                m_IsGuessNumberOne = !m_IsGuessNumberOne;
                checkForComputerTurn();

            }
        }

        private void changeSeenLetters(MemoryGameButton i_ChosenButton)
        {
            GameManager.s_AvailbleMoves.Remove(i_ChosenButton.Name);
            GameManager.s_ManageComputerTurns.KnownLetters(i_ChosenButton.Name, m_BoardGame.BoardGameWithSquares);
            i_ChosenButton.Enabled = false;
        }

        private void doWhenIncorrectGuess()
        {
            m_FirstButtonGeuss.Enabled = true;
            m_SecondButtonGeuss.Enabled = true;
            GameManager.s_AvailbleMoves.Add(m_FirstButtonGeuss.Name);
            GameManager.s_AvailbleMoves.Add(m_SecondButtonGeuss.Name);
        }

        private void changeScoreText()
        {
            if(m_IsFirstPlayerTurn)
            {
                m_FirstPlayerScore.Text = this.m_FirstPlayer.Name + ": " + this.m_FirstPlayer.Score + (this.m_FirstPlayer.Score < 2 ? " Pair(s)" : " Pairs");
            }
            else
            {
                m_SecondPlayerScore.Text = this.m_SecondPlayer.Name + ": " + this.m_SecondPlayer.Score + (this.m_SecondPlayer.Score < 2 ? " Pair(s)" : " Pairs");
            }
        }

        private void changeCurrentPlayerLabel()
        {
            if (m_IsFirstPlayerTurn)
            {
                m_CurrentPlayersTurn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                m_CurrentPlayersTurn.Text = "Current Player: " + this.m_FirstPlayer.Name;
            }
            else
            {
                m_CurrentPlayersTurn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
                m_CurrentPlayersTurn.Text = "Current Player: " + this.m_SecondPlayer.Name;
            }
        }

        private void checkForComputerTurn()
        {
            if(!m_IsFirstPlayerTurn && !m_SecondPlayer.isHumanPlayer)
            {
                // disable all buttons becouse it is computer turn......

            }
        }

        private void doComputerTurn()
        {
            string firstSquareGuess;
            string secondSquareGuess;

            MemoryGame.GameManager.makeComputerTurn(out firstSquareGuess, out secondSquareGuess);
            m_FirstButtonGeuss = m_BoardGame.BoardGameWithButtons[firstSquareGuess[0] - MemoryGame.GameManager.k_BottomLetersBound, firstSquareGuess[1] - MemoryGame.GameManager.k_BottomnumbersBound];
            m_SecondButtonGeuss = m_BoardGame.BoardGameWithButtons[secondSquareGuess[0] - MemoryGame.GameManager.k_BottomLetersBound, secondSquareGuess[1] - MemoryGame.GameManager.k_BottomnumbersBound];
            m_FirstButtonGeuss.PerformClick();
            Thread.Sleep(1000);
            m_SecondButtonGeuss.PerformClick();
        }
    }
}
