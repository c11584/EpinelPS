using Newtonsoft.Json;

namespace EpinelPS.Utils
{
    public class GameConfigRoot
    {
        public StaticData StaticData { get; set; } = new();
        public string ResourceBaseURL { get; set; } = "";
        public string GameMinVer { get; set; } = "";
        public string GameMaxVer { get; set; } = "";
    }

    public class StaticData
    {
        public string Url { get; set; } = "";
        public string Version { get; set; } = "";
        public string Salt1 { get; set; } = "";
        public string Salt2 { get; set; } = "";


        public byte[] GetSalt1Bytes()
        {
            return Convert.FromBase64String(Salt1);
        }
        public byte[] GetSalt2Bytes()
        {
            return Convert.FromBase64String(Salt2);
        }
    }

    public static class GameConfig
    {
        private static GameConfigRoot? _root;
        public static GameConfigRoot Root
        {
            get
            {
                if (_root == null)
                {
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/gameconfig.json"))
                    {
                        Console.WriteLine("Gameconfig.json is not found, the game WILL NOT work!");
                        _root = new GameConfigRoot();
                    }
                    Console.WriteLine("淘宝搜夜辉—夜辉奇妙屋，其他店铺均为倒卖，如遇被骗请差评退款，请勿用于商业用途。");
                    Console.WriteLine("Loaded game config");

                    _root = JsonConvert.DeserializeObject<GameConfigRoot>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/gameconfig.json"));

                    if (_root == null)
                    {
                        throw new Exception("Failed to read gameconfig.json");
                    }
                }

                return _root;
            }
        }
    }
}
