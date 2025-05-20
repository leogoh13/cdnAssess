using System.Text.Json.Serialization;

namespace CDN.Entities.Api;

public class HobbyRequest
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}