using System.Text;
using System.Text.Json;
using CDN.Entities;
using CDN.Entities.Api;
using CompleteDeveloperNetworkClient.Pages;

namespace CompleteDeveloperNetworkClient.Service;

public class UserService(HttpClient http)
{
    private const string EndpointUrl = "https://localhost:7154";
    public async Task<List<UserModel>> GetUsersAsync()
    {
        var result = new List<UserModel>();
        try
        {
            var response = await http.GetAsync(EndpointUrl + "/v1/User/GetAllUsers");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserModel>>(content);
            result = users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result ?? [];
    }

    public async Task<bool> ArchiveUserAsync(int userId)
    {
        var result = false;
        try
        {
            var url = $"{EndpointUrl}/v1/User/ArchiveUser";
            var body = new ArchivingRequest { UserId = userId };
            var httpContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await http.PostAsync(url, httpContent);

            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<bool>(responseData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result;
    }

    public async Task<bool> UnarchiveUserAsync(int userId)
    {
        var result = false;
        try
        {
            var url = $"{EndpointUrl}/v1/User/UnarchiveUser";
            var body = new ArchivingRequest { UserId = userId };
            var httpContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await http.PostAsync(url, httpContent);

            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<bool>(responseData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result;
    }

    public async Task<List<UserModel>> SearchUsers(string wildcardSearch, Home.SearchType type)
    {
        var result = new List<UserModel>();
        try
        {
            var query = type switch
            {
                Home.SearchType.Email => $"emailFilter={Uri.EscapeDataString(wildcardSearch)}",
                Home.SearchType.Username => $"nameFilter={Uri.EscapeDataString(wildcardSearch)}",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            var response = await http.GetAsync($"{EndpointUrl}/v1/User/Search?{query}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserModel>>(content);
            result = users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result ?? [];
    }

    public async Task<bool> UpdateUser(UserModel user)
    {
        var result = false;

        try
        {
            var url = $"{EndpointUrl}/v1/User/Update";
            var body = new UserModel();
            var httpContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await http.PutAsync(url, httpContent);

            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<bool>(responseData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result;
    }
}