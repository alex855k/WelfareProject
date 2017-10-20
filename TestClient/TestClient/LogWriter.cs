using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TestClient
{
    /* 
     Class to simulate data being written to the logfile in real time.
     Loads data from external *.txt file just because it's easier to type in large amounts of data.
         */

    public class LogWriter
    {
        private Thread writer;
        private LogFile _logFile;
        private bool _running;
        private char defaultSeperationChar = ';';
        private string _testDataFilePath = "testData.txt";
        private int _lineCount;
        private List<string> _data = new List<string>();
        private readonly int _writeInterval;
        // If the end of the data file has been reached start over
        private int _dataLineCounter = 0;
        //Line count wont change after being set in constructor
        private readonly int _dataLineCount;

        public LogWriter(int writeSec, LogFile logFile)
        {
            _logFile = logFile;
            //Write interval in seconds
            _writeInterval = writeSec * 1000;
            //Pull test data that is to written to file
            FetchTestData(_logFile.SeperationChar);
            _dataLineCount = _data.Count;
        }

        private void FetchTestData(char logSeperationChar)
        {
            using (StreamReader sr = new StreamReader(_testDataFilePath))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    // Changing the seperation char for the test data to match the log files actual seperation char, if it's different from the def one.
                    if (defaultSeperationChar != logSeperationChar) str = str?.Replace(defaultSeperationChar, logSeperationChar);
                    _data.Add(str);
                }
            }
        }

        public void Start()
        {
            writer= new Thread(Run);
            writer.Start();
        }

        private void Run()
        {
            while (_running)
            {
            // For the purpose of this demonstration the data is being written to the logs quite frequently.
                if (!_logFile.InUse) {
                        Thread.Sleep(_writeInterval);
                        WriteToLog();
                    }
            }
        }

        private void WriteToLog()
        {
            /* Refactor to logfile instead and use lock monitor or mutex */
            _logFile.InUse = true;
            using (StreamWriter sw = new StreamWriter(_logFile.FileLocation))
            {
                if (_dataLineCounter + 2 > _dataLineCount) _dataLineCounter = 0;
                sw.WriteLine(_data[_dataLineCounter]);
                _dataLineCounter++;
                sw.WriteLine(_data[_dataLineCounter]);
                _dataLineCounter++;
            }
            _logFile.InUse = false;
        }

        public void Stop()
        {
            writer.Abort();    
        }
    }
}
