using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class GameManager
    {
        public const int k_BottomLetersBound = 'A'; // Bottom boundary index for board columns.
        public const int k_BottomnumbersBound = '1'; // Bottom boundary index for board rows.
        public const int k_LettersInPair = 2; // Each letter has 2 appearances in the board game.
        private const bool v_HumanPlayer = true; // first player is always human
        public static Player m_FirstPlayer;
        public static Player m_SecondPlayer;
        private static int s_NumOfRows;
        private static int s_NumOfColumns;
        public static List<string> s_AvailbleMoves = new List<string>();
        public static ComputerManager s_ManageComputerTurns;
        private static Random rand;

        public static void StartGame(int i_NumOfRows, int i_NumOfColumns, string i_FirstPlayerName, string i_SecondPlayerName, bool i_IsAgainstHuman)
        {
            s_ManageComputerTurns = new ComputerManager(i_NumOfRows, i_NumOfColumns);
            m_FirstPlayer = new Player(i_FirstPlayerName, v_HumanPlayer);
            m_SecondPlayer = new Player(i_SecondPlayerName, i_IsAgainstHuman);
            s_NumOfColumns = i_NumOfColumns;
            s_NumOfRows = i_NumOfRows;
            generateAvailableMoves();
        }

        public static void makeComputerTurn(out string o_FirstSquareGuessed, out string o_SecondSquareGuessed, string i_ComputerLevel, BoardGame i_BoardGame)
        {
            bool firstGuessWasSmart = false;
            bool isGuessNumberOne = true;
            char unusedLetter = 'a';

            o_FirstSquareGuessed = makeGuesses(isGuessNumberOne, unusedLetter, ref firstGuessWasSmart, i_ComputerLevel, i_BoardGame); // ununsedLetter will make the method MakeGuesses
            s_ManageComputerTurns.KnownLetters(o_FirstSquareGuessed, i_BoardGame);
            s_AvailbleMoves.Remove(o_FirstSquareGuessed);
            char firstLetterGuessed = i_BoardGame.m_SuqaresValue[o_FirstSquareGuessed[1] - k_BottomnumbersBound, o_FirstSquareGuessed[0] - k_BottomLetersBound].letter;
            o_SecondSquareGuessed = makeGuesses(!isGuessNumberOne, firstLetterGuessed, ref firstGuessWasSmart, i_ComputerLevel, i_BoardGame);
            s_ManageComputerTurns.KnownLetters(o_SecondSquareGuessed, i_BoardGame);
        }

        private static string makeGuesses(bool i_IsGuessNumberOne, char i_FirstLetterGuessed, ref bool io_FirstGuessWasSmart, string i_ComputerLevel, BoardGame i_BoardGame)
        {
            string nextMove;
            int isSmartGuess = int.MaxValue; // Initialize to max int value so in the second guess it will be smart / not smart, depends on the first guess
            const int k_MakeSmartGuess = 1; // If RandomComputerOrSmartComputer = k_MakeSmartGuess, the computer will make a smart guess.

            if (i_IsGuessNumberOne)
            {
                isSmartGuess = isSmartMove(i_ComputerLevel); // if we get 1, it will be smart guess. if we get 2, it will be random guess
            }

            // checks if next move should be smart or not
            if (isSmartGuess == k_MakeSmartGuess || (!i_IsGuessNumberOne && io_FirstGuessWasSmart))
                {
                    nextMove = s_ManageComputerTurns.SmartMove(i_FirstLetterGuessed, i_IsGuessNumberOne, s_AvailbleMoves);
                    io_FirstGuessWasSmart = true;
                }
            else
                {
                    nextMove = s_ManageComputerTurns.GenerateRandomMove(s_AvailbleMoves);
                }

            i_BoardGame.m_SuqaresValue[nextMove[1] - k_BottomnumbersBound, nextMove[0] - k_BottomLetersBound].visible = true;

            return nextMove;
        }

        // Will determain if the next move is going to be smart one, depends on the chosen level.
        private static int isSmartMove(string i_ComputerLevel)
        {
            rand = new Random();
            int isSmart; // if the random number will be 1 - the next move will be smart

            switch(i_ComputerLevel)
            {
                case "Easy":
                    isSmart = rand.Next(1, 5);
                    break;
                case "Hard":
                    isSmart = rand.Next(1, 4);
                    break;
                case "Expert":
                    isSmart = rand.Next(1, 3);
                    break;
                default:
                    isSmart = rand.Next(1, 4);
                    break;
            }

            return isSmart;
        }

        private static void generateAvailableMoves()
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

        public static bool IsCorrectGuess(BoardGame i_BoardGame, string i_FirstGuess, string i_SecondGuess, Player io_Player)
        {
            Square firstGuess = i_BoardGame.m_SuqaresValue[i_FirstGuess[1] - k_BottomnumbersBound, i_FirstGuess[0] - k_BottomLetersBound];
            Square secondGuess = i_BoardGame.m_SuqaresValue[i_SecondGuess[1] - k_BottomnumbersBound, i_SecondGuess[0] - k_BottomLetersBound];
            bool isCorrectGuess;

            if(isCorrectGuess = firstGuess.letter.Equals(secondGuess.letter))
            {
                io_Player.Score++;
            }

            return isCorrectGuess;
        }
    }
}
