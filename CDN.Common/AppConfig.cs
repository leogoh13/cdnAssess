using CDN.Entities.DbEntity;
using Microsoft.Extensions.Configuration;

namespace CDN.Common;

public static class AppConfig
{
    public static DbConnectionSettings CdnDbConnection { get; private set; } = new();
    public static void LoadConfiguration(IConfiguration config)
    {
        CdnDbConnection = config.GetSection("ConnectionStrings:CDN").Get<DbConnectionSettings>() ?? throw new Exception("No CDN connection strings found in configuration file.");
    }
}