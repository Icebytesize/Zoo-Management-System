using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo_Management_System.AnimalStuff;
using Zoo_Management_System.Utilities;

namespace Zoo_Management_System.ZooStuff
{
    internal class Zoo
    {
        public string Name { get; set; }
        public List<Enclosure> ListOfEnclosures { get; set; } //En liste over alle bure i Zoo'en
        public List<Animal> AllAnimalsInZoo { get; set; } //En Liste over alle dyrene i parken
        public List<Animal> AnimalsNotAssigned { get; set; }



        public Zoo(string name) 
        { 
            Name = name;
            ListOfEnclosures = new List<Enclosure>();
            AllAnimalsInZoo = new List<Animal>();
            AnimalsNotAssigned = new List<Animal>();
        }
        public void AddAnimal()
        {
            Console.Clear();
            Console.WriteLine($"Opret nyt dyr i {Name}");

            Console.Write("vælg hvilket dyr vil du oprette:\n" +
                "1: Elephant\n" +
                "2: Giraffe\n" +
                "3: Lion\n" +
                "4: Penguin\n" +
                "> ");
            int.TryParse(Console.ReadLine(), out int speciesChoice);

            Animal animal = null;

            switch (speciesChoice)
            {
                case 1:
                    animal = new Elephant();
                    break;
                case 2:
                    animal = new Giraffe();
                    break;
                case 3:
                    animal = new Lion();
                    break;
                case 4:
                    animal = new Penguin();
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Annuleret.");
                    return;
            };

            if (AllAnimalsInZoo != null && AllAnimalsInZoo.Count > 0)
                animal.Id = AllAnimalsInZoo.Max(a => a.Id) + 1;
            else
                animal.Id = 1;


            Console.Write("Indtast navn: ");
            animal.Name = Console.ReadLine();

            Console.Write("Indtast fødselsdato (MM-dd-yyyy): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
            animal.BirthDate = birthDate;

            Console.Clear();
            Console.Write($"Ønsker du at oprette dette dyr i {Name}\n\n" +
                $"Art: {animal.Species}\n" +
                $"Navn: {animal.Name}\n" +
                $"Fødselsdato: {animal.BirthDate:dd-MM-yyyy}\n" +
                $"Alder: {animal.GetAge()}\n\n" +
                $"1: Opret\n" +
                $"2: Annuler\n\n" +
                $"> ");

            int.TryParse(Console.ReadLine(), out int input);
            if (input == 1)
            {
                AllAnimalsInZoo.Add(animal);
                AnimalsNotAssigned.Add(animal);
                string safeName = Name.Replace(" ", "_");
                string allAnimalsFile = Settings.GetFilePath($"{safeName}_AllAnimalsInZoo.txt");
                string animalsNotInEnclosureFile = Settings.GetFilePath($"{safeName}_AnimalsNotAssigned.txt");
                SaveListToFile(AllAnimalsInZoo, allAnimalsFile, a => $"{a.Species};{a.Id};{a.Name};{a.BirthDate:dd-MM-yyyy}");
                SaveListToFile(AnimalsNotAssigned, animalsNotInEnclosureFile, a => $"{a.Species};{a.Id};{a.Name};{a.BirthDate:dd-MM-yyyy}");

                Console.Write($"{animal.Name} oprette i {Name}");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("Oprettelse af dyr annuleret.");
                Console.ReadKey();
            }
        }

        public void ShowAllEnclosures()
        {
            for (int i = 0; i < ListOfEnclosures.Count; i++)
            {
                Console.WriteLine($"Bur {i + 1} ud af {ListOfEnclosures.Count}\n{ListOfEnclosures[i].Name}: ");

                for (int j = 0; j < ListOfEnclosures[i].AnimalsInEnclosure.Count; j++)
                {
                    
                }
            }
        }

        public void AddEnclosure(Enclosure enclosure)
        {
           
            if(ListOfEnclosures != null) enclosure.Id = ListOfEnclosures.Max(a => a.Id) + 1;
            else enclosure.Id = 1;

            Console.WriteLine($"Opret byt bur i {Name}");
            Console.Write("Indtast navn på det bur du ønsker at oprette: ");
            enclosure.Name = Console.ReadLine();
            if (enclosure.Name == "") enclosure.Name = "bur default navn";
            Console.Write("Indtast størrelsen i 10ere på det bur du ønsker at oprette: ");
            int.TryParse(Console.ReadLine(), out int size);
            if (size < 10) size = 10;
            enclosure.Size = size;

            Console.Clear();
            Console.Write($"Ønsker du at oprette dette bur i {Name}\n" +
                $"Bur navn: {enclosure.Name}\n" +
                $"Burets Størrelse: {enclosure.Size}\n\n" +
                $"1: Opret\n" +
                $"2: Annuler\n\n" +
                $"> ");

            int input;
            try
            {
                input = Convert.ToInt32(Console.ReadLine());
            }
            catch (NotFiniteNumberException e)
            {
                Console.WriteLine(e);
                input = 0;
            }
            catch (Exception e)
            {
                input = 0;
            }

            if (input == 1)
            {
                ListOfEnclosures.Add(enclosure);
                string safeName = Name.Replace(" ", "_");
                string enclosureFile = Settings.GetFilePath($"{safeName}_enclosures.txt");
                SaveListToFile(ListOfEnclosures, enclosureFile, e => $"{e.Id};{e.Name};{e.Size}");
                
            }
            else if (input == 2)
            {
                Console.WriteLine("Oprettelse af bur annuleret");
            }
        }

        //public void RemoveAnimalFromEnclosure()
        //{
            
        //}

        public void AssignAnimalToEnclosure()
        {
            Messages.VælgMenu(AnimalsNotAssigned, a => $"{a.Species}: {a.Name}", "Vælg dyr ikke tildelt bur");

            if (int.TryParse(Console.ReadLine(), out int animalChoice))
            {
                Console.Clear();
                Messages.VælgMenu(ListOfEnclosures, l => l.Name, "Vælg bur dyret skal tilføjes");

                if (int.TryParse(Console.ReadLine(), out int cageChoice))
                {
                    Console.Clear();
                    Console.Write("Ønser du at tilføjde dette dyr\n");
                    AnimalsNotAssigned[animalChoice - 1].ShowAnimal();
                    Console.Write($"\n\ntil dette bur?");
                    Console.Write($"\n\nCage id: {ListOfEnclosures[cageChoice-1].Id}   cage name: {ListOfEnclosures[cageChoice - 1].Name}   cage size: {ListOfEnclosures[cageChoice - 1].Size}\n\n" +
                        $"1: Tilføj\n" +
                        $"2: Annuler:\n\n" +
                        $"> ");
                    int.TryParse(Console.ReadLine(), out int input);
                    if (input == 1)
                    {
                        Console.Clear();
                        Console.WriteLine($"Tilykke du har tiføjet {AnimalsNotAssigned[animalChoice - 1].Name} til buret {ListOfEnclosures[cageChoice - 1].Name}");

                        ListOfEnclosures[cageChoice - 1].AddAnimalToEnclosure(AnimalsNotAssigned[animalChoice - 1]);
                        AnimalsNotAssigned.Remove(AnimalsNotAssigned[animalChoice - 1]);

                        string safeName = Name.Replace(" ", "_");
                        string animalsNotInEnclosureFile = Settings.GetFilePath($"{safeName}_AnimalsNotAssigned.txt");
                        string cageFile = Settings.GetFilePath($"{safeName}_enclosure_{ListOfEnclosures[cageChoice - 1].Id}.txt");
                        SaveListToFile(AnimalsNotAssigned, animalsNotInEnclosureFile, a => $"{a.Species};{a.Id};{a.Name};{a.BirthDate:dd-MM-yyyy}");
                        SaveListToFile(ListOfEnclosures[cageChoice - 1].AnimalsInEnclosure, cageFile, c => $"{c.Species};{c.Id};{c.Name};{c.BirthDate:dd-MM-yyyy}");


                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Oprettelse Annuleret");
                        Console.ReadKey();
                    }
     
                }
                else
                {
                    Messages.InputSvar(ListOfEnclosures, "Oprettelse Annuleret");
                }
            
            }
            
            else
            {
                Messages.InputSvar(AnimalsNotAssigned, "Oprettelse Annuleret");
            }
        }


        public void SaveListToFile<T>(List<T> list, string filePath, Func<T, string> lineFormatter)
        {

            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var item in list)
                {
                    writer.WriteLine(lineFormatter(item));
                }
            }
        }
    }
}
