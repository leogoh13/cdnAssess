namespace CDN.Entities.DbEntity;

public class Hobby : IDbTable
{
    public int UserId { get; set; }
    public required string Name { get; set; }
}