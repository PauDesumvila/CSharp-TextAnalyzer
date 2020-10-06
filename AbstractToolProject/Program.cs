using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbstractToolProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // creem un path al escriptori per el directori de nom "AbstractTool":
            string directoriAbstractTool = "AbstractTool";
            string pathAbstractTool = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), directoriAbstractTool);

            // comprobem que el directori existeix, en cas contrari el creem:
            if (!Directory.Exists(pathAbstractTool))
            {
                Directory.CreateDirectory(pathAbstractTool);
                Console.WriteLine("Directori de nom \"AbstractTool\" creat al escriptori.");
                Console.WriteLine("Siusplau desi els documents a analitzar dintre d'aquest directori.");
                Console.WriteLine("Premi enter un cop els documents estiguin desats...");
                Console.ReadLine();
            }
            
            // demanem el nom del arxiu que volem analitzar:
            Console.WriteLine("Siusplau, indiqueu el nom del arxiu (amb extensio) que voleu analitzar: ");
            string arxiuOriginal = Console.ReadLine();    
            
            // combinem el nom que ens han introduit amb el path de AbstractTool del desktop:
            string pathArxiuOriginal = Path.Combine(pathAbstractTool, arxiuOriginal);
            Console.WriteLine(pathArxiuOriginal);

            // comprobem que l'arxiu que es vol analitzar existeix:
            if (!File.Exists(pathArxiuOriginal))
            {
               Console.WriteLine("Ho sento, aquest arxiu no existeix...");
            }
            // en cas d'existir l'analitzem:
            else
            {
               extreureInfoClass.extreureInfo(pathArxiuOriginal);
               Console.WriteLine("Processant arxiu...");
               Thread.Sleep(2000);
               Console.WriteLine("Operació extreure info completada.");
               Console.WriteLine("Arxiu info desat a la carpeta AbstractTool de l'escriptori.");
               Console.WriteLine("Arxiu XML desat a la carpeta AbstractTool de l'escriptori.");
            }

            Console.WriteLine("Premi enter per sortir...");
            Console.ReadLine();
                       
            
        }
    }
}
