using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Services;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemoryHierarchy;
using MemoryHierarchySimulator;
using Microsoft.Win32;

namespace MemoryHierarchySimulator
{
    public class Cache
    {

        public readonly OpenConfigFile openConfigFile;

        public readonly OpenTraceFile openTraceFile; 
        private List<string> mockAddress = new List<string>();

        List<string> CacheList = new List<string>();

        private List<string> TagList { get; set; }

        private List<string> CacheTagList { get; set; }
        private List<string> IndexList { get; set; }
        private List<string> CacheIndexList { get; set; }

        private List<string> OffSetList { get; set; }
        private List<string> CaccheOffSetList { get; set; }

        private List<List<int[]>> Set { get; set; }

        private List<string>  IndexTable { get; set; }
        private List<string>  TagTable { get; set; }
        private List<string>  OffSetTable { get; set; }

        private List<string> ResultTable { get; set; }




        private double AddressSize { get; set; }
        private double Index { get; set; }
        private double OffSet { get; set; }
        private double SetSize { get; set; }

        

        List<List<int[]>> sets = new List<List<int[]>>();   
        List<Stack<string>> memoryAddress = new List<Stack<string>>();
        List<Stack<string>> CacheStackList = new List<Stack<string>>();    





        public Cache(OpenConfigFile openConfigFile)
        {
            this.openConfigFile = openConfigFile;
            IndexList = new List<string>();
            TagList = new List<string>();
            OffSetList = new List<string>();

            IndexTable = new List<string>();
            OffSetTable = new List<string>();
            TagTable = new List<string>();
            ResultTable = new List<string>();

            
        }       

       

        public string CreateCache(string hexAddress)
        {
            var result = "";
            Console.WriteLine("Creating Caches");
            var numberOfSet = openConfigFile.NumberofSets;
            var setSize = openConfigFile.SetSize;
            var lineSize = openConfigFile.LineSize;
            var addressSize = GetAddressSize(openConfigFile.PhysicalPages, openConfigFile.PageSize);
            var offSet = openConfigFile.OffSetBits;
            var index = openConfigFile.IndexBits;
            var tag = addressSize - (offSet + index);
            var hexAddressCopy = hexAddress;


            AddressSize = addressSize;
            Index = index;
            OffSet = offSet;
            SetSize = setSize;

            CacheStackList.Add(MemoryAddress(addressSize, offSet, index, tag, hexAddress));



            if(IndexList.Count==1)
            {
                result = "Miss";
            }
            
            if(IndexList.Count>1)
            {
                for (int i = 0; i < IndexList.Count; i++)
                {
                   
                    for (int j = 1; j < IndexList.Count+1; j++)
                    {
                       if(TagList[i] == TagList[j])
                       {
                          result = "Hit";
                          break ;
                       }
                       else
                       {
                          result = "Miss";
                          break;
                        }
                     }   
                    break;
                }
            }


           return result;   

        }


        /// <summary>
        /// Ignore Below
        /// </summary>
        /// <returns></returns>

        //public string getPhysicalAddress(string address)
        //{
        //    string bitAddress = "00" + Convert.ToString(Convert.ToInt64(address, 16), 2);//convert the address to individual bits, aditional 0's to make bits to 14 bit length needed
        //    string locAddress = bitAddress.Remove((int)OffSet);//remove all but the bits that identify where in the page table the physical address is.
        //    Console.WriteLine(Convert.ToString(Convert.ToInt64(bitAddress, 2), 16));
        //    int index = Convert.ToInt32(locAddress, 2);
        //   // string phyAddress = addressTable[index]; // get from trace file 
        //    string offAddress = bitAddress.Substring(0, (int)OffSet);//zero out parts of the string that represent the virtual address
        //    return Convert.ToString(Convert.ToInt64((phyAddress + offAddress), 2), 16);//combines the phyical address and the offset into one hexedecimal address
        //}

