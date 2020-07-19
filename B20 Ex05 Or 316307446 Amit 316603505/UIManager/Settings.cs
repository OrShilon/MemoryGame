using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIManager
{
    public partial class Settings : Form
    {
        private const string k_AgainstPlayer = "Against a Friend";
        private const string k_AgainstComputer = "Against Computer";
        private const string k_Computer = "-computer-";
        internal const char k_CharToIntOffSet = '0';
        private const int k_BoardSizesIndexOffSet = 1; // gets the appropriate board size as requested
        private const int k_RestartBoardSizes = 0;  // returns to 4 x 4 board size
        private const int k_ColumnIndex = 0; 
        private const int k_RowIndex = 4;
        private readonly List<string> r_BoardSize = new List<string> { "4 x 4", "4 x 5", "4 x 6", "5 x 4", "5 x 6", "6 x 4", "6 x 5", "6 x 6" };
        private int m_BoardSizePositionInList = 0;
        private bool m_ClosedForTheFirstTime = true;

        public Settings()
        {
            InitializeComponent();
        }

        private void m_AgainstFriendOrComputer_Click(object sender, EventArgs e)
        {
            m_TextBoxSecondPlayer.Enabled = !m_TextBoxSecondPlayer.Enabled;

            if (m_TextBoxSecondPlayer.Enabled)
            {
                m_AgainstFriendOrComputer.Text = k_AgainstComputer;
                m_TextBoxSecondPlayer.Text = String.Empty;
            }
            else
            {
                m_AgainstFriendOrComputer.Text = k_AgainstPlayer;
                m_TextBoxSecondPlayer.Text = k_Computer;
            }
        }

        private void m_BoardSizeButton_Click(object sender, EventArgs e)
        {
            if (m_BoardSizePositionInList == (r_BoardSize.Count - k_BoardSizesIndexOffSet))
            {
                m_BoardSizePositionInList = k_RestartBoardSizes;
            }
            else
            {
                m_BoardSizePositionInList++;
            }

            m_BoardSizeButton.Text = r_BoardSize[m_BoardSizePositionInList];
        }

        private void m_StartButton_Click(object sender, EventArgs e)
        {
            string boardSize = r_BoardSize[m_BoardSizePositionInList];
            int numOfColumns = boardSize[k_ColumnIndex] - k_CharToIntOffSet;
            int numOfRows = boardSize[k_RowIndex] - k_CharToIntOffSet;
            string firstPlayerName = m_TextBoxFirstPlayer.Text;
            string secondPlayerName = m_TextBoxSecondPlayer.Text;
            bool isSecondPlayerHuman = m_TextBoxSecondPlayer.Enabled; // false means that the second player is a computer
            m_ClosedForTheFirstTime = false;
            this.Hide();
            this.Close();

            MemoryGameWindows newGame = new MemoryGameWindows(numOfColumns, numOfRows, firstPlayerName, secondPlayerName, isSecondPlayerHuman);
            newGame.ShowDialog();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_ClosedForTheFirstTime)
            {
                m_StartButton_Click(sender, e);
            }
            else
            {
                this.Close();
            }
        }
    }
}
