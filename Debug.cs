using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_of_Hate
{
    public static class Debug
    {
        public static void Write(string message, bool clean = false)
        {
            if (clean)
                Clean();

            Console.WriteLine($"{message}");
        }

        public static void Clean()
        {
            Console.Clear();
        }
    }
}
