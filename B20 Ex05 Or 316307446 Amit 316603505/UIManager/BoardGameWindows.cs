using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIManager
{
    class BoardGameWindows
    {
        private readonly MemoryGame.BoardGame m_BoardGameLogic;
        private readonly MemoryGameButton[,] m_BoardGameButtons;

        public BoardGameWindows(int i_NumOfColumns, int i_NumOfRows)
        {
            m_BoardGameLogic = new MemoryGame.BoardGame(i_NumOfRows, i_NumOfColumns);
            m_BoardGameButtons = new MemoryGameButton[i_NumOfRows, i_NumOfColumns];
        }

        public MemoryGame.BoardGame BoardGameWithSquares
        {
            get
            {
                return m_BoardGameLogic;
            }
        }

        public MemoryGameButton[,] BoardGameWithButtons
        {
            get
            {
                return m_BoardGameButtons;
            }
        }
    }
}
