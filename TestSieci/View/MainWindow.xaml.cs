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
using System.Net.Sockets;
using System.Net;
using System.IO;
using TestSieci;
using TestSieci.Model;
using TestSieci.ViewModel;

namespace TestSieci
{
    public partial class MainWindow : Window
    {
        ServerViewModel serverViewModel;
        public MainWindow()
        {
            InitializeComponent();
            serverViewModel = new ServerViewModel(isConnectedLabel);
            this.Title = "Server v0.1";
        }

        private void button_Click(object sender, RoutedEventArgs e) { serverViewModel.GetConnection(); }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (serverViewModel!=null)
            {
                serverViewModel.SendText_Changed(sender);
            }
        }
    }
}
