using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHierarchySimulator
{
    public class OpenConfigFile
    {
        //Caleb added this
        //I need the local variables to be accessed in other classes 


        public int NumberofSets { get; set; }
        public int SetSize { get; set; }

        public int LineSize { get; set; }

        public int PageIndexBits { get; set; }

        public int PageOffSetBits { get; set; }

        public int DCIndexBits { get; set; }

        public int DCOffSetBits { get; set; }

        public int DCNumOfSets { get; set; }

        public int DCNumOfEntries { get; set; }

        public int L2IndexBits { get; set; }

        public int L2OffsetBits { get; set; }

        public int L2NumOfSets { get; set; }

        public int L2NumOfEntries { get; set; }

        public string WriteAllocate { get; set; }

        public string Word { get; set; }

        public string Temp { get; set; }


        public int PhysicalPages { get; set; }

        public int PageSize { get; set; }

        public int VirtualPages { get; set; }

        public int DTLBSets { get; set; }

        public int DTLBEntries { get; set; }

        //end of properties 


        public int arrayLength = 0;
        public int arrayMarker = 0;
        public string[] fileContents = null;
        public string[] splitContents = null;
        public string[] almostSplitContents = null;
        public string[] individualWords = null;
        public string configOutput = null;

        public OpenConfigFile()
        {
            
        }

        public void ReadFile()
        {
            string filePath = " ";
            bool keepGoing = true;
            string temp = null;
            string word = null;
            string contents = null;

            while (keepGoing)
            {
                Console.WriteLine("\nPlease enter the file path for the config file.");
                filePath = Console.ReadLine();

                try
                {
                    using (StreamReader sr = File.OpenText(filePath))
                    {
                        Console.WriteLine("File open.");
                        keepGoing = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"The file could not be opened: '{e}'");
                }
            }

            fileContents = File.ReadAllText(filePath).Split('\n');
            foreach (string line in fileContents)
            {
                contents += line;
            }

            contents = contents.Replace($"\r", " ");
            splitContents = contents.Split(' ');
            arrayLength = splitContents.Length;

            for (int i = 0; i < splitContents.Length; i++)
            {
                if (splitContents[i] == "Data" && splitContents[i + 1] == "TLB")
                {
                    SetTLB();
                }
                if (splitContents[i] == "Page" && splitContents[i + 1] == "Table")
                {
                    SetPageTable();
                }
                if (splitContents[i] == "Data" && splitContents[i + 1] == "Cache")
                {
                    arrayMarker = i;
                    SetDataCache();
                }
                if (splitContents[i] == "L2" && splitContents[i + 1] == "Cache")
                {
                    arrayMarker = i;
                    SetL2();
                }
            }

            Console.WriteLine(configOutput);
        }

        public void SetTLB()
        {
            configOutput = "\nData TLB Configuration:";
            int numberOfSets = 0;
            int setSize = 0;
            double indexBits = 0;
            string temp = null;
            string word = null;
            for (int i = 0; i < arrayLength; i++)
            {
                if (splitContents[i] == "sets:")
                {
                    word = splitContents[i + 1];
                    numberOfSets = Int32.Parse(word);
                    while (numberOfSets % 2 != 0)
                    {
                        Console.WriteLine("Your set is not a power of two. Please enter a new number that is a power of two.");
                        numberOfSets = Convert.ToInt32(Console.ReadLine());
                    }
                    configOutput += "\nData TLB contains " + numberOfSets + " sets.";
                }
                if (splitContents[i] == "size:")
                {
                    word = splitContents[i + 1];
                    setSize = Int32.Parse(word);
                    while (setSize % 2 != 0 && setSize > 64)
                    {
                        Console.WriteLine("The number of enteries is either greater than 64 or not a power of two. Please enter a new number that is a power of two and less than or equal to 64.");
                        setSize = Convert.ToInt32(Console.ReadLine());
                    }
                    configOutput += "\nEach set contains " + setSize + " entries.";
                    i = arrayLength + 1;
                }
            }

            indexBits = IndexAndOffset(numberOfSets);
            configOutput += ("\n" + $"Number of bits used for the index is {indexBits}");

            DTLBSets = numberOfSets;
            DTLBEntries = setSize;
        }
     

        public void SetPageTable()
        {
            configOutput += "\n\nPage Table Configuration:";
            int virtualPages = 0;
            int physicalPages = 0;
            int pageSize = 0;
            string word = null;
            string temp = null;
            for (int i = 0; i < arrayLength; i++)
            {
                if (splitContents[i] == "virtual" && splitContents[i + 1] == "pages:")
                {
                    word = splitContents[i + 2];
                    virtualPages = Int32.Parse(word);
                    while (virtualPages % 2 != 0 && virtualPages > 8192)
                    {
                        Console.WriteLine("The number of virutal pages is either greater than 8192 or not a power of two. " +
                            "Please enter a new number of pages that is a power of two and less than or equal to 8192.");
                        virtualPages = Convert.ToInt32(Console.ReadLine());
                    }
                    configOutput += "\nNumber of virtial pages is " + virtualPages;
                }
                if (splitContents[i] == "physical" && splitContents[i + 1] == "pages:")
                {
                    word = splitContents[i + 2];
                    physicalPages = Int32.Parse(word);
                    while (physicalPages % 2 != 0 && physicalPages > 2048)
                    {
                        Console.WriteLine("The number of pyhsical pages is either greater than 2048 or not a power of two. " +
                            "Please enter a new number of pages that is a power of two and less than or equal to 2048.");
                        physicalPages = Convert.ToInt32(Console.ReadLine());
                    }
                    configOutput += "\nNumber of physical pages is " + physicalPages;
                }
                if (splitContents[i] == "Page" && splitContents[i + 1] == "size:")
                {
                    word = splitContents[i + 2];
                    pageSize = Int32.Parse(word);
                    while (pageSize % 2 != 0 && pageSize > 4096)
                    {
                        Console.WriteLine("The page size is either greater than 4096 or not a power of two. " +
                            "Please enter a new number of pages that is a power of two and less than or equal to 2048.");
                        pageSize = Convert.ToInt32(Console.ReadLine());
                    }
                    configOutput += "\nPage size: " + pageSize;
                    i = arrayLength + 1;
                }
            }

            PageIndexBits = IndexAndOffset(virtualPages);
            configOutput += ("\n" + $"Number of bits used for the page table index is {PageIndexBits}.");
            PageOffSetBits = IndexAndOffset(pageSize);
            configOutput += ("\n" + $"Number of bits used for the page offset is {PageOffSetBits}.");


            PhysicalPages = physicalPages;
            PageSize = pageSize;
            VirtualPages = virtualPages;
        }

        public void SetDataCache()
        {
            configOutput += "\n\nData Cache Configuration:";
            int numberOfSets = 0;
            int setSize = 0;
            int lineSize = 0;
            int indexBits = 0;
            int offsetBits = 0;
            string writeAllocate = null;
            string word = null;
            string temp = null;



            for (int i = arrayMarker; i < arrayLength; i++)
            {
                if (splitContents[i] == "sets:")
                {
                    word = splitContents[i + 1];
                    numberOfSets = Int32.Parse(word);
                    while (numberOfSets % 2 != 0)
                    {
                        Console.WriteLine("The number of sets in your Data Cache is not a power of two. " +
                            "Please enter a new number of sets that is a power of two.");
                        numberOfSets = Convert.ToInt32(Console.ReadLine());

                    }
                    configOutput += "\nD-cache contains  " + numberOfSets + " sets.";
                }
                if (splitContents[i] == "Set" && splitContents[i + 1] == "size:")
                {
                    word = splitContents[i + 2];
                    setSize = Int32.Parse(word);
                    while (setSize % 2 != 0 && setSize > 128)
                    {
                        Console.WriteLine("The number entries for the Data Cache is either greater than 128 or not a power of two. " +
                            "Please enter a new number of entries that is a power of two and less than or equal to 128.");
                        setSize = Convert.ToInt32(Console.ReadLine());
                    }
                    configOutput += "\nEach set contains " + setSize + " entries.";
                }
                if (splitContents[i] == "Line" && splitContents[i + 1] == "size:")
                {
                    word = splitContents[i + 2];
                    lineSize = Int32.Parse(word);
                    while (lineSize % 2 != 0 && lineSize < 8)
                    {
                        Console.WriteLine("The line size for the Data Cache is either less than 8 or not a power of two. " +
                            "Please enter a new  that is a power of two and greater than or equal to 8.");
                        lineSize = Convert.ToInt32(Console.ReadLine());
                    }
                    configOutput += "\nEach line is " + lineSize + " bytes.";
                }
                if (splitContents[i] == "write" && splitContents[i + 1] == "allocate:")
                {
                    if (splitContents[i + 2] == "no")
                    {
                        configOutput += "\nThe cache uses a write-allocate and write-back policy.";
                    }
                    else
                    {
                        Console.WriteLine("NOT FINISHED");
                    }
                    i = arrayLength + 1;
                }
            }

            DCIndexBits = IndexAndOffset(numberOfSets);

            configOutput += ("\n" + $"Number of bits used for the index is {DCIndexBits}.");
            DCOffSetBits = IndexAndOffset(lineSize);
            configOutput += ("\n" + $"Number of bits used for the offset is {DCOffSetBits}.");

            DCNumOfSets = numberOfSets;
            DCNumOfEntries = setSize;

            NumberofSets = numberOfSets;
            SetSize = setSize;
            LineSize = lineSize;
            WriteAllocate = writeAllocate;
            Word = word;
            Temp = temp;
        }

        public void SetL2()
        {
            configOutput += "\n\nL2 Cache Configuration:";
            int numberOfSets = 0;
            int setSize = 0;
            int lineSize = 0;
            double indexBits = 0;
            double offsetBits = 0;
            string writeAllocate = null;
            string word = null;
            string temp = null;
            for (int i = arrayMarker; i < arrayLength; i++)
            {
                if (splitContents[i] == "sets:")
                {
                    word = splitContents[i + 1];
                    numberOfSets = Int32.Parse(word);
                    while (numberOfSets % 2 != 0)
                    {
                        Console.WriteLine("The number of sets in your L2 Cache is not a power of two. " +
                            "Please enter a new number of sets that is a power of two.");
                        numberOfSets = Convert.ToInt32(Console.ReadLine());
                    }
                    configOutput += "\nL2-cache contains  " + numberOfSets + " sets.";
                }
                if (splitContents[i] == "Set" && splitContents[i + 1] == "size:")
                {
                    word = splitContents[i + 2];
                    setSize = Int32.Parse(word);
                    while (setSize % 2 != 0 && setSize > 128)
                    {
                        Console.WriteLine("The number entries for the L2 is either greater than 128 or not a power of two. " +
                            "Please enter a new number of entries that is a power of two and less than or equal to 128.");
                        setSize = Convert.ToInt32(Console.ReadLine());
                    }
                    configOutput += "\nEach set contains " + setSize + " entries.";
                }
                if (splitContents[i] == "Line" && splitContents[i + 1] == "size:")
                {
                    word = splitContents[i + 2];
                    lineSize = Int32.Parse(word);
                    while (lineSize % 2 != 0 && lineSize < 8)
                    {
                        Console.WriteLine("The line size for the L2 not a power of two. " +
                            "Please enter a new that is a power of two.");
                        lineSize = Convert.ToInt32(Console.ReadLine());
                    }
                    configOutput += "\nEach line is " + lineSize + " bytes.";
                }
                if (splitContents[i] == "write" && splitContents[i + 1] == "allocate:")
                {
                    if (splitContents[i + 2] == "no")
                    {
                        configOutput += "\nThe cache uses a write-allocate and write-back policy.";
                    }
                    else
                    {
                        Console.WriteLine("NOT FINISHED");
                    }
                    i = arrayLength + 1;
                }
            }

            L2IndexBits = IndexAndOffset(numberOfSets);
            configOutput += ("\n" + $"Number of bits used for the page table index is {L2IndexBits}.");
            L2OffsetBits = IndexAndOffset(lineSize);
            configOutput += ("\n" + $"Number of bits used for the page offset is {L2OffsetBits}.");

            L2NumOfSets = numberOfSets;
            L2NumOfEntries = setSize;
        }

        public int IndexAndOffset(int i)
        {
            int temp = 0;
            temp = i;
            temp = (int)Math.Log(temp, 2);
            //temp = Math.Log2(temp);
            return temp;
        }
    }
}