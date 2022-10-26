using System;
using System.Collections.Generic;
using System.Data;
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

        public List<List<int[]>> sets = new List<List<int[]>>();   //i don't know which might be easier, int[] of string 

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
            Console.WriteLine("Creating Caches");
            var numberOfSet = openConfigFile.NumberofSets;
            var setSize = openConfigFile.SetSize;   //since i am using lists this might be redundant 
            var lineSize = openConfigFile.LineSize;
            var offSet = openConfigFile.OffSetBits;
            var addressSize = GetAddressSize(openConfigFile.PhysicalPages, openConfigFile.PageSize);
           
            
            while(numberOfSet > 0)
            {
                sets.Add(CreateSets(CreateBlock(lineSize), setSize));
                
                numberOfSet--;
            }

            Console.WriteLine("Number of Set: " + sets.Count);
            Console.WriteLine("");
            Console.WriteLine("Adress: " + addressSize);
            

        }

        public int[] CreateBlock(int lineSize)
        {
            int[] newArray = new int[lineSize];
            return newArray;
        }

        public List<int[]> CreateSets(int[] block, int setSize)
        {
            List<int[]> newList = new List<int[]>(setSize);
            newList.Add(block);
            return newList;
        }

        /// <summary>
        /// Returns the Address Size. This is derived by multiplying the physical page by the page size
        /// </summary>
        /// <param name="physicalPages"></param>
        /// <param name="pageSize"></param>
        /// <returns>addres size in bytes</returns>
        public int GetAddressSize(int physicalPages,int pageSize)
        {
            return physicalPages * pageSize;   
        }
       
    }
}
