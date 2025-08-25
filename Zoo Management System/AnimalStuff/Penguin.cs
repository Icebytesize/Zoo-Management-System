using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_Management_System.AnimalStuff
{
    internal class Penguin : Animal
    {
        public Penguin()
        {
            Food = "fisk";
            SoundString = "Noot noot\n";
            Species = "Penguin";
            InitSound();

        }


        public Penguin(string name, DateTime birthDate) : this()
        {
            Name = name;
            BirthDate = birthDate;
            GetAge();

        }

        //Har slettet MakeSound(), Sleep() og Eat() metoderne fordi de er skrevet ned i Animals




        public override void ShowAnimal()
        {
            /*        .___.
                     /     \
                    | O _ O |
                    /  \_/  \
                  .' /     \ `.
                 / _|       |_ \
                (_/ |       | \_)
                    \       /
                   __\_>-<_/__
                   ~;/     \;~
                            */
             
            Console.Write(
                "      .___.\n" +
                "     /     \\\n" +
                "    | O _ O |\n" +
                "    /  \\_/  \\\n" +
                "  .' /     \\ `.\n" +
                " / _|       |_ \\\n" +
                "(_/ |       | \\_)\n" +
                "    \\       /\n" +
                "   __\\_>-<_/__\n" +
                "   ~;/     \\;~\n");




            Console.WriteLine();
            base.ShowAnimal();

        }
    }
}
