using System.Text;
using System.Text.Json;
using CDN.Entities.Api;
using CDN.Entities.DbEntity;

namespace CompleteDeveloperNetworkClient.Service;

public class HobbyService(HttpClient http)
{
    private const string EndpointUrl = "https://localhost:7154";
    public async Task<List<Hobby>> GetHobbyList()
    {
        var result = new List<Hobby>();
        try
        {
            var response = await http.GetAsync(EndpointUrl + "/v1/Hobby");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<Hobby>>(content);
            result = users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result ?? [];
    }

    public async Task<List<Hobby>> AddHobby(Hobby newHobby)
    {
        var result = new List<Hobby>();
        try
        {
            var body = new HobbyRequest
            {
                Id = newHobby.Id,
                Name = newHobby.HobbyName
            };

            var httpContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await http.PutAsync(EndpointUrl + "/v1/Hobby", httpContent);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<Hobby>>(content);
            result = users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result ?? [];
    }

    public async Task<List<Hobby>> DeleteHobby(Hobby selectedHobby)
    {
        var result = new List<Hobby>();
        try
        {
            var response = await http.DeleteAsync(EndpointUrl + $"/v1/Hobby/{selectedHobby.Id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<Hobby>>(content);
            result = users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result ?? [];
    }
}