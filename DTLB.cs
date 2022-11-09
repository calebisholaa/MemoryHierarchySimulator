using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHierarchy
{
    public class DTLB
    {
        OpenFile openFile = new OpenFile();
        public static OpenTrace openTrace = new OpenTrace();
        double addressSpace = 0;
        string VPN = " ";
        string[] TLB = new string[openTrace.TLBSize];               //DTLB

        //create DTLB and check with the VPN if you have the translation if not send it to the page table if you do then translate. 
        //need to figure out the size of DTLB. create it dynamically not staticly. 


        /// <summary>
        /// Gets the virtual page number from the page. for now it is hard coded for the first trace file 
        /// </summary>
        /// <param name="virtualAddressses"></param>
        /// <returns></returns>
        public double GetVPN(string[,] virtualAddressses)
        {
            for(int i = 0; i < virtualAddressses.GetLength(0); i++)
            {
                for(int j = 1; j < 2; j++)
                {
                    VPN = Translate(virtualAddressses[i, j]);
                    Console.WriteLine(VPN);
                }
            }


            OpenFile of = new OpenFile();
            addressSpace = Math.Log2(of.pageSizeTLB);
            

            return 0;
        }


        /// <summary>
        /// wrote this method to convert 2D array
        /// </summary>
        /// <param name="stringarray"></param>
        /// <returns></returns>
        public string[] Convert2D(string[,] stringarray)
        {
            string[] convertedArray = new string[stringarray.GetLength(1)];
            int temp = 0;
            for (int i = 0; i < stringarray.GetLength(0); i++)
            {
                for (int j = 0; j < stringarray.GetLength(1); j++)
                {
                    if(stringarray[i,j].Equals("R"))
                    {
                        break;
                    }
                    else
                    {
                        convertedArray[temp] += stringarray[i,j];
                    }
                }

                temp++;
            }


            return convertedArray;
        }





        /// <summary>
        /// Gets the physical address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public string GetPhysicalAddress(string address)
        {
            OpenFile of = new OpenFile();
            OpenTrace ot = new OpenTrace();         
            string bitAddress = "00" + Convert.ToString(Convert.ToInt64(address, 16), 2);//convert the address to individual bits, aditional 0's to make bits to 14 bit length needed
            string locAddress = bitAddress.Remove((int)of.pageOffsetTLB);//remove all but the bits that identify where in the page table the physical address is.
            Console.WriteLine(Convert.ToString(Convert.ToInt64(bitAddress, 2), 16));
            int index = Convert.ToInt32(locAddress, 2);
            string phyAddress = ot.address[index];
            string offAddress = bitAddress.Substring(0, (int)of.pageOffsetTLB);//zero out parts of the string that represent the virtual address
            return Convert.ToString(Convert.ToInt64((phyAddress + offAddress), 2), 16);//combines the phyical address and the offset into one hexedecimal address
        }



        /// <summary>
        /// Translate the hex to binary
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string Translate(string s)
        {
            string bits = " ";
            switch(s)
            {
                case "1":
                    bits = "0001";
                    break;
                case "2":
                    bits = "0010";
                    break;
                case "3":
                    bits = "0011";
                    break;
                case "4":
                    bits = "0100";
                    break;
                case "5":
                    bits = "0101";
                    break;
                case "6":
                    bits = "0110";
                    break;
                case "7":
                    bits = "0111";
                    break;
                case "8":
                    bits = "1000";
                    break;
                case "9":
                    bits = "1001";
                    break;
                case "a":
                    bits = "1010";
                    break;
                case "b":
                    bits = "1011";
                    break;
                case "c":
                    bits = "1100";
                    break;
                case "d":
                    bits = "1101";
                    break;
                case "e":
                    bits = "1110";
                    break;
                case "f":
                    bits = "1111";
                    break;
                default:
                    break; 
            }

            return bits;

        }

    }
}
