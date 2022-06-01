using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.TeX;
using Aspose.TeX.Features;
using System.IO;

namespace project_Formula
{
    /// <summary>
    /// Klasse des Hauptmenüs, in welcher eine FormulaList durch das Klicken auf den jeweiligen Button geöffnet werden kann.
    /// </summary>
    public partial class Menu : Form
    {
        private TextBox search = new TextBox();
        private List<String> names = new List<String>();
        private Button[] buttons;
        private String folderpath = "";
        private FileInfo[] files;
        private static bool first = true;
        /// <summary>
        /// Standard-Konstruktor des Hauptmenüs
        /// </summary>
        public Menu()
        {    
            openFolder();
            InitializeComponent();
            first = false;
        }

        public Menu(String folderpath)
        {
            this.folderpath = folderpath;
            openFolder();
            InitializeComponent();
        }

        /// <summary>
        /// Öffnet einen Folderbrowserdialog zu Beginn des Programms. Anschließend werden für alle CSV-Files, die gefunden werden, Buttons erstellt. 
        /// </summary>
        private void openFolder()
        {
            bool first = true;
            while (folderpath == "")
            {
                if (!first)
                {
                    MessageBox.Show("Kein Ordner ausgewählt!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                first = false;
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.ShowDialog();
                folderpath = folderBrowserDialog.SelectedPath;
            }

            DirectoryInfo path = new DirectoryInfo(folderpath);
            files = path.GetFiles("*csv");

            buttons = new Button[files.Length];
            int x = 0;
            int y = 100;
            int i = 0;

            foreach (FileInfo info in files)
            {
                buttons[i] = new Button();
                String str = "";
                char[] name_cpy = info.Name.ToCharArray();
                for (int j = 0; j < name_cpy.Length; j++) //Abschneiden der Dateieindung im Namen
                {
                    if (name_cpy[j] == '.') break;
                    str += name_cpy[j];
                }
                names.Add(str);
                buttons[i].Text = str;
                buttons[i].Size = new Size(400, 100);
                buttons[i].Location = new Point(x, y);
                if (i % 2 == 0) //wenn erst ein Element in der Zeile ist, wird das Zweite hinzugefügt
                {
                    x += 400;
                }
                else
                {
                    y += 100; x = 0;
                }

                buttons[i].Show();
                this.Controls.Add(buttons[i]);
                buttons[i].Click += new EventHandler(button_clicked);
                i++;
            }
            searchBar();
        }
        /// <summary>
        /// Erstellt eine Suchleiste mit einem dazugehörigen Enter-Button und einem Return-Button, um die Suche wieder auf Standard zurückzustellen.
        /// Zudem wird ein Button zum Erstellen einer neuen FormulaList erstellt.
        /// </summary>
        public void searchBar()
        {
            Button addFileButton = new Button();
            addFileButton.Text = "+";
            addFileButton.Size = new Size(50, 30);
            addFileButton.Font = new Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            addFileButton.Location = new Point(30, 25);
            addFileButton.Click += new EventHandler(addFile);
            this.Controls.Add(addFileButton);
            addFileButton.Show();


            Button returnbutton = new Button();
            returnbutton.Text = "Return";
            returnbutton.Size = new Size(100, 30);
            returnbutton.Font = new Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            returnbutton.Location = new Point(110, 25);
            returnbutton.Click += new EventHandler(displayStandard);
            this.Controls.Add(returnbutton);
            returnbutton.Show();

            search.Size = new Size(400, 50);
            search.Font = new Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            search.Location = new Point(240, 25);
            this.Controls.Add(search);
            search.Show();


            Button searchbutton = new Button();
            searchbutton.Text = "Search";
            searchbutton.Size = new Size(100, 30);
            searchbutton.Font = new Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            searchbutton.Location = new Point(670, 25);
            searchbutton.Click += new EventHandler(search_started);
            this.Controls.Add(searchbutton);
            searchbutton.Show();
        }
        /// <summary>
        /// Beim Klicken des Return-Buttons wird die Suche zurückgesetzt und auf dem Screen erscheinen wieder alle FormulaLists.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void displayStandard(object sender, EventArgs e)
        {
            setToStandard();
        }
        /// <summary>
        /// Funktion, welche beim Klicken des AddFileButtons aufgerufen wird. Die AddFormulaList Form wird geöffnet und die Aktuelle geschlossen. 
        /// Beim Schließen der AddFormulaList Form wird die Menu Form mit den aktualisierten FormulaLists geladen.  /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void addFile(object sender, EventArgs e)
        {
            this.Hide();
            var addformulalist = new AddFormulaList(folderpath);
            addformulalist.Closed += (s, args) => { this.clearWindow(); this.openFolder(); this.Show();};
            addformulalist.Show();
        }

        /// <summary>
        /// Wird die Suche gestartet, werden nur Buttons, welche das angegebene Wort im Namen enthalten, angezeigt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void search_started(object sender, EventArgs e)
        {
            clearWindow();
            int i = 0;
            int x = 0;
            int y = 100;
            int counter = 0;
            foreach (Button button in buttons)
            {
                if (button.Text.ToLower().IndexOf(search.Text.ToLower()) != -1 && search.Text != null)
                {
                    counter++;
                    button.Location = new Point(x, y);
                    if (i % 2 == 0) //wenn erst ein Element in der Zeile ist, wird das Zweite hinzugefügt
                    {
                        x += 400;
                    }
                    else
                    {
                        y += 100; x = 0;
                    }

                    button.Show();
                    i++;
                }
            }
            if(counter == 0)
            {
                MessageBox.Show("Keine Formelliste gefunden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                setToStandard();

            }
            else {search.Clear();}
        }

        /// <summary>
        /// Die Suche wird zurückgesetzt und es erscheinen wieder alle Buttons für die Formulalists.
        /// </summary>
        public void setToStandard()
        {
            int i = 0;
            int x = 0;
            int y = 100;
            clearWindow();
            foreach (Button button in buttons)
            {
                button.Location = new Point(x, y);
                if (i % 2 == 0) //wenn erst ein Element in der Zeile ist, wird das Zweite hinzugefügt
                {
                    x += 400;
                }
                else
                {
                    y += 100; x = 0;
                }

                button.Show();
                i++;
            }
            search.Clear();
        }

        /// <summary>
        ///Versteckt alle FormulaLists im Fenster.
        /// </summary>
        public void clearWindow()
        {
            foreach (Button button in buttons)
            {
                button.Hide();
            }
        }

        /// <summary>
        /// Wird ein Button einer FormulaList angeklickt, wird das aktuelle Fenster versteckt und das dazugehörige FormulaList-Fenster geöffnet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button_clicked(object sender, EventArgs e)
        {
            this.Hide();
            var formulalistmenu = new FormulaListMenu(sender, folderpath);
            formulalistmenu.Closed += (s, args) => this.Show();
            formulalistmenu.Show();
        }
    }
}
