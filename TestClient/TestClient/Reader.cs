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

    public class Reader
    {
        //The responsibility of this class is to read the data and return only the data that is nessecary as a string array.
        private FileLocation _fileLocationType;
        private string _fileName;
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

        public string LocalDirectory { get; set; }

        public Reader(string localDirectory)
        {
            LocalDirectory = localDirectory;
        }

        public string[] GetData(DateTime cutoffDateTime)
        {
            // If file location is still remote fetch a copy and store it locally and set the filepath
            if (_fileLocationType == FileLocation.URI) CopyFileToLocalDirectory();

            if (_filePath != null)
            {
                // seperation character is stored here, just using a default character
                char seperationChar = ' ';
                // string array containing the needed strings to send over the network.
                List<string> parseAbleData = new List<string>();

                // Split all lines of data into a string arr
                string[] dataFile = File.ReadAllLines(_filePath, Encoding.GetEncoding("iso-8859-1"));
                int countLines = 0;
                int dateColumnIndex = 0;
                foreach (var line in dataFile)
                {
                    foreach (var specialC in specialChars)
                    {
                        if (line.Contains(specialC))
                        {
                            seperationChar = specialC;
                        }
                    }
                    // OBS might need to be changed, usually logs don't have headers.
                    if (countLines > 0)
                    {
                        // For every line of data split the data into columns
                        string[] columns = line.Split(seperationChar);
                        // If it's line one try to find the column where the date is located
                        if (countLines == 1) {
                            for (int columnCount = 0; columnCount < columns.Length; columnCount++)
                            {
                                Match m = _dateRegex.Match(columns[columnCount]);
                                if (m.Success)
                                {
                                    dateColumnIndex = columnCount;
                                }
                            }
                        }

                        // If date is before the cutoff date
                        if (DateTime.Compare(Convert.ToDateTime(columns[dateColumnIndex]), cutoffDateTime) > -1)
                        {
                            return parseAbleData.ToArray();
                        }
                        parseAbleData.Add(columns[dateColumnIndex]);
                    }
                }
            }
            throw new Exception("File not located!");
        }

        public string _filePath { get; private set; }

        private void CopyFileToLocalDirectory()
        {
            //throw new NotImplementedException();
            // Implement this yourselves
            _fileLocationType = FileLocation.Local;
            // Use webclient to grab file for instance.
            using (var client = new WebClient())
            {
                client.DownloadFile("https://pastebin.com/raw/N2TUeGnw", "example.txt");
            }
        }
    }
}