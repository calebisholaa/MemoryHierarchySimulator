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
		static int virtualSize;
		static int physicalSize;
		static int maxSize;
		static int numFrames;
		static int numPages;
		static int pageNumber;
		static int pageOffset;
		static int frameNumber;
		static int frameOffest;
		static bool[] presentTable;
		static string[] addressTable;
		static int pageNumberCounter;
		
		public PageTable(int vp, int pp, int ms)//making of page tables, vp is amount of virtual pages, pp is amount of physical pages, ms is max size of each page
		{
			numPages = vp;
			numFrames = pp;
			maxSize = ms;//size of Frames and Pages
			virtualSize = maxSize * numPages;
			physicalSize = numFrames * maxSize;
			pageNumber = (int)Math.Log(numPages, 2);//amount of bits used for page number
			pageOffset = (int)Math.Log(virtualSize, 2) - pageNumber;
			frameNumber = (int)Math.Log(numFrames, 2);//amount of bits used for frame number
			frameOffest = (int)Math.Log(physicalSize, 2) - frameNumber;
			//presentTable = new bool[(int)Math.Pow(2, pageNumber)];//holds weather a specific page holds a value currently
			//addressTable = new string[(int)Math.Pow(2, pageNumber)];//holds the physical address the page holds
			presentTable = new bool[vp];
			addressTable = new string[vp];
			pageNumberCounter = 0;
		}

		static string GetPhysicalAddress(string address)
		{
			string bitAddress = "00" + Convert.ToString(Convert.ToInt64(address, 16), 2);//convert the address to individual bits, aditional 0's to make bits to 14 bit length needed
			string locAddress = bitAddress.Remove(pageOffset);//remove all but the bits that identify where in the page table the physical address is.
			Console.WriteLine("what?" + Convert.ToString(Convert.ToInt64(bitAddress, 2), 16));//test
			int index = Convert.ToInt32(locAddress, 2);
			string phyAddress = addressTable[index];
			string offAddress = bitAddress.Substring(0, pageOffset);//zero out parts of the string that represent the virtual address
			Console.WriteLine("pA: " + phyAddress + "\noF :" + offAddress);//test
			return Convert.ToString(Convert.ToInt64((phyAddress + offAddress), 2), 16);//combines the phyical address and the offset into one hexedecimal address
		}

		static bool GetPresentBit(string address)
		{
			string bitAddress = "00" + Convert.ToString(Convert.ToInt64(address, 16), 2);//convert hes address to individual bits
			bitAddress = bitAddress.Remove(pageOffset);//remove all but the bits that identify where in the page table the physical address is.
			int index = Convert.ToInt32(bitAddress, 2);
			return presentTable[index];//returns whether or not the address would have anything to grab.
		}

		/// <summary>
		/// Checks the the page table is empty at that virtual page table entry.
		/// </summary>
		/// <param name="virtPage"></param>
		/// <returns>bool true or false</returns>
		static public bool CheckPT(int virtPage)
		{
			if (presentTable[virtPage])
			{
				return true;
			}

			presentTable[virtPage] = true;
			addressTable[virtPage] = pageNumberCounter + "";
			pageNumberCounter++;

			return false;
		}

		/// <summary>
		/// Returns the physicalPageNumber
		/// </summary>
		/// <param name="virtPage"></param>
		/// <returns>PhysicalPageNumber</returns>
		static public string GetPhysicalPageNumber(int virtPage)
		{
			if (virtPage > addressTable.Length)
			{

			}

			return addressTable[virtPage];
		}
	}
}
