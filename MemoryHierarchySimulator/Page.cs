/*
    Developer: Joshua Thomas
    Start Date: 10/12/22
    Last Revised: 10/12/22
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
            int pageNumber = Math.Log2(numPages);
            int pageOffset = Math.Log2(virtualSize) - pageNumber;
            int frameNumber = Math.Log2(numFrames);
            int frameOffest = Math.Log2(physicalSize) - frameNumber;
            int[,] table = new int[pageNumber,frameNumber];//initial version of what page table would be structured. Dosn't support offset yet. Maybe use two more layers for offset?
        }
    }
}