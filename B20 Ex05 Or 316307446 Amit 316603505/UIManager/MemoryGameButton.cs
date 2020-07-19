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
        private readonly MemoryGame.Square r_Button;
        private readonly Image r_Image;

        public MemoryGameButton(MemoryGame.Square i_SquareInBoardGame, Image i_Image) :
            base()
        {
            r_Button = i_SquareInBoardGame;
            r_Image = i_Image;

        }

        public MemoryGameButton(MemoryGame.Square i_SquareInBoardGame) :
            base()
        {
            r_Button = i_SquareInBoardGame;

        }

        public MemoryGame.Square Square
        {
            get
            {
                return r_Button;
            }
        }
        public Image ButtonImage
        {
            get
            {
                return r_Image;
            }
        }
    }
}