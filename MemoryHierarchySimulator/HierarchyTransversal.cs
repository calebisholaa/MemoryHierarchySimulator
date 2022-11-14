//using MemoryHierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private string dataCashTag = "";
        private string dataCashInd = "";
        private string dataCashResult = "";
        private string l2Tag = "";
        private string l2Ind = "";
        private string l2Result = "";

        /// <summary>
        /// List of informational variables
        /// </summary>
        private int dtlbHits = 0;
        private int dtlbMisses = 0;
        private int ptHits = 0;
        private int ptFaults = 0;
        private int dcHits = 0;
        private int dcMisses = 0;
        private int l2Hits = 0;
        private int l2Misses = 0;
        private int totalReads = 0;
        private int totalWrites = 0;
        private int mainMemoryRefs = 0;
        private int pageTableRefs = 0;
        private int diskRefs = 0;

        public void MemoryHierarchySimulator()
        {
            PrintVirtualAddressesTable();
            List<string> hexAddress = new List<string>(new string[] { "c84", "81c", "14c", "c84", "400", "148", "144", "c80", "008" });
            foreach (string addr in hexAddress)
            {
                virtAddress = addr.PadLeft(8, '0');
                virtPageNumber = GetPageNum(virtAddress, 8, 4); //(virtAddress, openfile.OffSetBitsPage, openfile.IndexBitsPage)
                pageOffset = GetPageOff(virtAddress, 8, 4); //(virtAddress, openfile.OffSetBitsPage, openfile.IndexBitsPage)
                CheckTLB();
                if (tLBResult.Equals("hit"))
                {
                    //get result 
                }
                else
                {
                    //page table walk 
                }
                PrintVirtualAddressesLine();

            }

            PrintStats();
        }

        /// <summary>
        /// Prints the header for the Virtual Addresses Table
        /// </summary>
        private void PrintVirtualAddressesTable()
        {
            Console.WriteLine("{0,-8} {1,-6} {2,-4} {3,-6} {4,-3} {5,-4} {6,-4} {7,-4} {8,-6} {9,-3} {10,-6} {11,-4} {12,-3} {13,-4}", "Virtual", "Virt.", "Page", "TLB", "TLB", "TLB",
                "PT", "Phys", "", "DC", "DC", "", "L2", "L2");
            Console.WriteLine("{0,-8} {1,-6} {2,-4} {3,-6} {4,-3} {5,-4} {6,-4} {7,-4} {8,-6} {9,-3} {10,-4} {11,-4} {12,-2} {13,-4}", "Address", "Page #", "Off", "Tag", "Ind", "Res.",
                "Res.", "Pg #", "DC Tag", "Ind", "Res.", "L2 Tag", "Ind", "Res.");
            Console.WriteLine("-------- ------ ---- ------ --- ---- ---- ---- ------ --- ---- ------ --- ----");
        }

        /// <summary>
        /// Prints an individual line of the Virtual Addresses Table
        /// </summary>
        private void PrintVirtualAddressesLine()
        {
            Console.WriteLine("{0,-13} {1,-3} {2,-4} {3,-6} {4,-1} {5,-4} {6,-4} {7,-4} {8,-6} {9,-3} {10,-6} {11,-4} {12,-3} {13,-4}", virtAddress, virtPageNumber, pageOffset, tLBTag, tLBInd, tLBResult, pTResult,
                physicalPageNumber, dataCashTag, dataCashInd, dataCashResult, l2Tag, l2Ind, l2Result);
        }

        private void CheckTLB()
        {
            //ToDo check the tlb table
            //if(check)
            tLBResult = "hit";
            dtlbHits++;
            //else
            tLBResult = "miss";
            dtlbMisses++;
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
        public string HexToDecimal(string hex)
        {
            string deci = Convert.ToString(Convert.ToInt32(hex, 16), 10);
            return deci;
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
            Console.WriteLine("\nSimulation statistics" +
                "\ndtlb hits: " + dtlbHits +
                "\ndtlb misses: " + dtlbMisses);

            if (dtlbMisses != 0)
                Console.WriteLine("dtlb hit ratio: " + dtlbHits / dtlbMisses);
            else
                Console.WriteLine("dtlb hit ratio: " + dtlbHits);

            Console.WriteLine("pt hits: " + ptHits +
                "\npt faults: " + ptFaults);
            if (ptFaults != 0)
                Console.WriteLine("pt hit ratio: " + ptHits / ptFaults);
            else
                Console.WriteLine("pt hit ratio: " + ptHits);

            Console.WriteLine("dc hits: " + dcHits +
                "\ndc misses: " + dcMisses);
            if (dcMisses != 0)
                Console.WriteLine("dc hit ratio: " + dcHits / dcMisses);
            else
                Console.WriteLine("dc hit ratio: " + dcHits);

            Console.WriteLine("L2 hits: " + l2Hits +
                "\nL2 misses: " + l2Misses);

            if (l2Misses != 0)
                Console.WriteLine("L2 hit ratio: " + l2Hits / l2Misses);
            else
                Console.WriteLine("L2 hit ratio: " + l2Hits);

            Console.WriteLine("Total reads: " + totalReads +
                "\nTotal writes: " + totalWrites);

            if (totalWrites != 0)
                Console.WriteLine("Ratio of reads: " + totalReads / totalWrites);
            else
                Console.WriteLine("Ratio of reads: " + totalReads);


            Console.WriteLine("main memory refs: " + mainMemoryRefs +
                "\npage table refs: " + pageTableRefs +
                "\ndisk refs: " + diskRefs);
        }


    }

}
