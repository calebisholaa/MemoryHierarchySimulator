using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemH
{
    public class DTLB
    {
        string[,] TLB = new string[64,1];
        int TLBSize = 0;

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
            for(int i = 0; i < TLB.GetLength(0); i++)
            {
                if (TLB[i,0] == tag)
                {
                    hit = true;
                    PPN = TLB[i,1];
                    return PPN;
                }
            }

            if(TLBSize > 64)
            {
                TLBSize = 0;
                TLB[TLBSize, 0] = tag;
                TLB[TLBSize,1] = " ";
                TLBSize++;
            }
            else
            {
                TLB[TLBSize, 0] = tag;
                TLBSize++;
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
            for(int i = 0; i < TLB.GetLength(0); i++)
            {
                if (TLB[i,0] == tag)
                {
                    TLB[i,1] = PPN;
                }
            }
        }

    }
}
