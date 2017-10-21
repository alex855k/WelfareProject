using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
	    private LogFileRepository rep = new LogFileRepository();
	    private ObservableCollection<LogFile> LogFiles => rep.Logs;
	    private LogReader reader;
		LogConverterService.ServiceClient LC = new LogConverterService.ServiceClient();
		public MainWindow()
		{
		    LoadLogFiles();
			InitializeComponent();
		}

	    private void LoadLogFiles()
	    {
	        rep.LoadLogFiles();
	    }

	    private void LoadLogReaders()
	    {
	       // using()
	    }

	    private void LogFile_Click(object sender, RoutedEventArgs e)
		{
			// Create OpenFileDialog 
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



			// Set filter for file extension and default file extension 
			dlg.DefaultExt = ".txt";
			dlg.Filter = "TXT Files (*.txt)|*.txt";


			// Display OpenFileDialog by calling ShowDialog method 
			Nullable<bool> result = dlg.ShowDialog();


			// Get the selected file name and display in a TextBox 
			if (result == true)
			{
				// Open document 
				string filename = dlg.FileName;
			   // _logFilePath = filename;
				StartService.IsEnabled = true;
				lbl_Warning.Content = "";
			}
		}

		private void StartService_Click(object sender, RoutedEventArgs e)
		{
            //Initialize reader if it's null otherwise read
           // reader = new LogReader(_logFilePath, FileLocation.Local);

            // 
          //  FilterLog();

            //Initialize reader
		    try
		    {
		      //  LogReader r = 
            }
		    catch (Exception exception)
		    {
                // Writing exceptions to log
		        EventLog.WriteEntry("", exception.Message);
		        throw;
		    }
		  

		//	string[] file = System.IO.File.ReadAllLines(LogFilePath);
			//var ParsedLog = LC.ParseLog(file);
			
		/*	foreach(string[] line in ParsedLog)
			{
				lstBx_Alarms.Items.Add(line.ToString());
			}
            */
		}

	    private void StopService_Click(object sender, RoutedEventArgs e)
	    {

	    }

        private void AddLog_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(TextBoxFilePath.Text) && TextboxDescription.Text != string.Empty &&
                TextBoxAlarmType.Text != string.Empty)
            {
                LogFile logFile = new LogFile(TextBoxFilePath.Text, TextboxDescription.Text, TextBoxAlarmType.Text);
                rep.SaveLog(logFile);
                MessageBox.Show("Successfully added logfile");
            }
            else
            {
                MessageBox.Show("Couldn't add log");
            }
            
        }
    }
}
