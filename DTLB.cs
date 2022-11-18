using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemH
{
    public class DTLB
    {
        string[,] evenTLB = new string[64,1];
        string[,] oddTLB = new string[64,1];
        int evenTLBSize = 0;
        int oddTLBSize = 0;

        /// <summary>
        /// Searches the DTLB for a tag. If the DTLB has the tag then it will return the Physical Page Number (PPN). 
        /// If the DTLB does not have the tag then it will copy the tag within the DTLB.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>Returns the PPN. If the PPN is not found then the method will return 'false'.</returns>
        public string SearchTLB(string tag)
        {
            bool hit = false;
            string PPN = " ";


            if(OddOrEven(tag))
            {
                for (int i = 0; i < evenTLB.GetLength(0); i++)
                {
                    if (evenTLB[i, 0] == tag)
                    {
                        hit = true;
                        PPN = evenTLB[i, 1];
                        return PPN;
                    }
                }

                if (evenTLBSize > 64)
                {
                    evenTLBSize = 0;
                    evenTLB[evenTLBSize, 0] = tag;
                    evenTLB[evenTLBSize, 1] = " ";
                    evenTLBSize++;
                }
                else
                {
                    evenTLB[evenTLBSize, 0] = tag;
                    evenTLBSize++;
                }
            }
            else
            {
                for (int i = 0; i < oddTLB.GetLength(0); i++)
                {
                    if (oddTLB[i, 0] == tag)
                    {
                        hit = true;
                        PPN = oddTLB[i, 1];
                        return PPN;
                    }
                }

                if (oddTLBSize > 64)
                {
                    oddTLBSize = 0;
                    oddTLB[oddTLBSize, 0] = tag;
                    oddTLB[oddTLBSize, 1] = " ";
                    oddTLBSize++;
                }
                else
                {
                    oddTLB[oddTLBSize, 0] = tag;
                    oddTLBSize++;
                }
            }

            PPN = "false"; 
            return PPN;
        }

        /// <summary>
        /// Method will set the PPN for a specific tag within the DTLB
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="PPN"></param>
        public void SetPPN(string tag, string PPN)
        {
            if(OddOrEven(tag))
            {
                for (int i = 0; i < evenTLB.GetLength(0); i++)
                {
                    if (evenTLB[i, 0] == tag)
                    {
                        evenTLB[i, 1] = PPN;
                    }
                }
            }
            else
            {
                for (int i = 0; i < oddTLB.GetLength(0); i++)
                {
                    if (oddTLB[i, 0] == tag)
                    {
                        oddTLB[i, 1] = PPN;
                    }
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
