using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo_Management_System.PersonStuff;

namespace Zoo_Management_System.ZooStuff
{
    internal class Enclosure
    {
        private static int _nextId = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; } // Størrelse på buret i m^2
        public List<Animal> AnimalsInEnclosure { get; set; } //Liste med dyr i buret
        public List<Person> VisitorsLookingAtEnclosure { get; set; }
        public List<Person> ZookeepersWorkingAtEnclosure { get; set; }


        public Enclosure()
        {

        }
        public Enclosure(int id, string name, int size)
        {
            Id = id;
            Name = name;
            Size = size;
            AnimalsInEnclosure = new List<Animal>();
            VisitorsLookingAtEnclosure = new List<Person>();
            ZookeepersWorkingAtEnclosure = new List<Person>();

        }
        public void AddAnimalToEnclosure(Animal animal)
        {
            AnimalsInEnclosure.Add(animal);
        }

        public void RemoveAnimalFromEnclosure(Animal animal) 
        {
            AnimalsInEnclosure.Remove(animal);
        }

        public void ListAnimals()
        {
            if (AnimalsInEnclosure.Count > 0)
            {
                Console.WriteLine($"Dyr i buret med navnet {Name} ");
                foreach (var animal in AnimalsInEnclosure)
                {
                    Console.WriteLine(animal.Name);
                }
            }
            else
            {
                Console.WriteLine("Der er ingen dyr i buret");
            }
        }
    }
}
