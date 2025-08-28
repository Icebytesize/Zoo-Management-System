using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo_Management_System.PersonStuff;
using Zoo_Management_System.Utilities;

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

        public Zoo ParentZoo { get; set; }

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

        public void RemoveAnimalFromEnclosure() 
        {
            Animal valgtDyr = null;
            int input = 0;
            if (AnimalsInEnclosure.Count == 0)
            {
                Messages.InputSvar(ZooManager.ValgtEnclosureMenuMuligheder, "Inden dyr i bur");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Messages.VælgMenu(AnimalsInEnclosure, a => $"{a.Species}: {a.Name}", "Vælg hvilket dyr du vil fjerne fra buret");
                if (int.TryParse(Console.ReadLine(), out input)) { valgtDyr = AnimalsInEnclosure[input - 1]; }


                if (input > 0 && input <= AnimalsInEnclosure.Count)
                {
                    Console.Clear();
                    Console.Write($"Ønsker du at fjerne {valgtDyr.Species}: {valgtDyr.Name} fra buret {Name}?\n1: Fjern\n2: Annuler\n\n> ");
                    if (int.TryParse(Console.ReadLine(), out input)) { }
                    if (input == 1)
                    {
                        AnimalsInEnclosure.Remove(valgtDyr);
                        ParentZoo.AnimalsNotAssigned.Add(valgtDyr);
                        string safeName = ParentZoo.Name.Replace(" ", "_");
                        string animalsNotInEnclosureFile = Settings.GetFilePath($"{safeName}_AnimalsNotAssigned.txt");
                        string animalsInEnclosureFile = Settings.GetFilePath($"{safeName}_enclosure_{Id}.txt");
                        ParentZoo.SaveListToFile(ParentZoo.AnimalsNotAssigned, animalsNotInEnclosureFile, a => $"{a.Species};{a.Id};{a.Name};{a.BirthDate:dd-MM-yyyy}");
                        ParentZoo.SaveListToFile(AnimalsInEnclosure, animalsInEnclosureFile, a => $"{a.Species};{a.Id};{a.Name};{a.BirthDate:dd-MM-yyyy}");

                        Console.Clear();
                        Console.WriteLine($"{valgtDyr.Name} fjernet fra {Name}");
                        Console.ReadKey();

                    }
                    


                }
            }
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
