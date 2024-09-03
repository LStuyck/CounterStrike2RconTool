using CounterStrike2RconTool.Shared.Enums;
using CounterStrike2RconTool.Shared.Exceptions;

namespace CounterStrike2RconTool.Shared.Services
{
    public class RconWrapper : IRconWrapper
    {
        private readonly string _serverIp;
        private readonly int _serverPort;
        private readonly string _serverPassword;

        private readonly RCONClient.RCONClient _rconClient;

        public RconWrapper()
        {
            _rconClient = new RCONClient.RCONClient();
        }

        public (bool Connected, bool Authorized) Connect(string ip, int port, string password)
        {
            if (string.IsNullOrWhiteSpace(ip) || port == default || string.IsNullOrWhiteSpace(password))
                throw new NotConfiguredException("Please update appsettings.json file.");

            _rconClient.Connect(ip, port);
            _rconClient.Authorize(password);

            return (_rconClient.Connected, _rconClient.Authorized);
        }

        public string SetGameType(Games game)
        {
            switch (game)
            {
                case Games.Casual:
                    return SetGameTypePrivate(GameTypes.Is0, GameModes.Is0);
                case Games.Competitive:
                    return SetGameTypePrivate(GameTypes.Is0, GameModes.Is1);
                case Games.ScrimCompetitive2v2:
                    return SetGameTypePrivate(GameTypes.Is0, GameModes.Is2);
                case Games.ScrimCompetitive5v5:
                    return SetGameTypePrivate(GameTypes.Is0, GameModes.Is2);
                case Games.ArmsRace:
                    return SetGameTypePrivate(GameTypes.Is1, GameModes.Is0);
                case Games.Demolition:
                    return SetGameTypePrivate(GameTypes.Is1, GameModes.Is1);
                case Games.Deathmatch:
                    return SetGameTypePrivate(GameTypes.Is1, GameModes.Is2);
                case Games.Training:
                    return SetGameTypePrivate(GameTypes.Is2, GameModes.Is0);
                case Games.Custom:
                    return SetGameTypePrivate(GameTypes.Is3, GameModes.Is0);
                case Games.Cooperative:
                    return SetGameTypePrivate(GameTypes.Is4, GameModes.Is0);
                case Games.Skirmish:
                    return SetGameTypePrivate(GameTypes.Is5, GameModes.Is0);
                default:
                    throw new NotImplementedException();
            }
        }

        public string ChangeLevel(Maps map)
        {
            var stringMap = map.ToString();

            return SendCommand($"changelevel {stringMap}");
        }

        private string SetGameTypePrivate(GameTypes gameType, GameModes gameMode)
        {
            var intGameType = (int)gameType;
            var intGameMode = (int)gameMode;

            var result1 = SendCommand($"game_type {intGameType}");
            var result2 = SendCommand($"game_mode {intGameMode}");

            return result1 + result2;
        }

        public void Close()
        {
            _rconClient.Close();
        }

        public string SendCommand(string command)
        {
            if (_rconClient.Connected)
            {
                if (_rconClient.Authorized)
                {
                    return _rconClient.SendCommand(command);
                }
                else
                {
                    throw new NotAuthorizedException();
                }
            }
            else
            {
                throw new NotConnectedException();
            }
        }

    }
}
