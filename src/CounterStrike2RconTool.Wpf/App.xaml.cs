using CounterStrike2RconTool.Shared.Services;
using CounterStrike2RconTool.Wpf.Options;
using CounterStrike2RconTool.Wpf.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CounterStrike2RconTool.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;

            var builder = Host.CreateDefaultBuilder();

            builder.ConfigureServices((ctx, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<IRconWrapper, RconWrapper>();
                services.AddSingleton<IWpfRconWrapper, WpfRconWrapper>();
                services.AddSingleton<IUpdateConfiguration, UpdateConfiguration>();

                services
                    .AddOptions<RconWpfOptions>()
                    .Bind(ctx.Configuration.GetSection(RconWpfOptions.Key));
            });

            AppHost = builder.Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("An unhandled exception occurred: {0}", e.Exception.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            // OR whatever you want like logging etc. MessageBox it's just example
            // for quick debugging etc.
            e.Handled = true;
        }
    }
}
