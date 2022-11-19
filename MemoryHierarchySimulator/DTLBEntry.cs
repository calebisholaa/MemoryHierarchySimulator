using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHierarchySimulator
{
    class DTLBEntry
    {
        public string tag { get; set; }
        public string ppn { get; set; }
        public DTLBEntry()
        {
            tag = "Empty";
            ppn = "Empty";
        }

        public bool TagCheck(string checkTag)
        {
            if (tag.Equals(checkTag))
            {
                return true;
            }

            return false;
        }
    }

    class DTLBSet 
    {
        DTLBEntry[] set { get; set; }
        int lastEntryReplaced;

        public DTLBSet(int numOfEntries)
        {
            lastEntryReplaced = 0;

            set = new DTLBEntry[numOfEntries];

            for (int i = 0; i < numOfEntries; i++)
            {
                set[i] = new DTLBEntry();
            }

        }

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

        public void ReplaceEntry(string tag)
        {
            set[lastEntryReplaced].tag = tag;
            set[lastEntryReplaced].ppn = "empty";
        }

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
