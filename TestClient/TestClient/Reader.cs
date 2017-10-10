using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace TestClient
{

    public enum FileLocation
    {
        Local,URI
    }

    //The responsibility of this class is to read the data and return only the data that is nessecary as a string array.
    public class Reader
    {
        private FileLocation _fileLocationType;

        public string FilePath => FileDirectory + FileName;
        public string FileName { get; }
        private string[] _dataFile;
        private bool _hasHeader = true;
        private char seperationChar = '\t';
        private readonly char[] specialChars = {'\t', ';'};
        private static string _dateRegexExp = 
                                    // DD-MM-YYYY 
                                    "((?:(?:[0-2]?\\d{1})|(?:[3][01]{1}))[-:\\/.](?:[0]?[1-9]|[1][012])[-"+
                                    ":\\/.](?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3})))(?![\\d])" +
                                    // White Space
                                    "(\\s+)"+
                                    // Hour:Minute:Sec
                                    "((?:(?:[0-1][0-9])|(?:[2][0-3])|(?:[0-9])):(?:[0-5][0-9])(?::[0-5][0-9])?(?:\\s?(?:am|AM|pm|PM))?)"
                                    ; 
        private readonly Regex _dateRegex = new Regex(_dateRegexExp, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private int _dateColumnIndex;

        public string FileDirectory { get; set; }

        public Reader(string fileDirectory, string fileName, FileLocation location)
        {
            FileDirectory = fileDirectory;
            FileName = fileName;
            InitializeFileToReader();
        }

        private void InitializeFileToReader()
        {
            // Split all lines of data into a string a array
            _dataFile = File.ReadAllLines(FilePath, Encoding.GetEncoding("iso-8859-1"));
            // seperation character is stored here, just using a default character
            seperationChar = GetSeperationChar(_dataFile);
            _dateColumnIndex = FindDateColumnIndex(_dataFile);
        }

        private int FindDateColumnIndex(string[] dataFile)
        {
            int countLines = 0;
            foreach (var line in dataFile)
            {
                // For every line of data split the data into columns
                string[] columns = line.Split(seperationChar);
                
                    for (int columnCount = 0; columnCount < columns.Length; columnCount++)
                    {
                        // If it's finding a date column index 0 it must mean it has no headers
                         Match m = _dateRegex.Match(columns[columnCount]);
                        if (m.Success)
                        {
                            if (countLines == 0) _hasHeader = false;
                            return columnCount;
                        }
                    }
                }
            return -1;
        }

        public char GetSeperationChar(string[] data)
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

        public string[] GetData(DateTime cutoffDateTime)
        {
            // If file location is still remote fetch a copy and store it locally and set the filepath
            if (_fileLocationType == FileLocation.URI) CopyFileToLocalDirectory();

            if (FilePath != string.Empty) { 
                         
                // string array containing the needed strings to send over the network.
                List<string> parseAbleData = new List<string>();

                foreach (var line in _dataFile)
                {
                    // For every line of data split the data into columns
                    string[] columns = line.Split(seperationChar);
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

        private void CopyFileToLocalDirectory()
        {
            //throw new NotImplementedException();
            // Implement this yourselves

            // Use webclient to grab file for instance.
            using (var client = new WebClient())
            {
                client.DownloadFile("https://pastebin.com/raw/N2TUeGnw", "example.txt");
            }
        }
    }
}