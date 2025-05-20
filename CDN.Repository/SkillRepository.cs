using CDN.Common;
using CDN.Common.Repository;
using CDN.Entities.DbEntity;
using CDN.Entities.Exception;
using Dapper;

namespace CDN.Repository;

public class SkillRepository
{
    private readonly IRepository _repository = new Repository(AppConfig.CdnDbConnection);

    public async Task<bool> AddSkill(Skill skill)
    {
        const string sql = """
                               INSERT INTO Skill
                               ( SkillName )
                               OUTPUT INSERTED.*
                               VALUES 
                               ( @skillName )
                           """;

        var parameters = new DynamicParameters();
        parameters.Add("skillName", skill.SkillName);

        try
        {
            var result = await _repository.ExecuteAsync(sql, parameters);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteSkill(Skill skill)
    {
        const string sql = """
                               DELETE FROM Skill
                               WHERE SkillName = @skillName
                           """;

        var parameters = new DynamicParameters();
        parameters.Add("skillName", skill.SkillName);

        try
        {
            var result = await _repository.ExecuteAsync(sql, parameters);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Skill>> GetSkillList()
    {
        const string sql = "SELECT * FROM Skill";

        try
        {
            var result = await _repository.QueryAsync<Skill>(sql);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> InsertUserSkill(User user, List<Skill> skills)
    {
        const string sql =
            """
                INSERT INTO @Input (UserId, SkillName, Level)
                VALUES (
                
                INSERT INTO Skill
                ( SkillName )
                VALUES 
                (
                    @skillName
                )
            """;
        var parameters = skills.Select(x =>
        new
        {
            skillName =  x.SkillName
        });

        try
        {
            var result = await _repository.ExecuteAsync(sql, parameters);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Skill>> GetUserSkills(int userId)
    {
        const string sql =
            """
            SELECT Id, SkillName FROM UserSkill
            JOIN Skill ON SkillId = Id
            WHERE UserId = @UserId
            """;

        var parameter = new DynamicParameters();
        parameter.Add("@UserId", userId);

        try
        {
            var result = await _repository.QueryAsync<Skill>(sql, parameter);
            return result ?? throw new DbNoRecordsFound();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}