using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_Management_System.ZooStuff
{
    internal class Zoo
    {
        public string Name { get; set; }
        public List<Enclosure> ListOfEnclosures { get; set; } //En liste over alle bure i Zoo'en
        public List<Animal> AllAnimals { get; set; } //En Liste over alle dyrene i parken
        public List<Animal> AnimalsNotAssignedAEnclosure { get; set; }

        public Zoo(string name) 
        { 
            Name = name;
        }

        public void AddEnclosure(Enclosure enclosure)
        {
            ListOfEnclosures.Add(enclosure);
        }
    }
}
