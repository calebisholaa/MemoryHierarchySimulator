using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHierarchy
{
    public class Driver
    {
        [STAThread]
        static void Main(string[] args)
        {
            OpenFile openFile = new OpenFile();
            openFile.ReadFile();
        }

        public static void Intro()
        {
            Console.WriteLine("Welcome to the Memory Hierarchy Simulator!");
            Console.WriteLine("Please press ENTER to continue.");
            Console.ReadKey();

        }
    }
}