using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClient.Helpers;

namespace TestClient.ViewModels
{
    public class ApplicationViewModel : ObservableObject
    {
        public LogFileRepository LogFileRep { set; private get; }
        private ObservableCollection<LogFile> _logs;
        public ObservableCollection<LogFile> LogFiles
        {
            get
            {
                if (_logs == null)
                {
                    _logs = LogFileRep.Logs;
                    //_logs = new ObservableCollection<LogFile>(){ new LogFile() { Id = 1, Description = "Log 2", FileLocation = "C:/dadadad/log1.txt"}, new LogFile() { Id = 2, Description = "Log 2", FileLocation = "C:/dadadad/log2.txt" } };
                }
                return _logs;
            }
        }
    }

}
