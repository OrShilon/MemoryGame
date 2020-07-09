using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic
{
    class MemoryGameButton : Button
    {
        private MemoryGame.Square m_Button;

        public MemoryGameButton(MemoryGame.Square i_SquareInBoardGame) :
            base()
        {
            m_Button = i_SquareInBoardGame;
        }

        public MemoryGame.Square Square
        {
            get
            {
                return m_Button;
            }
        }
    }
}
