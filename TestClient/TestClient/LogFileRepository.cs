using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    public class LogFileRepository
    {
        private DirectoryInfo repDir;
        private char seperationChar = ';';
        private const string idPath = "currentid.txt";
        public Dictionary<int, LogFile> _logs;
        
        
        public LogFileRepository()
        {
            InitRepository();
        }

        private void InitRepository()
        {
            repDir = new DirectoryInfo("LogFiles/");
            DirectoryExists();
            LoadLogFiles();
        }

        public string FormatFileName(int id)
        {
            return "log" + id + ".txt";
        }

        private void LoadLogFiles()
        {
            _logs.Clear();
            {
                foreach (var path in repDir.GetFiles("log*.txt"))
                {
                    LoadLog(path.Name);
                }
            }
        }

        private void LoadLog(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    // 0 
                    sr.ReadLine().Split(seperationChar);
                    _logs.Add(LogFile());
                }
            }
        }

        private void DirectoryExists()
        {
            if (!repDir.Exists)
            {
                repDir.Create();
            }
        }


        public LogFile GetLogFile(int id)
        {
            return _logs[id];
        }


        private void SaveLog(LogFile logfile)
        {
            using (StreamWriter wr = new StreamWriter(repDir + FileName(s))
            {

                wr.WriteLine(logfile.ToString());
            }
        }
    
}
