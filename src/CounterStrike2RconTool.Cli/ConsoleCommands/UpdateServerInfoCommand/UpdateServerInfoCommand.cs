using CounterStrike2RconTool.Cli.Enums;
using CounterStrike2RconTool.Shared.Services;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Text.Json;

namespace CounterStrike2RconTool.Cli.ConsoleCommands.UpdateServerInfoCommand
{
    public class UpdateServerInfoCommand : Command<UpdateServerInfoSettings>
    {
        private readonly IConfiguration _configuration;
        private readonly IUpdateConfiguration _updateConfiguration;

        public UpdateServerInfoCommand(IConfiguration configuration, IUpdateConfiguration updateConfiguration)
        {
            _configuration = configuration;
            _updateConfiguration = updateConfiguration;
        }

        public override int Execute(CommandContext context, UpdateServerInfoSettings settings)
        {
            try
            {
                _updateConfiguration.Update("RconWrapperSettings:DefaultConfiguration:Ip", settings.Ip);
                _updateConfiguration.Update("RconWrapperSettings:DefaultConfiguration:Port", settings.Port);
                _updateConfiguration.Update("RconWrapperSettings:DefaultConfiguration:Password", settings.Password);

                return ConsoleCommandResults.Success;
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);

                return ConsoleCommandResults.Fail;
            }
        }
    }
}
