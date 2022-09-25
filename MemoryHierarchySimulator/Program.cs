using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHierarchySimulator
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            OpenFile openfile = new OpenFile();
            openfile.Open();

            Console.ReadLine();

        }
    }
}
