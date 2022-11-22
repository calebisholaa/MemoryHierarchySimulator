using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHierarchySimulator
{
    /// <summary>
    /// Handles the DTLB
    /// </summary>
    public class DTLB
    {
        DTLBSet[] evenTLB;
        DTLBSet[] oddTLB;
        int lastSetReplaced;
        int numOfSets;

        /// <summary>
        /// Constructor for DTLB
        /// </summary>
        /// <param name="numOfSets">Number of sets for the cache</param>
        /// <param name="setSize">Number of entries for each set</param>
        public DTLB(int numOfSets, int setSize)
        {
            this.numOfSets = numOfSets;
            lastSetReplaced = 0;
            evenTLB = new DTLBSet[numOfSets/2 + numOfSets%2];
            oddTLB = new DTLBSet[numOfSets/2];

            for (int i = 0; i < evenTLB.Length; i++)
            {
                evenTLB[i] = new DTLBSet(setSize);
            }

            for (int i = 0; i < oddTLB.Length; i++)
            {
                oddTLB[i] = new DTLBSet(setSize);
            }
        }

        /// <summary>
        /// Searches the DTLB for a tag. If the DTLB has the tag then it will return the Physical Page Number (PPN). 
        /// If the DTLB does not have the tag then it will copy the tag within the DTLB.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>Returns the PPN. If the PPN is not found then the method will return 'false'.</returns>
        public string SearchTLB(string tag)
        {
            string ppn;
            
            if (OddOrEven(tag))
            {
                for (int i = 0; i < evenTLB.Length; i++)
                {
                    ppn = evenTLB[i].GetPPN(tag);
                    if (ppn.Equals("empty") != true)
                    {
                        return ppn;
                    }
                }

                for (int i = 0; i < evenTLB.Length; i++)
                {
                    if (!evenTLB[i].IsSetFull(tag))
                    {
                        return "empty";
                    }
                }

                if (lastSetReplaced >= numOfSets / 2 + numOfSets % 2)
                {
                    lastSetReplaced = 0;
                }

                evenTLB[lastSetReplaced].ReplaceEntry(tag);
                lastSetReplaced++;

                return "empty";

            }
            else
            {
                for (int i = 0; i < oddTLB.Length; i++)
                {
                    ppn = oddTLB[i].GetPPN(tag);
                    if (!ppn.Equals("empty"))
                    {
                        return ppn;
                    }
                }

                for (int i = 0; i < oddTLB.Length; i++)
                {
                    if (!oddTLB[i].IsSetFull(tag))
                    {
                        return "empty";
                    }
                }

                if (lastSetReplaced >= numOfSets / 2)
                {
                    lastSetReplaced = 0;
                }

                oddTLB[lastSetReplaced].ReplaceEntry(tag);
                lastSetReplaced++;

                return "empty";
            }
        } 
        /// <summary>
        /// Method will set the PPN for a specific tag within the DTLB
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="PPN"></param>
        public void SetPPN(string tag, string ppn)
        {
            if (OddOrEven(tag))
            {
                for (int i = 0; i < evenTLB.Length; i++)
                {
                    evenTLB[i].SetPPN(tag, ppn);
                }
            }
            else
            {
                for (int i = 0; i < oddTLB.Length; i++)
                {
                    oddTLB[i].SetPPN(tag, ppn);
                }
            }
        }


        /// <summary>
        /// Determines if the tag is odd or even
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool OddOrEven(string s)
        {
            bool even = false;
            switch (s)
            {
                case "0":
                    even = true;
                    break;
                case "1":
                    even = false;
                    break;
                case "2":
                    even = true;
                    break;
                case "3":
                    even = false;
                    break;
                case "4":
                    even = true;
                    break;
                case "5":
                    even = false;
                    break;
                case "6":
                    even = true;
                    break;
                case "7":
                    even = false;
                    break;
                case "8":
                    even = true;
                    break;
                case "9":
                    even = false;
                    break;
                case "a":
                    even = true;
                    break;
                case "b":
                    even = false;
                    break;
                case "c":
                    even = true;
                    break;
                case "d":
                    even = false;
                    break;
                case "e":
                    even = true;
                    break;
                case "f":
                    even = false;
                    break;
                default:
                    break;
            }

            return even;

        }

    }
}