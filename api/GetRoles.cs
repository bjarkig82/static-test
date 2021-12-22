using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Annata.Docs
{
  public static class GetRoles
  {
    [FunctionName("GetRoles")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {

      var groups = new Dictionary<string, string[]>() {
        { "DocsAccess", new String[] { "527090001", "527090002" } }
      };

      var body = await new StreamReader(req.Body).ReadToEndAsync();
      User user = JsonConvert.DeserializeObject<User>(body);

      if (user.accessToken == null)
      {
        return new UnauthorizedResult();
      }

      try
      {
        List<string> roles = new List<string>();

        foreach (var group in groups)
        {
          if (await user.isInGroup(group.Value))
          {
            roles.Add(group.Key);
          }
        }

        Dictionary<string, string[]> resp = new Dictionary<string, string[]>() {
          { "roles", roles.ToArray() }
        };

        return new OkObjectResult(resp);
      }
      catch (Exception ex)
      {
        return new BadRequestObjectResult(ex.Message);
      }
    }
  }
}
