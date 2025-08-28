using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Zoo_Management_System.ZooStuff;
using Zoo_Management_System;
using Zoo_Management_System.AnimalStuff;
using System.Threading;

namespace Zoo_Management_System.Utilities
{
    internal static class ZooManager
    {
        public static List<string> ValgtZooMenuMuligheder = new List<string>
        {
            "Se kort over Zoo",
            "Tilføj nyt bur til Zoo",
            "Tilføj nyt dyr til Zoo",
            "Tilføj dyr til bur",
            "Liste over alle bur i Zoo",
            "Liste over alle dyr i Zoo",
        };
        public static List<string> ValgtEnclosureMenuMuligheder = new List<string>
        {
            "Se Alle dyr i bur",
            "Fjern dyr fra bur"
        };
        public static List<Zoo> zoos = new List<Zoo>();
        // public static List<Enclosure> enclosures = new List<Enclosure>(); (Tror ikke jeg skal bruge den her)
        public static int ValgtZoo, ValgtEnclosure;


        public static void ZooSet()
        {
            zoos = LoadFromFile(zoos, Settings.GetFilePath("zoos.txt"), z => new Zoo(z));
            //zoos = LoadZoos();
        }

        public static bool enclosuresInitialized = false; //kig på at den her skal sættes til false igen hvis man åbner en ny zoo

        public static void EnclosureSet()
        {
            if (enclosuresInitialized) return;

            string safeName = zoos[ValgtZoo].Name.Replace(" ", "_");
            string enclosureFile = Settings.GetFilePath($"{safeName}_enclosures.txt");


            //zoos[ValgtZoo].ListOfEnclosures = LoadEnclosures();
            zoos[ValgtZoo].ListOfEnclosures = LoadFromFile(zoos[ValgtZoo].ListOfEnclosures, enclosureFile, e =>
            {
                var parts = e.Split(';');
                return new Enclosure(Convert.ToInt32(parts[0]), parts[1], Convert.ToInt32(parts[2]));
            });

            foreach (var enclosure in zoos[ValgtZoo].ListOfEnclosures)
            {
                string enclosureAnimalsFile = Settings.GetFilePath($"{safeName}_enclosure_{enclosure.Id}.txt");

                enclosure.AnimalsInEnclosure = LoadFromFile(
                    enclosure.AnimalsInEnclosure,
                    enclosureAnimalsFile,
                    line =>
                    {
                        var parts = line.Split(';');
                        string species = parts[0];
                        int id = int.Parse(parts[1]);
                        string name = parts[2];
                        DateTime birthDate = DateTime.ParseExact(parts[3], "dd-MM-yyyy", null);

                        Animal a;
                        switch (species)
                        {
                            case "Elephant": a = new Elephant(name, birthDate); break;
                            case "Giraffe": a = new Giraffe(name, birthDate); break;
                            case "Lion": a = new Lion(name, birthDate, Gender.Male); break;
                            case "Penguin": a = new Penguin(name, birthDate); break;
                            default: return null;
                        }
                        a.Id = id;
                        return a;
                    });
            }

            enclosuresInitialized = true;
        }

        public static void AnimalsSet(List<Animal> animals, string listName)
        {
            animals.Clear(); // husk at tjekke igen om den her virker 
            
            string safeName = zoos[ValgtZoo].Name.Replace(" ", "_");
            string whatFile = Settings.GetFilePath($"{safeName}_{listName}.txt");

            animals = LoadFromFile(animals, whatFile, w =>
            {
                var parts = w.Split(';');
                string species = parts[0];
                int id = int.Parse(parts[1]);
                string name = parts[2];
                Gender gender = Gender.Male;
                DateTime birthDate = DateTime.ParseExact(parts[3], "dd-MM-yyyy", null);

                switch (species)
                {
                    case "Elephant":
                        Animal elephant = new Elephant(name, birthDate);
                        elephant.Id = id;
                        animals.Add(elephant);
                        return elephant;
                    case "Giraffe":
                        Animal giraffe = new Giraffe(name, birthDate);
                        giraffe.Id = id;
                        animals.Add(giraffe);
                        return giraffe;
                    case "Lion":
                        Animal lion = new Lion(name, birthDate, gender);
                        lion.Id = id;
                        animals.Add(lion);
                        return lion;
                    case "Penguin":
                        Animal penguin = new Penguin(name, birthDate);
                        penguin.Id = id;
                        animals.Add(penguin);
                        return penguin;
                    default: return null;

                }


            });
        }

        public static List<T> LoadFromFile<T>(List<T> list, string filePath, Func<string, T> lineFormatter)
        {

            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }

            var lines = File.ReadAllLines(filePath);
            var result = new List<T>();

            
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        result.Add(lineFormatter(line));
                }
            
