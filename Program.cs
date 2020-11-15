using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace SP_0_LAB_1_PRISTAVKA_V_1._0
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ourWindow myApp = new ourWindow();
            Application.Run(myApp);
        }
    }
}
