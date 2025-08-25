using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo_Management_System.ZooStuff;

namespace Zoo_Management_System.PersonStuff
{
    internal class Zookeeper : Person
    {
        public Enclosure AssignedEnslosure { get; set; }

        public Zookeeper()
        {

        }

        public Zookeeper(string name, int age, Enclosure enclosure)
        {
            Name = name;
            Age = age;
            AssignedEnslosure = enclosure;
        }

        public void FeedAnimals()
        {

        }

        public void CleanEnclosure()
        {

        }
    }
}
