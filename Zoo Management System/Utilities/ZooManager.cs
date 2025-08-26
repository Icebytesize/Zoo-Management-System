using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Zoo_Management_System.ZooStuff;
using Zoo_Management_System;

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
            "Liste over alle Dyr i Zoo",
        };
        public static List<Zoo> zoos = new List<Zoo>();
        public static List<Enclosure> enclosures = new List<Enclosure>();
        public static int ValgtZoo;


        public static void ZooSet()
        {
            zoos = LoadZoos();
        }

        public static void EnclosureSet()
        {
            enclosures = LoadEnclosures();
        }

        public static List<Zoo> LoadZoos()
        {
            string zooListFile = Settings.GetFilePath("zoos.txt");

            if(!File.Exists(zooListFile))
            {
                string[] defaultZoos = { "Københavns Zoo" };
                Directory.CreateDirectory(Path.GetDirectoryName(zooListFile));
                File.WriteAllLines(zooListFile, defaultZoos);
            }

            
            string[] zooLines = File.ReadAllLines(zooListFile);

            foreach (var line in zooLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    zoos.Add(new Zoo(line));
            }

            return zoos;
        }

        public static List<Enclosure> LoadEnclosures()
        {
            string safeName = zoos[ValgtZoo].Name.Replace(" ", "_");
            string enclosureFile = Settings.GetFilePath($"{safeName}_enclosure.txt");

            if (!File.Exists(enclosureFile))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(enclosureFile));
                File.WriteAllLines(enclosureFile, Array.Empty<string>());
            }

            var enclosures = new List<Enclosure>();
            string[] enclosureLines = File.ReadAllLines(enclosureFile);

            foreach (var line in enclosureLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var parts = line.Split(';');
                    string name = parts[0];
                    int size = parts.Length > 1 && int.TryParse(parts[1], out int s) ? s : 0;
                    enclosures.Add(new Enclosure(name, size));
                }
            }

            return enclosures;
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

            bool zooMenu = true;

            while (zooMenu)
            {
                Console.Clear();
                int input = 0;
                Messages.VælgMenu(ValgtZooMenuMuligheder, z => z, $"Velkommen i {zoos[ValgtZoo].Name}");
                int.TryParse(Console.ReadLine(), out input);

                
                if (input > 0 && input <= ValgtZooMenuMuligheder.Count)
                {
                    Console.Clear();
                    Messages.ShowZooMap(enclosures, zoos[ValgtZoo].Name);     
                    Console.ReadKey();
                    // = input - 1;
                    
                }
                else if (input == 999)
                {
                    Messages.InputSvar(ValgtZooMenuMuligheder ,"Tilbage til valg af Zoo");
                    zooMenu = false;
                    Console.ReadKey();
                }
                else
                {
                    Messages.InputSvar(ValgtZooMenuMuligheder, "Input ikke forstået prøv igen");
                    Console.ReadKey();
                }
            }
        }

    }
}
