using System;
using System.Windows.Forms;
using System.IO;

namespace project_Formula
{
    /// <summary>
    /// Fenster zum Erstellen einer neuen FormulaList.
    /// </summary>
    public partial class AddFormulaList : Form
    {
        private String folderpath;
        public AddFormulaList(String folderpath)
        {
            this.folderpath = folderpath;
            InitializeComponent();
        }

        /// <summary>
        /// Wird der Dateiname (Name der neuen FormulaList) durch den Klick auf den Button bestätigt, wird der Name zuerst auf gültige Zeichen überprüft.
        /// Anschließend wird im Ordner, in welchem sich alle anderen CSV-Dateien (FormulaLists) befinden, eine neue Datei mit dem gewählten Namen erstellt und somit eine neue FormulaList hinzugefügt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void EnterButton_Click(object sender, EventArgs e)
        {
            int res = 0;
            foreach (char c in fileNameTextBox.Text)
            {
                if ((int)c >= 65 && (int)c <= 90 || (int)c >= 97 && (int)c <= 122 || (int)c == (int)' ')
                {
                    res++;
                }
            }

            if (res == fileNameTextBox.Text.Length)
            {
                File.Create(folderpath + "\\" + fileNameTextBox.Text + ".csv");
                MessageBox.Show("Datei wurde erfolgreich erstellt!");
            }
            else
            {
                MessageBox.Show("gültigen Dateinamen eingeben!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            fileNameTextBox.Clear();
        }
    }
}
