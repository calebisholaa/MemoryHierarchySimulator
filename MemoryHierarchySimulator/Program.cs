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
            //OpenTraceFile openTraceFile = new OpenTraceFile();
            Cache  cache = new Cache(openfile);
            //DTLB dTLB = new DTLB(2, 1);
            openfile.ReadFile();

            Console.WriteLine();
            //Console.WriteLine("Open Trace File...press Enter to select file.");
            //Console.ReadKey();
            //openTraceFile.OpenTrace();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("What");
            cache.CreateCaches();
            PageTable pages = new PageTable(openfile.VirtualPages, openfile.PhysicalPages, openfile.PageSize);
            //HierarchyTransversal hierarchyTransversal = new HierarchyTransversal(dTLB);
        }


        public static void Intro()
        {
            Console.WriteLine("Welcome to the Memory Hierarchy Simulator!");
            Console.WriteLine("Please press ENTER to continue.");
            Console.ReadKey();

        }
    }
}
