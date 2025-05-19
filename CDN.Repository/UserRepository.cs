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

                           """;

        var parameters = new DynamicParameters();
        parameters.Add("@nameFilter", nameFilter);
        parameters.Add("@emailFilter", emailFilter);

        try
        {
            var users = await _repository.QueryAsync<User>(sql, parameters);
            return users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User> GetUserOnId(int userId)
    {
        const string sql = "SELECT * FROM Users WHERE Id = @userId";

        var parameters = new DynamicParameters();
        parameters.Add("@userId", userId);

        try
        {
            var result = await _repository.QuerySingleAsync<User>(sql, parameters);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User?> GetSingleUser(int userId)
    {
        const string sql = "SELECT * FROM Users WHERE Id = @userId";

        var parameters = new DynamicParameters();
        parameters.Add("@userId", userId);

        try
        {
            var result = await _repository.QuerySingleAsync<User>(sql, parameters);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Hobby>> GetUserHobbies(int userId = 0)
    {
        const string sql =
            """
                SELECT Userid, HobbyName FROM UserHobbies
                JOIN Hobby ON HobbyId = Id 
                WHERE @userId = 0 OR UserId = @userId
            """;

        var parameters = new DynamicParameters();
        parameters.Add("@userId", userId);

        try
        {
            var result = await _repository.QueryAsync<Hobby>(sql, parameters);
            return result ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Skill>> GetUserSkills(int userId = 0)
    {
        const string sql =
            """
                SELECT SkillName, [Level] 
                FROM UserSkill
                JOIN Skill ON SkillId = Id
                WHERE UserId = @userId
            """;

        var parameters = new DynamicParameters();
        parameters.Add("@userId", userId);

        try
        {
            var result = await _repository.QueryAsync<Skill>(sql, parameters);
            return result ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddNewUser(User user)
    {
        const string sql =
            """
                INSERT INTO Users
                ( Username, Email, PhoneNumber )
                VALUES 
                ( @username, @email, @phone )
            """;

        var parameters = new DynamicParameters();
        parameters.Add("@username", user.Username);
        parameters.Add("@email", user.Email);
        parameters.Add("@phone", user.PhoneNumber);

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

    public async Task<bool> ArchiveUser(int userId)
    {
        const string sql = "UPDATE Users SET IsArchive = 1 WHERE Id = @userId";
        var parameters = new DynamicParameters();
        parameters.Add("@userId", userId);

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

    public async Task<bool> UnarchiveUser(int userId)
    {
        const string sql = "UPDATE Users SET IsArchive = 0 WHERE Id = @userId";
        var parameters = new DynamicParameters();
        parameters.Add("@userId", userId);

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
}