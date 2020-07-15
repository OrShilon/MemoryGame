using MemoryGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIManager
{
    public partial class NewMemoryGameWindows : Form
    {
        private const int k_ColumsOffset = 17; // When choosing (for example) square A1, then this offset will make the choice of columns to be A insted of '0'
        private const int k_RowOffset = 1; // Same as column offset, but for the rows
        private const bool k_HumanPlayer = true; // Use to create first player, that is always human
        public const int k_LettersInPair = 2; // Each letter has 2 appearances in the board game.
        private int m_NumOfColums;
        private int m_NumOfRows;
        private bool m_IsFirstPlayerTurn = true; // True means first player's turn, false means second player's turn.
        private bool m_IsComputerTurn = false;
        private bool m_IsGuessNumberOne = true;
        private bool m_HasInternetConnection;
        private BoardGameWindows m_BoardGame;
        private Label m_FirstPlayerScore;
        private Label m_SecondPlayerScore;
        private Label m_CurrentPlayersTurn;
        private MemoryGameButton m_FirstButtonGeuss;
        private MemoryGameButton m_SecondButtonGeuss;
        private Image[] m_GameImages;
        private MemoryGameButton m_ClickedButton;

        public NewMemoryGameWindows(int i_NumOfColumns, int i_NumOfRows, string i_FirstPlayerName, string i_SecondPlayerName, bool i_IsAgainstHuman)
        {
            m_NumOfColums = i_NumOfColumns;
            m_NumOfRows = i_NumOfRows;
            GameManager.m_FirstPlayer = new Player(i_FirstPlayerName, k_HumanPlayer);
            GameManager.m_SecondPlayer = new Player(i_SecondPlayerName, i_IsAgainstHuman);
            m_GameImages = new Image[(m_NumOfColums * m_NumOfRows) / k_LettersInPair]; // the number of images need is the number of (rows * number of columns) / 2
            InitializeComponents();
            m_BoardGame = new BoardGameWindows(m_NumOfColums, m_NumOfRows);
            CreateBoard();
            GameManager.StartGame(m_NumOfRows, m_NumOfColums, i_FirstPlayerName, i_SecondPlayerName, i_IsAgainstHuman);
        }

        public void CreateBoard()
        {
            WebClient w = new WebClient();

            try
            {
                for (int i = 0; i < m_GameImages.Length; i++)
                {
                    m_GameImages[i] = Image.FromStream(new MemoryStream(w.DownloadData("https://picsum.photos/80")));
                }

                m_HasInternetConnection = true;
            }
            catch(Exception we)
            {
                m_HasInternetConnection = false;
            }

            for (int i = 0; i < m_NumOfRows; i++)
            {
                for (int j = 0; j < m_NumOfColums; j++)
                {
                    buildSquareProperties(i, j);
                    //m_BoardGame.BoardGameWithButtons[i, j].Text = string.Empty;
                    //m_BoardGame.BoardGameWithButtons[i, j].UseVisualStyleBackColor = true;

                    // need to change 0 const
                    // First Square
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
                            constractFirstRow(i, j);
                        }
                        else
                        {
                            // need to change 0 const
                            if (j == 0)
                            {
                                constractFirstColumn(i, j);
                            }
                            else
                            {
                                constractMiddleSquares(i, j);
                            }
                        }
                    }

                    this.Controls.Add(m_BoardGame.BoardGameWithButtons[i, j]);
                }
            }

            CreatePlayersLabels();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void buildSquareProperties(int i_RowIndex, int i_ColumnIndex)
        {
            int columnIndex;
            int rowIndex;
            MemoryGame.Square currentSquare = m_BoardGame.BoardGameWithSquares.m_SuqaresValue[i_RowIndex, i_ColumnIndex];

            if(m_HasInternetConnection)
            {
                // להעביר לממורי גיים בוטום תמונה ספציפית ולא כל פעם את המערך של התמונות!!!!
                m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex] = new MemoryGameButton(currentSquare, m_GameImages);
            }
            else
            {
                m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex] = new MemoryGameButton(currentSquare);
            }

            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Click += new EventHandler(ButtonClicked);
            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].TabStop = false;
            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].FlatAppearance.BorderSize = 20;
            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Size = new System.Drawing.Size(80, 80);
            columnIndex = (int)(i_ColumnIndex + '0' + k_ColumsOffset);
            rowIndex = (int)(i_RowIndex + '0' + k_RowOffset);
            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Name = Convert.ToChar(columnIndex).ToString() + Convert.ToChar(rowIndex).ToString();
        }

        private void constractFirstRow(int i_RowIndex, int i_ColumnIndex)
        {
            int ButtonXLocation = m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex - 1].Location.X + m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Right + 12;
            int ButtonYLocation = m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex - 1].Location.Y;
            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Location = new System.Drawing.Point(ButtonXLocation, ButtonYLocation);
        }

        private void constractFirstColumn(int i_RowIndex, int i_ColumnIndex)
        {
            int ButtonXLocation = m_BoardGame.BoardGameWithButtons[i_RowIndex - 1, i_ColumnIndex].Location.X;
            int ButtonYLocation = m_BoardGame.BoardGameWithButtons[i_RowIndex - 1, i_ColumnIndex].Location.Y + m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Bottom + 12;
            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Location = new System.Drawing.Point(ButtonXLocation, ButtonYLocation);
        }

        private void constractMiddleSquares(int i, int j)
        {
            int ButtonXLocation = m_BoardGame.BoardGameWithButtons[i, j - 1].Location.X + m_BoardGame.BoardGameWithButtons[i, j].Right + 12;
            int ButtonYLocation = m_BoardGame.BoardGameWithButtons[i - 1, j].Location.Y + m_BoardGame.BoardGameWithButtons[i, j].Bottom + 12;
            m_BoardGame.BoardGameWithButtons[i, j].Location = new System.Drawing.Point(ButtonXLocation, ButtonYLocation);
        }

        private void CreatePlayersLabels()
        {
            // m_CurrentPlayersTurn
            this.m_CurrentPlayersTurn.AutoSize = true;
            int XLocationCurrent = m_BoardGame.BoardGameWithButtons[0, 0].Location.X;
            int YLocationCurrent = m_BoardGame.BoardGameWithButtons[m_NumOfRows - 1, 0].Bottom + 10;
            this.m_CurrentPlayersTurn.Location = new System.Drawing.Point(XLocationCurrent, YLocationCurrent);
            this.m_CurrentPlayersTurn.Name = "m_CurrentPlayersTurn";
            this.m_CurrentPlayersTurn.Size = new System.Drawing.Size(137, 20);
            this.m_CurrentPlayersTurn.TabIndex = 0;
            this.m_CurrentPlayersTurn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_CurrentPlayersTurn.Text = "Current Player: " + GameManager.m_FirstPlayer.Name;

            // m_FirstPlayerScore
            this.m_FirstPlayerScore.AutoSize = true;
            int XLocationFirst = m_CurrentPlayersTurn.Location.X;
            int YLocationFirst = m_CurrentPlayersTurn.Bottom + 10;
            this.m_FirstPlayerScore.Location = new System.Drawing.Point(XLocationFirst, YLocationFirst);
            this.m_FirstPlayerScore.Name = "m_FirstPlayerScore";
            this.m_FirstPlayerScore.Size = new System.Drawing.Size(137, 20);
            this.m_FirstPlayerScore.TabIndex = 0;
            this.m_FirstPlayerScore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_FirstPlayerScore.Text = GameManager.m_FirstPlayer.Name + ": " + GameManager.m_FirstPlayer.Score + (GameManager.m_FirstPlayer.Score < 2 ? " Pair(s)" : " Pairs");

            // m_SecondPlayerScore
            this.m_SecondPlayerScore.AutoSize = true;
            int XLocationSecond = m_FirstPlayerScore.Location.X;
            int YLocationSecond = m_FirstPlayerScore.Bottom + 10;
            this.m_SecondPlayerScore.Location = new System.Drawing.Point(XLocationSecond, YLocationSecond);
            this.m_SecondPlayerScore.Name = "m_SecondPlayerScore";
            this.m_SecondPlayerScore.Size = new System.Drawing.Size(137, 20);
            this.m_SecondPlayerScore.TabIndex = 0;
            this.m_SecondPlayerScore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_SecondPlayerScore.Text = GameManager.m_SecondPlayer.Name + ": " + GameManager.m_SecondPlayer.Score + (GameManager.m_SecondPlayer.Score < 2 ? " Pair(s)" : " Pairs");

            // add labels to form
            this.Controls.Add(m_CurrentPlayersTurn);
            this.Controls.Add(m_FirstPlayerScore);
            this.Controls.Add(m_SecondPlayerScore);

            // Set windows size
            // Need to change to const
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            int XWindowSize = m_BoardGame.BoardGameWithButtons[m_NumOfRows - 1, m_NumOfColums - 1].Right + 12;
            int YWindowSize = m_SecondPlayerScore.Bottom + 12;
            this.ClientSize = new System.Drawing.Size(XWindowSize, YWindowSize);
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            m_ClickedButton = sender as MemoryGameButton;
            if(m_HasInternetConnection)
            {
                m_ClickedButton.BackgroundImage = m_ClickedButton.ButtonImage;
            }
            else
            {
                m_ClickedButton.Text = m_ClickedButton.Square.letter.ToString();

            }

            m_ClickedButton.Click -= ButtonClicked;
            changeKnownLettersForComputer(m_ClickedButton);
            m_ClickedButton.Refresh();
            if (m_IsGuessNumberOne)
            {
                m_FirstButtonGeuss = m_ClickedButton;
                m_IsGuessNumberOne = !m_IsGuessNumberOne;
            }
            else
            {
                m_SecondButtonGeuss = m_ClickedButton;
                checkIfCurrectGuess();
            }
        }

        private void checkIfCurrectGuess()
        {
            Player currentPlayer = m_IsFirstPlayerTurn ? GameManager.m_FirstPlayer : GameManager.m_SecondPlayer;

            if (GameManager.isCorrectGuess(m_BoardGame.BoardGameWithSquares, m_FirstButtonGeuss.Name, m_SecondButtonGeuss.Name, currentPlayer))
            {
                m_FirstButtonGeuss.BackColor = m_IsFirstPlayerTurn ? m_FirstPlayerScore.BackColor : m_SecondPlayerScore.BackColor;
                m_SecondButtonGeuss.BackColor = m_FirstButtonGeuss.BackColor;
                changeScoreText();
                if (!m_IsFirstPlayerTurn && !GameManager.m_SecondPlayer.isHumanPlayer)
                {
                    Thread.Sleep(1000);
                }
            }
            else
            {
                Thread.Sleep(1500);
                m_IsFirstPlayerTurn = !m_IsFirstPlayerTurn;
                changeCurrentPlayerLabel();
                doWhenIncorrectGuess();
            }

            if (!m_IsComputerTurn)
            {
                m_IsGuessNumberOne = !m_IsGuessNumberOne;
            }

            // need to change to const
            if (GameManager.s_AvailbleMoves.Count == 0)
            {
                gameFinishedDialog();
            }
            else
            {
                m_FirstButtonGeuss.Refresh();
                m_SecondButtonGeuss.Refresh();
                m_CurrentPlayersTurn.Refresh();
                checkForComputerTurn();
            }
        }

        private void doWhenIncorrectGuess()
        {
            if (m_HasInternetConnection)
            {
                m_FirstButtonGeuss.BackgroundImage = null;
                m_SecondButtonGeuss.BackgroundImage = null;
            }
            else
            {
                m_FirstButtonGeuss.Text = string.Empty;
                m_SecondButtonGeuss.Text = string.Empty;
            }

            if (!m_IsComputerTurn)
            {
                m_FirstButtonGeuss.Click += ButtonClicked;
                m_SecondButtonGeuss.Click += ButtonClicked;
            }

            GameManager.s_AvailbleMoves.Add(m_FirstButtonGeuss.Name);
            GameManager.s_AvailbleMoves.Add(m_SecondButtonGeuss.Name);
        }


        private void checkForComputerTurn()
        {
            ////need to change 0 to const
            if (!m_IsFirstPlayerTurn && !GameManager.m_SecondPlayer.isHumanPlayer && GameManager.s_AvailbleMoves.Count > 0)
            {
                m_IsComputerTurn = true;
                doComputerTurn();
                m_IsComputerTurn = false;
            }
        }

        private void doComputerTurn()
        {
            string firstGuess;
            string secondGuess;
            GameManager.makeComputerTurn(out firstGuess, out secondGuess, m_BoardGame.BoardGameWithSquares);

            foreach (MemoryGameButton button in m_BoardGame.BoardGameWithButtons)
            {
                if (firstGuess.Equals(button.Name))
                {
                    m_FirstButtonGeuss = button;
                }

                if (secondGuess.Equals(button.Name))
                {
                    m_SecondButtonGeuss = button;
                }
            }

            computerClick(m_FirstButtonGeuss);
            Thread.Sleep(1500);
            computerClick(m_SecondButtonGeuss);
            checkIfCurrectGuess();
        }

        private void computerClick(MemoryGameButton i_ClickedButton)
        {
            if (m_HasInternetConnection)
            {
                i_ClickedButton.BackgroundImage = i_ClickedButton.ButtonImage;
            }
            else
            {
                i_ClickedButton.Text = i_ClickedButton.Square.letter.ToString();

            }

            changeKnownLettersForComputer(i_ClickedButton);
            i_ClickedButton.Refresh();
        }

        private void changeScoreText()
        {
            if (m_IsFirstPlayerTurn)
            {
                m_FirstPlayerScore.Text = GameManager.m_FirstPlayer.Name + ": " + GameManager.m_FirstPlayer.Score + (GameManager.m_FirstPlayer.Score < 2 ? " Pair(s)" : " Pairs");
                m_FirstPlayerScore.Refresh();
            }
            else
            {
                m_SecondPlayerScore.Text = GameManager.m_SecondPlayer.Name + ": " + GameManager.m_SecondPlayer.Score + (GameManager.m_SecondPlayer.Score < 2 ? " Pair(s)" : " Pairs");
                m_SecondPlayerScore.Refresh();
            }
        }

        private void changeKnownLettersForComputer(MemoryGameButton i_ChosenButton)
        {
            GameManager.s_AvailbleMoves.Remove(i_ChosenButton.Name);
            GameManager.s_ManageComputerTurns.KnownLetters(i_ChosenButton.Name, m_BoardGame.BoardGameWithSquares);
        }

        private void changeCurrentPlayerLabel()
        {
            if (m_IsFirstPlayerTurn)
            {
                m_CurrentPlayersTurn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                m_CurrentPlayersTurn.Text = "Current Player: " + GameManager.m_FirstPlayer.Name;
            }
            else
            {
                m_CurrentPlayersTurn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
                m_CurrentPlayersTurn.Text = "Current Player: " + GameManager.m_SecondPlayer.Name;
            }
        }

        private void gameFinishedDialog()
        {
            string winner;

            if (GameManager.m_FirstPlayer.Score > GameManager.m_SecondPlayer.Score)
            {
                winner = GameManager.m_FirstPlayer.Name + " won the game!";
            }
            else if (GameManager.m_FirstPlayer.Score < GameManager.m_SecondPlayer.Score)
            {
                winner = GameManager.m_SecondPlayer.Name + " won the game!";
            }
            else
            {
                winner = "Its a tie!";
            }

            DialogResult anotherRound = MessageBox.Show(winner + Environment.NewLine + "Do you want to play another round?", "Memory Game", MessageBoxButtons.YesNo);
            if (anotherRound == DialogResult.Yes)
            {
                restartGame();
            }
            else
            {
                exitProgram();
            }
        }

        private void exitProgram()
        {
            Environment.Exit(0);
        }

        private void restartGame()
        {
            Controls.Clear();
            m_IsFirstPlayerTurn = true;
            InitializeComponents();
            GameManager.m_FirstPlayer.Score = 0;
            GameManager.m_SecondPlayer.Score = 0;
            m_BoardGame = new BoardGameWindows(m_NumOfColums, m_NumOfRows);
            GameManager.StartGame(m_NumOfRows, m_NumOfColums, GameManager.m_FirstPlayer.Name, GameManager.m_SecondPlayer.Name, GameManager.m_SecondPlayer.isHumanPlayer);
            CreateBoard();
        }
    }
}