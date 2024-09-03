// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;
using Spectre.Console.Cli;
using Microsoft.Extensions.Configuration;
using CounterStrike2RconTool.Cli.ConsoleCommands.ReadFileCommand;
using CounterStrike2RconTool.Cli.ConsoleCommands.UpdateServerInfoCommand;
using CounterStrike2RconTool.Cli.ConsoleCommands.ExecOnlinerCommand;
using CounterStrike2RconTool.Shared.Services;
using CounterStrike2RconTool.Cli.Common;

AnsiConsole.Write(new FigletText("CS2 RCON TOOL"));
AnsiConsole.WriteLine("CS2 RCON TOOL CLI");
AnsiConsole.WriteLine();

var builder = Host.CreateDefaultBuilder(args);

// Add services to the container
builder.ConfigureServices(services =>
{
    services.AddSingleton<IRconWrapper, RconWrapper>();
    services.AddSingleton<IUpdateConfiguration, UpdateConfiguration>();
});

builder.ConfigureAppConfiguration(conf =>
{
    conf.AddJsonFile("appsettings.json", false, true);
});

var registrar = new TypeRegistrar(builder);
var app = new CommandApp(registrar);

app.Configure(config =>
{
    // Register available commands
    config
        .AddCommand<FileReaderCommand>("readfile")
        .WithDescription("Read file with commands to execute")
        .WithExample(new[] { "readfile", "--file commands.txt" });

    config
        .AddCommand<ExecOnlinerCommand>("exec")
        .WithDescription("Executes the oneliner command")
        .WithExample(new[] { "exec", "--command \"the command to exec\"" });

    config
        .AddCommand<UpdateServerInfoCommand>("updateinfo")
        .WithDescription("Updates the ip, port and password config")
        .WithExample(new[] { "updateinfo", "--ip \"172.30.240.1\"", "--port 27015", "--password \"1234\"" });
});

return app.Run(args);