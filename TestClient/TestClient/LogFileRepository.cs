using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    public class LogFileRepository
    {
        private DirectoryInfo repDir;
        private char seperationChar = 'å';
        private long currentID;
        private const string idPath = "currentid.txt";
        public ObservableCollection<LogFile> Logs { get; } = new ObservableCollection<LogFile>();
        public LogFileRepository()
        {
            InitRepository();
        }

        private void InitRepository()
        {
            repDir = new DirectoryInfo("../../LogFiles/");
            DirectoryExists();
            currentID = GetCurrentId();
            LoadLogFiles();   
        }

        private long GetCurrentId()
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
                }
            }
            return currentID;
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
            Logs.Clear();
                foreach (var path in repDir.GetFiles("log*.txt"))
                {
                    string s = repDir+path.Name;
                    Logs.Add(LoadLog(s));
                }
            return Logs;
        }

        public void DeleteLogFile(LogFile l)
        {
            long id = l.Id;
            string str = repDir.FullName + FormatFileName(id);
            File.Delete(str);
            Logs.Remove(Logs.First(log => log.Id == id));
        }

        public void DeleteLogFile(long id)
        {
            try
            {
                File.Delete(repDir + FormatFileName(id));
                Logs.Remove(Logs.First(log => log.Id == id));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private LogFile LoadLog(string path)
        {
            LogFile f = null;
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] filedata = sr.ReadLine().Split(seperationChar);
                    Logs.Add(CreateLogFile(filedata));
                }
            }
            return f;
        }

        private LogFile CreateLogFile(string[] data)
        {
            LogFile l = new LogFile();

            long id;
            long.TryParse(data[0], out id);
            l.Id = id;

            FileLocationType fl;
            Enum.TryParse(data[1], out fl);
            l.FileLocationType = fl;

            string fileLocation = data[2];
            l.FileLocation = fileLocation;

            char sepC;
            char.TryParse(data[3], out sepC);
            l.SeperationChar = sepC;

            bool hasheader;
            bool.TryParse(data[4], out hasheader);

            string description = data[5];
            l.Description = description;

            string alarmtype = data[6];
            l.AlarmType = alarmtype;
       
            return l;
        }

        private void DirectoryExists()
        {
            if (!repDir.Exists)
            {
                repDir.Create();
            }
        }

        public void SaveLog(LogFile logfile)
        {
            if (logfile.Id == 0)
            {
               logfile.Id = NextId();
            }
            using (StreamWriter wr = new StreamWriter(repDir + FormatFileName(logfile.Id)))
            {
                wr.WriteLine(logfile.FormatSaveString(seperationChar));
            }
            Logs.Add(logfile);
        }
    }
}
