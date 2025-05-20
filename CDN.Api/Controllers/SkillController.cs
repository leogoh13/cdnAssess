using CDN.Entities.DbEntity;
using CDN.Manager;
using Microsoft.AspNetCore.Mvc;

namespace CompleteDeveloperNetworkAPI.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class SkillController : ControllerBase
{
    [HttpGet]
    public async Task<List<Skill>> GetSkillList()
    {
        var result = new List<Skill>();
        try
        {
            result = await SkillManager.GetSkillList();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result;
    }

    [HttpPut]
    public async Task<bool> AddSkill(Skill skill)
    {
        try
        {
            return await SkillManager.AddSkill(skill);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false;
    }

    [HttpDelete]
    public async Task<bool> DeleteSkill(Skill skill)
    {
        try
        {
            return await SkillManager.DeleteSkill(skill);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false;
    }
}