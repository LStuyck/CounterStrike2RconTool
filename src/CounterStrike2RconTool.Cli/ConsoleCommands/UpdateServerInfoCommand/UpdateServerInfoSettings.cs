using Spectre.Console.Cli;
using System.ComponentModel;

namespace CounterStrike2RconTool.Cli.ConsoleCommands.UpdateServerInfoCommand
{
    public class UpdateServerInfoSettings : CommandSettings
    {
        [Description("The IP of the server.")]
        [CommandOption("--ip <IP>")]
        public string Ip { get; set; }

        [Description("The port.")]
        [CommandOption("--port <PORT>")]
        public int Port { get; set; }

        [Description("The RCON password.")]
        [CommandOption("--password <PASSWORD>")]
        public string Password { get; set; }
    }
}
