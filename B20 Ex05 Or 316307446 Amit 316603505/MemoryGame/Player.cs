using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class Player
    {
        private const int k_InitializedScore = 0;
        private int m_Score;

        public Player(string i_Name, bool i_IsHuman)
        {
            this.Name = i_Name;
            this.IsHumanPlayer = i_IsHuman; // Will be true if the player is a human player.
            m_Score = k_InitializedScore;
        }

        public string Name { get; }

        public bool IsHumanPlayer { get; set; }

        public int Score
        {
            get
            {
                return this.m_Score;
            }

            set
            {
                m_Score = value;
            }
        }
    }
}
