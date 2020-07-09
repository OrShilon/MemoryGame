using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    internal class Square
    {
        private char letterInSquare;

        public Square(char i_Letter)
        {
            this.letterInSquare = i_Letter;
        }

        public char letter
        {
            get
            {
                return this.letterInSquare;
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
