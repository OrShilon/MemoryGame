using System;
using System.Collections.Generic;
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
        private readonly List<string> r_BoardSize = new List<string> { "4 x 4", "4 x 5", "4 x 6", "5 x 4", "5 x 6", "6 x 4", "6 x 5", "6 x 6" };
        private int m_BoardSizePositionInList = 0;
        private bool m_ClosedForTheFirstTime = true;

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void m_StartButton_Click(object sender, EventArgs e)
        {
            string boardSize = r_BoardSize[m_BoardSizePositionInList];
            //need to change to const
            int numOfColumns = boardSize[0] - '0';
            int numOfRows = boardSize[4] - '0';
            string firstPlayerName = m_TextBoxFirstPlayer.Text;
            string secondPlayerName = m_TextBoxSecondPlayer.Text;
            bool isSecondPlayerHuman = m_TextBoxSecondPlayer.Enabled; // false mean that the player is computer
            this.Close();
            m_ClosedForTheFirstTime = false;
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

        private void m_BoardSizeButton_Click(object sender, EventArgs e)
        {
            //need to change 1 to const
            if (m_BoardSizePositionInList == (r_BoardSize.Count - 1))
            {
                m_BoardSizePositionInList = 0;
            }
            else
            {
                m_BoardSizePositionInList++;
            }

            m_BoardSizeButton.Text = r_BoardSize[m_BoardSizePositionInList];
        }

        private void m_Against_Click(object sender, EventArgs e)
        {
            m_TextBoxSecondPlayer.Enabled = !m_TextBoxSecondPlayer.Enabled;

            if (m_TextBoxSecondPlayer.Enabled)
            {
                m_Against.Text = k_AgainstComputer;
                m_TextBoxSecondPlayer.Text = String.Empty;
            }
            else
            {
                m_Against.Text = k_AgainstPlayer;
                m_TextBoxSecondPlayer.Text = k_Computer;
            }

        }

    }
}
