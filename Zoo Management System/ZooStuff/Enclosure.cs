using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_Management_System.ZooStuff
{
    internal class Enclosure
    {
        private static int _nextId = 1;
        public int Id { get; private set; }
        public string Name { get; set; }
        public int Size { get; set; } // Størrelse på buret i m^2
        public List<Animal> AnimalsInEnclosure { get; set; } //Liste med dyr i buret

        public Enclosure(string name, int size)
        {
            Id = _nextId++;
            Name = name;
            Size = size;
            AnimalsInEnclosure = new List<Animal>();

        }
        public void AddAnimal(Animal animal)
        {
            AnimalsInEnclosure.Add(animal);
        }

        public void RemoveAnimal(Animal animal) 
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
