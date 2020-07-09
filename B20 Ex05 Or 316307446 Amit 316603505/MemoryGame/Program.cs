using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            gameManager.InitializePlayers();
            gameManager.InitializeBoardSize();
            gameManager.StartGame();
            while (InputManager.PlayAgain())
            {
                gameManager.InitializeBoardSize();
                gameManager.StartGame();
            }

            InputManager.ExitProgram();
        }
    }
}
