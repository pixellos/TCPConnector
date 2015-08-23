using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Common_Files
{
    public class Commands
    {
        const char StartCommandChar = ':';
        static Dictionary<string, Func<string, string>> CommandBind = new Dictionary<string, Func<string, string>>();
        const string COMMANDFAILED = "##NAZWA???###";
        public const string DONOTDISPLAY = ":DND!";

        public Commands() { AddToDict(); }

        public string Decode(string input)
        {
            if (input.ToCharArray()[0] == ':')
            {
                if (CommandBind.ContainsKey(input))
                {
                    Func<string, string> doit = CommandBind.Single(x => x.Key == input).Value;
                    if (doit != null)
                        return doit(input);
                }
                return COMMANDFAILED;
            }
            else return input;
        }

        /// <summary>
        /// You can override it to add new functions;
        /// </summary>
        public virtual void AddToDict()
        {
            Commands.CommandBind.Add(":ES", CommandDelegates.EmptyString);
            Commands.CommandBind.Add(":AYT?", CommandDelegates.YesThereAm);
        }
    }

    public static class CommandDelegates
    {
        public static string EmptyString(string text) { return ""; }
        public static string YesThereAm(string text) { return Commands.DONOTDISPLAY; }
    }
}
