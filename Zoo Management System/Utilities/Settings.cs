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

        public static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string GetSoundPath(string fileName)
        {
            return Path.Combine(BasePath, fileName);
        }
    }
}
