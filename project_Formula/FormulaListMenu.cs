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
    /// Menü zum Navigieren in einer FomulaList.
    /// </summary>
    public partial class FormulaListMenu : Form
    {
        private TextBox search = new TextBox();
        private FormulaList forMenu;
        List<String> names = new List<String>();
        private String fileName;
        private String folderpath;
        private Button[] buttons;
        private Button tmp;

        public FormulaListMenu(object sender, String folderpath)
        {
            tmp = (Button)sender;
            fileName = tmp.Text;
            this.folderpath = folderpath;
            PreList.importFormulaList(folderpath);
            InitializeComponent();
        }

        private void FormulaListMenu_Load(object sender, EventArgs e)
        {
            //Wenn das Programm geschlossen wird, sollen alle png-Dateien gelöscht werden
            //Funktioniert nicht, da wenn man sie löschen will ein anderer Prozess auf das Bild zugreift! (keine Lösung gefunden)
            //AppDomain.CurrentDomain.ProcessExit += new EventHandler(deleteAllPNG);     

            fillButtons();
        }

        /*
            private void deleteAllPNG(object sender, EventArgs e)
            {
                //alle Png-Dateien bei Ende des Programms löschen
                DirectoryInfo dir = new DirectoryInfo(folderpath);
                FileInfo[] imageFiles = dir.GetFiles("*.png");
                foreach (FileInfo file in imageFiles)
                {
                    file.Delete();
                }
            }
        */

        /// <summary>
        /// Alle Buttons werden genauso wie in Menu dynamisch erstellt. Nur diesmal mit Formeln und nicht mit Dateien.
        /// </summary>
        public void fillButtons()
        {
            forMenu = PreList.importSingleFL(folderpath + "\\" + fileName + ".csv");
            List<Formula> tmp = forMenu.getFormulas();
            buttons = new Button[tmp.Count];
            int i = 0;
            int x = 0;
            int y = 100;

            foreach (Formula formula in tmp)
            {
                buttons[i] = new Button();
                buttons[i].Text = formula.getName();
                buttons[i].Size = new Size(400, 100);
                buttons[i].Location = new Point(x, y);
                if (i % 2 == 0)
                {
                    x += 400;
                }
                else
                {
                    y += 100; x = 0;
                }
                buttons[i].Show();
                Controls.Add(buttons[i]);
                buttons[i].Click += new EventHandler(button_clicked);
                i++;
            }
            searchbar();
        }

        /// <summary>
        /// Erstellt eine Suchleiste mit einem dazugehörigen Enter-Button und einem Return-Button, um die Suche wieder auf Standard zurückzustellen.
        /// Zudem wird ein Button zum Hinzufügen einer neuen Formel erstellt.
        /// </summary>
        public void searchbar()
        {
            Button addFileButton = new Button();
            addFileButton.Text = "+";
            addFileButton.Size = new Size(50, 30);
            addFileButton.Font = new Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            addFileButton.Location = new Point(30, 25);
            addFileButton.Click += new EventHandler(addFormula);
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
        /// Öffnet ein Fenster zum Hinzufügen einer neuen Formel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void addFormula(object sender, EventArgs e)
        {
            this.Hide();
            var addFormula = new AddFormula(folderpath + "\\" + fileName + ".csv");
            addFormula.Closed += (s, args) => { this.clearWindow(); this.fillButtons(); this.Show(); };
            addFormula.Show();
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
                counter++;
                if (button.Text.ToLower().IndexOf(search.Text.ToLower()) != -1 && search.Text != null)
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
            }
            if (counter == 0)
            {
                MessageBox.Show("Keine Formelliste gefunden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                setToStandard();

            }
            else { search.Clear(); }
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
        ///Versteckt alle FormulaLists (Buttons) im Fenster.
        /// </summary>
        public void clearWindow()
        {
            foreach (Button button in buttons)
            {
                button.Hide();
            }
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
        /// Wird ein Button einer FormulaList angeklickt, wird das aktuelle Fenster versteckt und das dazugehörige FormulaList-Fenster geöffnet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button_clicked(object sender, EventArgs e)
        {
            this.Hide();
            Button tmp = (Button)sender;
            List<Formula> temp = forMenu.getFormulas();
            foreach (Formula f in temp)
            {
                if(tmp.Text == f.getName())
                {
                    var formulaMenu = new FormulaMenu(f, folderpath);
                    formulaMenu.Closed += (s, args) => this.Show();
                    formulaMenu.Show();
                    break;
                }
            }
        }
    }
}
