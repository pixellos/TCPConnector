using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Common
{
    public class LogManager
    {
        private string _Logs;

        public string Logs
        {
            get { return _Logs; }
            set { _Logs = value; }
        }
        
        public void ToFile()
        {
            using (StreamWriter writeStream = new StreamWriter("logsClient.txt"))
            {
                writeStream.Write(_Logs);
            }
        }
        
    }
}
