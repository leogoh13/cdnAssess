using System.Text.Json;

namespace CDN.Common;

public static class ConnectionUtil
{
    public static async Task<T> PostAsync<T>(string endpointUrl, string json)
    {
        var client = new HttpClient();
        var requestMessage = new HttpRequestMessage();
        requestMessage.Method = HttpMethod.Post;
        requestMessage.RequestUri = new Uri(endpointUrl);
        requestMessage.Content = new StringContent(json);
        var response = client.SendAsync(requestMessage).Result;

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();

        var resultObj = JsonSerializer.Deserialize<T>(body);

        return resultObj;
    }
}