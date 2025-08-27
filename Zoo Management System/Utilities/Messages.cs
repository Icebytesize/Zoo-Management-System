using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Zoo_Management_System.ZooStuff;

namespace Zoo_Management_System.Utilities
{
    internal static class Messages
    {
       
        public static void VælgMenu<T>(List<T> liste, Func<T, string> tekstSelector, string titel)
        {

            int maxLength = liste.Max(item => $"{tekstSelector(item)}".Length) + 6; 
            int boxWidth = Math.Max(maxLength, titel.Length + 19); 
            int totalWidth = boxWidth + 2; 


            Console.WriteLine("╔" + new string('═', boxWidth) + "╗");
            string titelText = $"  {titel}  ";
            int padLeft = (boxWidth - titelText.Length) / 2;
            int padRight = boxWidth - titelText.Length - padLeft;
            Console.WriteLine("║" + new string(' ', padLeft) + titelText + new string(' ', padRight) + "║");
            Console.WriteLine("╠" + new string('═', boxWidth) + "╣");


            Console.WriteLine("║" + new string(' ', boxWidth) + "║");


            for (int i = 0; i < liste.Count; i++)
            {
                string text = $"{i + 1}. {tekstSelector(liste[i])}";
                string paddedText = text.PadRight(boxWidth);
                Console.WriteLine($"║{paddedText}║");
            }

            
            for(int i = 0; i < 2; i++)Console.WriteLine("║" + new string(' ', boxWidth) + "║");

            string inputText = "999: Afslut";
            string inputPadded = inputText.PadRight(boxWidth);
            Console.WriteLine($"║{inputPadded}║");

            Console.WriteLine("║" + new string(' ', boxWidth) + "║");
            Console.WriteLine("╠" + new string('═', boxWidth) + "╣");

            
            inputText = " >   ";
            inputPadded = inputText.PadRight(boxWidth);
            Console.WriteLine($"║{inputPadded}║");

            
            Console.WriteLine("╚" + new string('═', boxWidth) + "╝");

            Console.SetCursorPosition(4, liste.Count + 9);

            //Console.Write("╔══════════════════════════════════════╗\n");
            //Console.Write("║                                      ║\n");
            //foreach (var item in liste)
            //{
            //    Console.Write("║                                      ║\n");
            //}
            //Console.Write("║                                      ║\n");
            //Console.Write("╠══════════════════════════════════════╣\n");
            //Console.Write("║                                      ║\n");
            //Console.Write("╚══════════════════════════════════════╝\n");
            //Det gamle skellet

        }

