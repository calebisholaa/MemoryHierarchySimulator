using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHierarchySimulator
{
    public class OpenTraceFile
    {
       public List<string> accessType = new List<string>();
       public List<string> hexAddress = new List<string>();

        string lineReplace;
        public int arrayLength = 0;
        public int arrayMarker = 0;
        public string[] fileContents = null;



        public void ReadFile()
        {
            string filePath = " ";
            bool keepGoing = true;
            int position;

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

            fileContents = File.ReadAllText(filePath).Split('\n');
            foreach (string line in fileContents)
            {
                position = line.IndexOf(":");
                if (position > 0)
                {
                    lineReplace = line.Replace("\r", "");
                    accessType.Add(line.Substring(0, position));
                    hexAddress.Add(lineReplace.Substring(position + 1));
                }
            }

        }
    }
}
