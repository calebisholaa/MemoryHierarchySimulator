using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHierarchySimulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Intro();
            OpenConfigFile openfile = new OpenConfigFile();
            //OpenTraceFile openTraceFile = new OpenTraceFile();
            
            openfile.ReadFile();

            Console.WriteLine();
            Console.WriteLine("Open Trace File...press Enter to select file.");
            Console.ReadKey();
            //openTraceFile.OpenTrace();
            Console.WriteLine();
            Console.WriteLine();

            //cache.CreateCaches();
            PageTable pages = new PageTable(openfile.VirtualPages, openfile.PhysicalPages, openfile.PageSize);
            List<string> hexAddress = new List<string>(new string[] { "c84", "81c", "14c", "c84", "400", "148", "144", "c80", "008" });
            
            HierarchyTransversal hierarchyTransversal = new HierarchyTransversal(openfile, hexAddress);
        }


        public static void Intro()
        {
            Console.WriteLine("Welcome to the Memory Hierarchy Simulator!");
            Console.WriteLine("Please press ENTER to continue.");
            Console.ReadKey();

        }
    }
}
