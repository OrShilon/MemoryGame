using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIManager
{
    class BoardGameWindows
    {
        private MemoryGame.BoardGame m_BoardGameLogic;
        private MemoryGameButton[,] m_BoardGameButtons;
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

        //public void InitBoard()
        //{
        //    for (int i = 0; i < m_BoardGameLogic.NumOfRows; i++)
        //    {
        //        for (int j = 0; j < m_BoardGameLogic.NumOfColumns; j++)
        //        {
        //            MemoryGame.Square currentSquare = m_BoardGameLogic.m_SuqaresValue[i, j];
        //            m_BoardGameButtons[i, j] = new MemoryGameButton(currentSquare);
        //            m_BoardGameButtons[i, j].Click += new EventHandler(ButtonClicked);

        //            // need to change 0 const
        //            if (i == 0 && j == 0)
        //            {
        //                m_BoardGameButtons[i, j].Location.X;
        //            }
        //            else
        //            {

        //            }
        //            m_BoardGameButtons[i, j].Location.X;
        //        }
        //    }
        //}

        //private void ButtonClicked(object sender, EventArgs e)
        //{
        //    MemoryGameButton thisButton = sender as MemoryGameButton;
        //    thisButton.Text = thisButton.Square.letter.ToString();
        //}
    }
}
