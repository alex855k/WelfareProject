using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestClient.Helpers;

namespace TestClient
{
    public class LogFile : ObservableObject
    {
        private readonly char[] specialChars = { '\t', ';' };
        // Regex for finding date column.
        private static string _dateRegexExp =
                // DD-MM-YYYY 
                "((?:(?:[0-2]?\\d{1})|(?:[3][01]{1}))[-:\\/.](?:[0]?[1-9]|[1][012])[-" +
                ":\\/.](?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3})))(?![\\d])" +
                // White Space
                "(\\s+)" +
                // Hour:Minute:Sec
                "((?:(?:[0-1][0-9])|(?:[2][0-3])|(?:[0-9])):(?:[0-5][0-9])(?::[0-5][0-9])?(?:\\s?(?:am|AM|pm|PM))?)"
            ;
        private readonly Regex _dateRegex = new Regex(_dateRegexExp, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public long Id { get; set; } 
        public string Description { get; set; }
        public string AlarmType { get; set; }
        public Object _logfileLock = new Object();
        public bool InUse { get; set; }
        public FileLocationType FileLocationType { get; set; }
        public string Url { get; set; }

        private int _dateColumnIndex;
        public string[] LogData { get; set; } 
        public char SeperationChar { get; set; } = ';';
        private string _fileDir = "LogFiles/";
        private string[] _dataFile;
        public string FileName { get; }
        private string _lastStringAdded;
        public string FileLocation
        {
            get;
            set;
        }
        public bool HasHeader { get; set; } = true;
        public LogFile()
        {
        }
        public LogFile(string fileLocation, FileLocationType location, string description, string alarmtype)
        {
            Description = description;
            AlarmType = alarmtype;
            FileLocation = fileLocation;
            FileLocationType = location;
           if (SeperationChar == ' ') SeperationChar = FindSeperationChar(LogData);
          // _dateColumnIndex = FindDateColumnIndex(LogData);
            GetAllLogData();
        }

        private int FindDateColumnIndex(string[] dataFile)
        {
            int countLines = 0;
            foreach (var line in dataFile)
            {
                // For every line of data split the data into columns
                string[] columns = line.Split(SeperationChar);

                for (int columnCount = 0; columnCount < columns.Length; columnCount++)
                {
                    // If it is finding a date datatype matching the regex in one of the columns at row index 0, it must mean it has no headers.
                    Match m = _dateRegex.Match(columns[columnCount]);
                    if (m.Success)
                    {
                        if (countLines == 0) HasHeader = false;
                        return columnCount;
                    }
                }
            }
            return -1;
        }

        private char FindSeperationChar(string[] data)
        {
            char seperationChar = ';';
            foreach (var specialC in specialChars)
            {
                if (data[0].Contains(specialC))
                {
                    seperationChar = specialC;
                }
            }
            return seperationChar;
        }

        private void GetAllLogData()
        {
            // Split all lines of data into a string a array
            LogData = File.ReadAllLines(FileLocation, Encoding.GetEncoding("iso-8859-1"));
        }

        /* public LogFile(string fileName,string url, FileLocationType location, string description, string alarmtype)
        {
            Url = url;
            FileName = fileName;
            FileLocationType = location;
            //  CopyFileToLocalDirectory();
        }
        */
        private void CopyFileToLocalDirectory()
        {
            // Use webclient to grab file and save a local copy of it.
            using (var client = new WebClient())
            {
                client.DownloadFile("https://pastebin.com/raw/N2TUeGnw", "LogFiles/example.txt");
            }
            FileLocationType = FileLocationType.Local;
        }

        public string[] GetInitialData(DateTime cutoffDateTime)
        {
            // If file location is still remote fetch a copy and store it locally and set the filepath
            // MOVE THIS  if (FileLocationType == FileLocation.URI) CopyFileToLocalDirectory();

            if (File.Exists(FileLocation))
            {

                // string array containing the needed strings to send over the network.
                List<string> parseAbleData = new List<string>();

                foreach (var line in LogData)
                {
                    // For every line of data split the data into columns
                    string[] columns = line.Split(SeperationChar);
                    // If date is before the cutoff date
                    if (DateTime.Compare(Convert.ToDateTime(columns[_dateColumnIndex]), cutoffDateTime) > -1)
                    {
                        return parseAbleData.ToArray();
                    }
                    parseAbleData.Add(columns[_dateColumnIndex]);
                }
            }
            throw new Exception("File not located!");
        }

        public string[] GetNewLines()
        {
            List<string> newLines= new List<string>();
            using (StreamReader sr = new StreamReader(_fileDir + FileName))
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
                        var rl = sr.ReadLine();
                        if (rl != null) newLines.Add(rl);
                    }
                }
            }
            return newLines.ToArray();
        }


        public string FormatSaveString(char sepC)
        {
            StringBuilder str = new StringBuilder();
            str.Append(Id);
            str.Append(sepC);
            str.Append(FileLocationType);
            str.Append(sepC);
            str.Append(FileLocation);
            str.Append(sepC);
            str.Append(SeperationChar);
            str.Append(sepC);
            str.Append(HasHeader);
            str.Append(sepC);
            str.Append(Description);
            str.Append(sepC);
            str.Append(AlarmType);
            return str.ToString();
        }
    }
}
