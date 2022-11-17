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

            OpenConfigFile openfile = new OpenConfigFile();
            //  OpenTraceFile openTraceFile = new OpenTraceFile();
            Cache  cache = new Cache(openfile);

            openfile.ReadFile();

            Console.WriteLine();
            Console.WriteLine("Open Trace File...press Enter to select file.");
            Console.ReadKey();
           // openTraceFile.OpenTrace();
            Console.WriteLine();
            Console.WriteLine();

            cache.CreateCaches();
            Console.ReadLine();
            PageTable pages = new PageTable(openfile.VirtualPages, openfile.PhysicalPages, openfile.PageSize);
            HierarchyTransversal hierarchyTransversal = new HierarchyTransversal();
        }


        public static void Intro()
        {
            Console.WriteLine("Welcome to the Memory Hierarchy Simulator!");
            Console.WriteLine("Please press ENTER to continue.");
            Console.ReadKey();

        }
    }
}
