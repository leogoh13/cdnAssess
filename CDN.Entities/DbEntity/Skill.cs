using System.Text.Json.Serialization;

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
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string SkillName { get; set; }
}