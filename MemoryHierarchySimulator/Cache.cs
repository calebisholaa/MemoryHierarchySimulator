using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MemoryHierarchy;
using MemoryHierarchySimulator;
using Microsoft.Win32;

namespace MemoryHierarchySimulator
{
    public class Cache
    {

        public  readonly OpenConfigFile openConfigFile;

        public List<int[]> sets = new List<int[]>();

        //public OpenConfigFile OpenConfigFile => openConfigFile;

        public Cache(OpenConfigFile openConfigFile)
        {
            this.openConfigFile = openConfigFile;
        }       

       

        public void print()
        {
            Console.WriteLine(openConfigFile.SetSize);
            Console.WriteLine(openConfigFile.NumberofSets);
        }
  

        public void CreateCaches()
        {
            
            var numberOfSet = openConfigFile.NumberofSets;
            var setSize = openConfigFile.SetSize;
            
            while(numberOfSet > 0)
            {
                sets.Add(MakeArray(setSize));
                numberOfSet--;
            }

            Console.WriteLine("Number of Set -> " + sets.Count);
            foreach (var item in sets)
            {
                Console.WriteLine(item.ToString());
            }

        }

        public int[] MakeArray(int size)
        {
            int[] newArray = new int[size];

            return newArray;
        }


       
    }
}
