using CounterStrike2RconTool.Cli.Enums;
using CounterStrike2RconTool.Shared.Services;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CounterStrike2RconTool.Cli.ConsoleCommands.ExecOnlinerCommand
{
    public class ExecOnlinerCommand : Command<ExecOnlinerSettings>
    {
        private readonly IConfiguration _configuration;
        private readonly IRconWrapper _rconWrapper;

        private string _ip;
        private int _port;
        private string _password;

        public ExecOnlinerCommand(IConfiguration configuration, IRconWrapper rconWrapper)
        {
            _configuration = configuration;
            _rconWrapper = rconWrapper;
        }

        public override int Execute(CommandContext context, ExecOnlinerSettings settings)
        {
            try
            {
                _ip = _configuration.GetValue<string>("RconWrapperSettings:DefaultConfiguration:Ip");
                _port = _configuration.GetValue<int>("RconWrapperSettings:DefaultConfiguration:Port");
                _password = _configuration.GetValue<string>("RconWrapperSettings:DefaultConfiguration:Password");

                _rconWrapper.Connect(_ip, _port, _password);

                var commands = settings.Command;
                if (!string.IsNullOrWhiteSpace(commands))
                {
                    var commandResult = _rconWrapper.SendCommand(commands);
                }

                return ConsoleCommandResults.Success;
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);

                return ConsoleCommandResults.Fail;
            }
            finally
            {
                _rconWrapper.Close();
            }
        }
    }
}
