using CDN.Common;
using CDN.Common.Repository;
using CDN.Entities.DbEntity;
using CDN.Entities.Exception;
using Dapper;

namespace CDN.Repository;

public class HobbiesRepository
{
    private readonly IRepository _repository = new Repository(AppConfig.CdnDbConnection);

    public async Task<List<Hobby>> GetUserHobby(int userId)
    {
        const string sql =
            """
            SELECT Id, HobbyName FROM UserHobbies
            JOIN Hobby ON HobbyId = Id 
            WHERE UserId = @UserId
            """;

        var parameter = new DynamicParameters();
        parameter.Add("@UserId", userId);

        try
        {
            var result = await _repository.QueryAsync<Hobby>(sql, parameter);
            return result ?? throw new DbNoRecordsFound();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Hobby>> GetHobbyList()
    {
        const string sql = "SELECT * FROM Hobby";

        try
        {
            var result = await _repository.QueryAsync<Hobby>(sql);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteHobby(int hobbyId)
    {
        const string sql = "DELETE FROM Hobby FROM Id = @hobbyId";

        var parameter = new DynamicParameters();
        parameter.Add("@hobbyId", hobbyId);

        try
        {
            var result = await _repository.ExecuteAsync(sql, parameter);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddHobby(Hobby hobby)
    {
        const string sql = """
                           INSERT INTO Hobby ( Id, HobbyName )
                           VALUES ( @hobbyId, @hobbyName)
                           """;

        var parameter = new DynamicParameters();
        parameter.Add("@hobbyId", hobby.Id);
        parameter.Add("@hobbyName", hobby.HobbyName);

        try
        {
            var result = await _repository.ExecuteAsync(sql, parameter);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}