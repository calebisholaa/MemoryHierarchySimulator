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
                virtPageNumber = getPageNum(virtAddress, 8, 4); //(virtAddress, openfile.OffSetBitsPage, openfile.IndexBitsPage)
                pageOffset = getPageOff(virtAddress, 8, 4); //(virtAddress, openfile.OffSetBitsPage, openfile.IndexBitsPage)
                PrintVirtualAddressesLine();
            }
        }



        private void PrintVirtualAddressesTable()
        {
            Console.WriteLine("{0,-8} {1,-6} {2,-4} {3,-6} {4,-3} {5,-4} {6,-4} {7,-4} {8,-6} {9,-3} {10,-6} {11,-4} {12,-3} {13,-4}", "Virtual", "Virt.", "Page", "TLB", "TLB", "TLB",
                "PT", "Phys", "", "DC", "DC", "", "L2", "L2");
            Console.WriteLine("{0,-8} {1,-6} {2,-4} {3,-6} {4,-3} {5,-4} {6,-4} {7,-4} {8,-6} {9,-3} {10,-4} {11,-4} {12,-2} {13,-4}", "Address", "Page #", "Off", "Tag", "Ind", "Res.",
                "Res.", "Pg #", "DC Tag", "Ind", "Res.", "L2 Tag", "Ind", "Res.");
            Console.WriteLine("-------- ------ ---- ------ --- ---- ---- ---- ------ --- ---- ------ --- ----");
        }

        private void PrintVirtualAddressesLine()
        {
            Console.WriteLine("{0,-13} {1,-3} {2,-4} {3,-6} {4,-3} {5,-4} {6,-4} {7,-4} {8,-6} {9,-3} {10,-6} {11,-4} {12,-3} {13,-4}", virtAddress, virtPageNumber, pageOffset, tLBTag, tLBInd, tLBResult, pTResult,
                physicalPageNumber, dataCashTag, dataCashInd, dataCashResult, l2Tag, l2Ind, l2Result);
        }


        public string binaryToHex(string binary)
        {
            string hex = Convert.ToString(Convert.ToInt32(binary, 2), 16);
            return hex;
        }

        public string hexToBinary(string hex)
        {
            string binary = Convert.ToString(Convert.ToInt32(hex, 16), 2).PadLeft(32, '0');
            return binary;
        }

        public string hexToDecimal(string hex)
        {
            string deci = Convert.ToString(Convert.ToInt32(hex, 16), 10);
            return deci;
        }

        private string getPageNum(string str, double offset, double index)
        {
            int length = 32 - (int)offset - (int)index;
            str = hexToBinary(str);
            str = str.Substring(length, (int)index);
            str = binaryToHex(str);
            return str;
        }

        private string getPageOff(string str, double offset, double index)
        {
            int length = 32 - (int)offset - (int)index;
            str = hexToBinary(str);
            str = str.Substring(length + (int)index);
            str = binaryToHex(str);
            return str;
        }
    }

}
