using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Web;

namespace LogDataConversionServiceApplication
{
    public enum FileLocation
    {
        Local,URI
    }

    public class Reader
    {
        private FileLocation _fileLocation;
        private string _filePath;
        private string _read;
        private bool _running; 
        public Reader()
        {

        }

        public void ReadFile()
        {
            // If file location is still remote fetch a copy and store it locally and set the filepath
            if (_fileLocation == FileLocation.URI) CopyFileToLocal();

            if (_filePath != null)
            {
                while (_running)
                {
                
                }
            }
            else
            {
                throw new Exception("File location is wrong");
            }
        }

        private void CopyFileToLocal()
        {
            // Implement this yourselves
        }

        private void ReadData(string[][] file)
        {   

        }


        public void StopReader()
        {
            _running = false;
        }
    }
}