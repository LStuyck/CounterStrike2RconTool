using CounterStrike2RconTool.Cli.Enums;
using CounterStrike2RconTool.Cli.Extensions;
using CounterStrike2RconTool.Shared.Services;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CounterStrike2RconTool.Cli.ConsoleCommands.ReadFileCommand
{
    public class FileReaderCommand : Command<FileReaderSettings>
    {
        private readonly IConfiguration _configuration;
        private readonly IRconWrapper _rconWrapper;
        private string _ip;
        private int _port;
        private string _password;

        public FileReaderCommand(IConfiguration configuration, IRconWrapper rconWrapper)
        {
            _configuration = configuration;
            _rconWrapper = rconWrapper;
        }

        public override int Execute(CommandContext context, FileReaderSettings settings)
        {
            try
            {
                _ip = _configuration.GetValue<string>("RconWrapperSettings:DefaultConfiguration:Ip");
                _port = _configuration.GetValue<int>("RconWrapperSettings:DefaultConfiguration:Port");
                _password = _configuration.GetValue<string>("RconWrapperSettings:DefaultConfiguration:Password");

                _rconWrapper.Connect(_ip, _port, _password);

                var fileName = settings.FileName ?? _configuration.GetValue<string>("RconWrapperSettings:FileReader:DefaultFileName");
                var commandsDirectoryName = _configuration.GetValue<string>("RconWrapperSettings:FileReader:RconCommandsDirectoryName");
                var filePath = Path.Combine(commandsDirectoryName, fileName);

                var commands = File.ReadAllLines(filePath);

                var table = new Table();
                table.AddColumn("Command");
                table.AddColumn("State");

                double percentageIncrement = 100.0 / commands.Length;

                AnsiConsole
                    .Progress()
                    .AutoRefresh(true) // Turn off auto refresh
                    .AutoClear(false)   // Do not remove the task list when done
                    .HideCompleted(false)   // Hide tasks as they are completed
                    .Columns(new ProgressColumn[]
                    {
                        new TaskDescriptionColumn(),    // Task description
                        new ProgressBarColumn(),        // Progress bar
                        new PercentageColumn(),         // Percentage
                        new RemainingTimeColumn(),      // Remaining time
                        new SpinnerColumn(),            // Spinner
                    })
                    .Start(ctx =>
                    {
                        var progressTask = ctx.AddTask("[green]Executing commands[/]");

                        foreach (var command in commands)
                        {
                            try
                            {
                                if (!string.IsNullOrWhiteSpace(command))
                                {
                                    var commandResult = _rconWrapper.SendCommand(command);
                                    table.AddRow(command.Trim().Truncate(30), string.IsNullOrWhiteSpace(commandResult) ? "OK" : commandResult);
                                    progressTask.Increment(percentageIncrement);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    });

                table.Expand();

                AnsiConsole.Write(table);

                var consolePath = new TextPath(filePath);
                AnsiConsole.WriteLine();
                AnsiConsole.Write(consolePath);
                AnsiConsole.WriteLine();

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
