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
		ServiceClient LC = new ServiceClient();
		public MainWindow()
		{
			InitializeComponent();
			Alarms.Items.Add("Test");
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Alarms_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
