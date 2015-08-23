using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Common_Files
{
    public class Commands
    {
        const char StartCommandChar = ':';
        static Dictionary<string, Func<string,string>> CommandBind = new Dictionary<string, Func<string,string>>();
        const string COMMANDFAILED = "##NAZWA???###";

        public Commands()
        {
            AddToDict();
        }
        
        public string Decode(string input)
        {
            if (input.ToCharArray()[0] == ':')
            {
                if (CommandBind.ContainsKey(input))
                {
                    Func<string,string> doit = CommandBind.Single(x => x.Key == input).Value;
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
            Commands.CommandBind.Add(":ES", EmptyString);
        }

        string EmptyString(string text) { return ""; }
    }
}
