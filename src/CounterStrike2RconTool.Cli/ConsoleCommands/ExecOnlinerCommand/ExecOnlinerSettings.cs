using Spectre.Console.Cli;
using System.ComponentModel;

namespace CounterStrike2RconTool.Cli.ConsoleCommands.ExecOnlinerCommand
{
    public class ExecOnlinerSettings : CommandSettings
    {
        [Description("The command to exec")]
        [CommandOption("--command <COMMAND>")]
        public string Command { get; set; }
    }
}
