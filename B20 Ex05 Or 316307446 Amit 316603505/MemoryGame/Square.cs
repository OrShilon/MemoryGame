using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class Square
    {
        private char m_LetterInSquare;

        public Square(char i_Letter)
        {
            this.m_LetterInSquare = i_Letter;
        }

        public char letter
        {
            get
            {
                return this.m_LetterInSquare;
            }

            set
            {
                this.letter = value;
            }
        }

        public bool visible { get; set; } = false;

        public string LocationInBoard { get; set; }
    }
}
