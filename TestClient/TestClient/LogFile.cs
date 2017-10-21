using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    public class LogFile
    {
        public long ID { get; set; }
        public Object _logfileLock = new Object();
        public bool InUse { get; set; }
        public FileLocationType FileLocationType { get; private set; }
        public string Url { get; set; }
        public string[] LogData { get; set; } 
        public char SeperationChar { get => seperationChar; set => seperationChar = value; }
        private string _fileDir = "LogFiles/";
        private string[] _dataFile;
        private char seperationChar = '\t';
        private readonly string _fileName;
        private string _lastStringAdded;
        public string FileLocation => _fileDir + _fileName;
        public bool HasHeader { get; set; } = true;

        public LogFile(string fileName, FileLocationType location)
        {
            _fileName = fileName;
            FileLocationType = location;
            //  CopyFileToLocalDirectory();
        }
        public LogFile(string fileName,string url, FileLocationType location)
        {
            Url = url;
            _fileName = fileName;
            FileLocationType = location;
            //  CopyFileToLocalDirectory();
        }

        private void CopyFileToLocalDirectory()
        {
            // Use webclient to grab file and save a local copy of it.
            using (var client = new WebClient())
            {
                client.DownloadFile("https://pastebin.com/raw/N2TUeGnw", "LogFiles/example.txt");
            }
            FileLocationType = FileLocationType.Local;
        }

        public List<string> GetNewLines()
        {
            List<string> newLines= new List<string>();
            using (StreamReader sr = new StreamReader(_fileDir + _fileName))
            {
                bool isLastStringAdded = false;
                while (!sr.EndOfStream && !isLastStringAdded)
                {
                    string str = sr.ReadLine();
                    if (str == _lastStringAdded)
                    {
                        isLastStringAdded = true;
                    }
                    else
                    {
                        newLines.Add(sr.ReadLine());
                    }
                }
            }
            return newLines;
        }

        public string FormatSaveString(char sepC)
        {
            StringBuilder str = new StringBuilder();
            str.Append(this.ID+ sepC);
            str.Append(this.FileLocationType+ sepC);
            str.Append(this.FileLocation+ sepC);
            str.Append(this.SeperationChar+ sepC);
            str.Append(this.HasHeader);
            return str.ToString();
        }
    }
}
