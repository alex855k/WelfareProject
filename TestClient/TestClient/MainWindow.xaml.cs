using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using TestClient.LogConverterService;
using TestClient.ViewModels;

namespace TestClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
	    private bool _running;
	    private DateTime _cutOffDateTime;
	    private Thread t;
	    private ApplicationViewModel _vm;
	    private LogFileRepository rep = new LogFileRepository();
        //private ObservableCollection<LogFile> LogFiles => rep.Logs;
	    private LogParser _parser;
		ServiceClient LC = new ServiceClient();
		public MainWindow()
		{
			InitializeComponent();
		    _vm = new ApplicationViewModel();
		    _vm.LogFileRep = rep; 
		    this.DataContext = _vm;
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

	    private void RunService()
	    {
	        bool firstUse = true;
	        while (_running)
	        {
	            Dispatcher.BeginInvoke(new Action(() =>
	            {
                    //Store collective data
                    List<string[]> collectiveData = new List<string[]>();
                    // collectiveData.Sort(l => l[0] > 0);

	                //send request to service
	                foreach (LogFile l in rep.Logs)
	                {
	                    string[][] data;
	                    if (firstUse)
	                    {
	                        data = LC.ParseFromFile(l.GetInitialData(_cutOffDateTime));
	                        firstUse = false;
	                    }
	                    else
	                    {
	                        data = LC.ParseFromFile(l.GetInitialData(_cutOffDateTime));
	                    }
	                    List<string[]> newData = new List<string[]>();
                        foreach (var line in data)
	                    {
	                        newData.Add(line);
                        }
	                    collectiveData.AddRange(newData);
                        // ***** Finish IMPLEMENTAION *****


                        // Sort data by date
                    }
                    
                 //store data in ParsedLogfile object

                //put new
            }));
	            Thread.Sleep(TimeSpan.FromSeconds(5));
            }
	    }
		private void StartService_Click(object sender, RoutedEventArgs e)
		{
            if(ComboBoxAlarmType.SelectionBoxItem != null && DatePicker.SelectedDate !=null && PersonName.Text != string.Empty)
            {
                _cutOffDateTime = DatePicker.SelectedDate.Value;
		        _running = true; 
		        t = new Thread(RunService);
                t.Start();
                MessageBox.Show("Service started");
            }
        }

	    private void StopService_Click(object sender, RoutedEventArgs e)
	    {
            t.Abort();
	        t = null;
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
