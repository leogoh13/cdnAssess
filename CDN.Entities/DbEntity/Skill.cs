namespace CDN.Entities.DbEntity;

public class Skill
{
    public int UserId { get; set; }
    public required string Name { get; set; }
    public SkillLevel Level { get; set; }
}

public class UserSkills
{
    public int UserId { get; set; }
    public int SkillId { get; set; }
}

public class UserHobbies
{
    public int UserId { get; set; }
    public int HobbyId { get; set; }
}