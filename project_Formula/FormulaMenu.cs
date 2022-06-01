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
    public partial class FormulaMenu : Form
    {
        private Formula formula;
        private Formula currentVersion;
        private String folderpath;
        private String finalPath;
        private int whatversion = 0;
        private Label[] labels;
        private NumericUpDown[] updowns;
        public FormulaMenu(Formula formula, String folderpath)
        {
            labels = new Label[formula.getVars().Count];
            updowns = new NumericUpDown[formula.getVars().Count];
            this.folderpath = folderpath;
            this.formula = formula;
            currentVersion = formula;
            InitializeComponent();
        }

        private void FormulaMenu_Load(object sender, EventArgs e)
        {
            renderFormula(formula.getVersions()[0]);                                        //Default-Formel rendern
            mkPicturebox();
        }

        /// <summary>
        /// Eine Formel in Form von einem String wird der Funktion übergeben und diese wird dann mithilfe einer auf LaTeX-basierenden api in eine png-Datei gerendert.
        /// </summary>
        /// <param name="version"></param>

        public void renderFormula(String version)
        {
            MathRendererOptions options = new PngMathRendererOptions() { Resolution = 150 };

            options.Preamble = @"\usepackage{amsmath}
                    \usepackage{amsfonts}
                    \usepackage{amssymb}
                    \usepackage{color}";

            options.Scale = 3000;
            options.TextColor = System.Drawing.Color.Black;
            options.BackgroundColor = this.BackColor;

            options.LogStream = new MemoryStream();
            System.Drawing.SizeF size = new System.Drawing.SizeF();

            //illegale Filechars filtern
            finalPath = folderpath + "\\" + formula.getName() + "_" + whatversion + ".png";
            whatversion++;
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                if (c != '\\' && c != ':')                       //'\' und ';' sollen im Pfad bleiben
                {
                    finalPath = finalPath.Replace(c.ToString(), "");
                }
            }

            using (Stream stream = File.Open(finalPath, FileMode.Create))
            {
                PngMathRenderer renderer = new PngMathRenderer();
                // Run rendering.
                renderer.Render(version, stream, options, out size);
            }
            formulaImage.Image = Image.FromFile(finalPath);                                            
        }

        /// <summary>
        /// Alle nötigen grafischen Inhalte der Form werden erstellt. Am ende wird noch die Funktion mkcalc() ausgeführt)
        /// </summary>

        public void mkPicturebox()
        {
            //Label für Titel
            FormulaName.Text = formula.getName();
            FormulaName.AutoSize = true;

            //Picturebox für Png-Datei
            formulaImage.SizeMode = PictureBoxSizeMode.CenterImage;
            Controls.Add(formulaImage);

            //Drop down für Versionen
            List<char> tmp1 = formula.getVars();
            for (int j = 0; j < tmp1.Count; j++)
            {
                umstellen_auf.Items.Add(tmp1[j]);
            }

            //Label für Variablennamen
            List<String> tmp2 = formula.getVarNames();
            if(tmp2.Count == 0)
            {
                return;
            }
            if (String.IsNullOrEmpty(tmp2[0])) { 
                variableNames.Hide();
            }
            else
            {
                foreach (String s in tmp2)
                {
                    variablenNamen.Text += s;
                }
                variablenNamen.Text += " (Default-Formel Reihenfolge)";
                variablenNamen.BackColor = Color.DarkGray;
            }

            //updowns und Labels für berechnungen
            mkcalc(currentVersion);
        }

        /// <summary>
        /// dropdowns und labels die für die Berechnung gebraucht werden, werden dynamisch erstellt und gerendert
        /// </summary>
        /// /// <param name="version"></param>

        public void mkcalc(Formula version)
        {
            List<char> tmp3 = version.getVars();
            for (int j = 0; j < tmp3.Count; j++)                                    //Erste Variable löschen damit für diese keine Werteeingabe erstellt wird
            {
                if (tmp3[j] != ' ')
                {
                    tmp3.RemoveAt(j);
                    break;
                }
            }
            int multi = 50;
            int i = 0;
            foreach (char c in tmp3)
            {
                if(labels[i] == null)
                {
                    labels[i] = new Label();
                    updowns[i] = new NumericUpDown();
                }
                labels[i].Text = c + "   ->";
                labels[i].Font = new Font(Label.DefaultFont, FontStyle.Bold);
                labels[i].Location = new Point(70, 310 + multi);
                labels[i].AutoSize = true;
                Controls.Add(labels[i]);
                labels[i].Show();

                updowns[i].Width = 100;
                updowns[i].Location = new Point(150, 310 + multi);
                //updowns[i].Minimum = -1000000M;                                   //funktioniert sowieso nicht mit negativen Zahlen!
                Controls.Add(updowns[i]);
                updowns[i].Show();

                multi += 50;
                i++;
            }
        }

        /// <summary>
        /// Wenn sich der Wert des updowns ändert soll die jeweilige Version der Formel gerendert werden und auch die berechnung muss die Formel ändern.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void umstellen_auf_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                renderFormula(formula.getVersions()[umstellen_auf.SelectedIndex]);
                currentVersion = new Formula(formula.getName(), formula.getVersions()[umstellen_auf.SelectedIndex], formula.getVarNames());
                mkcalc(currentVersion);
            }catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Es wurde keine Version für die ausgewählte Variable gefunden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Wenn der Button gedrückt wird soll alles mit den eingegebenen Werten berechnet werden. 
        /// Leider Funtkioniert die Berechnung nicht mit negativen Werten.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void calculate_Click(object sender, EventArgs e)
        {
            float[] allValues = new float[updowns.Length];
            int i;
            for (i = 0; i < updowns.Length; i++)
            {
                if (updowns[i] == null)
                {
                    updowns[i] = new NumericUpDown();
                }
                allValues[i] = (float)updowns[i].Value;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(currentVersion.getFormulaStandard());
            i = 0;
            while (i < sb.Length)
            {
                for (int j = 0; j < labels.Length - 1; j++)
                {
                    if (sb[i] == labels[j].Text[0])
                    {
                        string s = "" + allValues[j];
                        sb.Remove(i, 1);
                        sb.Insert(i, s);
                    }
                }
                i++;
            }
            i = 0;

            //ab '=' abspalten
            StringBuilder cpy = new StringBuilder();
            while (sb[i] != '=')
            {
                i++;
            }
            while(i < sb.Length)
            {
                if(sb[i] != ' ')
                {
                    cpy.Append(sb[i]);
                }
                i++;
            }
            cpy.Remove(0, 1);
            double res = Eval(cpy.ToString());
            result.Text = res.ToString();
        }

        /// <summary>
        /// Es musste ein Algorithmus zur Berechnung gefunden werden. 
        /// Wir mussten uns entscheiden: einige gingen mit negativen Werten aber dafür ohne das '^' und einige gingen umgekehrt.
        /// </summary>

        private string[] _operators = { "-", "+", "/", "*", "^" };

        private Func<double, double, double>[] _operations = {
        (a1, a2) => a1 - a2,
        (a1, a2) => a1 + a2,
        (a1, a2) => a1 / a2,
        (a1, a2) => a1 * a2,
        (a1, a2) => Math.Pow(a1, a2)
        };

        public double Eval(string expression)
        {
            List<string> tokens = getTokens(expression);
            Stack<double> operandStack = new Stack<double>();
            Stack<string> operatorStack = new Stack<string>();
            int tokenIndex = 0;

            while (tokenIndex < tokens.Count)
            {
                string token = tokens[tokenIndex];
                if (token == "{")
                {
                    string subExpr = getSubExpression(tokens, ref tokenIndex);
                    operandStack.Push(Eval(subExpr));
                    continue;
                }
                if (token == "}")
                {
                    throw new ArgumentException("Mis-matched parentheses in expression");
                }
                //If this is an operator  
                if (Array.IndexOf(_operators, token) >= 0)
                {
                    while (operatorStack.Count > 0 && Array.IndexOf(_operators, token) < Array.IndexOf(_operators, operatorStack.Peek()))
                    {
                        string op = operatorStack.Pop();
                        double arg2 = operandStack.Pop();
                        double arg1 = operandStack.Pop();
                        operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
                    }
                    operatorStack.Push(token);
                }
                else
                {
                    operandStack.Push(double.Parse(token));
                }
                tokenIndex += 1;
            }

            while (operatorStack.Count > 0)
            {
                string op = operatorStack.Pop();
                double arg2 = operandStack.Pop();
                double arg1 = operandStack.Pop();
                operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
            }
            return operandStack.Pop();
        }

        private string getSubExpression(List<string> tokens, ref int index)
        {
            StringBuilder subExpr = new StringBuilder();
            int parenlevels = 1;
            index += 1;
            while (index < tokens.Count && parenlevels > 0)
            {
                string token = tokens[index];
                if (tokens[index] == "{")
                {
                    parenlevels += 1;
                }

                if (tokens[index] == "}")
                {
                    parenlevels -= 1;
                }

                if (parenlevels > 0)
                {
                    subExpr.Append(token);
                }

                index += 1;
            }

            if ((parenlevels > 0))
            {
                throw new ArgumentException("Mis-matched parentheses in expression");
            }
            return subExpr.ToString();
        }

        private List<string> getTokens(string expression)
        {
            string operators = "{}^*/+-";
            List<string> tokens = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (char c in expression.Replace(" ", string.Empty))
            {
                if (operators.IndexOf(c) >= 0)
                {
                    if ((sb.Length > 0))
                    {
                        tokens.Add(sb.ToString());
                        sb.Length = 0;
                    }
                    tokens.Add(c + "");
                }
                else
                {
                    sb.Append(c);
                }
            }

            if ((sb.Length > 0))
            {
                tokens.Add(sb.ToString());
            }
            return tokens;
        }
    }
}
