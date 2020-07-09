using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{

    internal class ComputerManager
    {
        internal const int k_BottomSpotedLetterBound = 'G'; // Bottom boundary for letters to guess in the board game.
        private SpottedLetters[] smartGuess; // Location 0 represent 'G', location 1 represent 'H', location 2 represent 'I', and so on..

        public ComputerManager(int i_NumOfRows, int i_NumOfColumns)
        {
            smartGuess = new SpottedLetters[i_NumOfRows * i_NumOfColumns / gameManager.k_LettersInPair];
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
                Random rand = new Random();
                int moveNum = rand.Next(k_startOfList, endOfList);
                move = i_AvailableMoves[moveNum];
            }

            return move;
        }

        public void KnownLetters(string i_SquareGuessed, BoardGame i_BoardGame)
        {
            char letterGuessed = i_BoardGame.m_SuqaresValue[i_SquareGuessed[1] - gameManager.k_BottomnumbersBound, i_SquareGuessed[0] - gameManager.k_BottomLetersBound].letter;
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
                for (int i = 0; i < smartGuess.Length; i++)
                {
                    if (smartGuess[i].SeenBoth == true && i_AvailableMoves.Contains(smartGuess[i].LocationNumberOne))
                    {
                        newMove = smartGuess[i].LocationNumberOne; // incase of smart guess && seen both return first_spot
                        break;
                    }
                }

                if (newMove.Equals(string.Empty))
                {
                    newMove = GenerateRandomMove(i_AvailableMoves);
                }
            }
            else
            {
                for (int i = 0; i < smartGuess.Length; i++)
                {
                    if (smartGuess[i].SeenBoth)
                    {
                        // if smart guess and other match is known, return the matched letter
                        // else, if smart guess and second pair has just dicovered previously
                        if (i_FirstLetterGuessed.Equals(smartGuess[i].Letter) && i_AvailableMoves.Contains(smartGuess[i].LocationNumberTwo))
                        {
                            newMove = smartGuess[i].LocationNumberTwo;
                            break;
                        }
                        else if (i_FirstLetterGuessed.Equals(smartGuess[i].Letter) && i_AvailableMoves.Contains(smartGuess[i].LocationNumberOne))
                        {
                            newMove = smartGuess[i].LocationNumberOne;
                            break;
                        }
                    }
                }

                if (newMove.Equals(string.Empty))
                {
                    newMove = GenerateRandomMove(i_AvailableMoves);
                }
            }

            return newMove;
        }
    }
}
