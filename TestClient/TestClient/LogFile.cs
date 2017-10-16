using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    public class LogFile
    {

        public FileLocation FileLocationType { get; private set; }

        public LogFile(string fileDirectory, string URL, FileLocation location)
        {
            FileDirectory = fileDirectory;
            Url = URL;
            FileLocationType = location;
            InitializeFileToReader();
        }


        // Reader constructor for a file that is already stored locally.
        public LogFile(string fileDirectory, FileLocation location)
        {
            FileDirectory = fileDirectory;
            FileLocationType = location;
            InitializeFileToReader();
        }

        private void IncrementIdCounter()
        {
            try
            {
                int logid;
                using (StreamReader sr = File.OpenText("RemoteLogFiles/idcounter.txt"))
                {
                    int.TryParse(sr.ReadLine(), out logid);
                }
                logid++;
                File.WriteAllText("RemoteLogFiles/idcounter.txt", String.Format("{0}", logid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
