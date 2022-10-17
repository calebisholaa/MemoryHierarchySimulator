/*
    Developer: Joshua Thomas
    Start Date: 10/12/22
    Last Revised: 10/17/22
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
        static void main(int vs, int ps, int ms)//making of page tables, vs is virtual size, ps is physical size, ms is max size of virtual entries
        {
            int virtualSize = vs;
            int physicalSize = ps;
            int maxSize = ms;//size of Frames and Pages
            int numFrames = physicalSize / maxSize;
            int numPages = virtualSize / maxSize;
            int pageNumber = Math.Log2(numPages);//amount of bits used for page number
            int pageOffset = Math.Log2(virtualSize) - pageNumber;
            int frameNumber = Math.Log2(numFrames);//amount of bits used for frame number
            int frameOffest = Math.Log2(physicalSize) - frameNumber;
            bool[] presentTable = new bool[pageNumber];//holds weather a specific page holds a value currently
            string[] addressTable = new string[pageNumber];//holds the physical address the page holds
        }//potential two arrays, with virtual page number as index, first with weather its durty, and the other is the physical frame number

        static string getPhysicalAddress(string address)
        {
            string bitAddress = Convert.ToString(Convert.ToInt64(bitAddress, 16), 2);//convert hes address to individual bits
            bitAddress = bitAddress >> pageOffset;//remove all but the bits that identify where in the page table the physical address is.
            int index = Convert.ToInt32(bitAddress,2);
            return addressTable[index];//returns address to be used in physical memory. Need offset added.
        }

        static bool getPresentBit(string address)
        {
            string bitAddress = Convert.ToString(Convert.ToInt64(bitAddress, 16), 2);//convert hes address to individual bits
            bitAddress = bitAddress >> pageOffset;//remove all but the bits that identify where in the page table the physical address is.
            int index = Convert.ToInt32(bitAddress,2);
            return presentTable[index];//returns whether or not the address would have anything to grab.
        }
    }
}
