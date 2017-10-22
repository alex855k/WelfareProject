using System;
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
    public class LogParser
    {
        private LogFileRepository _logrep;
        private IService _logService;
        private Thread _reader;
        private int _frequencySec = 5;
        private DateTime _cutoffDateTime;
        private LogFile _logFile;
 
        public string FileDirectory { get; set; }
        private string _firstLine;
        private bool _running;

        /* Reader for URI fileDirectory is where it should be stored once copied locally, filename is the filename on the domain
         * Implemented this as a second option as it wasn't specified where the file would be coming from.
         * However our test solution will be simulating fake data being written to a log
         */
        public LogParser(LogFile log, DateTime readFromDateTime, IService logService)
        {
            _logService = logService;
            _logFile = log;
            _cutoffDateTime = readFromDateTime;
        }

        public LogParser(LogFile log)
        {
            _logFile = log;
        }

        private void SendNewLines()
        {
            _logService.ParseFromFile(_logFile.GetNewLines());
            foreach (var log in _logrep.Logs)
            {

            }
        }
    }
}