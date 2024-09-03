using CounterStrike2RconTool.Shared.Services;
using System.Windows;

namespace CounterStrike2RconTool.Wpf.Services
{
    public class WpfRconWrapper : IWpfRconWrapper
    {
        private readonly IRconWrapper _rconWrapper;

        public WpfRconWrapper(IRconWrapper rconWrapper)
        {
            _rconWrapper = rconWrapper;
        }

        public (bool Connected, bool Authorized) Connect(string ip, int port, string password)
        {
            return _rconWrapper.Connect(ip, port, password);
        }

        public string SendCommand(string command)
        {
            var commandResult = _rconWrapper.SendCommand(command);

            //if (!string.IsNullOrWhiteSpace(commandResult))
            //{
            //    MessageBox.Show(commandResult, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}

            return commandResult;
        }

        public void Close()
        {
            _rconWrapper.Close();
        }
    }
}
