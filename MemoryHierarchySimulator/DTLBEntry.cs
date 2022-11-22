using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHierarchySimulator
{
    /// <summary>
    /// Handles individual DTLB Entries
    /// </summary>
    class DTLBEntry
    {
      
        public string tag { get; set; }//virtual tag for the entry
        public string ppn { get; set; }//physical page number

        /// <summary>
        /// Constructor
        /// </summary>
        public DTLBEntry()
        {
            tag = "Empty";
            ppn = "Empty";
        }

        /// <summary>
        /// Checks if the virtual tag is equal
        /// </summary>
        /// <param name="checkTag">Virtual tag</param>
        /// <returns>true if equal</returns>
        public bool TagCheck(string checkTag)
        {
            if (tag.Equals(checkTag))
            {
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Handles a set of DTLB entries
    /// </summary>
    class DTLBSet 
    {
        DTLBEntry[] set { get; set; }//array of DTLBEntry

        int lastEntryReplaced;//Keeps track of the last entry replace

        /// <summary>
        /// Constructor for DTLBSet
        /// </summary>
        /// <param name="numOfEntries">number of entries</param>
        public DTLBSet(int numOfEntries)
        {
            lastEntryReplaced = 0;

            set = new DTLBEntry[numOfEntries];

            for (int i = 0; i < numOfEntries; i++)
            {
                set[i] = new DTLBEntry();
            }

        }

        /// <summary>
        /// Checks if the tag is in the set of entries
        /// </summary>
        /// <param name="tag">Virtual tag</param>
        /// <returns>true if found</returns>
        public bool TagCheck(string tag)
        {
            foreach (DTLBEntry entry in set)
            {
                if (entry.TagCheck(tag))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Sets the physical page number of the entry
        /// </summary>
        /// <param name="tag">Virtual Tag</param>
        /// <param name="ppn">Physical Page Number</param>
        public void SetPPN(string tag, string ppn)
        {
            foreach (DTLBEntry entry in set)
            {
                if (entry.TagCheck(tag))
                {
                    entry.ppn = ppn;
                }
            }
        }

        /// <summary>
        /// Retrieves the Physical Page Number
        /// </summary>
        /// <param name="tag">Virtual tag</param>
        /// <returns>string of the physical number or empty</returns>
        public string GetPPN(string tag)
        {
            foreach (DTLBEntry entry in set)
            {
                if (entry.TagCheck(tag))
                {
                    return entry.ppn;
                }
            }
            return "empty";
        }

        /// <summary>
        /// Replaces the entry
        /// </summary>
        /// <param name="tag">virtual tag number</param>
        public void ReplaceEntry(string tag)
        {
            set[lastEntryReplaced].tag = tag;
            set[lastEntryReplaced].ppn = "empty";
        }

        /// <summary>
        /// Checks if the set is full
        /// </summary>
        /// <param name="tag">virtual tag</param>
        /// <returns>true if full</returns>
        public bool IsSetFull(string tag)
        {
            foreach (DTLBEntry entry in set)
            {
                if(entry.TagCheck("empty"))
                {
                    entry.tag = tag;
                    return false;
                }
            }
            return true;
        }

    }
}
