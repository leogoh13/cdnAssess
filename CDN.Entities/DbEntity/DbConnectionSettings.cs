namespace CDN.Entities.DbEntity;

public class DbConnectionSettings
{
    public string DataSource { get; init; } = "";
    public string InitialCatalog { get; init; } = "";
    public string UserId { get; init; } = "";
    public string Password { get; init; } = "";
    public int CommandTimeout { get; init; } = 30;
    public bool TrustServerCertificate { get; init; } = true;
}