        public List<string> GenerateAddress()
        {
            List<string> addresses = new List<string>();
            addresses.Add("c84");
            addresses.Add("81c");
            addresses.Add("14c");
            addresses.Add("c84");
            addresses.Add("400");
            addresses.Add("148");
            addresses.Add("144");
            addresses.Add("c80");
            addresses.Add("c80");
            addresses.Add("008");
            addresses.Add(" ");


            return addresses;
        }

        public void CreateCaches()
        {
            //Console.WriteLine("Creating Caches");
            var numberOfSet = openConfigFile.NumberofSets;
            
            var setSize = openConfigFile.SetSize;   
            var lineSize = openConfigFile.LineSize;
            var addressSize = GetAddressSize(openConfigFile.PhysicalPages, openConfigFile.PageSize);        
            var offSet = openConfigFile.OffSetBits;
            var index = openConfigFile.IndexBits;
            var tag = addressSize - (offSet + index);
            var address = GenerateAddress();


           
                while (numberOfSet > 0)
                {
                        for (int i = 0; i < address.Count; i++)
                        {
                            memoryAddress.Add(MemoryAddress(addressSize, offSet, index, tag, address[i]));
                            sets.Add(CreateSets(CreateBlock(lineSize), setSize));
                            numberOfSet--;
                        }
                }


            Set = sets;
            AddressSize = addressSize;
            Index = index;
            OffSet = offSet;
            SetSize = setSize; 
            

        }
        public void DataCache(List<string> index, List<string> offSet, List<string> tag, int numberOfSet)
        {
            List<string> indexTable = new List<string>();
            List<string> offSetTable = new List<string>();
            List<string> tagTable = new List<string>();
            List<string> resultTable = new List<string>();


            for (int i = 0; i < memoryAddress.Count; i++)
            {
                if(IndexList[i] == index[i] && TagList[i] == tag[i])
                {
                    indexTable.Add(index[i]);                  
                    resultTable.Add("Hit");
                    tagTable.Add(tag[i]);
                    offSetTable.Add(offSet[i]);      
                }
                else if (IndexList[i] != index[i])
                {
                    resultTable.Add("Miss");
                    tagTable.Add(tag[i]);
                    indexTable.Add(index[i]);
                    offSetTable.Add(offSet[i]); 
                }
            }
            ResultTable = resultTable;
            IndexTable = indexTable;
            OffSetTable = offSetTable;
            TagTable = tagTable;            
        }



