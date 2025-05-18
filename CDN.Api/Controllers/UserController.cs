using CDN.Entities.DbEntity;
using CDN.Manager;
using Microsoft.AspNetCore.Mvc;

namespace CompleteDeveloperNetworkAPI.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class UserController : ControllerBase
{
    [HttpPut]
    public void RegisterUser()
    {

    }

    [HttpDelete]
    public void DeleteUser()
    {

    }

    [HttpPost]
    public void UpdateUser()
    {

    }

    [HttpGet("GetUser")]
    public async Task<List<User>> GetUser(List<int> userIds)
    {
        return await FreelancerManager.GetUsers(userIds);
    }
}