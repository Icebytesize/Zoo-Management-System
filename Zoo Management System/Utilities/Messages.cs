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

        public static void InputSvar<T>(List<T> liste, string print)
        {
            Console.SetCursorPosition(1, liste.Count + 9);
            Console.WriteLine(print);
            Console.SetCursorPosition(0, liste.Count + 11);
        }

    }
}
