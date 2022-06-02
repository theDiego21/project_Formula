using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace project_Formula
{
    public class test_Drehbuch
    {
        /// <summary>
        /// Bereitet die PreList für einen Test vor
        /// </summary>
        /// <returns>gibt den Pfad zum aktuellen Ordner zurück</returns>
        public string initializeTests()
        {
            FormulaList fl;
            List<string> list;
            Formula fr;

            fl = new FormulaList("Mathe");

            list = new List<string>();
            list.Add("Hypothenuse");
            list.Add("Kathete");
            list.Add("Ankathete");

            fr = new Formula("Satz des Pythagoras", "c^2=a^2+b^2", list);
            fr.addVersion("a={c^2-b^2}^0.5");
            fr.addVersion("b=+-√(-a^2+c^2)");

            fl.addFormula(fr);


            list = new List<string>();
            list.Add("Zahl vor dem x^2");
            list.Add("Zahl vor dem x");
            list.Add("Zahl ohne x");

            fr = new Formula("Mitternachtsformel", "x=(-b+-√(b^2-4ac))/(2a)", list);
            fr.addVersion("-2x*a-√(b^2-4c*a)=b");
            fl.addFormula(fr);

            PreList.addExistingFL(fl);



            fl = new FormulaList("TK");
            list = new List<string>();
            list.Add("Spannung");
            list.Add("Widerstand");
            list.Add("Strom");

            fr = new Formula("Ohm'sches Gesetz", "U=R*I", list);
            fr.addVersion("R=U/I");
            fr.addVersion("I=U/R");

            fl.addFormula(fr);

            list = new List<string>();
            list.Add("Strahlungsleistung");
            list.Add("Radius");

            fr = new Formula("Strahlungsdichte", "S = P/(4*pi*r^2", list);

            fl.addFormula(fr);

            PreList.addExistingFL(fl);

            return Directory.GetCurrentDirectory();
        }

        ///<summary>
        ///1. Test der Funktionen zum ein-auslesen
        ///</summary>
        ///<returns> Gibt bei Erfolg 0 zurück, ansonsten den Fehler</returns>
        public int test1(string pathToTest)
        {
            PreList.setFolderPath(pathToTest + "\\");
            PreList.save();

            List<FormulaList> list1 = PreList.getLists();
            PreList.removeAll();
            PreList.importFormulaList(pathToTest);
            List<FormulaList> list2 = PreList.getLists();

            int output = vergleich(list1, list2);
            if (output == 0)
            {
                Console.WriteLine("Test 1: ein-auslesen OK");
            }
            else
            {
                Console.WriteLine("FEHLER beim ein-auslesen 1, Test " + output);
            }
            return output;
        }
        ///<summary>
        ///2. Test der Funktionen zum ein-auslesen
        ///</summary>
        ///<returns> Gibt bei Erfolg 0 zurück, ansonsten den Fehler</returns>
        public int test2(string pathToTest)
        {
            List<FormulaList> list1 = new List<FormulaList>();
            List<FormulaList> list2 = new List<FormulaList>();

            list1.Add(PreList.getLists().ElementAt(0));
            list2.Add(PreList.importSingleFL(pathToTest));

            int output = vergleich(list1, list2);
            if (output == 0)
            {
                Console.WriteLine("Test 2: single ein-auslesen OK");
            }
            else
            {
                Console.WriteLine("FEHLER beim single ein-auslesen 2, Test " + output);
            }
            return output;
        }
        ///<summary>
        ///3. Test der Funktionen zum ein-auslesen
        ///</summary>
        ///<returns> Gibt bei Erfolg 0 zurück, ansonsten den Fehler</returns>
        public int test3(string pathToFile)
        {
            List<FormulaList> list1 = new List<FormulaList>();
            List<FormulaList> list2 = new List<FormulaList>();

            list1.Add(PreList.getLists().ElementAt(0));
            list2 = list1;

            list1.ElementAt(0).addFormula(PreList.getLists().ElementAt(0).getFormulas().ElementAt(0));



            PreList.saveFormula(pathToFile, PreList.getLists().ElementAt(0).getFormulas().ElementAt(0));


            list2.Add(PreList.importSingleFL(pathToFile));

            int output = vergleich(list1, list2);
            if (output == 0)
            {
                Console.WriteLine("Test 3: save Formula OK");
            }
            else
            {
                Console.WriteLine("FEHLER beim single ein-auslesen 3, Test " + output);
            }
            return output;
        }

        ///<summary>
        ///Vergleicht 2 listen aus FormulaLists miteinander
        ///</summary>
        ///<returns> Gibt bei Erfolg 0 zurück, ansonsten den Fehler</returns>
        int vergleich(List<FormulaList> list1, List<FormulaList> list2)
        {
            if (list1.Count != list2.Count)
            {
                return 1;
            }
            for (int i = 0; i < list1.Count; i++)
            {
                FormulaList fl1 = list1.ElementAt(i);
                FormulaList fl2 = list2.ElementAt(i);

                if (fl1.getFormulas().Count != fl2.getFormulas().Count)
                {
                    return 2;
                }
                if (fl1.getName() != fl2.getName())
                {
                    return 3;
                }
                for (int j = 0; j < fl1.getFormulas().Count; j++)
                {
                    Formula fr1 = fl1.getFormulas().ElementAt(j);
                    Formula fr2 = fl2.getFormulas().ElementAt(j);
                    if (!fr1.ToCsv().Equals(fr2.ToCsv()))
                    {
                        Console.WriteLine(list1.ElementAt(i).getFormulas().ElementAt(j).ToCsv());
                        Console.WriteLine(list2.ElementAt(i).getFormulas().ElementAt(j).ToCsv());

                        return 4;
                    }
                    if (fr1.getName() != fr2.getName())
                    {
                        return 5;
                    }

                    if (!fr1.getFormulaStandard().Equals(fr2.getFormulaStandard()))
                    {
                        return 6;
                    }

                    for (int k = 0; k < fr1.getVersions().Count; k++)
                    {
                        string v1 = fr1.getVersions().ElementAt(k);
                        string v2 = fr2.getVersions().ElementAt(k);

                        if (v1 != v2)
                        {
                            return 7;
                        }
                    }
                    for (int k = 0; k < fr1.getVars().Count; k++)
                    {
                        char var1 = fr1.getVars().ElementAt(k);
                        char var2 = fr2.getVars().ElementAt(k);

                        if (var1 != var2)
                        {
                            return 8;
                        }
                    }
                }
            }
            return 0;
        }
        ///<summary>
        /// Test aller restlicher Funktionen
        ///</summary>
        ///<returns> Gibt bei Erfolg 0 zurück, ansonsten den Fehler</returns>
        public int test4()
        {
            int output = check();
            if (output == 0)
            {
                Console.WriteLine("Test 4: Funktionen OK");
            }
            else
            {
                Console.WriteLine("FEHLER einer Funktion, Test " + output);
            }
            return output;
        }
        public int check()
        {
            FormulaList fl = PreList.getLists().ElementAt(0);
            Formula fr = fl.getFormulas().ElementAt(0);

            Formula search = fl.searchFormula("Satz des Pythagoras");

            if (search == null)
            {
                Console.WriteLine(fl.getFormulas().ElementAt(0).getName());
                return 1;
            }
            if (search.getName() != "Satz des Pythagoras")
            {
                return 2;
            }

            fl.removeFormula("Satz des Pythagoras");

            search = fl.searchFormula("Satz des Pythagoras");

            if (search != null)
            {
                return 3;
            }

            string standart = fr.getFormulaStandard();

            if (standart != "a^2+b^2=c^2")
            {
                return 4;
            }

            PreList.removeFormulaList("TK");

            for (int i = 0; i < PreList.getLists().Count; i++)
            {
                if (PreList.getLists().ElementAt(i).getName() == "TK")
                {
                    return 5;
                }
            }

            PreList.removeAll();

            if (PreList.getLists().Count != 0)
            {
                return 6;
            }

            return 0;
        }

        /// <summary>
        /// Diese Funktion sollte nach der Beendigung des letzten tests ausgeführt werden. Sie macht alle Sachen, die während der Tests getestet wurden rückgängig
        /// </summary>
        public void stopTests()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string pathToTest = Path.Combine(currentPath, "TestDir");
            string pathToFile = Path.Combine(pathToTest, "Mathe.csv");
            if (Directory.Exists(pathToTest))
                Directory.Delete(pathToTest, true);
        }






        /// <summary>
        /// Diese funktion sollte beim ersten Ausführen des Programms auch ausgeführt werden. Sie fügt viele Standardformeln zur Prelist hinzu
        /// </summary>
        /// <param name="pathToFolder"></param>

        public void formeln_hzfg(string pathToFolder)
        {
            FormulaList fl;
            List<string> list;
            Formula fr;

            fl = new FormulaList("Mathe");

            list = new List<string>();
            list.Add("Hypothenuse");
            list.Add("Kathete");
            list.Add("Ankathete");

            fr = new Formula("Satz des Pythagoras", "c^2=a^2+b^2", list);
            fr.addVersion("a^2=c^2-b^2");
            fr.addVersion("b^2=c^2-a^2");

            fl.addFormula(fr);

            PreList.addExistingFL(fl);


            fl = new FormulaList("TK");
            list = new List<string>();
            list.Add("Spannung");
            list.Add("Widerstand");
            list.Add("Strom");

            fr = new Formula("Ohm'sches Gesetz", "U=R*I", list);
            fr.addVersion("R=U/I");
            fr.addVersion("I=U/R");

            fl.addFormula(fr);


            list = new List<string>();
            list.Add("omega");
            list.Add("pi");
            list.Add("Frequenz");

            fr = new Formula("Omega berechnen", "w=2*π*f", list);
            fr.addVersion("f=2/{2*π}");

            fl.addFormula(fr);


            list = new List<string>();
            list.Add("Strahlungsleistung");
            list.Add("Radius");

            fr = new Formula("Strahlungsdichte", "S = P/{4*π*r^2}", list);

            fl.addFormula(fr);

            PreList.addExistingFL(fl);

            fl = new FormulaList("Physik");
            list = new List<string>();
            list.Add("Gewichtskraft");
            list.Add("Masse");
            list.Add("Fallbeschleunigung");

            fr = new Formula("2. Newtonsches Gesetz", "F=m*g", list);
            fr.addVersion("g=F/m");
            fr.addVersion("m=F/g");

            fl.addFormula(fr);


            list = new List<string>();
            list.Add("Dichte");
            list.Add("Masse");
            list.Add("Volumen");

            fr = new Formula("Dichte berechnen", "p=m/V", list);
            fr.addVersion("V=p/m");
            fr.addVersion("m=p*V");

            fl.addFormula(fr);

            list = new List<string>();
            list.Add("Erster Hebelarm");
            list.Add("Kraft 1");
            list.Add("Zweiter Hebelarm");
            list.Add("Kraft 2");

            fr = new Formula("Hebelgesetz", "L={f*l}/F", list);
            fr.addVersion("F={f*l}/L");
            fr.addVersion("f={F*L}/l");
            fr.addVersion("l={F*L}/f");
            fl.addFormula(fr);


            list = new List<string>();
            list.Add("Druck");
            list.Add("Kraft");
            list.Add("Fläche");

            fr = new Formula("Druck berechnen", "p=F/A", list);
            fr.addVersion("F=p*A");
            fr.addVersion("A=F/p");
            fl.addFormula(fr);

            list = new List<string>();
            list.Add("Arbeit");
            list.Add("Weg");
            list.Add("Zeit");

            fr = new Formula("Arbeit", "W=F*s", list);
            fr.addVersion("s=W/F");
            fr.addVersion("F=W/s");

            fl.addFormula(fr);


            list = new List<string>();
            list.Add("Druck 1");
            list.Add("abs. Temperatur");
            list.Add("abs. Temperatur");
            list.Add("Druck 2");
            list.Add("Volumen 2");
            list.Add("Volumen 1");

            fr = new Formula("allgemeine Gasgleichung", "{V*P}/T={v*p}/t", list);
            fr.addVersion("V={v*p*T}/{t*P}");

            fl.addFormula(fr);


            PreList.addExistingFL(fl);


            list = new List<string>();
            list.Add("Energie");
            list.Add("Masse");
            list.Add("Lichtgeschwindigkeit");

            fr = new Formula("Energiegesetz", "E=m*c^2", list);
            fr.addVersion("m=E/(c^2)");

            fl.addFormula(fr);


            PreList.setFolderPath(pathToFolder + "\\");
            PreList.save();
        }
    }
}
