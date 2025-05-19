using CDN.Entities.DbEntity;

namespace CDN.Entities.Api;

public class RegisterUserRequest
{
    public User User { get; set; }
    public List<Hobby> Hobbies { get; set; }
    public SkillView skills { get; set; }
}