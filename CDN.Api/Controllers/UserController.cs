using CDN.Entities;
using CDN.Entities.Api;
using CDN.Entities.DbEntity;
using CDN.Manager;
using Microsoft.AspNetCore.Mvc;

namespace CompleteDeveloperNetworkAPI.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class UserController : ControllerBase
{
    [HttpPut("Register")]
    public UserModel RegisterUser(RegisterUserRequest request)
    {
        var response = new UserModel();
        try
        {

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return response;
    }

    [HttpDelete("Delete")]
    public void DeleteUser()
    {

    }

    [HttpPost("Update")]
    public void UpdateUser()
    {

    }

    [HttpGet("GetUser")]
    public async Task<UserModel> GetUser([FromQuery] int userId = 0)
    {
        return await FreelancerManager.GetUserBasedOnId(userId);
    }

    [HttpGet("Search")]
    public async Task<List<UserModel>> SearchUser(string? nameFilter, string? emailFilter)
    {
        return await FreelancerManager.GetUsersSearch(nameFilter, emailFilter);
    }

    [HttpGet("GetAllUsers")]
    public async Task<List<UserModel>> GetAllUsers()
    {
        return await FreelancerManager.GetUsersSearch(null, null);
    }

    [HttpPost("ArchiveUser")]
    public async Task<IActionResult> ArchiveUser([FromBody] ArchivingRequest request)
    {
        var result = await FreelancerManager.ArchiveUser(request.UserId);
        return Ok(result);
    }

    [HttpPost("UnarchiveUser")]
    public async Task<IActionResult> UnarchiveUser([FromBody] ArchivingRequest request)
    {
        var result = await FreelancerManager.UnarchiveUser(request.UserId);
        return Ok(result);
    }
}