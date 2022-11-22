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
            OpenTraceFile openTraceFile = new OpenTraceFile();
            
            openfile.ReadFile();

            Console.WriteLine();
            openTraceFile.ReadFile();
            Console.WriteLine();
            Console.WriteLine();

            //cache.CreateCaches();
            PageTable pages = new PageTable(openfile.VirtualPages, openfile.PhysicalPages, openfile.PageSize);

            HierarchyTransversal hierarchyTransversal = new HierarchyTransversal(openfile, openTraceFile.hexAddress, openTraceFile.accessType);
        }


        public static void Intro()
        {
            Console.WriteLine("Welcome to the Memory Hierarchy Simulator!");
            Console.WriteLine("Please press ENTER to continue.");
            Console.ReadKey();

        }
    }
}
