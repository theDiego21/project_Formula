using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Drawing;

namespace project_Formula
{
    public partial class Formula
    {
        /// <summary>
        /// Eine Formel hat einen Namen, verschiedene Versionen, variablen (alle Buchstaben) und Variablennamen.
        /// Variablennamen werden in der Reihenfolge abgespeichert, die die erste Eingabe der Formel vorgibt (Default-Formel oder FormulaStandard)
        /// </summary>
        private String name;
        private List<String> formula_versions = new List<String>();
        private List<char> vars = new List<char>();
        private List<String> varNames = new List<string>();

        public Formula(String name, String formula, List<String> varNames)
        {
            this.name = name;
            this.varNames = varNames;
            formula_versions.Add(formula);
            find_vars();
        }

        /// <summary>
        /// Durchsucht die Standard-Formel nach Variablen und speichert in eine Liste.
        /// </summary>

        public void find_vars()
        {
            for (int i = 0; i < formula_versions[0].Length; i++)
            {
                if (formula_versions[0][i] >= 'a' && formula_versions[0][i] <= 'z' || formula_versions[0][i] >= 'A' && formula_versions[0][i] <= 'Z')
                {
                    vars.Add(formula_versions[0][i]);
                }
            }
            Console.WriteLine(vars.ToString());
        }
   
        public List<String> getVarNames() { return this.varNames; }

        public String getFormulaStandard() { return formula_versions.ElementAt(0); }

        public String getName() { return this.name; }

        public void addVersion(String formula_version)
        {
            this.formula_versions.Add(formula_version);
        }

        /// <summary>
        /// Fügt der Formel eine Version (Umstellung) hinzu. 
        /// </summary>
        /// <param name="formula_version">neue Formelversion</param>
        /// <param name="varName">Name der Variable, auf die umgestellt wird</param>
        
        public void addVersion(String formula_version, String varName)
        {
            this.formula_versions.Append(formula_version);
        }

        public List<string> getVersions() { return formula_versions; }
        public List<char> getVars() { return this.vars; }

        public String ToCsv()
        {
            String finString = name + "\n";
            for (int i = 0; i < formula_versions.Count; i++)
            {
                finString += formula_versions[i] + ";";
            }
            finString += "|";
            for (int i = 0; i < varNames.Count; i++)
            {
                finString += varNames[i] + ";";
            }

            return finString;
        }
    }
}

