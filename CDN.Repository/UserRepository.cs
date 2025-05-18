using CDN.Common;
using CDN.Common.Repository;
using CDN.Entities.DbEntity;
using CDN.Entities.Exception;
using Dapper;

namespace CDN.Repository;

public class UserRepository()
{
    private readonly IRepository _repository = new Repository(AppConfig.CdnDbConnection);

    public async Task<List<User>> GetAllUsers(string? nameFilter, string? emailFilter)
    {
        const string sql = """

                           SELECT * 
                           FROM Users 
                           WHERE 
                               (@nameFilter IS NULL OR Username LIKE '%' + @nameFilter + '%') AND
                               (@emailFilter IS NULL OR Email LIKE '%' + @emailFilter + '%')

                           SELECT HobbyName FROM UserHobbies
                           JOIN Hobby ON HobbyId = Id 
                           WHERE UserId = @USERID

                           SELECT SkillName, [Level] FROM UserSkill
                           JOIN Skill ON SkillId = Id
                           WHERE UserId = @USERID
                            
                           """;

        var parameters = new DynamicParameters();
        parameters.Add("@nameFilter", nameFilter);
        parameters.Add("@emailFilter", emailFilter);

        try
        {
            await using var reader = await _repository.QueryMultipleAsync(sql, parameters);

            if (reader == null) throw new DbSqlExecuteReaderException();

            var user = await reader.ReadSingleAsync<User>();
            var hobbies = await reader.ReadAsync<Hobby>();
            var skills = await reader.ReadAsync<Skill>();

            user.Hobbies =  hobbies.ToList();
            user.SkillSets = skills.ToList();

            return [ user ];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<User> GetSingleUser(int userId)
    {
        const string sql = """
                           
                           SELECT @USERID = Id 
                           FROM Users
                           
                           SELECT * FROM Users WHERE Id = @USERID
                           
                           SELECT HobbyName FROM UserHobbies
                           JOIN Hobby ON HobbyId = Id 
                           WHERE UserId = @USERID
                           
                           SELECT SkillName, [Level] FROM UserSkill
                           JOIN Skill ON SkillId = Id
                           WHERE UserId = @USERID
                            
                           """;

        var parameters = new DynamicParameters();
        parameters.Add("@USERID", userId);

        try
        {
            await using var reader = await _repository.QueryMultipleAsync(sql, parameters);

            if (reader == null) throw new DbSqlExecuteReaderException();

            var user = await reader.ReadSingleAsync<User>();
            var hobbies = await reader.ReadAsync<Hobby>();
            var skills = await reader.ReadAsync<Skill>();

            user.Hobbies =  hobbies.ToList();
            user.SkillSets = skills.ToList();

            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


}