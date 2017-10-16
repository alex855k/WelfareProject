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
        private const string idPath = "currentid.txt";
        public Dictionary<int, LogFile> _logs;

        public LogFileRepository()
        {
            InitializeRepository();
        }

        private void InitializeRepository()
        {
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
                using (StreamReader sr = new StreamReader())
                {
                    foreach (var s in sr.ReadLine())
                    {
                    }
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


        public LogFile LoadLog(int id)
        {
            return FormatFileName(id);
        }


        private void SaveLog(LogFile logfile)
        {
            using (StreamWriter wr = new StreamWriter(repDir + FileName(s))
            {

                wr.WriteLine(logfile.ToString());
            }
        }
    
}
