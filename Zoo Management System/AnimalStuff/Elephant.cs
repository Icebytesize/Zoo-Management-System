using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Zoo_Management_System.AnimalStuff
{
    internal class Elephant : Animal
    {
        public Elephant() 
        {
            Food = "rødder og frugter";
            SoundString = "Trut trut\n";
            Species = "Elephant"; 
            InitSound();

        }

        public Elephant(string name, DateTime birthDate) : this()
        {
            Name = name;
            BirthDate = birthDate;
            GetAge();

        }

        //Har slettet MakeSound(), Sleep() og Eat() metoderne fordi de er skrevet ned i Animals

        public override void ShowAnimal()
        {

            
            Console.Write(
             "        ___.---.___\n" +
             "     _--           --_\n" +
             "   .' .''. )   ( .''. '.\n" +                
             "   ) |   '(°   °)'   | (\n" +
             "   '  ' ' ' ;-; ' ' '  '\n" +
             "    '  '  '| - |'  '  '\n" +
             "     ',_.' | - | '._,'\n" +   
             "           | - |\n" + 
             "           | - |\n" + 
             "           ( \" )\n"); 
            
            
            Console.WriteLine();
            base.ShowAnimal();

        }

    }
}
