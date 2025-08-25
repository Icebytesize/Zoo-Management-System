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
            SoundString = "Roar!!!\n";
            Species = "Lion"; // Kan ikke komme på andre løve "racer", så den er fast sat
            InitSound();
           
        }
        public Lion(string name, DateTime birthDate, Gender gender) : this()
        {
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            GetAge();
            
        }
        
        //public override void MakeSound()
        //{
            
        //    base.MakeSound();
           
        //}

        public override void Eat()
        {
            base.Eat();
        }

        public override void Sleep()
        {
           base.Sleep();
        }

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
