using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterStrike2RconTool.Wpf.Options
{
    public class RconWpfOptions
    {
        public static string Key = "RconWpfSettings";

        public DefaultConfig DefaultConfiguration { get; set; }
        public MapConfig MapConfiguration { get; set; }
        public GameModeConfig GameModeConfiguration { get; set; }

        public class DefaultConfig
        {
            public string Ip { get; set; }
            public int Port { get; set; }
            public string Password { get; set; }
        }

        public class MapConfig
        {
            public List<string> Maps { get; set; }
        }

        public class GameModeConfig
        {
            public List<string> GameModes { get; set; }
        }
    }
}
