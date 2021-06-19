namespace mulova.unicore
{
    /// <summary>
    /// Get Build setting from 'asset_config'
    /// </summary>
    public static class AssetConfig
    {
        public const string FILE_NAME = "asset_config.txt";
        public static bool TEX_NPOT { get; private set; }

        public const string KEY_TEX_NPOT = "TEX_NPOT";

        public static Properties config
        {
            get
            {
                Properties c = new Properties();
                c.LoadFile(FILE_NAME);
                return c;
            }
        }

        static AssetConfig()
        {
            Load();
        }

        public static void Load()
        {
            var c = config;
            TEX_NPOT = c.GetBool(KEY_TEX_NPOT, false);
        }

        public new static string ToString()
        {
            return config.ToString();
        }
    }
}
