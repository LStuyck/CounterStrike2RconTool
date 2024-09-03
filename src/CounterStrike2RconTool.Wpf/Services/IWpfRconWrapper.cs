namespace CounterStrike2RconTool.Wpf.Services
{
    public interface IWpfRconWrapper
    {
        (bool Connected, bool Authorized) Connect(string ip, int port, string password);
        string SendCommand(string command);
        void Close();
    }
}