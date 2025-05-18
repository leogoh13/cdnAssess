using System.Data;
using CDN.Entities.DbEntity;
using Microsoft.Data.SqlClient;

namespace CDN.Common.Repository;

public class DbConnectionHelper(DbConnectionSettings dbSetting)
{
    public IDbConnection GetConnection()
    {
        var stringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = dbSetting.DataSource,
            InitialCatalog = dbSetting.InitialCatalog,
            UserID = dbSetting.UserId,
            Password = dbSetting.Password,
            CommandTimeout = dbSetting.CommandTimeout,
            TrustServerCertificate = dbSetting.TrustServerCertificate
        };
        return new SqlConnection(stringBuilder.ConnectionString);
    }
}