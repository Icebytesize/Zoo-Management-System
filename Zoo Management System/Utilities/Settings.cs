using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo_Management_System;

namespace Zoo_Management_System.Utilities
{
    internal class Settings
    {
        //static string ProjectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\")); //Til at komme ud til projekt mappe (til senere brug)

        public static string GetSoundPath(string fileName)
        {
            return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "AnimalStuff", fileName);
        }
        public static string GetFilePath(string fileName)
        {
            //return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ZooData", fileName);
            return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "ZooData", fileName);
        }


    }
}
