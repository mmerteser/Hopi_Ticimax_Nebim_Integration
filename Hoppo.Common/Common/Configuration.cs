using Microsoft.Extensions.Configuration;

namespace Hoppo.Common.Common
{
    public static class Configuration
    {
        private static ConfigurationManager ConfigurationManager
        {
            get
            {
                ConfigurationManager configuration = new();
                configuration.SetBasePath(Directory.GetCurrentDirectory());
                configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return configuration;
            }
        }
        public static string ProductionAppConnectionString => ConfigurationManager.GetConnectionString("AppConnectionString");
        public static string TicimaxUyeKodu => ConfigurationManager.GetSection("TicimaxSettings").GetSection("UyeKodu").Value;
        public static string WebSiteUrl => ConfigurationManager.GetSection("TicimaxSettings").GetSection("WebSiteUrl").Value;
        public static string XmlPath => ConfigurationManager.GetSection("XmlPath").Value;
        public static double TimerTickFromMinute => Convert.ToDouble(ConfigurationManager.GetSection("TimerTickFromMinute").Value);

    }
}
