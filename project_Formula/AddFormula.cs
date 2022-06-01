using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace project_Formula
{
    public partial class AddFormula : Form
    {
        private String folderPath;
        private String formulaName;
        private String formula;
        private List<String> variableNames = new List<String>();
        private Formula finFormula;
        private Label[] labels;
        private TextBox[] textBoxes;

        public AddFormula(String folderPath)
        {
            this.folderPath = folderPath;
            InitializeComponent();
        }

        /// <summary>
        /// Wenn der Button gedrückt wird werden alle Textboxen überprüft.
        /// Falls alles passt werden die nächsten 2 Funktionen aufgerufen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void enterbtn_Click(object sender, EventArgs e)
        {
            formulaName = nameInput.Text;
            formula = formulaInput.Text;
            if (String.IsNullOrEmpty(formulaName))
            {
                MessageBox.Show("Bitte geben Sie den Namen der Formel ein", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrEmpty(formula))
            {
                MessageBox.Show("Bitte geben Sie die Formel ein", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrEmpty(variableNamesInput.Text))
            {
                MessageBox.Show("Bitte geben Sie die VariablenNamen ein", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                getVarNames();
                mkTextboxes();
            }
        }

        /// <summary>
        /// Filtert alle variablennamen aus dem Input.
        /// </summary>

        public void getVarNames()
        {
            String tmp = variableNamesInput.Text;
            String tmp2 = "";
            foreach(char c in tmp)
            {
                if(c != ',')
                {
                    tmp2 += c;
                }
                else
                {
                    variableNames.Add(tmp);
                    tmp = "";
                }
            }
        }

        /// <summary>
        /// Erstellt dynamische Textboxen für die Eingabe von verschiedenen Versionen nach der ersten Eingabe
        /// </summary>

        public void mkTextboxes()
        {
            enterbtn.Enabled = false;
            formulaInput.Enabled = false;
            nameInput.Enabled = false;
            variableNamesInput.Enabled = false;
            finFormula = new Formula(formulaName, formula, variableNames);
            List<char> tmp = finFormula.getVars();
            if(tmp.Count > 1)
            {
                for(int j = 0; j < tmp.Count; j++)                                    //Erste Variable löschen damit für diese keine Versionseingabe erstellt wird
                {
                    if(tmp[j] != ' ')
                    {
                        tmp.RemoveAt(j);
                        break;
                    }
                }
                int multi = 50;
                labels = new Label[tmp.Count];
                textBoxes = new TextBox[tmp.Count];
                int i = 0;
                foreach (char c in tmp)
                {
                    labels[i] = new Label();
                    labels[i].Text = c + "                 ->";
                    labels[i].Font = new Font(Label.DefaultFont, FontStyle.Bold);
                    labels[i].BackColor = Color.Transparent;

                    labels[i].Location = new Point(40, 310 + multi);
                    Controls.Add(labels[i]);
                    labels[i].Show();

                    textBoxes[i] = new TextBox();
                    textBoxes[i].Width = 475;
                    textBoxes[i].Location = new Point(230, 310 + multi);
                    Controls.Add(textBoxes[i]);
                    textBoxes[i].Show();

                    multi += 50;
                    i++;
                }
                Button finButton = new Button();
                finButton.Text = "Fertig";
                finButton.FlatStyle = FlatStyle.Popup;
                finButton.BackColor = Color.OldLace;
                finButton.Size = new Size(171, 65);
                finButton.Font = new Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                finButton.Location = new Point(277, 290 + multi);
                finButton.Click += new EventHandler(finishInput);
                this.Controls.Add(finButton);
                finButton.Show();
                this.Height += 30;
            }
            else
            {
                finishInput(enterbtn, new EventArgs());
            }
        }

        /// <summary>
        /// Wenn der Button gedrückt wird oder die Funktion von mkTextboxes aufgerufen wird, soll die Formel mit allen Versionen hinzugefügt werden.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void finishInput(object sender, EventArgs e)
        {
            if(textBoxes != null)
            {
                for (int i = 0; i < textBoxes.Length; i++)
                {
                    if (String.IsNullOrEmpty(textBoxes[i].Text))
                    {
                        continue;
                    }
                    else
                    {
                        finFormula.addVersion(textBoxes[i].Text);                   //Der Formel die Version hinzufügen
                    }
                }
            }
            PreList.saveFormula(folderPath, finFormula);
            MessageBox.Show("Formel wurde hinzugefügt.", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
