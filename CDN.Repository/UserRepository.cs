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
                OUTPUT Inserted.Id
                VALUES 
                ( @username, @email, @phone )
                
                INSERT INTO UserHobbies
                ( UserId, HobbyId )
                VALUES
                ( @userId, @hobbyId )
                    
                INSERT INTO UserSkills
                ( UserId, SkillId )
                VALUES
                ( @userId, @skillId )
            """;

        var parameters = new DynamicParameters();
        parameters.Add("@userId", user.Id);
        parameters.Add("@username", user.Username);
        parameters.Add("@email", user.Email);
        parameters.Add("@phone", user.PhoneNumber);
        parameters.Add("@skillId", user.SkillSets.Select(x => x.Id).ToList());
        parameters.Add("@hobbyId", user.Hobbies.Select(x => x.Id).ToList());

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

    public async Task<bool> UpdateUser(User user)
    {
        const string sql =
            """
                UPDATE Users
                SET Username = @username, Email = @email, PhoneNumber = @phone
                WHERE Id = @userId;
                
                DELETE FROM UserSkill WHERE UserId = @userId;
                DELETE FROM UserHobbies WHERE UserId = @userId;
                
                INSERT INTO UserSkill (UserId, SkillId)
                VALUES ( @userId, @skillId)
                
                INSERT INTO UserHobbies (UserId, HobbyId)
                VALUES ( @userId, @hobbyId)
            """;

        var parameters = new DynamicParameters();
        parameters.Add("@userId", user.Id);
        parameters.Add("@username", user.Username);
        parameters.Add("@email", user.Email);
        parameters.Add("@phone", user.PhoneNumber);
        parameters.Add("@skillId", user.SkillSets);
        parameters.Add("@hobbyId", user.Hobbies);

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

    public async Task<bool> DeleteUser(User user)
    {
        const string sql = "DELETE FROM Users WHERE Id = @userId";

        var parameters = new DynamicParameters();
        parameters.Add("@userId", user.Id);

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