        /// <summary>
        /// Set Up the memory Address for more visual understanding 
        /// </summary>
        /// <param name="addressSize"></param>
        /// <param name="offSet"></param>
        /// <param name="index"></param>
        /// <param name="tag"></param>
        /// <returns>Stack<string></string></returns>
        public Stack<string> MemoryAddress(double addressSize, double offSet, double index, double tag, string HexAddress)
        {

            Stack<string> stack = new Stack<string>((int)addressSize);

            List<string> addresses = new List<string>();
            List<char> listChar = new List<char>();
            Stack<string> invertedTag = new Stack<string>();
            Stack<string> invertedIndex = new Stack<string>();
            Stack<string> invertedOffset = new Stack<string>();
            var address = HexToBinary(HexAddress);
            char[] charArray = address.ToCharArray();


            int flag = 0;

            var tempOffset = offSet;
            var tempIndex = index;
            var tempTag = tag;
            var remain = charArray.Length - (offSet + index);
            var solution = remain / tag;
            var result = Math.Round(solution, 0, MidpointRounding.AwayFromZero);
            var tempResult = result - 1;

            var increment = result - solution;
            increment = Math.Round(increment, 0, MidpointRounding.AwayFromZero);

            var tempCharArryLength = charArray.Length;

            for (int i = 0; i < charArray.Length; i++)
            {
                while (tempTag != 0)
                {

                    if (charArray.Length - (offSet + index) % tag == 0)
                    {


                        while (tempResult != 0)
                        {
                            listChar.Add(charArray[flag]);
                            flag++;
                            tempResult--;
                        }
                        char[] newArryChar = listChar.ToArray();
                        string str = new string(newArryChar);
                        addresses.Add(str);
                        listChar.Clear();

                        for (int j = 0; j < addresses.Count; j++)
                        {
                            stack.Push(addresses[j]);
                            invertedTag.Push(addresses[j]);
                        }
                        addresses.Clear();

                    }
                    else if (charArray.Length - (offSet + index) % tag != 0)
                    {

                        tempResult = result;
                        if (increment != 0)
                        {
                            stack.Push("xx");
                            invertedTag.Push("xx");
                            increment--;
                            tempTag--;

                        }
                        while (tempResult != 0)
                        {
                            listChar.Add(charArray[flag]);
                            flag++;
                            tempResult--;
                        }

                        char[] newArryChar = listChar.ToArray();
                        string str = new string(newArryChar);
                        addresses.Add(str);
                        listChar.Clear();

                        for (int j = 0; j < addresses.Count; j++)
                        {
                            stack.Push(addresses[j]);
                            invertedTag.Push(addresses[j]);
                        }
                        addresses.Clear();
                    }
                    tempTag--;
                }
                if (tempTag == 0)
                {
                    while (tempIndex != 0)
                    {
                        stack.Push(Char.ToString(charArray[flag]));
                        invertedIndex.Push(Char.ToString(charArray[flag]));
                        flag++;
                        tempIndex--;
                    }
                }
                if (tempIndex == 0)
                {
                    while (tempOffset != 0)
                    {
                        stack.Push(Char.ToString(charArray[flag]));
                        invertedOffset.Push(Char.ToString(charArray[flag]));
                     
                        flag++;
                        tempOffset--;
                    }
                }

                var tagConvert = ConcatStack(ReverseStack(invertedTag));
                var offSetConvert = ConcatStack(ReverseStack(invertedOffset));
                var indexConvert = ConcatStack(ReverseStack(invertedIndex));
                IndexList.Add(indexConvert);
                OffSetList.Add(offSetConvert);
                TagList.Add(tagConvert);

                IndexList.RemoveAll(s => s == "");
                OffSetList.RemoveAll(s => s == "");
                TagList.RemoveAll(s => s == "");

            }
           
            //IndexStack = ReverseStack(invertedIndex) ;
            //OffSetStack = ReverseStack(invertedOffset);
            //TagStack = ReverseStack(invertedTag);

            Stack<string> reverseStack = ReverseStack(stack);

            return reverseStack;

        }

        private string ConcatStack(Stack<string> stack)
        {
            //List<char> result = new List<char>(); 
            List<string> temp = new List<string>(); 
            while(stack.Count !=0)
            {
                temp.Add(stack.Pop());  
              //  result.Add(char.Parse(stack.Pop()));
            }

            //char[] newArryChar = result.ToArray();
            //string str = new string(newArryChar);
            StringBuilder builder = new StringBuilder();

            foreach (var item in temp)
            {
                builder.Append(item);
            }

           string str = builder.ToString();
            
            return str;
        }
      

        private Stack<string> ReverseStack(Stack<string>  stack)
        {
            Stack<string> reverseStack = new Stack<string>();
            while (stack.Count !=0)
            {
                reverseStack.Push(stack.Pop());
            }

            return reverseStack;
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
             string[][] table = null;
            string result = "";

            for (int i = 0; i < memoryAddress.Count; i++)
            {
                Console.WriteLine("Set " + count);
                PrintAddressStack(memoryAddress[i]);
                count++;
            }

            Console.WriteLine("Tag       " + "Index  " + "Offset  " + "Result");
            for(int j=0; j< memoryAddress.Count; j++)
            {
                DataCache(IndexList, OffSetList, TagList, sets.Count);
                Console.WriteLine(TagTable[j] + "  " + IndexTable[j] + "     " + OffSetTable[j] + "   " + ResultTable[j]);
            }
            Console.WriteLine();

           

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

        public string HexToBinary(string address)
        {
            string binarystring = String.Join(String.Empty,
              address.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            );

            return binarystring;
        }


        public void PrintBinaryAddress()
        {
            Console.WriteLine("Binary Address");
            foreach(var item in GenerateAddress())
            {
                Console.WriteLine(HexToBinary(item));
            }
        }
       
    }
}
