using CDN.Entities.DbEntity;
using CDN.Repository;

namespace CDN.Manager;

public class HobbyManager
{
    public static async Task<List<Hobby>> GetHobbyList()
    {
        var hobbyRepo = new HobbiesRepository();
        return await hobbyRepo.GetHobbyList();
    }

    public static async Task<bool> AddHobby(Hobby hobby)
    {
        var hobbyRepo = new HobbiesRepository();
        return await hobbyRepo.AddHobby(hobby);
    }

    public static async Task<bool> DeleteHobby(int hobbyId)
    {
        var hobbyRepo = new HobbiesRepository();
        return await hobbyRepo.DeleteHobby(hobbyId);
    }
}