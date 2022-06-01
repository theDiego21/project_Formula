using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Formula
{
    /// <summary>
    /// Dient zum speichern von Formeln
    /// </summary>
    internal class FormulaList
    {
        /// <summary>
        /// enthält alle Formulas
        /// </summary>
        private List<Formula> formulas;

        /// <summary>
        /// name der FormulaList/des Fachs
        /// </summary>
        private String name;

        public FormulaList(String name)
        {
            this.name = name;
            formulas = new List<Formula>();
        }

        public List<Formula> getFormulas() { return this.formulas; }

        public String getName() { return this.name; }

        public void addFormula(Formula formula)
        {
            formulas.Add(formula);
        }

        public void removeFormula(String name)
        {
            while (searchFormula(name) != null)
            {
                foreach (Formula formula in formulas)
                {
                    if (name.Equals(formula.getName()))
                    {
                        formulas.Remove(formula);
                        break;
                    }

                    if (formula.getVersions().Contains(name))
                    {
                        formulas.Remove(formula);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// sucht Formula
        /// </summary>
        /// <param name="name"> Name der Formula</param>
        /// <returns> returnt null, wenn nichts gefunden wurde, ansonsten die formula</returns>
         
        public Formula searchFormula(String name)
        {
            foreach (Formula formula in formulas)
            {
                if (name.Equals(formula.getName()))
                {
                    return formula;
                }

                if (name.Equals(formula.getFormulaStandard()))
                {
                    return formula;
                }
            }
            return null;
        }
    }
}