            return result;
               
        }


        public static void ZooValg()
        {
            ZooSet();

            bool zooValgMenu = true;
            



            while (zooValgMenu)
            {
                Console.Clear();
                int input = 0;
                Messages.VælgMenu(zoos, z => z.Name, "Vælg Zoologisk Have");
                int.TryParse(Console.ReadLine(), out input);

                if (input > 0 && input <= zoos.Count)
                {
                    ValgtZoo = input - 1;
                    VisValgtZoo();
                }
                else if (input == 999)
                {
                    Messages.InputSvar(zoos, "Programmet Afsluttes");
                    zooValgMenu = false;
                    Console.ReadKey();
                }
                else
                {
                    Messages.InputSvar(zoos, "Input ikke forstået prøv igen");
                    Console.ReadKey();
                }
            }
        }

        public static void VisValgtZoo()
        {
            EnclosureSet();
            AnimalsSet(zoos[ValgtZoo].AllAnimalsInZoo, "AllAnimalsInZoo");
            AnimalsSet(zoos[ValgtZoo].AnimalsNotAssigned, "AnimalsNotAssigned");
            Zoo zoo = zoos[ValgtZoo];


            bool zooMenu = true;



            while (zooMenu)
            {
                bool lookingAtMap = true;
                Console.Clear();
                int input = 0;
                Messages.VælgMenu(ValgtZooMenuMuligheder, z => z, $"Velkommen i {zoos[ValgtZoo].Name}");
                int.TryParse(Console.ReadLine(), out input);

                
                if (input == 1)
                {
                    input = 0;

                    while (lookingAtMap)
                    {
                        Console.Clear();
                        Messages.ShowZooMap(zoo.ListOfEnclosures, zoo.Name);
                        int.TryParse(Console.ReadLine(), out input);
                        if (input > 0 && input <= zoo.ListOfEnclosures.Count)
                        {
                            ValgtEnclosure = input - 1;
                            VisValgtEnclosure();
                        }
                        else if (input == 999)
                        { 
                            lookingAtMap = false;
                        }
                    }
                    
                }
                else if (input == 2)
                {
                    Console.Clear();
                    Enclosure enclosure = new Enclosure();
                    zoo.AddEnclosure(enclosure);
                    
                }
                else if (input == 3)
                {
                    Console.Clear();
                    zoo.AddAnimal();
                    

                }
                else if (input == 4)
                {
                    Console.Clear();
                    zoo.AssignAnimalToEnclosure();

                }
                
                else if (input == 5)
                {
                    Console.Clear();
                    //something.something.showAllCageinZoo;
                }
                else if (input == 6)
                {
                    Console.Clear();
                    zoos[ValgtZoo].AllAnimalsInZoo.ForEach(x => Console.WriteLine($"Id: {x.Id}  Art: {x.Species}   Navn: {x.Name}   Alder: {x.GetAge()} År   Født den: {x.BirthDate:dd-MM-yyyy}"));
                    Console.ReadKey();
                }
                else if (input == 999)
                {
                    Messages.InputSvar(ValgtZooMenuMuligheder, "Tilbage til valg af Zoo");
                    zooMenu = false;
                    enclosuresInitialized = false;
                    Console.ReadKey();
                }
                else
                {
                    Messages.InputSvar(ValgtZooMenuMuligheder, "Input ikke forstået prøv igen");
                    Console.ReadKey();
                }
            }
        }

        public static void VisValgtEnclosure()
        {
            bool enclosureMenu = true;
            Zoo zoo = zoos[ValgtZoo];
            Enclosure enclosure = zoo.ListOfEnclosures[ValgtEnclosure];
            enclosure.ParentZoo = zoo;

            while (enclosureMenu)
            {
                Console.Clear();
                int input = 0;
                Messages.VælgMenu(ValgtEnclosureMenuMuligheder, z => z, $"Du kigger på buret {enclosure.Name}");
                int.TryParse(Console.ReadLine(), out input);
                if (input == 1)
                {
                    
                    if (enclosure.AnimalsInEnclosure.Count == 0)
                    {
                        Messages.InputSvar(ValgtEnclosureMenuMuligheder, "Ingen dyr i buret at vise");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        for (int i = 0; i < enclosure.AnimalsInEnclosure.Count; i++)
                        {
                            enclosure.AnimalsInEnclosure[i].ShowAnimal();
                            Console.WriteLine($"Dyr {i + 1} ud af {enclosure.AnimalsInEnclosure.Count}");
                            if (i + 1 == enclosure.AnimalsInEnclosure.Count) { Console.WriteLine("Tryk på en task for at afslutte"); }
                            else { Console.WriteLine("Tryk på en task for at fortsætte\n"); }
                            Console.ReadKey();
                        }
                    }
                }
                else if (input == 2)
                {
                    enclosure.RemoveAnimalFromEnclosure();
                }

                else if (input == 999)
                {
                    Messages.InputSvar(ValgtEnclosureMenuMuligheder, "Tilbage til kort");
                    Console.ReadKey();
                    enclosureMenu = false;
                }

            }
        }

    }
}
