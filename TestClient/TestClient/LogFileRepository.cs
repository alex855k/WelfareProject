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
            Logs.Clear();
                foreach (var path in repDir.GetFiles("log*.txt"))
                {
                    Logs.Add(LoadLog(path.Name));
                }
            return Logs;
        }

        public void DeleteLogFile(LogFile l)
        {
            long id = l.ID;
            File.Delete(repDir + FormatFileName(id));
            Logs.Remove(Logs.First(log => log.ID == id));
        }

        public void DeleteLogFile(long id)
        {
            try
            {
                File.Delete(repDir + FormatFileName(id));
                Logs.Remove(Logs.First(log => log.ID == id));
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
                    //Implement
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
            l.ID = id;

            FileLocationType fl;
            Enum.TryParse(data[1], out fl);
            l.FileLocationType = fl;

            string fileLocation = data[2];
            l.FileLocation = fileLocation;

            char sepC = char.Parse(data[3]);
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
                wr.WriteLine(logfile.FormatSaveString(seperationChar));
            }
        }
    }
}
