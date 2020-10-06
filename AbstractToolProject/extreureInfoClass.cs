using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AbstractToolProject
{
    class extreureInfoClass
    {
        public static void extreureInfo(string arxiuOriginal)
        {
            // agafem el nom del arxiu original, li treiem la extensio, i l'afegim Info al final de nom i l'extensio:
            // podem fer servir altres extensions de text pla apart de txt.
            //string arxiuInfo = Path.GetFileNameWithoutExtension(arxiuOriginal) + "Info" + Path.GetExtension(arxiuOriginal);
            string arxiuInfo ="";
            try
            {
                arxiuInfo = Path.GetFileNameWithoutExtension(arxiuOriginal) + "Info" + Path.GetExtension(arxiuOriginal);
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Path contains one or more of the invalid characters defined in GetInvalidPathChars()");
            }

            // variables per poder accedir al directori AbstractTool del escriptori:
            //string directoriAbstractTool = "AbstractTool";
            //string pathAbstractTool = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), directoriAbstractTool);
            //pathAbstractTool = Path.Combine(pathAbstractTool, arxiuInfo);
            string directoriAbstractTool = "AbstractTool";
            string pathAbstractTool = "";
            try
            {
                pathAbstractTool = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), directoriAbstractTool);
                pathAbstractTool = Path.Combine(pathAbstractTool, arxiuInfo);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("One of the strings in the array is null.");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Path contains one or more of the invalid characters defined in GetInvalidPathChars()");
            }

            // variables per comptar numero de paraules:
            int numeroParaules = 0;
            string delimitadors = " ,.:;)?!/'-";
            string[] fields = null;
            string line = null;

            // comptem el numero de paraules:
            StreamReader sr = new StreamReader(arxiuOriginal);
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                line.Trim();
                fields = line.Split(delimitadors.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                numeroParaules += fields.Length;
            }
            sr.Close();

            // variables per fer un diccionari amb totes les paraules del text
            // a les key guardarem les paraules, als values el numero d'ocurrencies de la paurala:
            Dictionary<String, int> llistaParaules = new Dictionary<string, int>();
            string[] paraules;

            // afegim al diccionari totes les paraules del text, comprobant primer que no 
            // existeix una key igual per no repetir paraules:
            StreamReader sr2 = new StreamReader(arxiuOriginal);
            while (!sr2.EndOfStream)
            {
                // primer creem un array amb totes les paraules. 
                paraules = sr2.ReadToEnd().Split(' ', ',', '.', ':', ';', ')', '?', '!', '\'', '\n');
                for(int i = 0; i < paraules.Length; i++)
                {
                    if (llistaParaules.ContainsKey(paraules[i].ToLower())) // fem toLower per no repetir la mateixa paraula amb i sense majuscula
                    {
                        llistaParaules[paraules[i].ToLower()] += 1;
                    } else
                    {
                        llistaParaules.Add(paraules[i].ToLower(), 1);
                    }
                }
            }
            sr2.Close();

            // farem que automaticament es crei un arxiu dintre del mateix directori
            // amb totes les paraules que volem eliminar.
            // primer creem un array amb totes les paraules que volem eliminar:
            string[] paraulesAEliminar = { "a", "i", "ni", "o", "si", "no", "però", "sinó", "que", "obstant", "això", "malgrat", "tanmateix", "ara", "adés", "és", "siga", "doncs", "per", "encara", "endemés", "fins",
                                            "després", "mentre", "com", "tal", "encara", "perquè", "perque", "car", "el", "els", "en", "es", "ets", "l", "la", "les", "lo", "los", "n", "'", "la", "les", "lo", "los", "na",
                                            "s", "sa", "ses", "un", "una", "unes", "uns", "amb", "arran", "cap", "contra", "dalt", "damunt", "davall", "de", "deçà", "dellà", "des", "devers", "devora", "dintre", "durant",
                                            "en", "entre", "envers", "excepte", "fins", "llevat", "mitjançant", "per", "pro", "salvant", "salvat", "segon", "sens", "sense", "sobre", "sota", "sots", "tret", "ultra", "via",
                                            "al", "als", "del", "dels", "pel", "pels", "as", "des", "dets", "pes", "can", "cal", "cals", "cas", "son", "çon", "d", "més", "ha", "\n", "aquest", "aquests",
                                            "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "/",  };

            // guardem totes les paraules dintre d'un arxiu al mateix directori:
            //string arxiuParaulesEliminades = "paraulesEliminades.txt";
            //string pathArxiuParaulesEliminades = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), directoriAbstractTool);
            //pathArxiuParaulesEliminades = Path.Combine(pathArxiuParaulesEliminades, arxiuParaulesEliminades);
            string arxiuParaulesEliminades = "paraulesEliminades.txt";
            string pathArxiuParaulesEliminades = "";
            try
            {
                pathArxiuParaulesEliminades = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), directoriAbstractTool);
                pathArxiuParaulesEliminades = Path.Combine(pathArxiuParaulesEliminades, arxiuParaulesEliminades);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("One of the strings in the array is null.");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Path contains one or more of the invalid characters defined in GetInvalidPathChars()");
            }
            using (StreamWriter writer = new StreamWriter(pathArxiuParaulesEliminades))
            {
               foreach(string s in paraulesAEliminar)
               {
                    writer.Write(s + " ");                    
               }
            }
            Console.WriteLine("Creat un arxiu paraulesEliminades.txt dintre del directori que conte totes les paraules a eliminar.");
            Console.WriteLine("Pot afegir mes paraules en el mateix arxiu si ho desitja.");
            Console.WriteLine("Premi enter per continuar...");
            Console.ReadLine();

            // eliminem del diccionari aquelles paraules  que
            // es trobin dintre del arxiu paraules a eliminar:            
            StreamReader sr3 = new StreamReader(pathArxiuParaulesEliminades);
            string[] paraules2;
            while (!sr3.EndOfStream)
            {                
                paraules2 = sr3.ReadToEnd().Split(' ');
                for (int i = 0; i < paraules2.Length; i++)
                {
                    if (llistaParaules.ContainsKey(paraules2[i]))
                    {
                        llistaParaules.Remove(paraules2[i]);
                    }                    
                }
            }
            sr3.Close();

            // printem diccionari per fer comprobacions:
            /*foreach (KeyValuePair<String, int> kvp in llistaParaules)
            {
                Console.WriteLine("Key = {0} -> Value = {1}", kvp.Key, kvp.Value);
            }*/   

            // generem un xml a partir del diccionari:
            /*string arxiuXML = "keysAndValues.xml";
            string pathArxiuXML = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), directoriAbstractTool);
            pathArxiuXML = Path.Combine(pathArxiuXML, arxiuXML);
            XElement xElement = new XElement("root",
                llistaParaules.Select(kv => new XElement(kv.Key, kv.Value)));
            xElement.Save(pathArxiuXML);*/


            // ordenem el diccionari per numero d'ocurrencies de cada paraula:
            var llistaParaulesOrdenada = llistaParaules.OrderByDescending(x => x.Value);


            // guardem en 5 variables les keys de les paraules amb mes occurencies:
            String occurencia1 = llistaParaulesOrdenada.ElementAt(0).Key;
            String occurencia2 = llistaParaulesOrdenada.ElementAt(1).Key;
            String occurencia3 = llistaParaulesOrdenada.ElementAt(2).Key;
            String occurencia4 = llistaParaulesOrdenada.ElementAt(3).Key;
            String occurencia5 = llistaParaulesOrdenada.ElementAt(4).Key;


            // escribim la info al arxiu final (l'arxiu amb la info):
            using (StreamWriter writer2 = new StreamWriter(pathAbstractTool))
            {                
                writer2.WriteLine("Nom del fitxer: " + Path.GetFileNameWithoutExtension(arxiuOriginal));
                writer2.WriteLine("Extensió: " + Path.GetExtension(arxiuOriginal));
                writer2.WriteLine("Data: {0:dd-MM-yyyy}", DateTime.Now);
                writer2.WriteLine("Número de paraules: " + numeroParaules);
                writer2.WriteLine("Temàtica: " + occurencia1 + ", " + occurencia2 + ", " + occurencia3 + ", " + occurencia4 + ", " + occurencia5 );
            }


        }
    }
}




