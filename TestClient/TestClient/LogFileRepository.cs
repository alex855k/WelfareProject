using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    public class LogFileRepository
    {
        private ObservableCollection<LogFile> _logs = new ObservableCollection<LogFile>();
        private DirectoryInfo repDir;
        private char seperationChar = ';';
        private long currentID;
        private const string idPath = "currentid.txt";


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

        private long NextId()
        {
            DirectoryExists();

            if (!File.Exists(repDir + idPath))
            {
                WriteCurrentIdToFile();
            }
            else
            {
                using (StreamReader sr = new StreamReader(repDir + idPath))
                {
                    string cID = sr.ReadLine();
                    sr.Close();
                    long.TryParse(cID, out currentID);
                    currentID++;
                    WriteCurrentIdToFile();
                }
            }
            return currentID;
        }

        private void WriteCurrentIdToFile()
        {
            using (StreamWriter sw = new StreamWriter(repDir + idPath))
            {
                sw.WriteLine(currentID);
                sw.Close();
            }
        }

        private string FormatFileName(long id)
        {
            return "log" + id + ".txt";
        }

        public ObservableCollection<LogFile> LoadLogFiles()
        {
            _logs.Clear();
                foreach (var path in repDir.GetFiles("log*.txt"))
                {
                    _logs.Add(LoadLog(path.Name));
                }
            return _logs;
        }

        private void DeleteLogFile(LogFile l)
        {
            File.Delete(repDir + l.);
        }
        private void DeleteLogFile()
        {
            File.Delete(repDir + );
        }

        private LogFile LoadLog(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    //Implement
                    string[] filedata = sr.ReadLine().Split(seperationChar);
                    //0
                    FileLocationType fl;
                    Enum.TryParse(filedata[0], out fl);
                    LogFile l = new LogFile(path, FileLocationType.Local);
                    sr.ReadLine().Split(seperationChar);
                    //_logs.Add(, );
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

        private void SaveLog(LogFile logfile)
        {
            long id;
            // if id = 0 means it's a new file then get next ID
            if (logfile.ID == 0)
            {
                id = logfile.ID;
            }
            else
            {
                id = NextId();
            }
            
            using (StreamWriter wr = new StreamWriter(repDir + FormatFileName(id)))
            {
                //Implement
                
                
                wr.WriteLine(logfile.SaveStringFormat());
            }
        }
    }
}
