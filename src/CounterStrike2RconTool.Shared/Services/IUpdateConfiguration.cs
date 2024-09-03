namespace CounterStrike2RconTool.Shared.Services
{
    public interface IUpdateConfiguration
    {
        void Update<T>(string key, T value, string? environment = null);
    }
}