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
using System.Threading;
using System.ComponentModel;    
using TestSieciKlient.Model;
using TestSieciKlient.ViewModel;
namespace TestSieciKlient
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClientViewModel clientViewModel;
        public MainWindow()
        {
            InitializeComponent();
            clientViewModel = new ClientViewModel(isConnected,recivedText);
        }
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            clientViewModel.StartClientClick(sender,e);           
        }

        private void textToSend_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
