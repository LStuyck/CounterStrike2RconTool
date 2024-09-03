using CounterStrike2RconTool.Shared.Services;
using CounterStrike2RconTool.Wpf.Options;
using CounterStrike2RconTool.Wpf.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Windows;

namespace CounterStrike2RconTool.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IConfiguration _configuration;
        private readonly IWpfRconWrapper _wpfRconWrapper;
        private readonly IUpdateConfiguration _updateConfiguration;
        private readonly IOptions<RconWpfOptions> _options;

        public MainWindow(IConfiguration configuration, IWpfRconWrapper wpfRconWrapper, IUpdateConfiguration updateConfiguration, IOptions<RconWpfOptions> options)
        {
            InitializeComponent();

            _configuration = configuration;
            _wpfRconWrapper = wpfRconWrapper;
            _updateConfiguration = updateConfiguration;
            _options = options;

            InitValues();
        }

        private void InitValues()
        {
            ipTb.Text = _options.Value.DefaultConfiguration.Ip;
            portTb.Text = _options.Value.DefaultConfiguration.Port.ToString();
            passwordTb.Text = _options.Value.DefaultConfiguration.Password;

            var maps = _options.Value.MapConfiguration.Maps;
            mapsCb.ItemsSource = maps;
            mapsCb.SelectedIndex = 0;

            var gameModes = _options.Value.GameModeConfiguration.GameModes;
            gameModesCb.ItemsSource = gameModes;
            gameModesCb.SelectedIndex = 0;
        }

        private void connectRconBtn_Click(object sender, RoutedEventArgs e)
        {
            var ip = ipTb.Text;
            var port = portTb.Text;
            var password = passwordTb.Text;

            _updateConfiguration.Update("RconWpfSettings:DefaultConfiguration:Ip", ip);
            _updateConfiguration.Update("RconWpfSettings:DefaultConfiguration:Port", port);
            _updateConfiguration.Update("RconWpfSettings:DefaultConfiguration:Password", password);

            var connectionResult = _wpfRconWrapper.Connect(ip, int.Parse(port), password);

            rconConnectedCb.IsChecked = connectionResult.Connected;
            rconAuthenticatedCb.IsChecked = connectionResult.Authorized;

            if (connectionResult.Connected && connectionResult.Authorized)
            {
                EnableComponentsAfterConnect();
            }
        }

        private void EnableComponentsAfterConnect()
        {
            mapsCb.IsEnabled = true;

            changeMapBtn.IsEnabled = true;
            restartGameBtn.IsEnabled = true;

            gameModesCb.IsEnabled = true;
            changeGameModeBtn.IsEnabled = true;

            addBotCtBtn.IsEnabled = true;
            addBotTBtn.IsEnabled = true;
            kickAllBotsBtn.IsEnabled = true;

            manualCommandTb.IsEnabled = true;
            sendManualCommandBtn.IsEnabled = true;
        }

        private void changeMapBtn_Click(object sender, RoutedEventArgs e)
        {
            var map = mapsCb.SelectedValue.ToString();

            if (!string.IsNullOrWhiteSpace(map) && mapsCb.SelectedIndex != 0)
            {
                logOutputTb.Text = _wpfRconWrapper.SendCommand($"map {map}");
            }
        }

        private void restartGameBtn_Click(object sender, RoutedEventArgs e)
        {
            logOutputTb.Text = _wpfRconWrapper.SendCommand("mp_restartgame 5");
        }

        private void changeGameModeBtn_Click(object sender, RoutedEventArgs e)
        {
            var gameMode = gameModesCb.SelectedValue.ToString();

            if(!string.IsNullOrWhiteSpace(gameMode) && gameModesCb.SelectedIndex != 0)
            {
                switch (gameMode)
                {
                    case "Competitive":
                        logOutputTb.Text = _wpfRconWrapper.SendCommand("game_type 0; game_mode 1; mp_restartgame 5;");
                        break;
                    case "Deathmatch":
                        logOutputTb.Text = _wpfRconWrapper.SendCommand("game_type 1; game_mode 2; mp_restartgame 5;");
                        break;
                    case "Gungame":
                        logOutputTb.Text = _wpfRconWrapper.SendCommand("game_type 1; game_mode 0; mp_restartgame 5;");
                        break;
                    case "Wingman":
                        logOutputTb.Text = _wpfRconWrapper.SendCommand("game_type 0; game_mode 2; mp_restartgame 5;");
                        break;
                    default:
                        break;
                }
            }
        }

        private void addBotCtBtn_Click(object sender, RoutedEventArgs e)
        {
            logOutputTb.Text = _wpfRconWrapper.SendCommand("bot_add_ct");
        }

        private void addBotTBtn_Click(object sender, RoutedEventArgs e)
        {
            logOutputTb.Text = _wpfRconWrapper.SendCommand("bot_add_t");
        }

        private void kickAllBotsBtn_Click(object sender, RoutedEventArgs e)
        {
            logOutputTb.Text = _wpfRconWrapper.SendCommand("bot_kick");
        }

        private void sendManualCommandBtn_Click(object sender, RoutedEventArgs e)
        {
            var manualCommand = manualCommandTb.Text;
            if(!string.IsNullOrWhiteSpace(manualCommand))
            {
                logOutputTb.Text = _wpfRconWrapper.SendCommand(manualCommand);
            }
        }
    }
}