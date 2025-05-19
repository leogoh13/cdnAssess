namespace CDN.Entities.DbEntity;

public class User : IDbTable
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public List<Skill> SkillSets { get; set; } =  [];
    public List<Hobby> Hobbies { get; set; } = [];
}