using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Zoo_Management_System.AnimalStuff
{
    internal class Giraffe : Animal
    {
        public Giraffe()
        {
            Food = "blade fra træer og buske";
            SoundString = "Fucking ?\n";
            Species = "Giraffe"; 
            InitSound();

        }
        

        public Giraffe(string name, DateTime birthDate) : this()
        {
            Name = name;
            BirthDate = birthDate;
            GetAge();

        }

        //Har slettet MakeSound(), Sleep() og Eat() metoderne fordi de er skrevet ned i Animals




        public override void ShowAnimal()
        {
            Console.Write(
                "  ___,^.^_ ___\n" +
                "  \\   ,    \"_/\n" +
                "   ~\"T(  ° °)\n" +
                "     | \\    |\n" +
                "     | ~\\  .|\n" +
                "     |   |`-'\n" +
                "     |   |\n" +
                "     |   |\n" +
                "     |   |\n" +
                "     |   |\n");  

           


            Console.WriteLine();
            base.ShowAnimal();

        }
    }
}
