using System;
using System.Collections.Generic;
using System.Linq;
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
           

            if (listeOverBure.Count == 0)
            {
                InputSvar(ZooManager.ValgtZooMenuMuligheder, "Intet at tegne kort over");
                return;
            }

            int maxDimension = listeOverBure.Max(bur => bur.Size / 10) + 2;
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
                    string line1 = DrawCageLine(cage1, maxDimension, line);
                    string line2 = cage2 != null ? DrawCageLine(cage2, maxDimension, line) : new string(' ', maxDimension);

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
        }
        private static string DrawCageLine(Enclosure cage, int segmentSize, int line)
        {
            int cageSize = cage.Size / 10 + 2;
            int padTop = (segmentSize - cageSize) / 2;
            int padLeft = (segmentSize - cageSize) / 2;

            if (line < padTop || line >= padTop + cageSize)
                return new string(' ', segmentSize);

            int cageLine = line - padTop;
            string content;

            if (cageLine == 0) content = "╔" + new string('═', cageSize - 2) + "╗";
            else if (cageLine == cageSize - 1) content = "╚" + new string('═', cageSize - 2) + "╝";
            else content = "║" + new string(' ', cageSize - 2) + "║";

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
