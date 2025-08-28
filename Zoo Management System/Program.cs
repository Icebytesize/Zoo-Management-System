using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo_Management_System.AnimalStuff;
using Zoo_Management_System.ZooStuff;
using Zoo_Management_System.Utilities;
using System.IO;

namespace Zoo_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Denne opgave er den afsluttende opgave for OOP1 på H1 TEC Ballerup
               Der skal står mere om Inkapsulering i Animal
               Der skal mere om nedarvning i Lion
               Der står en del mere om abstraktion i Animal */


            int width = Console.LargestWindowWidth;
            int height = Console.LargestWindowHeight;

            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            ZooManager.ZooValg();




        }
    }
}
