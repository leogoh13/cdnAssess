namespace CDN.Entities.DbEntity;

public class SkillView
{
    public int UserId { get; set; }
    public List<Skill> Skills { get; set; }
}

public class UserSkill : IDbTable
{
    public int UserId { get; set; }
    public int SkillId { get; set; }
    public SkillLevel Level {  get; set; }
}

public class UserHobbies : IDbTable
{
    public int UserId { get; set; }
    public int HobbyId { get; set; }
}

public class Skill : IDbTable
{
    public int SkillId { get; set; }
    public string Name { get; set; }
}