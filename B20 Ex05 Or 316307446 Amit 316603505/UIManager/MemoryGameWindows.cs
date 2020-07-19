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
    public partial class MemoryGameWindows : Form
    {
        private const int k_ColumsOffset = 17; // When choosing (for example) square A1, then this offset will make the choice of columns to be A insted of '0'
        private const int k_RowOffset = 1; // Same as column offset, but for the rows
        private const bool v_HumanPlayer = true; // Use to create first player, that is always human
        public const int k_LettersInPair = 2; // Each letter has 2 appearances in the board game.
        private const int k_FirstRowOrColumn = 0; // This const is to check whether we are trying to build a square from the first row / column
        private const int k_BuildSquareOffset = 1; // When constructing a sqaure we need to build it proportional to the previous square and the square above. This offset access them.  
        private const int k_SpaceBetweenSquares = 12; // Fixed space between each square.
        private const int k_SpaceBetweenWindowToButtons = 12; // Fixed space between window to buttons.
        private const int k_SpaceBetweenPlayerLabels = 10; // Fixed space between each label of players.
        private const int k_IsMultiple = 2; // When a player has zero or single pair, this const will make the windows show pair(s), and if mulpitle - pairs.
        private const int k_SquareSize = 80;
        private const int k_EmptyList = 0;
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
        private MemoryGameButton m_CurrentButtonClickedByPlayer; // Onlu used by a human player

        public MemoryGameWindows(int i_NumOfColumns, int i_NumOfRows, string i_FirstPlayerName, string i_SecondPlayerName, bool i_IsAgainstHuman)
        {
            m_NumOfColums = i_NumOfColumns;
            m_NumOfRows = i_NumOfRows;
            GameManager.m_FirstPlayer = new Player(i_FirstPlayerName, v_HumanPlayer);
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

                    // First Square (top left one)
                    if (i == k_FirstRowOrColumn && j == k_FirstRowOrColumn)
                    {
                        m_BoardGame.BoardGameWithButtons[i, j].Location = new Point(k_SpaceBetweenSquares, k_SpaceBetweenSquares);
                    }
                    else
                    {
                        // if true, we are in the first row
                        if (i == k_FirstRowOrColumn)
                        {
                            constructFirstRow(i, j);
                        }
                        else
                        {
                            // if true, we are in the first column
                            if (j == k_FirstRowOrColumn)
                            {
                                constructFirstColumn(i, j);
                            }
                            else
                            {
                                constructMiddleSquares(i, j);
                            }
                        }
                    }

                    this.Controls.Add(m_BoardGame.BoardGameWithButtons[i, j]);
                }
            }

            createPlayersLabels();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void buildSquareProperties(int i_RowIndex, int i_ColumnIndex)
        {
            int columnIndex;
            int rowIndex;
            Square currentSquare = m_BoardGame.BoardGameWithSquares.m_SuqaresValue[i_RowIndex, i_ColumnIndex];
            Image squareImage = squareImage = m_GameImages[(int)(Convert.ToChar(currentSquare.letter) - ComputerManager.k_BottomSpotedLetterBound)];

            if (m_HasInternetConnection)
            {
                m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex] = new MemoryGameButton(currentSquare, squareImage);
            }
            else
            {
                m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex] = new MemoryGameButton(currentSquare);
            }

            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Click += new EventHandler(buttonClicked);
            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].TabStop = false;
            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Size = new Size(k_SquareSize, k_SquareSize);
            columnIndex = (int)(i_ColumnIndex + Settings.k_CharToIntOffSet + k_ColumsOffset);
            rowIndex = (int)(i_RowIndex + Settings.k_CharToIntOffSet + k_RowOffset);
            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Name = Convert.ToChar(columnIndex).ToString() + Convert.ToChar(rowIndex).ToString();
        }

        private void constructFirstRow(int i_RowIndex, int i_ColumnIndex)
        {
            int ButtonXLocation = m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex - k_BuildSquareOffset].Location.X + m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Right + k_SpaceBetweenSquares;
            int ButtonYLocation = m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex - k_BuildSquareOffset].Location.Y;
            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Location = new Point(ButtonXLocation, ButtonYLocation);
        }

        private void constructFirstColumn(int i_RowIndex, int i_ColumnIndex)
        {
            int ButtonXLocation = m_BoardGame.BoardGameWithButtons[i_RowIndex - k_BuildSquareOffset, i_ColumnIndex].Location.X;
            int ButtonYLocation = m_BoardGame.BoardGameWithButtons[i_RowIndex - k_BuildSquareOffset, i_ColumnIndex].Location.Y + m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Bottom + k_SpaceBetweenSquares;
            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Location = new Point(ButtonXLocation, ButtonYLocation);
        }

        private void constructMiddleSquares(int i_RowIndex, int i_ColumnIndex)
        {
            int ButtonXLocation = m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex - k_BuildSquareOffset].Location.X + m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Right + k_SpaceBetweenSquares;
            int ButtonYLocation = m_BoardGame.BoardGameWithButtons[i_RowIndex - k_BuildSquareOffset, i_ColumnIndex].Location.Y + m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Bottom + k_SpaceBetweenSquares;
            m_BoardGame.BoardGameWithButtons[i_RowIndex, i_ColumnIndex].Location = new Point(ButtonXLocation, ButtonYLocation);
        }

        private void createPlayersLabels()
        {
            // m_CurrentPlayersTurn
            this.m_CurrentPlayersTurn.AutoSize = true;
            int XLocationCurrent = m_BoardGame.BoardGameWithButtons[0, 0].Location.X;
            int YLocationCurrent = m_BoardGame.BoardGameWithButtons[m_NumOfRows - 1, 0].Bottom + k_SpaceBetweenPlayerLabels;
            this.m_CurrentPlayersTurn.Location = new Point(XLocationCurrent, YLocationCurrent);
            this.m_CurrentPlayersTurn.Name = "m_CurrentPlayersTurn";
            this.m_CurrentPlayersTurn.Size = new Size(137, 20);
            this.m_CurrentPlayersTurn.TabIndex = 0;
            this.m_CurrentPlayersTurn.BackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_CurrentPlayersTurn.Text = "Current Player: " + GameManager.m_FirstPlayer.Name;

            // m_FirstPlayerScore
            this.m_FirstPlayerScore.AutoSize = true;
            int XLocationFirst = m_CurrentPlayersTurn.Location.X;
            int YLocationFirst = m_CurrentPlayersTurn.Bottom + k_SpaceBetweenPlayerLabels;
            this.m_FirstPlayerScore.Location = new Point(XLocationFirst, YLocationFirst);
            this.m_FirstPlayerScore.Name = "m_FirstPlayerScore";
            this.m_FirstPlayerScore.Size = new Size(137, 20);
            this.m_FirstPlayerScore.TabIndex = 0;
            this.m_FirstPlayerScore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_FirstPlayerScore.Text = GameManager.m_FirstPlayer.Name + ": " + GameManager.m_FirstPlayer.Score + (GameManager.m_FirstPlayer.Score < k_IsMultiple ? " Pair(s)" : " Pairs");

            // m_SecondPlayerScore
            this.m_SecondPlayerScore.AutoSize = true;
            int XLocationSecond = m_FirstPlayerScore.Location.X;
            int YLocationSecond = m_FirstPlayerScore.Bottom + k_SpaceBetweenPlayerLabels;
            this.m_SecondPlayerScore.Location = new Point(XLocationSecond, YLocationSecond);
            this.m_SecondPlayerScore.Name = "m_SecondPlayerScore";
            this.m_SecondPlayerScore.Size = new Size(137, 20);
            this.m_SecondPlayerScore.TabIndex = 0;
            this.m_SecondPlayerScore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_SecondPlayerScore.Text = GameManager.m_SecondPlayer.Name + ": " + GameManager.m_SecondPlayer.Score + (GameManager.m_SecondPlayer.Score < k_IsMultiple ? " Pair(s)" : " Pairs");

            // Add labels to form
            this.Controls.Add(m_CurrentPlayersTurn);
            this.Controls.Add(m_FirstPlayerScore);
            this.Controls.Add(m_SecondPlayerScore);

            // Set windows size appropriate to the buttons (x) and the second player label (y)
            setGameWindowSize();
        }

        private void setGameWindowSize()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            int XWindowSize = m_BoardGame.BoardGameWithButtons[m_NumOfRows - 1, m_NumOfColums - 1].Right + k_SpaceBetweenWindowToButtons;
            int YWindowSize = m_SecondPlayerScore.Bottom + k_SpaceBetweenWindowToButtons;
            this.ClientSize = new System.Drawing.Size(XWindowSize, YWindowSize);
        }

        private void buttonClicked(object sender, EventArgs e)
        {
            m_CurrentButtonClickedByPlayer = sender as MemoryGameButton;
            if(m_HasInternetConnection)
            {
                m_CurrentButtonClickedByPlayer.BackgroundImage = m_CurrentButtonClickedByPlayer.ButtonImage;
            }
            else
            {
                m_CurrentButtonClickedByPlayer.Text = m_CurrentButtonClickedByPlayer.Square.letter.ToString();

            }

            m_CurrentButtonClickedByPlayer.Click -= buttonClicked;
            changeKnownLettersForComputer(m_CurrentButtonClickedByPlayer);
            m_CurrentButtonClickedByPlayer.Refresh();
            if (m_IsGuessNumberOne)
            {
                m_FirstButtonGeuss = m_CurrentButtonClickedByPlayer;
                m_IsGuessNumberOne = !m_IsGuessNumberOne;
            }
            else
            {
                m_SecondButtonGeuss = m_CurrentButtonClickedByPlayer;
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

            if (GameManager.s_AvailbleMoves.Count == k_EmptyList)
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
                m_FirstButtonGeuss.Click += buttonClicked;
                m_SecondButtonGeuss.Click += buttonClicked;
            }

            GameManager.s_AvailbleMoves.Add(m_FirstButtonGeuss.Name);
            GameManager.s_AvailbleMoves.Add(m_SecondButtonGeuss.Name);
        }


        private void checkForComputerTurn()
        {
            if (!m_IsFirstPlayerTurn && !GameManager.m_SecondPlayer.isHumanPlayer && GameManager.s_AvailbleMoves.Count > k_EmptyList)
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
                m_FirstPlayerScore.Text = GameManager.m_FirstPlayer.Name + ": " + GameManager.m_FirstPlayer.Score + (GameManager.m_FirstPlayer.Score < k_IsMultiple ? " Pair(s)" : " Pairs");
                m_FirstPlayerScore.Refresh();
            }
            else
            {
                m_SecondPlayerScore.Text = GameManager.m_SecondPlayer.Name + ": " + GameManager.m_SecondPlayer.Score + (GameManager.m_SecondPlayer.Score < k_IsMultiple ? " Pair(s)" : " Pairs");
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
                m_CurrentPlayersTurn.BackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                m_CurrentPlayersTurn.Text = "Current Player: " + GameManager.m_FirstPlayer.Name;
            }
            else
            {
                m_CurrentPlayersTurn.BackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
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