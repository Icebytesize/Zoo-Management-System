using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Zoo_Management_System.Utilities;
using System.IO;

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

        public string Name { get; set; }
        public string Species { get; set; }
        public DateTime BirthDate { get; set; }
        public string Food { get; set; } //Hvad dyret spiser
        public SleepState SleepState { get; set; }
        public string SoundString { get; set; }

        private int age;
        internal string soundPath;
        internal SoundPlayer animalSound;
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
        public virtual void MakeSound()
        {
            if (SleepState == SleepState.Awake)
            {
                Console.WriteLine(SoundString);
                animalSound.Play();
            }

            
        }

        public virtual void Eat()
        {
            Console.WriteLine($"{Species}: {Name} spiser {Food}");
        }

        public virtual void Sleep()
        {
            if (SleepState == SleepState.Awake)
            { 
            Console.WriteLine($"{Species}: {Name} is wide Awake");
            SleepState = SleepState.GoingToSleep;
            }
        }

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
