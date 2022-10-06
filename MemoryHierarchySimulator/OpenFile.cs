using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryHierarchySimulator
{
    internal class OpenFile
    {

        /// IDictionary<string, string> trace = new Dictionary<string, string>();
        /// List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
        List<string> accessType = new List<string>();
        List<string> hexAddress = new List<string>();
        string filePath = "";
        string fileContent = "";
       
        public void OpenAndParse()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    this.filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();



                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        var lineCounter = 0;
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(':');

                            if (values.Length == 1)
                            {
                                //trace[lineCounter - 1] += values[0];

                            }
                            else
                            {
                                accessType.Add(values[0]);
                                hexAddress.Add(values[1]);

                               // trace.Add(values[0], values[1]);  dictionaries would not allow duplicate keys 
                                
                                lineCounter++;
                            }



                        }




                        reader.Close();

                      //  versionProductID = productID;

                        // PrintResult();
                    }
                }

            }
        }



        public void Open()
        {
            OpenAndParse();

            Print();
        }

        public void Print()
        {
            for (int i = 0; i < hexAddress.Count; i++)
            {
                Console.WriteLine(accessType[i] + ":" + hexAddress[i]) ;
            }
        }
    }
}
