using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        internal string _soundPath;

        public string Name { get; set; }
        public string Species { get; set; }
        public DateTime BirthDate { get; set; }
        public string Food { get; set; } //Hvad dyret spiser
        public SleepState SleepState { get; set; }

        private int age;

        public virtual void MakeSound()
        {

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
