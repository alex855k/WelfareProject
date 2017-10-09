using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
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

        public string[] GetNewData()
        {
            // If file location is still remote fetch a copy and store it locally and set the filepath
            if (_fileLocationType == FileLocation.URI) CopyFileToLocalDirectory();

            if (_filePath != null)
            {
                string[] data = File.ReadAllLines(_filePath, Encoding.GetEncoding("iso-8859-1"));

                foreach (var l in data)
                {
                    string[] arr = l.Split('\t');
                    if (arr) { }
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