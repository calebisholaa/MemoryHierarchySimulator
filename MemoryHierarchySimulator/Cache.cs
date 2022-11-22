using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace MemoryHierarchySimulator
{
    /// <summary>
    /// Handles the Caches for the Memory Hierarchy Transversal
    /// </summary>
    public class Cache
    {
        private string offset;

        private int indexBits;
        private int offsetBits;
        private CacheSet[] cache;
        public string index { get; set; }
        public string tag { get; set; }

        /// <summary>
        /// Constructor for the Cache
        /// </summary>
        /// <param name="indexBits">number of bits for the index</param>
        /// <param name="offsetBits">number of bits for the offset</param>
        /// <param name="numOfSets">number of sets</param>
        /// <param name="numOfEntries">number of entries per set</param>
        public Cache(int indexBits, int offsetBits, int numOfSets, int numOfEntries)
        {
            this.indexBits = indexBits;
            this.offsetBits = offsetBits;

            cache = new CacheSet[numOfSets];

            for (int i = 0; i < numOfSets; i++)
            {
                cache[i] = new CacheSet(numOfEntries);
            }
        }

        /// <summary>
        /// Searchs the cache for the physical Address.
        /// </summary>
        /// <param name="physicalAddress">the physical address</param>
        /// <param name="physicalPageNumber">the physical page number</param>
        /// <returns>true if found</returns>
        public bool SearchCache(string physicalAddress, string physicalPageNumber)
        {
            offset = "";
            index = "";
            tag = "";

            ReadPhysicalAddress(physicalAddress);

            if (cache[BinaryToDecimal(index)].SearchCache(tag, offset, physicalPageNumber))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reads the physical address into the offset, index, and tag.
        /// </summary>
        /// <param name="physicalAddress">the physical address</param>
        private void ReadPhysicalAddress(string physicalAddress)
        {
            string convertedAddress = HexToBinary(physicalAddress); 

            for (int i = 0; i < 32; i++)
            {
                if (i > 31 - offsetBits)
                {
                    offset += convertedAddress[i];
                }
                else if (i > 31 - offsetBits - indexBits)
                {
                    index += convertedAddress[i];
                }
                else
                {
                    tag += convertedAddress[i];                   
                }               
            }
        }

        /// <summary>
        /// Removes all the entries with on that physical page
        /// </summary>
        /// <param name="physicalPageNumber"></param>
        public void RemoveEntries(string physicalPageNumber)
        {
            foreach (CacheSet set in cache)
            {
                set.RemoveEntries(physicalPageNumber);
            }
        }

        /// <summary>
        /// Converts hex to binary
        /// </summary>
        /// <param name="hex"></param>
        /// <returns>binary</returns>
        private string HexToBinary(string hex)
        {
            string binary = Convert.ToString(Convert.ToInt32(hex, 16), 2).PadLeft(32, '0');
            return binary;
        }

        /// <summary>
        /// Binary to Decimal
        /// </summary>
        /// <param name="binary"></param>
        /// <returns>Decimal value of the binary</returns>
        private int BinaryToDecimal(string binary)
        {
            return Convert.ToInt32(binary, 2);
        }
    }

    /// <summary>
    /// Handles a single CacheEntry
    /// </summary>
    public class CacheEntry
    {
        private string tag;
        public string PhysicalPageNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="offset"></param>
        /// <param name="physicalPageNumber"></param>
        public CacheEntry(string tag, string offset, string physicalPageNumber)
        {
            this.tag = tag;
            this.PhysicalPageNumber = physicalPageNumber;
        }

        /// <summary>
        /// Compares the entry tags
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>true is they are equal</returns>
        public bool CompareToEntry(string tag)
        {
            if (this.tag.Equals(tag))
            {
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Handles a set of the cache
    /// </summary>
    public class CacheSet
    {
        public string set { get; }

        private int numOfEntries;
        private List<CacheEntry> cacheEntries;
        private int lRUTracker;

        /// <summary>
        /// Constructor for Cacheset
        /// </summary>
        /// <param name="numOfEntries">number of entries in the set</param>
        public CacheSet(int numOfEntries)
        {
            cacheEntries = new List<CacheEntry>(numOfEntries);
            this.numOfEntries = numOfEntries;
            lRUTracker = 0;
        }

        /// <summary>
        /// Searches the cache for an entry
        /// </summary>
        /// <param name="tag">the tag</param>
        /// <param name="offset">the offset</param>
        /// <param name="physicalPageNumber"> the physical page number</param>
        /// <returns>true is found</returns>
        public bool SearchCache(string tag, string offset, string physicalPageNumber)
        {
            bool doesContain = false;
            int foundAt = 0;

            foreach (CacheEntry entry in cacheEntries)
            {
                if (entry.CompareToEntry(tag))
                {
                    doesContain = true;
                }
                else
                {
                    foundAt++;
                }
            }


            if (!doesContain)
            {


                if (cacheEntries.Count == numOfEntries)
                {
                    cacheEntries.RemoveAt(0);
                    cacheEntries.Insert(numOfEntries - 1, new CacheEntry(tag, offset, physicalPageNumber));


                }
                else
                {
                    cacheEntries.Insert(lRUTracker, new CacheEntry(tag, offset, physicalPageNumber));
                    lRUTracker++;
                }

                return false;

            }
            else
            {
                // Remove the indexes page
                cacheEntries.RemoveAt(foundAt);

                // insert the current page
                cacheEntries.Insert(cacheEntries.Count, new CacheEntry(tag, offset, physicalPageNumber));

                return true;
            }
        }

        /// <summary>
        /// Removes all the entries attach to the physical page
        /// </summary>
        /// <param name="physicalPageNumber">the physical page number</param>
        public void RemoveEntries(string physicalPageNumber)
        {
            for(int i = 0; i < cacheEntries.Count; i++)
            {
                if (cacheEntries[i].PhysicalPageNumber.Equals(physicalPageNumber))
                {
                    cacheEntries.RemoveAt(i);
                    lRUTracker--;
                }
            }
        }
    }
}
