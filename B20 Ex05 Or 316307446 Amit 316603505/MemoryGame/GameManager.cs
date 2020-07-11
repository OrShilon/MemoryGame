using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryGame
{
    internal class gameManager
    {
        public const int k_BottomLetersBound = 'A'; // Bottom boundary index for board columns.
        public const int k_BottomnumbersBound = '1'; // Bottom boundary index for board rows.
        public const int k_LettersInPair = 2; // Each letter has 2 appearances in the board game.
        private static Player m_FirstPlayer;
        private static Player m_SecondPlayer;
        private static int s_NumOfRows;
        private static int s_NumOfColumns;
        private static BoardGame s_BoardGame;
        private static int s_PointsLeftUntilFinish;
        private static int s_DifficultyLevel;
        private static List<string> s_AvailbleMoves = new List<string>();
        private static ComputerManager s_ManageComputerTurns;

        public static void InitializePlayers()
        {
            const bool v_VsHumanOrComputer = true; // false is for computer, will be used only if the user want to play against computer

            InputManager.WelcomeMessage();
            m_FirstPlayer = InputManager.MakePlayer();
            if (InputManager.VsHumanOrComputer(out s_DifficultyLevel).Equals("1"))
            {
                m_SecondPlayer = InputManager.MakePlayer();
            }
            else
            {
                m_SecondPlayer = new Player("Computer", !v_VsHumanOrComputer);
            }
        }

        public static void InitializeBoardSize()
        {
            InputManager.BoardSize(out s_NumOfRows, out s_NumOfColumns);
            s_BoardGame = new BoardGame(s_NumOfRows, s_NumOfColumns);
            s_ManageComputerTurns = new ComputerManager(s_NumOfRows, s_NumOfColumns);
            GenerateAvailableMoves();
            s_PointsLeftUntilFinish = (s_NumOfRows * s_NumOfColumns) / k_LettersInPair;
        }
        public static void StartGame()
        {
            bool isFirstPlayerTurn = true;
            bool isGuessNumberOne = true;
            string firstSquareGuessed;
            string secundSquareGuessed;
            const int k_EndOfTheGame = 0;

            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(s_BoardGame.PrintBoardGame());

            while (s_PointsLeftUntilFinish != k_EndOfTheGame)
            {
                makeTurn(out firstSquareGuessed, out secundSquareGuessed, isFirstPlayerTurn, isGuessNumberOne);
                if (isCorrectGuess(firstSquareGuessed, secundSquareGuessed))
                {
                    Console.WriteLine("{0} your guess is correct!{1}", isFirstPlayerTurn ? m_FirstPlayer.Name : m_SecondPlayer.Name, Environment.NewLine);
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
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine(s_BoardGame.PrintBoardGame());
                    isFirstPlayerTurn = !isFirstPlayerTurn;
                }
            }

            InputManager.PrintResult(m_FirstPlayer, m_SecondPlayer);
        }

        private static void makeTurn(out string i_FirstSquareGuessed, out string i_SecondSquareGuessed, bool i_IsFirstPlayerTurn, bool i_IsGuessNumberOne)
        {
            bool firstGuessWasSmart = false;
            char unusedLetter = 'a';

            computerRestMode();
            i_FirstSquareGuessed = makeGuesses(i_IsFirstPlayerTurn, i_IsGuessNumberOne, unusedLetter, ref firstGuessWasSmart); // ununsedLetter will make the method MakeGuesses
            s_ManageComputerTurns.KnownLetters(i_FirstSquareGuessed, s_BoardGame);
            s_AvailbleMoves.Remove(i_FirstSquareGuessed);
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(s_BoardGame.PrintBoardGame());
            computerRestMode();
            char firstLetterGuessed = s_BoardGame.m_SuqaresValue[i_FirstSquareGuessed[1] - k_BottomnumbersBound, i_FirstSquareGuessed[0] - k_BottomLetersBound].letter;
            i_SecondSquareGuessed = makeGuesses(i_IsFirstPlayerTurn, !i_IsGuessNumberOne, firstLetterGuessed, ref firstGuessWasSmart);
            s_ManageComputerTurns.KnownLetters(i_SecondSquareGuessed, s_BoardGame);
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(s_BoardGame.PrintBoardGame());
        }

        private static string makeGuesses(bool i_IsFirstPlayerTurn, bool i_IsGuessNumberOne, char i_FirstLetterGuessed, ref bool io_FirstGuessWasSmart)
        {
            string nextMove;

            if (!i_IsFirstPlayerTurn && !m_SecondPlayer.isHumanPlayer)
            {
                Random rand = new Random();
                int RandomComputerOrSmartComputer = -1;
                const int k_MakeSmartGuess = 1; // If RandomComputerOrSmartComputer = k_MakeSmartGuess, the computer will make a smart guess.

                // Checks wanted difficulty level 0 - easy, 1 - mediom, 2 - hard
                if (i_IsGuessNumberOne)
                {
                    switch (s_DifficultyLevel)
                    {
                        case 0:
                            RandomComputerOrSmartComputer = rand.Next(1, 6);
                            break;
                        case 1:
                            RandomComputerOrSmartComputer = rand.Next(1, 4);
                            break;
                        case 2:
                            RandomComputerOrSmartComputer = rand.Next(1, 3);
                            break;
                        default:
                            RandomComputerOrSmartComputer = rand.Next(1, 6);
                            break;
                    }
                }

                // checks if next move should be smart or not
                if (RandomComputerOrSmartComputer == k_MakeSmartGuess || (!i_IsGuessNumberOne && io_FirstGuessWasSmart))
                {
                    nextMove = s_ManageComputerTurns.SmartMove(i_FirstLetterGuessed, i_IsGuessNumberOne, s_AvailbleMoves);
                    io_FirstGuessWasSmart = true;
                }
                else
                {
                    nextMove = s_ManageComputerTurns.GenerateRandomMove(s_AvailbleMoves);
                }
            }
            else
            {
                nextMove = InputManager.MakeHumanGuess(i_IsFirstPlayerTurn, i_IsGuessNumberOne, m_FirstPlayer, m_SecondPlayer, s_NumOfRows, s_NumOfColumns, s_AvailbleMoves);
            }

            s_BoardGame.m_SuqaresValue[nextMove[1] - k_BottomnumbersBound, nextMove[0] - k_BottomLetersBound].visible = true;

            return nextMove;
        }

        private static void computerRestMode()
        {
            if (!m_SecondPlayer.isHumanPlayer)
            {
                Thread.Sleep(1000);
            }
        }

        private static bool isCorrectGuess(string i_FirstGuess, string i_SecondGuess)
        {
            Square firstGuess = s_BoardGame.m_SuqaresValue[i_FirstGuess[1] - k_BottomnumbersBound, i_FirstGuess[0] - k_BottomLetersBound];
            Square secondGuess = s_BoardGame.m_SuqaresValue[i_SecondGuess[1] - k_BottomnumbersBound, i_SecondGuess[0] - k_BottomLetersBound];

            return firstGuess.letter.Equals(secondGuess.letter);
        }

        private static void GenerateAvailableMoves()
        {
            for (int i = 0; i < s_NumOfColumns; i++)
            {
                for (int j = 0; j < s_NumOfRows; j++)
                {
                    char column = Convert.ToChar(k_BottomLetersBound + i);
                    char row = Convert.ToChar(k_BottomnumbersBound + j);
                    s_AvailbleMoves.Add(string.Empty + column + row);
                }
            }
        }

        private static void increaseScore(bool i_IsFirstPlayerTurn)
        {
            if (i_IsFirstPlayerTurn)
            {
                m_FirstPlayer.Score++;
            }
            else
            {
                m_SecondPlayer.Score++;
            }
        }
    }
}
