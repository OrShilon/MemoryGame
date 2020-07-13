using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{

    public class ComputerManager
    {
        public const int k_BottomSpotedLetterBound = 'G'; // Bottom boundary for letters to guess in the board game.
        private SpottedLetters[] smartGuess; // Location 0 represent 'G', location 1 represent 'H', location 2 represent 'I', and so on..
        Random rand;

        public ComputerManager(int i_NumOfRows, int i_NumOfColumns)
        {
            smartGuess = new SpottedLetters[i_NumOfRows * i_NumOfColumns / GameManager.k_LettersInPair];
            generateSpottedLetters();
        }

        private void generateSpottedLetters()
        {
            for (int i = 0; i < smartGuess.Length; i++)
            {
                smartGuess[i] = new SpottedLetters();
            }
        }

        public string GenerateRandomMove(List<string> i_AvailableMoves)
        {
            string move = string.Empty;

            if (i_AvailableMoves.Any())
            {
                const int k_startOfList = 0;
                int endOfList = i_AvailableMoves.Count();
                rand = new Random();
                int moveNum = rand.Next(k_startOfList, endOfList);
                move = i_AvailableMoves[moveNum];
            }

            return move;
        }

        public void KnownLetters(string i_SquareGuessed, BoardGame i_BoardGame)
        {
            char letterGuessed = i_BoardGame.m_SuqaresValue[i_SquareGuessed[1] - GameManager.k_BottomnumbersBound, i_SquareGuessed[0] - GameManager.k_BottomLetersBound].letter;
            int index = letterGuessed - k_BottomSpotedLetterBound;

            if (!smartGuess[index].SeenBoth)
            {
                // Checks if first time letter spotted
                if (smartGuess[index].LocationNumberOne.Equals("Uninitialized") || smartGuess[index].LocationNumberOne.Equals(i_SquareGuessed))
                {
                    smartGuess[index].LocationNumberOne = i_SquareGuessed;
                    smartGuess[index].Letter = letterGuessed;
                }
                else if (smartGuess[index].LocationNumberTwo.Equals("Uninitialized"))
                {
                    smartGuess[index].LocationNumberTwo = i_SquareGuessed;
                    smartGuess[index].SeenBoth = true; // marks seen both true
                }
            }
        }

        // makes smart computer move. if seen both of a letter is true will get that match if not available do random move 
        public string SmartMove(char i_FirstLetterGuessed, bool i_IsFirstGuess, List<string> i_AvailableMoves)
        {
            string newMove = string.Empty;

            if (i_IsFirstGuess)
            {
                newMove = firstSmartMoce(i_AvailableMoves, newMove);
            }
            else
            {
                newMove = secondSmartMove(i_FirstLetterGuessed, i_AvailableMoves, newMove);
            }

            return newMove;
        }

        private string firstSmartMoce(List<string> i_AvailableMoves, string i_FirstMove)
        {
            for (int i = 0; i < smartGuess.Length; i++)
            {
                if (smartGuess[i].SeenBoth == true && i_AvailableMoves.Contains(smartGuess[i].LocationNumberOne))
                {
                    i_FirstMove = smartGuess[i].LocationNumberOne; // incase of smart guess && seen both return first_spot
                    break;
                }
            }

            if (i_FirstMove.Equals(string.Empty))
            {
                i_FirstMove = GenerateRandomMove(i_AvailableMoves);
            }

            return i_FirstMove;
        }

        private string secondSmartMove(char i_FirstLetterGuessed, List<string> i_AvailableMoves, string i_SecondMove)
        {
            for (int i = 0; i < smartGuess.Length; i++)
            {
                if (smartGuess[i].SeenBoth)
                {
                    // if smart guess and other match is known, return the matched letter
                    // else, if smart guess and second pair has just dicovered previously
                    if (i_FirstLetterGuessed.Equals(smartGuess[i].Letter) && i_AvailableMoves.Contains(smartGuess[i].LocationNumberTwo))
                    {
                        i_SecondMove = smartGuess[i].LocationNumberTwo;
                        break;
                    }
                    else if (i_FirstLetterGuessed.Equals(smartGuess[i].Letter) && i_AvailableMoves.Contains(smartGuess[i].LocationNumberOne))
                    {
                        i_SecondMove = smartGuess[i].LocationNumberOne;
                        break;
                    }
                }
            }

            if (i_SecondMove.Equals(string.Empty))
            {
                i_SecondMove = GenerateRandomMove(i_AvailableMoves);
            }

            return i_SecondMove;
        }
    }
}
