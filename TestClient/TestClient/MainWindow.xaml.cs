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
using TestClient.LogConverterService;
using TestClient.ViewModels;

namespace TestClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
	    private ApplicationViewModel _vm;
	    private LogFileRepository rep = new LogFileRepository();
        //private ObservableCollection<LogFile> LogFiles => rep.Logs;
	    public ObservableCollection<LogFile> LogFils { get; } = new ObservableCollection<LogFile>();
	    private LogParser _parser;
		ServiceClient LC = new ServiceClient();
		public MainWindow()
		{
			InitializeComponent();
		    _vm = new ApplicationViewModel();
		    _vm.LogFileRep = rep; 
		    this.DataContext = _vm;
            //LoadLogFiles();
		}

	    private void LoadLogFiles()
	    {
	        LogFile l = new LogFile();
	        l.Id = 1;
	        LogFile l2 = new LogFile();
	        l2.Id = 2;
            LogFils.Add(l);
            LogFils.Add(l2);
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
			bool? result = dlg.ShowDialog();

		    TextBoxFilePath.Text = dlg.FileName;

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
            //Initialize _parser if it's null otherwise read
           // _parser = new LogParser(_logFilePath, FileLocation.Local);

            // 
          //  FilterLog();

            //Initialize _parser
		    try
		    {
		      //  LogParser r = 
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
                LogFile logFile = new LogFile(TextBoxFilePath.Text, FileLocationType.Local, TextboxDescription.Text, TextBoxAlarmType.Text);
                rep.SaveLog(logFile);
                MessageBox.Show("Successfully added logfile");
            }
            else
            {
                MessageBox.Show("Couldn't add log");
            }
            
        }

        private void RemoveLogButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listView_LogFiles.SelectedItem != null)
                {
                    LogFile f = (LogFile) listView_LogFiles.SelectedItem;
                    if(f != null) rep.DeleteLogFile(f);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
            
        }
    }
}
