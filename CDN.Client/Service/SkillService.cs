using System.Text.Json;
using CDN.Entities.DbEntity;

namespace CompleteDeveloperNetworkClient.Service;

public class SkillService(HttpClient http)
{
    private const string EndpointUrl = "https://localhost:7154";
    public async Task<List<Skill>> GetSkillList()
    {
        var result = new List<Skill>();
        try
        {
            var response = await http.GetAsync(EndpointUrl + "/v1/Skill");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<Skill>>(content);
            result = users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result ?? [];
    }
}