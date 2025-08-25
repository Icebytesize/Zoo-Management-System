using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo_Management_System.ZooStuff;

namespace Zoo_Management_System.PersonStuff
{
    internal class Visitor : Person
    {
        public Enclosure LookingAtEnclosure { get; set; }

        public Visitor()
        {

        }

        public Visitor(string name, int age, Zoo zoo)
        { 
            Name = name;
            Age = age;
            if (zoo.ListOfEnclosures != null && zoo.ListOfEnclosures.Count > 0)
            {
                Random randomEnclosure = new Random();
                int index = randomEnclosure.Next(0, zoo.ListOfEnclosures.Count);
            }

        }
    }
}
