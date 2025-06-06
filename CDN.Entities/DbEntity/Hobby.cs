﻿using System.Text.Json.Serialization;

namespace CDN.Entities.DbEntity;

public class Hobby : IDbTable
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string HobbyName { get; set; }
}