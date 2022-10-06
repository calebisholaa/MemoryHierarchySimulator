using MemoryHierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHierarchySimulator
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Intro();

            OpenFile openfile = new OpenFile();
            OpenTraceFile openTraceFile = new OpenTraceFile();

            openfile.ReadFile();

            Console.WriteLine();
            Console.WriteLine("Open Trace File...press Enter to select file.");
            Console.ReadKey();
            openTraceFile.OpenTrace();

            Console.ReadLine();

        }


        public static void Intro()
        {
            Console.WriteLine("Welcome to the Memory Hierarchy Simulator!");
            Console.WriteLine("Please press ENTER to continue.");
            Console.ReadKey();

        }
    }
}
