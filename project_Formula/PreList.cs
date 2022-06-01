using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Formula
{
    /// <summary>
    /// Die PreList dient zum managen und abspeichern der Formeln
    /// </summary> 
    internal static class PreList
    {
        /// <summary>
        /// DieListe list dient zum speichern der FormulaLists
        /// </summary>
        private static List<FormulaList> lists = new List<FormulaList>();
        /// <summary>
        /// folderPath gibt den Pfad zum Ordner mit den csv Dateien an 
        /// </summary>
        private static string folderPath;

        /// <summary>
        /// Importiert eine einzelne FormulaList aus einer csv Datei
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns> gibt die eingelesene FormulaList zurück</returns>
        public static FormulaList importSingleFL(string filePath)
        {
            string name = Path.GetFileNameWithoutExtension(filePath);
            string s = File.ReadAllText(filePath, Encoding.UTF8);
            FormulaList fl = new FormulaList(name);

            string[] splitarr = s.Split("\n".ToCharArray());

            string[] formulaNames = splitarr.Where((x, j) => j % 2 == 0).ToArray();
            string[] formulaContents = splitarr.Where((x, j) => j % 2 != 0).ToArray();


            for (int j = 0; j < formulaContents.Length; j++)
            {
                string str = formulaContents[j];

                String delimiter = "|";
                string[] items = str.Split(delimiter.ToCharArray());

                string[] versions = items.Where((x, y) => y % 2 == 0).ToArray();
                string[] varNames = items.Where((x, y) => y % 2 != 0).ToArray();


                for (int k = 0; k < versions.Length; k++)
                {
                    string version = versions[k];
                    string varName = varNames[k];

                    delimiter = ";";
                    string[] versionsFin = version.Split(delimiter.ToCharArray());
                    string[] varNamesFin = varName.Split(delimiter.ToCharArray());
                    varNamesFin = varNamesFin.Take(varNamesFin.Length - 1).ToArray();


                    Formula fr = new Formula(formulaNames[j], versionsFin[0], varNamesFin.ToList());

                    for (int l = 1; l < versionsFin.Length; l++)
                    {
                        if (versionsFin[l].Length != 0)
                        {
                            fr.addVersion(versionsFin[l]);
                        }
                    }
                    fl.addFormula(fr);
                }
            }
            return fl;
        }

        ///<summary>
        /// Importiert meherer FormulaList aus einem Ordner
        /// </summary>
        /// <param name="setPath"></param>
        public static void importFormulaList(string setPath)
        {
            folderPath = setPath;
            List<string> s = new List<string>();
            List<string> names = new List<string>();

            foreach (string file in Directory.EnumerateFiles(folderPath, "*.csv"))
            {
                names.Add(Path.GetFileNameWithoutExtension(file));
                if (File.ReadAllText(file, Encoding.UTF8) != null)                  //Wenn die Datei leer ist
                {
                    s.Add(File.ReadAllText(file, Encoding.UTF8));
                }
            }

            for (int i = 0; i < s.Count; i++)
            {
                if (s.Count != 0)       //isEmpty
                {
                    //s[0}
                    //Satz des Pythagoras
                    //c=√(a^2+b^2);a^2+b^2=c^2| Hypothenuse,Kathete,Ankathete
                    //Mitternachtsformel;...

                    //s[1]
                    //Ohmsches Gesetz;....

                    string[] splitarr = s.ElementAt(i).Split("\n".ToCharArray());
                    //subjects[0]=Satz des Pythagora
                    //subjects[1]=c=√(a^2+b^2);a^2+b^2=c^2|Hypo...
                    //subjects[2]=Mitternachtsformel


                    string[] formulaNames = splitarr.Where((x, j) => j % 2 == 0).ToArray();
                    string[] formulaContents = splitarr.Where((x, j) => j % 2 != 0).ToArray();

                    //formulaNames[0]=Satz des Pythagora
                    //formulaNames[0]=Mitternachtsformel

                    //formulaContents[0]=c=√(a^2+b^2);a^2+b^2=c^2|Hypo...

                    FormulaList fl = new FormulaList(names[i]);

                    for (int j = 0; j < formulaContents.Length; j++)
                    {
                        string str = formulaContents[j];
                        //str=c=√(a^2+b^2);a^2+b^2=c^2|Hypo...

                        String delimiter = "|";
                        string[] items = str.Split(delimiter.ToCharArray());
                        //Items[0]=c=√(a^2+b^2);a^2+b^2=c^2
                        //Items[1]=Hypo...

                        string[] versions = items.Where((x, y) => y % 2 == 0).ToArray();
                        string[] varNames = items.Where((x, y) => y % 2 != 0).ToArray();


                        for (int k = 0; k < versions.Length; k++)
                        {
                            string version = versions[k];
                            string varName = varNames[k];

                            //version=c=√(a^2+b^2);a^2+b^2=c^2
                            //varNameHypo...

                            delimiter = ";";
                            string[] versionsFin = version.Split(delimiter.ToCharArray());
                            string[] varNamesFin = varName.Split(delimiter.ToCharArray());
                            varNamesFin = varNamesFin.Take(varNamesFin.Length - 1).ToArray();


                            Formula fr = new Formula(formulaNames[j], versionsFin[0], varNamesFin.ToList());

                            for (int l = 1; l < versionsFin.Length; l++)
                            {
                                if (versionsFin[l].Length != 0)
                                {
                                    fr.addVersion(versionsFin[l]);
                                }
                            }
                            fl.addFormula(fr);
                        }
                    }
                    lists.Add(fl);
                }
            }
        }

        /// <summary>
        /// Fügt eine einzelne Formel zu einer csv Datei hinzu
        /// </summary>
        /// <param name="path"></param>
        /// <param name="formula"></param>
        public static void saveFormula(string path, Formula formula)
        {
            if (File.Exists(path))
            {
                FormulaList fl = importSingleFL(path);
                fl.addFormula(formula);
                saveSingle(path, fl);
            }
        }

        public static List<FormulaList> getLists()
        {
            return lists;
        }

        public static string getFolderPath()
        {
            return folderPath;
        }

        public static void setFolderPath(string setPath)
        {
            folderPath = setPath;
        }
        public static void addFormulaList(String name)
        {
            lists.Add(new FormulaList(name));
        }
        public static void addExistingFL(FormulaList fl)
        {
            lists.Add(fl);
        }
        public static void removeFormulaList(FormulaList fL)
        {
            lists.Remove(fL);
        }
        public static void removeFormulaList(String name)
        {
            for (int i = 0; i < lists.Count; i++)
            {
                if (lists.ElementAt(i).getName().Equals(name))
                {
                    removeFormulaList(lists[i]);
                }
            }
        }

        public static void removeAll()
        {
            lists = new List<FormulaList>();
        }
        
        /// <summary>
        /// speichert alle FormulaLists in lists
        /// </summary>
        public static void save()
        {
            StringBuilder csv;

            for (int i = 0; i < lists.Count; i++)
            {
                csv = new StringBuilder();
                FormulaList current = lists.ElementAt(i);
                string filePath = folderPath + current.getName() + ".csv";

                saveSingle(filePath, current);
            }
        }
        /// <summary>
        /// Speichert eine FormulaList in die csv Datei, die übergeben wird
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fl"></param>
        public static void saveSingle(string filePath, FormulaList fl)
        {
            StringBuilder csv;

            csv = new StringBuilder();
            //File Mathe
            //Satz des Pythagoras
            //c=√(a^2+b^2);a^2+b^2=c^2|Hypotenuse,Kathete,Ankathete
            //Mitternachtsformel;;|;;
            //
            //File TK
            //Ohmsches Gesetz;....
            var newLine = "";

            for (int j = 0; j < fl.getFormulas().Count; j++)
            {
                newLine = fl.getFormulas()[j].ToCsv();

                csv.AppendLine(newLine);

            }
            File.WriteAllText(filePath, csv.ToString());
        }
    }
}
