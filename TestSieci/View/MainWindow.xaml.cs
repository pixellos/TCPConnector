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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServerViewModel serverViewModel;
        public MainWindow()
        {
           
            InitializeComponent();
            serverViewModel = new ServerViewModel(isConnectedLabel);
            
            
            this.Title = "Serwer";
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            serverViewModel.GetConnectionButtonClick(sender, e);
        }


        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            serverViewModel.SendText_Changed(textBox.Text);
        }
    }
}
