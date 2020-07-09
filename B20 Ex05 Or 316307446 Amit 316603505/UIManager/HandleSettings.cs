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
        private const string k_AgainstPlayer = "Against a Player";
        private const string k_AgainstComputer = "Against a Computer";
        private const string k_Computer = "-computer-";
        private readonly List<string> r_BoardSize = new List<string> { "4 x 4", "4 x 5", "4 x 6", "5 x 4", "5 x 6", "6 x 4", "6 x 5", "6 x 6" };
        private int m_BoardSizePositionInList = 0;


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

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
            m_TextBoxFriend.Enabled = !m_TextBoxFriend.Enabled;

            if (m_Against.Text.Equals(k_AgainstPlayer))
            {
                m_Against.Text = k_AgainstComputer;
                m_TextBoxFriend.Text = k_Computer;
            }
            else
            {
                m_Against.Text = k_AgainstPlayer;
                m_TextBoxFriend.Text = String.Empty;
            }

        }

    }
}
