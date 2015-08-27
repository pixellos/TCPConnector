using System.IO;
using System.Net.Sockets;
using System.Net;
using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common
{

    /// <summary>
    /// If u want get Backgroundworker at loop, at updateGui cal (sender as BackgroundWorker).RunWorkerAsync();
    /// </summary>         
    public class BackgroundConnectionHelper
    {
        BackgroundWorker backgroundWorker;

        public BackgroundConnectionHelper(DoWorkEventHandler AsyncDelegate, RunWorkerCompletedEventHandler GUIDelegate, ProgressChangedEventHandler ProgressChanged)
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += AsyncDelegate;
            backgroundWorker.RunWorkerCompleted += GUIDelegate;
            backgroundWorker.ProgressChanged += ProgressChanged;
        }

        public BackgroundConnectionHelper(DoWorkEventHandler AsyncDelegate, RunWorkerCompletedEventHandler GUIDelegate)
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += AsyncDelegate;
            backgroundWorker.RunWorkerCompleted += GUIDelegate;
        }

        public void Start() { backgroundWorker.RunWorkerAsync(); }

        public void Stop() { backgroundWorker.CancelAsync(); }

        public bool IsReady
        {
            get
            {
                return backgroundWorker.IsBusy;
            }
        }

        
    }
}
