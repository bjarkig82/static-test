using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Annata.Docs
{
  public static class DataverseUtils
  {
    public static async Task<T> Get<T>(string entity, string options)
    {
      string tenantId = System.Environment.GetEnvironmentVariable("TENANT_ID");
      string environmentUrl = System.Environment.GetEnvironmentVariable("ENVIRONMENT_URL");
      string apiVersion = "9.2";

      var scope = new[] { $"{environmentUrl}/.default" };
      var webAPI = $"{environmentUrl}/api/data/v{apiVersion}/";
      var authority = $"https://login.microsoftonline.com/{tenantId}";

      var clientApp = ConfidentialClientApplicationBuilder.Create(System.Environment.GetEnvironmentVariable("CLIENT_ID"))
           .WithClientSecret(System.Environment.GetEnvironmentVariable("CLIENT_SECRET"))
           .WithAuthority(new Uri(authority))
           .Build();
      var authResult = await clientApp.AcquireTokenForClient(scope).ExecuteAsync();

      using var httpClient = new HttpClient
      {
        BaseAddress = new Uri(webAPI),
        Timeout = new TimeSpan(0, 0, 10)
      };

      httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
      httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

      var response = await httpClient.GetAsync($"{entity}?{options}");
      var jsonSerializerOptions = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };

      if (response.IsSuccessStatusCode)
      {
        var resp = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(resp, jsonSerializerOptions);
      }
      else
      {
        var errorResponse = await response.Content.ReadAsStringAsync();
        var error = JsonSerializer.Deserialize<ErrorResponse>(errorResponse, jsonSerializerOptions);
        throw new Exception($"HTTP: {(int)response.StatusCode} ReasonPhrase: {response.ReasonPhrase}\nError: {error.Error.Message}");
      }
    }
  }

  class Account
  {
    public string Name { get; set; }
    public Guid AccountId { get; set; }
  }
  class ErrorResponse
  {
    public ErrorMessage Error { get; set; }
    public class ErrorMessage
    {
      public string Message { get; set; }
    }
  }
}
