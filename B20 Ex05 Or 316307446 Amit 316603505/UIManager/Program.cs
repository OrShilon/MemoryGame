using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIManager
{
    class Program
    {
        static void Main()
        {
            MemoryGame temp = new MemoryGame(1, 1, true);
            temp.ShowDialog();
            //Settings temp = new Settings();
            //temp.ShowDialog();

        }
    }
}
