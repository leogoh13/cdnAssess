using CDN.Entities.DbEntity;
using CDN.Repository;

namespace CDN.Manager;

public static class SkillManager
{
    public static async Task<List<Skill>> GetSkillList()
    {
        var skillRepo = new SkillRepository();
        return await skillRepo.GetSkillList();
    }

    public static async Task<bool> AddSkill(Skill skill)
    {
        var skillRepo = new SkillRepository();
        return await skillRepo.AddSkill(skill);
    }

    public static async Task<bool> DeleteSkill(Skill skill)
    {
        var skillRepo = new SkillRepository();
        return await skillRepo.DeleteSkill(skill);
    }
}