using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemoryHierarchy;
using MemoryHierarchySimulator;
using Microsoft.Win32;

namespace MemoryHierarchySimulator
{
    public class Cache
    {

        public  readonly OpenConfigFile openConfigFile;

        private double AddressSize { get; set; }
        private double Index { get; set; }
        private double OffSet { get; set; }

        private double SetSize { get; set; }

        List<List<int[]>> sets = new List<List<int[]>>();   
        List<Stack<string>> memoryAddress = new List<Stack<string>>();


        public Cache(OpenConfigFile openConfigFile)
        {
            this.openConfigFile = openConfigFile;
        }       

        public void Caching()
        {
            CreateCaches();
            Print();

        }


        public void CreateCaches()
        {
            Console.WriteLine("Creating Caches");
            var numberOfSet = openConfigFile.NumberofSets;
            
            var setSize = openConfigFile.SetSize;   
            var lineSize = openConfigFile.LineSize;
            var addressSize = GetAddressSize(openConfigFile.PhysicalPages, openConfigFile.PageSize);        
            var offSet = openConfigFile.OffSetBits;
            var index = openConfigFile.IndexBits;
            var tag = addressSize - (offSet + index);

            while (numberOfSet > 0)
            {
                sets.Add(CreateSets(CreateBlock(lineSize), setSize));
                memoryAddress.Add(MemoryAddress(addressSize,offSet,index,tag));
                
                numberOfSet--;
            }    

            AddressSize = addressSize;
            Index = index;
            OffSet = offSet;
            SetSize = setSize; 
            

        }
        /// <summary>
        /// Set Up the memory Address for more visual understanding 
        /// </summary>
        /// <param name="addressSize"></param>
        /// <param name="offSet"></param>
        /// <param name="index"></param>
        /// <param name="tag"></param>
        /// <returns>Stack<string></string></returns>
        public Stack<string> MemoryAddress(double addressSize, double offSet, double index, double tag)
        {
            //    var addressSize = GetAddressSize(openConfigFile.PhysicalPages, openConfigFile.PageSize);
            //    var offSet = openConfigFile.OffSetBits;
            //    var index = openConfigFile.IndexBits;
            //    var tag = addressSize - (offSet + index);
            Stack<string> stack = new Stack<string>((int)addressSize);

            for (double k = 0; k < offSet; k++)
            {
                stack.Push("O");
            }

            for (double j = 0; j < index; j++)
            {

                stack.Push("I");
            }

            for (double i = 0; i < tag; i++)
            {
                stack.Push("T");
            }

          
            return stack;

        }

        public void Print()
        {
            Console.WriteLine("Number of Set: " + sets.Count);
            Console.WriteLine("Set Size: " + SetSize);

            Console.WriteLine("");
            Console.WriteLine("Address: " + AddressSize + "bits");
            Console.WriteLine();
            Console.WriteLine("OffSet: " + OffSet);
            Console.WriteLine("Index: " + Index);
            Console.WriteLine();

            int count = 1;

            for (int i = 0; i < memoryAddress.Count; i++)
            {
                Console.WriteLine("Set " + count);
                PrintAddressStack(memoryAddress[i]);
                count++;
            }
        }

        /// <summary>
        /// Private print stack method 
        /// </summary>
        /// <param name="stack"></param>
        private void PrintAddressStack(Stack<string> stack)
        {
            var setSize = SetSize;
            Console.WriteLine("----------------------------------------");
            for(int i=0; i < setSize; i++)
            {
                foreach (var item in stack)
                {
                    Console.Write(item + " | ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n----------------------------------------");
        }


        /// <summary>
        /// Creates Block /Lines giving the line/block size
        /// </summary>
        /// <param name="lineSize"></param>
        /// <returns></returns>
        public int[] CreateBlock(int lineSize)
        {
            int[] newArray = new int[lineSize];
            return newArray;
        }

        /// <summary>
        /// create sets given the block and the setsize
        /// </summary>
        /// <param name="block"></param>
        /// <param name="setSize"></param>
        /// <returns></returns>
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
        /// <returns>address size in bits</returns>
        public double GetAddressSize(int physicalPages,int pageSize)
        {
            int address = physicalPages * pageSize;   //returns byte

            var result = Math.Log(address, 2);

            return result;
        }
       
    }
}
