using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{

    internal class SpottedLetters
    {
        public string LocationNumberOne { get; set; } = "Uninitialized";

        public string LocationNumberTwo { get; set; } = "Uninitialized";

        public char Letter { get; set; }

        public bool SeenBoth { get; set; } = false;
    }
}
