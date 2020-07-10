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
            MemoryGame temp = new MemoryGame(4, 4, true, "amit", "or");
            temp.ShowDialog();
            //Settings temp = new Settings();
            //temp.ShowDialog();

        }
    }
}
