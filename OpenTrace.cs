using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHierarchy
{
    public class OpenTrace
    {

        int index = 0;
        public string[] fileContents = null;
        Dictionary<string,string> virtualAddressesq = new Dictionary<string,string>();
        public string[,] ReadFile()
        {
            string filePath = " ";
            bool keepGoing = true;
            string contents = " ";
            char[] temp = null;
            string tempString = null;

            while (keepGoing)
            {
                Console.WriteLine("\nPlease enter the file path for the trace file.");
                filePath = Console.ReadLine();

                try
                {
                    using (StreamReader sr = File.OpenText(filePath))
                    {
                        Console.WriteLine("File open.");
                        keepGoing = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"The file could not be opened: '{e}'");
                }
            }

            fileContents = File.ReadAllText(filePath).Split(':');
            foreach(string s in fileContents)
            {
                contents += s + " ";
            }

            contents = contents.Replace($"\n", " ");
            fileContents = contents.Split(' ');

            for(int y = 0; y < fileContents.Length; y++)
            {
                if(fileContents[y].Equals("R"))
                {
                    index++;
                }
            }

            string[,] virtualAddresses = new string[index, 8];

            index = 0;

            for(int k = 0; k < fileContents.Length; k++)
            {
                if (fileContents[k].Equals("R"))
                {
                    tempString = fileContents[k + 1];
                    temp = tempString.ToCharArray();
                    virtualAddresses[index,0] = fileContents[k];
                    for(int t = 0; t < temp.Length; t++)
                    {
                        virtualAddresses[index, t + 1] = temp[t].ToString();
                        
                    }
                    index++;
                    
                  
                }
            }

            foreach (string st in virtualAddresses)
            {
                Console.Write("{0} \n", st);
            }

            return virtualAddressees;
        }

    }
}
