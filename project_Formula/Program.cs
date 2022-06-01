using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_Formula
{

    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Welcome_Screen_1());

            /*//beim 1. Mal ausführen, um alle formeln hinzuzufügen
            //do gibsch oanfoch in Pfad eini wo die Formeln speichern willsch donn speicherts dir de olle zemm hin
            string path="";x
            test_Drehbuch t = new Test-Drehbuch();
            t.formeln_hzfg(path);
            */
        }
    }
}
