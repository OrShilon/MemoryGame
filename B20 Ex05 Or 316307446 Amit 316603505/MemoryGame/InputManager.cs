using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryGame
{
    internal class InputManager
    {
        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to the memory game!");
            Thread.Sleep(1200);
        }
        public static Player MakePlayer()
        {
            Player newPlayer;
            const bool v_IsHuman = true; // will remain true, becuse we will get here only when initializing player, but not a computer.

            Console.WriteLine("Please enter your name: ");
            string playerName = Console.ReadLine();
            while (!IsValidName(playerName))
            {
                Console.WriteLine("Not a valid name. Please enter your name: ");
                playerName = Console.ReadLine();
            }

            newPlayer = new Player(playerName, v_IsHuman);

            return newPlayer;
        }

        public static string VsHumanOrComputer(out int o_DifficultyLevel)
        {
            string humanOrComputer;
            string difficultyLevel = "-1";

            Console.WriteLine("Enter 1 if you want to play against human, or 0 if you want to play against the computer: ");
            humanOrComputer = Console.ReadLine();
            while (!IsValidComputerOrHumanInput(humanOrComputer))
            {
                Console.WriteLine("Not a valid Input.{0} Enter 1 if you want to play against human, or 0 if you want to play against the computer: ", Environment.NewLine);
                humanOrComputer = Console.ReadLine();
            }

            if (humanOrComputer.Equals("0"))
            {
                Console.WriteLine("Please enter difficulty Level: Press 0 for easy, 1 for medium or 2 for hard"); // checks difficulty level
                difficultyLevel = Console.ReadLine();
                while (!isValidDifficultyLevel(difficultyLevel))
                {
                    Console.WriteLine("Invalid input. Please enter difficulty Level: Press 0 for easy, 1 for medium or 2 for hard");
                    difficultyLevel = Console.ReadLine();
                }
            }

            int.TryParse(difficultyLevel, out o_DifficultyLevel);

            return humanOrComputer;
        }

        public static bool IsValidName(string i_InputName)
        {
            return !i_InputName.Equals(string.Empty);
        }

        public static bool IsValidComputerOrHumanInput(string i_IsHuman)
        {
            return i_IsHuman.Equals("1") || i_IsHuman.Equals("0");
        }

        public static void BoardSize(out int o_Rows, out int o_Columns)
        {
            string numOfWords;
            string numOfColumns;

            Console.WriteLine("What size of board do you want? You can choose anything between 4*4 to 6*6 except 5*5." +
                "{0}lets start with number of rows, how many rows would you like?", Environment.NewLine);
            numOfWords = Console.ReadLine();
            Console.WriteLine("And now how many columns would you like?");
            numOfColumns = Console.ReadLine();
            while (!isValiBoardSize(numOfWords, numOfColumns, out o_Rows, out o_Columns))
            {
                Console.WriteLine("Unfortunately the size you have entered is wrong please try again. How many rows would you like?");
                numOfWords = Console.ReadLine();
                Console.WriteLine("And now, how many columns would you like?");
                numOfColumns = Console.ReadLine();
            }
        }

        private static bool isValiBoardSize(string io_Rows, string io_Columns, out int o_Rows, out int o_Columns)
        {
            const int k_MaxBoardSize = 6;
            const int k_MinBoardSize = 4;
            bool IsValid = false;

            int.TryParse(io_Rows, out o_Rows);
            int.TryParse(io_Columns, out o_Columns);
            if (k_MinBoardSize <= o_Rows && o_Rows <= k_MaxBoardSize && k_MinBoardSize <= o_Columns && o_Columns <= k_MaxBoardSize)
            {
                if ((o_Rows * o_Columns) % 2 == 0)
                {
                    IsValid = true;
                }
            }

            return IsValid;
        }

        public static string MakeHumanGuess(bool i_IsFirstPlayerTurn, bool i_IsGuessNumberOne, Player i_FirstPlayer, Player i_SecondPlayer,
            int i_NumOfRows, int i_NumOfColumns, List<string> i_AvailableMoves)
        {
            string nextMove;

            Console.WriteLine("Hey {0} it's your turn to play!", i_IsFirstPlayerTurn ? i_FirstPlayer.Name : i_SecondPlayer.Name);
            if (i_IsGuessNumberOne)
            {
                Console.WriteLine("Which spot would you like to display first?");
            }
            else
            {
                Console.WriteLine("Please make your second guess: ");
            }

            nextMove = Console.ReadLine();
            if (nextMove.Equals("Q"))
            {
                ExitProgram();
            }

            while (!isValidMove(nextMove, i_NumOfRows, i_NumOfColumns, i_AvailableMoves))
            {
                Console.WriteLine("Sorry {0}, your move is illegal please try again.", i_IsFirstPlayerTurn ? i_FirstPlayer.Name : i_SecondPlayer.Name);
                nextMove = Console.ReadLine();
                if (nextMove.Equals("Q"))
                {
                    ExitProgram();
                }
            }

            return nextMove;
        }

        private static bool isValidMove(string i_PlayerMove, int i_NumOfRows, int i_NumOfColumns, List<string> i_AvailableMoves)
        {
            bool isValidMove = false;

            if (i_PlayerMove.Length == 2)
            {
                char numberChosen = i_PlayerMove[1];
                char letterChosen = i_PlayerMove[0];

                if (letterChosen < (gameManager.k_BottomLetersBound + i_NumOfColumns) && letterChosen >= gameManager.k_BottomLetersBound)
                {
                    if (numberChosen < (gameManager.k_BottomnumbersBound + i_NumOfRows) && numberChosen >= gameManager.k_BottomnumbersBound)
                    {
                        if (i_AvailableMoves.Contains(i_PlayerMove))
                        {
                            isValidMove = true;
                        }
                    }
                }
            }

            return isValidMove;
        }

        public static void PrintResult(Player i_FirstPlayer, Player i_SecondPlayer)
        {
            Console.WriteLine("{0} score is: {1}", i_FirstPlayer.Name, i_FirstPlayer.Score);
            Console.WriteLine("{0} score is: {1}", i_SecondPlayer.Name, i_SecondPlayer.Score);
            if (i_FirstPlayer.Score != i_SecondPlayer.Score)
            {
                Console.WriteLine("Congratulations! {0} won the game!", i_FirstPlayer.Score > i_SecondPlayer.Score ? i_FirstPlayer.Name : i_SecondPlayer.Name);
            }
            else
            {
                Console.WriteLine("Its a tie!!");
            }

            Console.WriteLine(Environment.NewLine);
        }

        private static bool isValidDifficultyLevel(string i_DifficultyLevel)
        {
            int difficultyInput;

            return int.TryParse(i_DifficultyLevel, out difficultyInput) && difficultyInput >= 0 && difficultyInput < 3;
        }

        public static bool PlayAgain()
        {
            Console.WriteLine("Would you like to play again? Press 1 for yes, 0 for no.");
            string playAgain = Console.ReadLine();
            while(!isValidPlayAgainInput(playAgain))
            {
                Console.WriteLine("Not a valid input. Would you like to play again? Press 1 for yes, 0 for no.");
                playAgain = Console.ReadLine();
            }

            return playAgain.Equals("1");
        }

        private static bool isValidPlayAgainInput(string i_PlayAgain)
        {
            return i_PlayAgain.Equals("1") || i_PlayAgain.Equals("0");
        }

        public static void ExitProgram()
        {
            Console.WriteLine("Exit program...");
            Thread.Sleep(1000);
            Environment.Exit(1);
        }
    }
}
