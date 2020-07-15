using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIManager
{
    class MemoryGameButton : Button
    {
        private readonly MemoryGame.Square m_Button;
        private readonly Image m_image;

        public MemoryGameButton(MemoryGame.Square i_SquareInBoardGame, Image[] i_image) :
            base()
        {
            m_Button = i_SquareInBoardGame;
            m_image = i_image[(int)(Convert.ToChar(m_Button.letter) - MemoryGame.ComputerManager.k_BottomSpotedLetterBound)];

        }

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
        public Image ButtonImage
        {
            get
            {
                return m_image;
            }
        }
    }
}