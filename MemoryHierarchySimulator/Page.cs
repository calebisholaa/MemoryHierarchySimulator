/*
    Developer: Joshua Thomas
    Start Date: 10/12/22
    Last Revised: 10/31/22
    Description: Page table object type and traversal
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHierarchySimulator
{
    public class PageTable
    {
        int virtualSize;
        int physicalSize;
        int maxSize;
        int numFrames;
        int numPages;
        int pageNumber;
        int pageOffset;
        int frameNumber;
        int frameOffest;
        bool[] presentTable;
        string[] addressTable;
        public PageTable(int vp, int pp, int ms)//making of page tables, vp is amount of virtual pages, pp is amount of physical pages, ms is max size of each page
        {
            numPages = vp;
            numFrames = pp;
            maxSize = ms;//size of Frames and Pages
            virtualSize = maxSize * numPages;
			physicalSize = numFrames * maxSize;
			pageNumber = (int)Math.Log2(numPages);//amount of bits used for page number
            pageOffset = (int)Math.Log2(virtualSize) - pageNumber;
            frameNumber = (int)Math.Log2(numFrames);//amount of bits used for frame number
            frameOffest = (int)Math.Log2(physicalSize) - frameNumber;
            presentTable = new bool[pageNumber];//holds weather a specific page holds a value currently
            addressTable = new string[pageNumber];//holds the physical address the page holds
        }

        static string getPhysicalAddress(string address)
        {
            string bitAddress = Convert.ToString(Convert.ToInt64(address, 16), 2);//convert the address to individual bits
			string locAddress = bitAddress.Remove(pageOffset);//remove all but the bits that identify where in the page table the physical address is.
			int index = Convert.ToInt32(locAddress, 2);
			string phyAddress = addressTable[index];
			string offAddress = bitAddress.Substring(0,pageOffset);// << pageNumber) >> pageNumber;//zero out parts of the string that represent the virtual address
			return Convert.ToString(Convert.ToInt64((phyAddress + offAddress), 2), 16);//combines the phyical address and the offset into one hexedecimal address
		}

        static bool getPresentBit(string address)
        {
            string bitAddress = Convert.ToString(Convert.ToInt64(address, 16), 2);//convert hes address to individual bits
            bitAddress = bitAddress.Remove(pageOffset);//remove all but the bits that identify where in the page table the physical address is.
            int index = Convert.ToInt32(bitAddress,2);
            return presentTable[index];//returns whether or not the address would have anything to grab.
        }
    }
}
