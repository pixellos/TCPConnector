﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Common
{
    public class Commands
    {
        #region Definitions of commands
        public const string StringIsEmptyCommand =":ES";
        public const string IsTherePartnerCommand = ":AYT?";
        public const string ShowMessangeBoxCommand = ":MBShow";
        #endregion
        const char StartCommandChar = ':';
        static Dictionary<string, Func<string,string>> _CommandBind = new Dictionary<string, Func<string,string>>();
        const string COMMANDFAILED = "##COMMAND???###";
        string _LastMessage;

        public Commands()
        {
            AddToDict();
        }
        
        public string Decode(string input)
        {
            if (input != String.Empty && input.ToCharArray()[0] == ':')
            {
                if (_CommandBind.ContainsKey(input))
                {
                    Func<string, string> doit = _CommandBind.Single(x => x.Key == input).Value;
                    return doit(input);
                }
                return COMMANDFAILED;
            }
            else
            {
                _LastMessage = input;
                return input;
            }
        }

        /// <summary>
        /// You can override it to add new functions;
        /// </summary>
        public virtual void AddToDict()
        {
            Commands._CommandBind.Add(Commands.StringIsEmptyCommand, EmptyString);
            Commands._CommandBind.Add(Commands.IsTherePartnerCommand, ReturnLastText);
            Commands._CommandBind.Add(Commands.ShowMessangeBoxCommand, MessangeBoxShow);
        }

        string EmptyString(string text) { return ""; }
        string MessangeBoxShow(string text)
        {
            MessageBox.Show("Rozpoznano komendę :MBShow, wyświetliłem się :D");
            return "";
        }
        string ReturnLastText(string text) { return _LastMessage; }
    }
}
