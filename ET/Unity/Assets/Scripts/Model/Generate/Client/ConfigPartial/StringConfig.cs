using System.Collections.Generic;

namespace ET
{
    public partial class StringConfigCategory
    {
        public readonly Dictionary<string, StringConfig> StringMap = new Dictionary<string, StringConfig>();

        public override void EndInit()
        {
            foreach (StringConfig item in this.dict.Values)
            {
                this.StringMap.Add(item.Chinese, item);
            }
        }
    }

    public partial class StringConfig
    {
        [StaticField]
        public static int Launchange = 0;

        public static string GetString(string key)
        {
            if (!StringConfigCategory.Instance.StringMap.ContainsKey(key))
            {
                return key;
            }
            StringConfig config = StringConfigCategory.Instance.StringMap[key];
            return SwitchString(config);
        }

        public static string GetString(int key)
        {
            StringConfig config = StringConfigCategory.Instance.Get(key);
            return SwitchString(config);
        }

        private static string SwitchString(StringConfig config)
        {
            switch (Launchange)
            {
                case 6:
                    return config.Chinese;
                case 10:
                    return config.English;
                case 40:
                    return config.Chinese;
                default:
                    return config.English;
            }
        }
    }
}