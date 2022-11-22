//using MemoryHierarchy
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MemoryHierarchySimulator
{
    class HierarchyTransversal
    {
        /// <summary>
        /// Local Variables for table
        /// </summary>
        private string virtAddress = "";
        private string virtPageNumber = "";
        private string pageOffset = "";
        private string tLBTag = "";
        private string tLBInd = "";
        private string tLBResult = "";
        private string pTResult = "";
        private string physicalPageNumber = "";
        private string dataCacheTag = "";
        private string dataCacheInd = "";
        private string dataCacheResult = "";
        private string l2Tag = "";
        private string l2Ind = "";
        private string l2Result = "";
        private string physicalAddress = "";

        /// <summary>
        /// List of informational variables
        /// </summary>
        private int dTLBHits = 0;
        private int dTLBMisses = 0;
        private int pTHits = 0;
        private int pTFaults = 0;
        private int dCHits = 0;
        private int dCMisses = 0;
        private int l2Hits = 0;
        private int l2Misses = 0;
        private int totalReads = 0;
        private int totalWrites = 0;
        private int mainMemoryRefs = 0;
        private int pageTableRefs = 0;
        private int diskRefs = 0;

        private OpenConfigFile openFile;
        private DTLB dTLB;
        private Cache dataCache;
        private Cache l2Cache;
        private OpenConfigFile ofconfig;

        /// <summary>
        /// Simulates the Memory Hierarchy Transversal
        /// </summary>
        public HierarchyTransversal(OpenConfigFile openfile, List<string> hexAddress, List<string> accessType)
        {


            this.openFile = openfile;
            this.dTLB = new DTLB(openfile.DTLBSets, openfile.DTLBEntries);
            dataCache = new Cache(openfile.DCIndexBits, openfile.DCOffSetBits, openfile.DCNumOfSets, openfile.DCNumOfEntries);
            l2Cache = new Cache(openfile.L2IndexBits, openfile.L2OffsetBits, openfile.L2NumOfSets, openfile.L2NumOfEntries);

            foreach (string line in accessType)
            {
                if (line.Equals("R"))
                {
                    totalReads++;
                }
                else
                {
                    totalWrites++;
                }
            }

            PrintVirtualAddressesTable();

            if(!ofconfig.VATorF)
            {
                foreach (string addr in hexAddress)
                {
                    tLBTag = "";
                    tLBInd = "";
                    tLBResult = "";
                    pTResult = "";
                    physicalPageNumber = "";
                    dataCacheTag = "";
                    dataCacheInd = "";
                    dataCacheResult = "";
                    l2Tag = "";
                    l2Ind = "";
                    l2Result = "";
                    physicalAddress = "";

                    physicalPageNumber = DecimalToHex(PageTable.PhysicalPageNumber);
                    physicalAddress = physicalPageNumber + pageOffset.PadLeft(openFile.PageOffSetBits / 4, '0');
                    CheckdC();

                }
            }

            if(ofconfig.TLBTorF)
            {
                foreach (string addr in hexAddress)
                {
                    tLBTag = "";
                    tLBInd = "";
                    tLBResult = "";
                    pTResult = "";
                    physicalPageNumber = "";
                    dataCacheTag = "";
                    dataCacheInd = "";
                    dataCacheResult = "";
                    l2Tag = "";
                    l2Ind = "";
                    l2Result = "";
                    physicalAddress = "";

                    virtAddress = addr.PadLeft(8, '0');
                    virtPageNumber = GetPageNum(virtAddress, openFile.PageOffSetBits, openFile.PageIndexBits);
                    pageOffset = GetPageOff(virtAddress, openFile.PageOffSetBits, openFile.PageIndexBits);
                    CheckTLB();
                    PrintVirtualAddressesLine();

                }
            }
            else
            {
                foreach (string addr in hexAddress)
                {
                    tLBTag = "";
                    tLBInd = "";
                    tLBResult = "";
                    pTResult = "";
                    physicalPageNumber = "";
                    dataCacheTag = "";
                    dataCacheInd = "";
                    dataCacheResult = "";
                    l2Tag = "";
                    l2Ind = "";
                    l2Result = "";
                    physicalAddress = "";

                    virtAddress = addr.PadLeft(8, '0');
                    virtPageNumber = GetPageNum(virtAddress, openFile.PageOffSetBits, openFile.PageIndexBits);
                    pageOffset = GetPageOff(virtAddress, openFile.PageOffSetBits, openFile.PageIndexBits);
                    CheckpT();
                    PrintVirtualAddressesLine();

                }
            }

            PrintStats();
        }

        /// <summary>
        /// Prints the header for the Virtual Addresses Table
        /// </summary>
        private void PrintVirtualAddressesTable()
        {
            Console.WriteLine("{0,-8} {1,-6} {2,-4} {3,-6} {4,-3} {5,-4} {6,-4} {7,-4} {8,-6} {9,-3} {10,-6} {11,-4} {12,-3} {13,-4}", "Virtual", "Virt.", "Page", "TLB", "TLB", "TLB",
                "pT", "Phys", "", "dC", "dC", "", "L2", "L2");
            Console.WriteLine("{0,-8} {1,-6} {2,-4} {3,-6} {4,-3} {5,-4} {6,-4} {7,-4} {8,-6} {9,-3} {10,-4} {11,-4} {12,-2} {13,-4}", "Address", "Page #", "Off", "Tag", "Ind", "Res.",
                "Res.", "Pg #", "dC Tag", "Ind", "Res.", "L2 Tag", "Ind", "Res.");
            Console.WriteLine("-------- ------ ---- ------ --- ---- ---- ---- ------ --- ---- ------ --- ----");
        }

        /// <summary>
        /// Prints an individual line of the Virtual Addresses Table
        /// </summary>
        private void PrintVirtualAddressesLine()
        {
            Console.WriteLine("{0,-13} {1,-3} {2,-4} {3,-6} {4,-1} {5,-4} {6,-4} {7,-4} {8,-6} {9,-3} {10,-6} {11,-4} {12,-3} {13,-4}", virtAddress, virtPageNumber, pageOffset, tLBTag, tLBInd, tLBResult, pTResult,
                physicalPageNumber, dataCacheTag, dataCacheInd, dataCacheResult, l2Tag, l2Ind, l2Result);
        }

        /// <summary>
        /// Checks the TLB and gets the physical address if
        /// it exists in the TLB
        /// </summary>
        private void CheckTLB()
        {
            string tLBSearchResults = dTLB.SearchTLB(virtPageNumber);

            if (!tLBSearchResults.Equals("empty"))
            {
                physicalPageNumber = tLBSearchResults;
                physicalAddress = physicalPageNumber + pageOffset.PadLeft(openFile.PageOffSetBits / 4, '0');
                tLBResult = "hit";
                dTLBHits++;
                CheckdC();
            }
            else
            {
                tLBResult = "miss";
                dTLBMisses++;
                CheckpT();
                dTLB.SetPPN(virtPageNumber, DecimalToHex(PageTable.PhysicalPageNumber));
            }

            if (dTLB.OddOrEven(virtPageNumber))
            {
                tLBInd = "0";
            }
            else
            {
                tLBInd = "1";
            }

            tLBTag = HexToDecimal(virtPageNumber) / 2 + "";

        }

        /// <summary>
        /// Checks the Page Table and gets a physical address if
        /// it is in the page table.
        /// </summary>
        private void CheckpT()
        {

            pageTableRefs++;
            if (!PageTable.CheckForPhysicalPageNumber(HexToDecimal(virtPageNumber)).Equals("empty"))
            {
                pTResult = "hit";
                pTHits++;
            }
            else
            {
                pTResult = "miss";
                pTFaults++;

                if (PageTable.PageRemoved)
                {

                }
            }
            physicalPageNumber = DecimalToHex(PageTable.PhysicalPageNumber);
            physicalAddress = physicalPageNumber + pageOffset.PadLeft(openFile.PageOffSetBits / 4, '0');
            CheckdC();
        }

        /// <summary>
        /// Checks the L1 Data Cache with the physical address for the data requested.
        /// </summary>
        private void CheckdC()
        {
            if (dataCache.SearchCache(physicalAddress))
            {
                dataCacheResult = "hit";
                dCHits++;
            }
            else
            {
                dataCacheResult = "miss";
                dCMisses++;
                if(ofconfig.L2TorF)
                {
                    CheckL2();
                }
            }
            dataCacheInd = BinaryToHex(dataCache.index);
            dataCacheTag = BinaryToHex(dataCache.tag);
        }

        /// <summary>
        /// Checks the L2 Cache with the physical address for the data requested.
        /// </summary>
        private void CheckL2()
        {
            if (l2Cache.SearchCache(physicalAddress))
            {
                l2Result = "hit";
                l2Hits++;
            }
            else
            {
                l2Result = "miss";
                l2Misses++;
                CheckMemoryAndDisk();
            }
            l2Ind = BinaryToHex(l2Cache.index);
            l2Tag = BinaryToHex(l2Cache.tag);
        }

        /// <summary>
        /// Checks the main memory and disk
        /// </summary>
        private void CheckMemoryAndDisk()
        {
            mainMemoryRefs++;
            diskRefs++;
        }

        /// <summary>
        /// Converts binary to hex
        /// </summary>
        /// <param name="binary"></param>
        /// <returns></returns>
        public string BinaryToHex(string binary)
        {
            string hex = Convert.ToString(Convert.ToInt32(binary, 2), 16);
            return hex;
        }

        /// <summary>
        /// Converts hex to binary
        /// </summary>
        /// <param name="hex"></param>
        /// <returns>binary</returns>
        public string HexToBinary(string hex)
        {
            string binary = Convert.ToString(Convert.ToInt32(hex, 16), 2).PadLeft(32, '0');
            return binary;
        }

        /// <summary>
        /// Converts hex to decimal
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public int HexToDecimal(string hex)
        {
            int deci = Convert.ToInt32(hex, 16);
            return deci;
        }

        public string DecimalToHex(int num)
        {
            return num.ToString("X");
        }

        /// <summary>
        /// Calculates the page number
        /// </summary>
        /// <param name="str"></param>
        /// <param name="offset"></param>
        /// <param name="index"></param>
        /// <returns>page number</returns>
        private string GetPageNum(string str, double offset, double index)
        {
            int length = 32 - (int)offset - (int)index;
            str = HexToBinary(str);
            str = str.Substring(length, (int)index);
            str = BinaryToHex(str);
            return str;
        }

        /// <summary>
        /// Calculates the page offset
        /// </summary>
        /// <param name="str"></param>
        /// <param name="offset"></param>
        /// <param name="index"></param>
        /// <returns>page offset</returns>
        private string GetPageOff(string str, double offset, double index)
        {
            int length = 32 - (int)offset - (int)index;
            str = HexToBinary(str);
            str = str.Substring(length + (int)index);
            str = BinaryToHex(str);
            return str;
        }

        /// <summary>
        /// Prints the stats at the end.
        /// </summary>
        private void PrintStats()
        {
            double dTLBRatio = (double)dTLBHits / (dTLBMisses + dTLBHits);
            double pTRatio = (double)pTHits / (pTFaults + pTHits);
            double dCRatio = (double)dCHits / (dCMisses + dCHits);
            double l2Ratio = (double)l2Hits / (l2Misses + l2Hits);
            double ReadWriteRatio = (double)totalReads / (totalWrites + totalReads);

            Console.WriteLine("\nSimulation statistics" +
                "\ndTLB hits: " + dTLBHits +
                "\ndTLB misses: " + dTLBMisses);

            Console.WriteLine("dTLB hit ratio: " + dTLBRatio.ToString("P"));

            Console.WriteLine("pT hits: " + pTHits +
                "\npT faults: " + pTFaults);

            Console.WriteLine("pT hit ratio: " + pTRatio.ToString("P"));

            Console.WriteLine("dC hits: " + dCHits +
                "\ndC misses: " + dCMisses);

            Console.WriteLine("dC hit ratio: " + dCRatio.ToString("P"));

            Console.WriteLine("L2 hits: " + l2Hits +
                "\nL2 misses: " + l2Misses);

            Console.WriteLine("L2 hit ratio: " + l2Ratio.ToString("P"));

            Console.WriteLine("Total reads: " + totalReads +
                "\nTotal writes: " + totalWrites);

            Console.WriteLine("Ratio of reads: " + ReadWriteRatio.ToString("P"));


            Console.WriteLine("main memory refs: " + mainMemoryRefs +
                "\npage table refs: " + pageTableRefs +
                "\ndisk refs: " + diskRefs);
        }


    }

}