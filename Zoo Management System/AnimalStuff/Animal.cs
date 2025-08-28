using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Zoo_Management_System.Utilities;
using System.IO;
using System.ComponentModel;

namespace Zoo_Management_System
{
    enum SleepState
    {
        Awake,
        GoingToSleep,
        Sleeping,
        WakingUp
    }

    internal abstract class Animal
    {

        /* Indkapsling: Data og funktionalitet samles i en klasse, dataen er beskyttet ved at styre adgangen til den.
        Eksemplet med property og private field viser, hvordan man kan beskytte data:
         
        private int myVar;

            public int MyProperty
            {
                get { return myVar; }
                set { myVar = value; }
            } 
        
        Eller med den korte version:
            public int MyVar { get; set; }
        Begge er indkapsling, fordi de styrer, hvordan feltet tilgås.
         */

        // Abstraktion: Klassen Animal er abstrak da den er skjult og ikke skal kunne tilgåes direkte.

        // Polymorfi: De klasser der arver fra Animal, overskrive metode ShowAnimal(). der er polymorfi, at metoder opføre sig forskelligt. alt udfra hvilken klasse der kalder dem
        private static int _nextId = 1;       
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public DateTime BirthDate { get; set; }
        public string Food { get; set; } //Hvad dyret spiser
        public SleepState SleepState { get; set; }
        public string SoundString { get; set; }
        

        private int age;
        internal string soundPath;
        internal SoundPlayer animalSound;

      
        protected Animal() 
        {
            
        }

        /// <summary>
        /// En metode der inituiere hvilken lyd, det pågældende dyr skal lave. er sat som en metode for at spare 6 linjer kode i hver klasse der skal arve fra Animal
        /// </summary>
        protected void InitSound()
        {
            if (!string.IsNullOrEmpty(Species))
            {
                soundPath = Settings.GetSoundPath($"{Species}.wav");
                if (File.Exists(soundPath))
                {
                    animalSound = new SoundPlayer(soundPath);
                }
                else
                {
                    Console.WriteLine($"Advarsel: Lydfil ikke fundet: {soundPath}");
                }
            }
        }

        /// <summary>
        /// En metode der afspiller dyret lyd, hvis altså dyret er våget
        /// </summary>
        public void MakeSound()
        {
            if (SleepState == SleepState.Awake)
            {
                Console.WriteLine(SoundString);
                if(File.Exists(soundPath)) animalSound.Play();
            }

            else
            {
                Console.WriteLine($"{Species}: {Name} sover og laver ingen lyd");
            }
            
        }

        public virtual void Eat()
        {
            if (SleepState == SleepState.Awake)
            {
                Console.WriteLine($"{Species}: {Name} spiser {Food}");
            }

            else
            {
                Console.WriteLine($"{Species}: {Name} sover og spiser derfor ikke");
            }
        }

        public virtual void Sleep()
        {
            switch (SleepState)
            {

                case SleepState.Awake:
                    Console.WriteLine($"{Species}: {Name} er lys vågen");
                    SleepState = SleepState.GoingToSleep;
                    break;
                case SleepState.GoingToSleep:
                    Console.WriteLine($"{Species}: {Name} er ved at falde i søvn");
                    SleepState = SleepState.Sleeping;
                    break;
                case SleepState.Sleeping:
                    Console.WriteLine($"{Species}: {Name} is sover tungt");
                    SleepState = SleepState.WakingUp;
                    break;
                case SleepState.WakingUp:
                    Console.WriteLine($"{Species}: {Name} is er ved at vågne op");
                    SleepState = SleepState.Awake;
                    break;
                default:
                    Console.WriteLine("Noget gik galt");
                    break;
            }

            
        }


        /// <summary>
        /// Udskriver information om det pågældene dyr
        /// </summary>
        public virtual void ShowAnimal()
        {
            if (Species != null) Console.WriteLine($"Art: {Species}");
            if (Name != null) Console.WriteLine($"Navn: {Name}");
            if (BirthDate != DateTime.MinValue)
            {
                Console.WriteLine($"Fødselsdato: {BirthDate:dd:MM:yyyy}");
                GetAge();
                Console.WriteLine($"Alder: {age}");
            }
            MakeSound();

        }

        public int GetAge()
        {
            DateTime Today = DateTime.Today;
            age = Today.Year - BirthDate.Year;

            if (BirthDate.Date > Today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
