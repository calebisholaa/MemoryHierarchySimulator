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
        string filePath = "";
        string fileContent = "";
    //    public void OpenReadFile()
    //    {
    //        using (OpenFileDialog openFileDialog = new OpenFileDialog())
    //        {
    //            openFileDialog.InitialDirectory = "c:\\";
    //            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
    //            openFileDialog.FilterIndex = 2;
    //            openFileDialog.RestoreDirectory = true;

    //            if (openFileDialog.ShowDialog() == DialogResult.OK)
    //            {
    //                //Get the path of specified file
    //                this.filePath = openFileDialog.FileName;

    //                //Read the contents of the file into a stream
    //                var fileStream = openFileDialog.OpenFile();

    //                using (StreamReader reader = new StreamReader(fileStream))
    //                {
    //                    this.fileContent = reader.ReadToEnd();
    //                }
    //            }

    //        }
    //    }

    //    public void OpenAndParse()
    //    {
    //        using (OpenFileDialog openFileDialog = new OpenFileDialog())
    //        {
    //            openFileDialog.InitialDirectory = "c:\\";
    //            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
    //            openFileDialog.FilterIndex = 2;
    //            openFileDialog.RestoreDirectory = true;

    //            if (openFileDialog.ShowDialog() == DialogResult.OK)
    //            {
    //                //Get the path of specified file
    //                this.filePath = openFileDialog.FileName;

    //                //Read the contents of the file into a stream
    //                var fileStream = openFileDialog.OpenFile();



    //                using (StreamReader reader = new StreamReader(fileStream))
    //                {
    //                    var lineCounter = 0;
    //                    while (!reader.EndOfStream)
    //                    {
    //                        var line = reader.ReadLine();
    //                        var values = line.Split(':');

    //                        if (values.Length == 1)
    //                        {
                               

    //                        }
    //                        else
    //                        {
    //                            accessType.Add(values[0]);
    //                            hexAddress.Add(values[1]);

                                

    //                            lineCounter++;
    //                        }



    //                    }




    //                    reader.Close();

    //                }
    //            }

    //        }
    //    }



    //    public void OpenTrace()
    //    {
    //        OpenAndParse();

    //        Print();
    //    }

    //    public void Print()
    //    {
    //        for (int i = 0; i < hexAddress.Count; i++)
    //        {
    //            Console.WriteLine(accessType[i] + ":" + hexAddress[i]);
    //        }
    //    }
    }
}
