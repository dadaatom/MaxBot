using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBot_V2
{
    class Commands
    {
        Dictionary<string, string> commandDict = new Dictionary<string, string>();
        public Commands()
        {
            commandDict.Add("help", "Defines commands, use .help [command] or .help");
            commandDict.Add("expand", "Expands words, use .help [phrase]");
            commandDict.Add("cursive", "Converts phrases to cursive, use .cursive [phrase]");
            commandDict.Add("translate", "Translates phrase to a different language, use .translate [language-symbol] [phrase]");
            //commandDict.Add("languages", "Gets language symbols, use .languages [language] or .languages");

        }

        public Dictionary<string, string> getCommands()
        {
            return commandDict;
        }
    }
}


