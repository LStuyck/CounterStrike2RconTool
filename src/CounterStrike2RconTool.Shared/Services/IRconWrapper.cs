namespace CounterStrike2RconTool.Shared.Services
{
    public interface IRconWrapper
    {
        (bool Connected, bool Authorized) Connect(string ip, int port, string password);
        void Close();
        string SendCommand(string command);
    }
}