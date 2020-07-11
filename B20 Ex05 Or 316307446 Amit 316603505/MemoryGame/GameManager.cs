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
        private static Player m_FirstPlayer;
        private static Player m_SecondPlayer;
        private static int s_NumOfRows;
        private static int s_NumOfColumns;
        private static BoardGame s_BoardGame;
        public static List<string> s_AvailbleMoves = new List<string>();
        public static ComputerManager s_ManageComputerTurns;
        private static Random rand;

        public static void StartGame(int i_NumOfRows, int i_NumOfColumns)
        {
            s_ManageComputerTurns = new ComputerManager(i_NumOfRows, i_NumOfColumns);
            GenerateAvailableMoves();
        }

        public static void makeComputerTurn(out string i_FirstSquareGuessed, out string i_SecondSquareGuessed)
        {
            bool firstGuessWasSmart = false;
            bool isGuessNumberOne = true;
            char unusedLetter = 'a';

            computerRestMode();
            i_FirstSquareGuessed = makeGuesses(isGuessNumberOne, unusedLetter, ref firstGuessWasSmart); // ununsedLetter will make the method MakeGuesses
            s_ManageComputerTurns.KnownLetters(i_FirstSquareGuessed, s_BoardGame);
            s_AvailbleMoves.Remove(i_FirstSquareGuessed);
            computerRestMode();
            char firstLetterGuessed = s_BoardGame.m_SuqaresValue[i_FirstSquareGuessed[1] - k_BottomnumbersBound, i_FirstSquareGuessed[0] - k_BottomLetersBound].letter;
            i_SecondSquareGuessed = makeGuesses(!isGuessNumberOne, firstLetterGuessed, ref firstGuessWasSmart);
            s_ManageComputerTurns.KnownLetters(i_SecondSquareGuessed, s_BoardGame);
        }

        private static string makeGuesses(bool i_IsGuessNumberOne, char i_FirstLetterGuessed, ref bool io_FirstGuessWasSmart)
        {
            string nextMove;
            rand = new Random();
            int isSmartGuess = rand.Next(1, 3); // if we get 1, it will be smart guess. if we get 2, it will be random guess
            const int k_MakeSmartGuess = 1; // If RandomComputerOrSmartComputer = k_MakeSmartGuess, the computer will make a smart guess.

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
                /*
            else
            {
                nextMove = InputManager.MakeHumanGuess(i_IsFirstPlayerTurn, i_IsGuessNumberOne, m_FirstPlayer, m_SecondPlayer, s_NumOfRows, s_NumOfColumns, s_AvailbleMoves);
            }*/

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

        public static bool isCorrectGuess(BoardGame i_BoardGame, string i_FirstGuess, string i_SecondGuess, Player io_Player)
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
