using MemoryHierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHierarchySimulator
{
    public class HierarchyTraverse
    {
        public void printTable(OpenTraceFile openTraceFile, OpenConfigFile openfile)
        {
            PageTable pageTable = new PageTable(openfile.VirtualPages, openfile.PhysicalPages, openfile.PageSize);

            Console.WriteLine("{0,-8} {1,-6} {2,-4} {3,-6} {4,-3} {5,-4} {6,-4} {7,-4} {8,-6} {9,-3} {10,-4}", "Virtual", "Virt.", "Page", "TLB", "TLB", "TLB", "PT", "Phys", "", "DC", "DC");
            Console.WriteLine("{0,-8} {1,-6} {2,-4} {3,-6} {4,-3} {5,-4} {6,-4} {7,-4} {8,-6} {9,-3} {10,-4}", "Address", "Page #", "Off", "Tag", "Ind", "Res.", "Res.", "Pg #", "DC Tag", "Ind", "Res.");
            Console.WriteLine("________ ______ ____ ______ ___ ____ ____ ____ ______ ___ ____");

            foreach(string addr in openTraceFile.hexAddress)
            {
                string virAddr = addr.PadLeft(8, '0');
                string virPage = getPageNum(virAddr, openfile.OffSetBitsPage, openfile.IndexBitsPage);
                string virOff = getPageOff(virAddr, openfile.OffSetBitsPage, openfile.IndexBitsPage);
                Console.WriteLine("{0,8} {1,6} {2,4} {3,6} {4,3} {5,4} {6,4} {7,4} {8,6} {9,3} {10,4}", virAddr, virPage, virOff, "", "", "", "", "", "", "", "");
            }
            /*foreach (string addr in openTraceFile.hexAddress)
            {
                string virAddr = convertToBinary(addr);
                Console.WriteLine(virAddr);
            }*/
        }

        public string convertToHex(string binary)
        {
            string hex = Convert.ToString(Convert.ToInt32(binary, 2), 16);
            return hex;
        }

        public string convertToBinary(string hex)
        {
            string binary = Convert.ToString(Convert.ToInt32(hex, 16), 2).PadLeft(32, '0');
            return binary;
        }

        public string convertToDecimal(string hex)
        {
            string deci = Convert.ToString(Convert.ToInt32(hex, 16), 10);
            return deci;
        }

        public string getPageNum(string str, double offset, double index)
        {
            int length = 32 - (int)offset - (int)index;
            str = convertToBinary(str);
            str = str.Substring(length, (int)index);
            str = convertToHex(str);
            return str;
        }

        public string getPageOff(string str, double offset, double index)
        {
            int length = 32 - (int)offset - (int)index;
            str = convertToBinary(str);
            str = str.Substring(length + (int)index);
            str = convertToHex(str);
            return str;
        }
    }
}
