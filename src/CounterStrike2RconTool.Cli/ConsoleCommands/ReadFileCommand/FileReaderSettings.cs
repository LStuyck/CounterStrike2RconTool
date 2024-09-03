using Spectre.Console.Cli;
using System.ComponentModel;

namespace CounterStrike2RconTool.Cli.ConsoleCommands.ReadFileCommand
{
    public class FileReaderSettings : CommandSettings
    {
        [Description("The name of the file to read. Defaults to commands.txt.")]
        [CommandOption("--file <FILENAME>")]
        public string FileName { get; set; }
    }
}
