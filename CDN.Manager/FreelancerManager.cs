using CDN.Entities;
using CDN.Entities.DbEntity;
using CDN.Repository;

namespace CDN.Manager;

public static class FreelancerManager
{
    public static async Task<UserModel> GetUserBasedOnId(int userId = 0)
    {
        var result = await new UserRepository().GetUserOnId(userId);

        var skills = await new SkillRepository().GetUserSkills(result.Id);
        var hobbies = await new HobbiesRepository().GetUserHobby(result.Id);

        var userModel = new UserModel
        {
            Id = result.Id,
            UserName = result.Username,
            Email = result.Email ?? "Not Found",
            PhoneNumber = result.PhoneNumber ?? "Not Found",
            Hobbies = hobbies.Select(user => user.Name).ToList(),
            Skills = skills.Select(user => user.Name).ToList(),
            IsArchived = false
        };

        return userModel;
    }

    public static async Task<List<UserModel>> GetUsersSearch(string? nameFilter, string? emailFilter)
    {
        var result = new List<UserModel>();
        var users = await new UserRepository().GetAllUsers(nameFilter, emailFilter);

        foreach (var user in users)
        {
            var skills = await new SkillRepository().GetUserSkills(user.Id);
            var hobbies = await new HobbiesRepository().GetUserHobby(user.Id);

            var userModel = new UserModel
            {
                Id = user.Id,
                UserName = user.Username,
                Email = user.Email ?? "Not Found",
                PhoneNumber = user.PhoneNumber ?? "Not Found",
                Hobbies = hobbies.Select(x => x.Name).ToList(),
                Skills = skills.Select(x => x.Name).ToList(),
                IsArchived = false
            };

            result.Add(userModel);
        }

        return result;
    }

    public static async Task<bool> AddUser(User user, List<Hobby> hobbies, SkillView skills)
    {
        var repo = new UserRepository();
        var isUserCreated = await repo.AddNewUser(user);
        if (!isUserCreated)
            return false;

        return true;
    }

    private static async Task<bool> AdduserSkills()
    {
        return true;
    }

    private static async Task<List<Skill>> AddNewSkill(SkillView skills)
    {
        var repo = new SkillRepository();
        var newSkillAdded = await repo.AddNewSkills(skills.Skills.Select(x => x.Name).ToList());
        return newSkillAdded;
    }

    public static async Task<bool> ArchiveUser(int userId)
    {
        try
        {
            var isSuccess = await new UserRepository().ArchiveUser(userId);
            return isSuccess;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }

    public static async Task<bool> UnarchiveUser(int userId)
    {
        try
        {
            var isSuccess = await new UserRepository().UnarchiveUser(userId);
            return isSuccess;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }
}