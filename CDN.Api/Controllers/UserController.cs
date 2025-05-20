using CDN.Entities;
using CDN.Entities.Api;
using CDN.Manager;
using Microsoft.AspNetCore.Mvc;

namespace CompleteDeveloperNetworkAPI.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class UserController : ControllerBase
{
    [HttpPut("Register")]
    public async Task<bool> RegisterUser(UserModel request)
    {
        try
        {
            return await FreelancerManager.AddUser(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false;
    }

    [HttpDelete("Delete")]
    public async Task<bool> DeleteUser(UserModel request)
    {
        try
        {
            return await FreelancerManager.DeleteUser(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false;
    }

    [HttpPut("Update")]
    public async Task<bool> UpdateUser([FromBody] UserModel user)
    {
        try
        {
            return await FreelancerManager.UpdateUser(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false;
    }

    [HttpGet("GetUser")]
    public async Task<UserModel> GetUser([FromQuery] int userId = 0)
    {
        var result = new UserModel();
        try
        {
            result = await FreelancerManager.GetUserBasedOnId(userId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result;
    }

    [HttpGet("Search")]
    public async Task<List<UserModel>> SearchUser(string? nameFilter, string? emailFilter)
    {
        var result = new List<UserModel>();
        try
        {
            result = await FreelancerManager.GetUsersSearch(nameFilter, emailFilter);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result;
    }

    [HttpGet("GetAllUsers")]
    public async Task<List<UserModel>> GetAllUsers()
    {
        var result = new List<UserModel>();
        try
        {
            result = await FreelancerManager.GetUsersSearch(null, null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return result;
    }

    [HttpPost("ArchiveUser")]
    public async Task<IActionResult> ArchiveUser([FromBody] ArchivingRequest request)
    {
        try
        {
            var result = await FreelancerManager.ArchiveUser(request.UserId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(false);
    }

    [HttpPost("UnarchiveUser")]
    public async Task<IActionResult> UnarchiveUser([FromBody] ArchivingRequest request)
    {
        try
        {
            var result = await FreelancerManager.UnarchiveUser(request.UserId);
            return Ok(result);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(false);
    }
}