        public static void ShowZooMap(List<Enclosure> listeOverBure, string zooNavn)
        {
            ZooManager.enclosuresInitialized = false;
            ZooManager.EnclosureSet();

            if (listeOverBure.Count == 0)
            {
                InputSvar(ZooManager.ValgtZooMenuMuligheder, "Intet at tegne kort over");
                return;
            }

            int maxDimensionSize = listeOverBure.Max(bur => bur.Size / 10) + 6;
            int maxDimensionName = listeOverBure.Max(bur => bur.Name.Length + 6);
            int maxDimension = Math.Max(maxDimensionName, maxDimensionSize);

            int cageRows = (int)Math.Ceiling(listeOverBure.Count / 2.0);


            int innerWidth = maxDimension * 2 + 9;
            string title = $" Kort over {zooNavn} ";
            int titlePadding = Math.Max(0, innerWidth - title.Length);
            int leftPad = titlePadding / 2;
            int rightPad = titlePadding - leftPad;

            

            Console.WriteLine("╔" + new string('═', innerWidth) + "╗");
            Console.WriteLine("║" + new string(' ', leftPad) + title + new string(' ', rightPad) + "║");
            Console.WriteLine("║" + new string(' ', innerWidth) + "║");


            for (int row = 0; row < cageRows; row++)
            {
                Enclosure cage1 = listeOverBure[row * 2];
                Enclosure cage2 = (row * 2 + 1 < listeOverBure.Count) ? listeOverBure[row * 2 + 1] : null;
                

                for (int line = 0; line < maxDimension; line++)
                {
                    string line1 = DrawCage(cage1, maxDimension, line, maxDimension);
                    string line2 = cage2 != null ? DrawCage(cage2, maxDimension, line, maxDimension) : new string(' ', maxDimension);

                    string verticalRoad = " │   │ "; // lodret vej mellem burene

                    Console.WriteLine("║ " + line1 + verticalRoad + line2 + " ║");
                }

                // vandret vej under bure (kun hvis ikke sidste række)
                if (row < cageRows - 1)
                {
                    Console.WriteLine("║ " + new string('─', maxDimension) + "─┘   └" + new string('─', maxDimension) + "  ║");
                    Console.WriteLine("║" + new string(' ', innerWidth) + "║");
                    Console.WriteLine("║ " + new string('─', maxDimension) + "─┐   ┌" + new string('─', maxDimension) + "  ║");
                }
            }

            Console.WriteLine("╚" + new string('═', innerWidth) + "╝");

            string[] ikonForklaring = {"L: Løve", "G: Giraf", "E: Elefant", "P: Penguin", "X: Gæst", "Z: Zookeeper" };
            Console.SetCursorPosition(innerWidth + 5, 1);
            Console.WriteLine("Ikon betydning");
            for (int i = 0; i < ikonForklaring.Length; i++)
            {
                Console.SetCursorPosition(innerWidth + 5, i+2);
                Console.WriteLine(ikonForklaring[i]);
            }
            Console.SetCursorPosition(innerWidth + 5, ikonForklaring.Length + 5);
            Console.WriteLine("Valgmuligheder:");
            Console.SetCursorPosition(innerWidth + 5, ikonForklaring.Length + 6);
            Console.WriteLine($"1 - {listeOverBure.Count}: Se på bur");
            Console.SetCursorPosition(innerWidth + 5, ikonForklaring.Length + 7);
            Console.WriteLine("999: Afslut");
            Console.SetCursorPosition(innerWidth + 5, ikonForklaring.Length + 9);
            Console.Write("> ");







        }
        private static string DrawCage (Enclosure cage, int segmentSize, int line, int maxDimension, Random rnd = null)
        {
            
            if (rnd == null)
            {
                rnd = new Random();
            }

            
            int cageSize = cage.Size / 10 + 2;
            int padTop = (segmentSize - cageSize) / 2;
            int padLeft = (segmentSize - cageSize) / 2;
            int interiorSize = cageSize - 2;
            char[] lineContent = Enumerable.Repeat(' ', interiorSize).ToArray();
        
                

            // Linje til navn + ID skal være præcis 2 linjer over burets start
            int textLine = 0;
            if (line == textLine && textLine >= 0)
            {
                string label = $"{cage.Id}: {cage.Name}";
                int padding = Math.Max(0, (segmentSize - label.Length) / 2);
                return new string(' ', padding) + label + new string(' ', segmentSize - label.Length - padding);
            }


            if (line < padTop || line >= padTop + cageSize)
                return new string(' ', segmentSize);


            int cageLine = line - padTop;
            string content;


            if (cageLine == 0)
                content = "╔" + new string('═', cageSize - 2) + "╗";

            else if (cageLine == cageSize - 1)
                content = "╚" + new string('═', cageSize - 2) + "╝";

            else
            {

                // Placer dyr tilfældigt (hvis linje passer)
                if (cage.AnimalsInEnclosure != null)
                {
                    for (int i = 0; i < cage.AnimalsInEnclosure.Count && i < interiorSize; i++)
                    {
                        char symbol = cage.AnimalsInEnclosure[i].Species.ToUpper()[0];

                        // Beregn en deterministisk position: diagonal-mønster
                        int y = i + 1; // lodret placering
                        int x = rnd.Next(0, interiorSize); // vandret placering (øverst til højre → nederst til venstre)

                        if (cageLine == y) // placer kun, når vi er på den korrekte linje
                            lineContent[x] = symbol;
                    }
                }
                //if (cage.VisitorsLookingAtEnclosure != null)
                //{
                //    for (int i = 0; i < cage.VisitorsLookingAtEnclosure.Count && i < interiorSize; i++)
                //    {
                //        char symbol = 'x';

                //    }

                //} Hvis jeg får tid, så skal det her udvides til at placere nogle tilfældige gæster på kortet

                return new string(' ', padLeft) + "║" + new string(lineContent) + "║" + new string(' ', segmentSize - cageSize - padLeft);

            }

            return new string(' ', padLeft) + content + new string(' ', segmentSize - cageSize - padLeft);
        }

        public static void InputSvar<T>(List<T> liste, string print)
        {
            Console.SetCursorPosition(1, liste.Count + 9);
            Console.WriteLine(print);
            Console.SetCursorPosition(0, liste.Count + 11);
        }

    }
}
