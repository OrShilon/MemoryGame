using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    internal class Player
    {
        private int m_Score = 0;

        public Player(string i_Name, bool i_IsHuman)
        {
            this.Name = i_Name;
            this.isHumanPlayer = i_IsHuman; // Will be true if the player is a human player.
        }

        public string Name { get; }

        public bool isHumanPlayer { get; set; }

        public int Score
        {
            get
            {
                return this.m_Score;
            }

            set
            {
                m_Score++;
            }
        }
    }
}
