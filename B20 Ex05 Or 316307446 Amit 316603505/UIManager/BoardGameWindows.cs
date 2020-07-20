using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIManager
{
    internal class BoardGameWindows
    {
        private readonly MemoryGame.BoardGame r_BoardGameLogic;
        private readonly MemoryGameButton[,] r_BoardGameButtons;

        public BoardGameWindows(int i_NumOfColumns, int i_NumOfRows)
        {
            r_BoardGameLogic = new MemoryGame.BoardGame(i_NumOfRows, i_NumOfColumns);
            r_BoardGameButtons = new MemoryGameButton[i_NumOfRows, i_NumOfColumns];
        }

        public MemoryGame.BoardGame BoardGameWithSquares
        {
            get
            {
                return r_BoardGameLogic;
            }
        }

        public MemoryGameButton[,] BoardGameWithButtons
        {
            get
            {
                return r_BoardGameButtons;
            }
        }
    }
}
