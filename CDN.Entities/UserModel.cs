using System.Text.Json.Serialization;
using CDN.Entities.DbEntity;

namespace CDN.Entities;

public class UserModel
{
    public bool IsChecked { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("userName")]
    public string UserName { get; set; } = "";

    [JsonPropertyName("email")]
    public string Email { get; set; } = "";

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = "";

    [JsonPropertyName("hobbies")]
    public List<Hobby> Hobbies { get; set; } = new();

    [JsonPropertyName("skills")]
    public List<Skill> Skills { get; set; } = new();

    [JsonPropertyName("isArchived")]
    public bool IsArchived { get; set; }
}