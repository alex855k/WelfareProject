using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    public class LogFile
    {
        public Object _logfileLock = new Object();
        public bool InUse { get; set; }
        private string _fileDir = "LogFiles/";
        public FileLocationType FileLocationType { get; private set; }
        public string Url { get; }
        public string[] LogData { get; set; } 
        public char SeperationChar { get => seperationChar; set => seperationChar = value; }
        private string[] _dataFile;
        private char seperationChar = '\t';
        private string _fileName = "";
        public string FileLocation => _fileDir + _fileName;
        public bool HasHeader { get; set; } = true;

        public LogFile(string fileName, string URL, FileLocationType location)
        {
            Url = URL;
            _fileName = fileName;
            FileLocationType = location;
            //  CopyFileToLocalDirectory();
        }

        private void IncrementIdCounter()
        {
            try
            {
                int logid;
                using (StreamReader sr = File.OpenText("RemoteLogFiles/idcounter.txt"))
                {
                    int.TryParse(sr.ReadLine(), out logid);
                }
                logid++;
                File.WriteAllText("RemoteLogFiles/idcounter.txt", String.Format("{0}", logid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
    }
}
