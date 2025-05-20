using CDN.Entities.Api;
using CDN.Entities.DbEntity;
using CDN.Manager;
using Microsoft.AspNetCore.Mvc;

namespace CompleteDeveloperNetworkAPI.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class HobbyController : ControllerBase
{
    [HttpGet]
    public async Task<List<Hobby>> GetHobbyList()
    {
        var result =  new List<Hobby>();

        try
        {
            result = await HobbyManager.GetHobbyList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result;
    }

    [HttpPut]
    public async Task<bool> AddHobby(HobbyRequest request)
    {
        try
        {
            var hobby = new Hobby
            {
                Id = request.Id,
                HobbyName = request.Name
            };
            return await HobbyManager.AddHobby(hobby);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false;
    }

    [HttpDelete]
    public async Task<bool> DeleteHobby(int hobbyId)
    {
        try
        {
            return await HobbyManager.DeleteHobby(hobbyId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }
}