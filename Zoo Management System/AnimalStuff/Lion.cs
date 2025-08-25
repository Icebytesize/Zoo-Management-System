using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Zoo_Management_System.Utilities;
using System.IO;

namespace Zoo_Management_System.AnimalStuff
{
    public enum Gender
    {
        Male,
        Female,
        Unknown
    }
    internal class Lion : Animal 
    {
        
        public Gender Gender { get; set; }

        public Lion()
        {
            Food = "råt kød";
            SoundString = "Roar!!!\n";
            Species = "Lion"; // Kan ikke komme på andre løve "racer", så den er fast sat
            InitSound();
           
        }
        // Nedarvning (Initiueret med (Lion : Animal) oppe i toppen), gør at Lion arver egenskaber og metoder fra Animal
        // hvilket er grunden til den eneste property der er sat i lion er Gender. Alt andet i Animal

        public Lion(string name, DateTime birthDate, Gender gender) : this()
        {
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            GetAge();
            
        }
        
        //Har slettet MakeSound(), Sleep() og Eat() metoderne fordi de er skrevet ned i Animals



        
        public override void ShowAnimal()
        {

            if (Gender == Gender.Female)
            {
                Console.Write(
                    "\n" +
                    "     .-.       .-.\r\n" +
                    "    (   \\_.-._/   )\r\n" +
                    "     \\           /\r\n" +
                    "     | __     __ |\r\n" +
                    "     | \\O\\   /O/ |\r\n" +
                    "      \\  \"   \"  /\r\n" +
                    "      /\\_`-v-'_/\\\r\n" +
                    "     /  \\._|_./  \\\r\n" +
                    "    |    \\___/    |\r\n" +
                    "    |             |\r\n");
            }

            else
            {
                Console.Write(
                    "   .:\\\\||||||||//:.\n" +
                    "  /\\\\.-.\\||||//.-.//\\\r\n" +
                    " |**( * \\_.-._/ * )**|\r\n" +
                    "  |*\\\\           //*|\r\n" +
                    "   \\\\| __     __ |//\r\n" +
                    "   /|| \\O\\   /O/ ||\\\r\n" +
                    "  |\\\\\\\\  \"   \"  ////|\r\n" +
                    "   \\\\\\\\\\_`-v-'_/////\r\n" +
                    "    \\\\\\\\\\._|_./////\r\n" +
                    "    |\\\\\\\\\\___/////|\r\n" +
                    "    | \\\\\\\\\\|///// |\r\n");
            }
            
            Console.WriteLine();
            base.ShowAnimal();





        }
    }
}
