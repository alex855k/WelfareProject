using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows;

namespace TestClient
{

    public enum FileLocation
    {
        Local,URI
    }

    public class Reader
    {
        //The responsibility of this class is to read the data and return only the data that is nessecary as a string array.
        private FileLocation _fileLocationType;
        private string _filePath;
        private string _read;
        private bool _running;

        public string LocalDirectory { get; set; }

        public Reader(string localDirectory)
        {
            LocalDirectory = localDirectory;
        }

        public void ChangeFilePath()
        {
            
        }

        public string[] GetData()
        {
            // If file location is still remote fetch a copy and store it locally and set the filepath
            if (_fileLocationType == FileLocation.URI) CopyFileToLocalDirectory();

            if (_filePath != null)
            {
                char[] specialChars = { '\t', ';' };
                


                // Split all lines of data into a string arr
                string[] data = File.ReadAllLines(_filePath, Encoding.GetEncoding("iso-8859-1"));
                int countLines = 0;
                foreach (var line in data)
                {
                    if (countLines == 1)
                    {
                        //line.
                    }
                    // Looks for special character either horizontal tab hex09 or semi-colon hex3B
                    char seperationChar;
                    


                    // For every line of data split the data into columns
                    string[] columns = line.Split('\t');
                    foreach (var col in columns)
                    {
                        //check column for location of datetime data then store location
                        char[] columnCharArr = col.ToCharArray();
                        foreach (char c in columnCharArr)
                        {

                        }
                        if ()
                        {
                            
                        }
                    }
                countLines++;
                }


                return data; 

                //Thread t = new Thread();
            }
            else
            {
                throw new Exception("File location is wrong");
            }
        }

        private void SendData()
        {
            while (_running)
            {

            }
        }

        private void CopyFileToLocalDirectory()
        {
            throw new NotImplementedException();
            // Implement this yourselves
        }



        public void StopReader()
        {
            _running = false;
        }
    }
}