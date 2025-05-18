using CDN.Entities.DbEntity;
using CDN.Repository;

namespace CDN.Manager;

public static class FreelancerManager
{
    public static async Task<List<User>> GetUsers(List<int>? userId = null)
    {
        var users = new List<User>();
        if (userId != null)
        {
            if (userId.Count == 0)
                Console.WriteLine();
            else
            {
                var user = await new UserRepository().GetSingleUser(userId.FirstOrDefault());
                return [ user ];
            }
        }
        else
        {
            users = await new UserRepository().GetAllUsers("", "");
        }
        
        return users;
    }
}