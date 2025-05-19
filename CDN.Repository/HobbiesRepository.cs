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
            SELECT HobbyName AS Name FROM UserHobbies
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
}