using System;
using System.Collections.Generic;
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

namespace TestClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
<<<<<<< HEAD
		ServiceClient LC = new ServiceClient();
=======
		string LogFilePath;
		LogConverterService.ServiceClient LC = new LogConverterService.ServiceClient();
>>>>>>> cc98d3f54ea687212ce47bd75533c1202392a618
		public MainWindow()
		{
			InitializeComponent();
			Alarms.Items.Add("Test");
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
				LogFilePath = filename;
				StartService.IsEnabled = true;
				lbl_Warning.Content = "";
			}
		}

		private void StartService_Click(object sender, RoutedEventArgs e)
		{
			string[] file = System.IO.File.ReadAllLines(LogFilePath);
			var ParsedLog = LC.ParseLog(file);
			
			foreach(string[] line in ParsedLog)
			{
				lstBx_Alarms.Items.Add(line.ToString());
			}
		}

		private void Alarms_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
