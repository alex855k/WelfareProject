﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using Microsoft.Win32;
using TestClient.LogConverterService;

namespace TestClient
{

    //The responsibility of this class is to read the data and return only the data that is nessecary as a string array.
    public class LogReader
    {
        private IService _logService;
        private Thread _reader;
        private int _frequencySec = 5;
        private DateTime _cutoffDateTime;
        private LogFile _logFile;
        private readonly char[] specialChars = { '\t', ';' };
        // Regex for finding date column.
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
        private string _firstLine;
        private bool _running;

        /* Reader for URI fileDirectory is where it should be stored once copied locally, filename is the filename on the domain
         * Implemented this as a second option as it wasn't specified where the file would be coming from.
         * However our test solution will be simulating fake data being written to a log
         */
        public LogReader(LogFile log, DateTime readFromDateTime, IService logService)
        {
            _logService = logService;
            // REMOVE
            _logService = new ServiceClient();
            _logFile = log;
            _cutoffDateTime = readFromDateTime;
            InitializeFileToReader();
        }

        public LogReader(LogFile log)
        {
            _logFile = log;
            InitializeFileToReader();
        }

        private void InitializeFileToReader()
        {
            // Split all lines of data into a string a array
            _logFile.LogData = File.ReadAllLines(_logFile.FileLocation, Encoding.GetEncoding("iso-8859-1"));
            // seperation character is found here if it wasn't already located
            if (_logFile.SeperationChar == ' ') _logFile.SeperationChar = FindSeperationChar(_logFile.LogData);
            _dateColumnIndex = FindDateColumnIndex(_logFile.LogData);
        }


        private int FindDateColumnIndex(string[] dataFile)
        {
            int countLines = 0;
            foreach (var line in dataFile)
            {
                // For every line of data split the data into columns
                string[] columns = line.Split(_logFile.SeperationChar);
                
                    for (int columnCount = 0; columnCount < columns.Length; columnCount++)
                    {
                        // If it is finding a date datatype matching the regex in one of the columns at row index 0, it must mean it has no headers.
                         Match m = _dateRegex.Match(columns[columnCount]);
                        if (m.Success)
                        {
                            if (countLines == 0) _logFile.HasHeader = false;
                            return columnCount;
                        }
                    }
                }
            return -1;
        }

        public char FindSeperationChar(string[] data)
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

        private string[][] GetNewLines()
        {
            return _logService.ParseFromFile(_logFile.GetNewLines().ToArray());
        }

        private string[] GetData()
        {
            // If file location is still remote fetch a copy and store it locally and set the filepath
           // MOVE THIS  if (FileLocationType == FileLocation.URI) CopyFileToLocalDirectory();

            if (File.Exists(_logFile.FileLocation)) { 
                         
                // string array containing the needed strings to send over the network.
                List<string> parseAbleData = new List<string>();

                foreach (var line in _logFile.LogData)
                {
                    // For every line of data split the data into columns
                    string[] columns = line.Split(_logFile.SeperationChar);
                    // If date is before the cutoff date
                    if (DateTime.Compare(Convert.ToDateTime(columns[_dateColumnIndex]), _cutoffDateTime) > -1)
                    {
                        return parseAbleData.ToArray();
                    }
                    parseAbleData.Add(columns[_dateColumnIndex]);
                }
            }
            throw new Exception("File not located!");
        }
    }
}