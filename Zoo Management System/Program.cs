using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo_Management_System.AnimalStuff;

namespace Zoo_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Beskriv på disse linjer hvad opgaven er og måske beskriv begreb
             * 
             * 
             * 
             */

            Lion lion1 = new Lion("Brian", new DateTime(1993, 9, 16), Gender.Male);
            //lion1.Name = "Brian";
            
            lion1.MakeSound();
            lion1.ShowAnimal();
            Console.ReadKey();

        }
    }
}
