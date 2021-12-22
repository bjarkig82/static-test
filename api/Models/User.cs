using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Annata.Docs
{
  internal class User
  {
    private static readonly HttpClient client = new HttpClient();
    public string accessToken { get; set; }
    public List<string> tenantIds { get; set; }

    private string getTenantId()
    {
      var handler = new JwtSecurityTokenHandler();
      var token = handler.ReadJwtToken(accessToken);

      return token.Payload.TryGetValue("tid", out object value) ? value.ToString() : null;
    }

    public async Task<bool> isInGroup(string[] groupIds)
    {
      var tenantId = getTenantId();

      string docsAccessFilter = $"annata_docsaccess eq {String.Join(" or annata_docsaccess eq ", groupIds)}";
      // string tenantIdFilter = $"annata_tenantid eq {String.Join(" or annata_tenantid eq ", tenantIds)}";
      string tenantIdFilter = $"annata_tenantid eq {tenantId}";
      string options = $"$filter=({docsAccessFilter}) and ({tenantIdFilter})";

      AzureTenant tenantsInGroup = await DataverseUtils.Get<AzureTenant>("annata_azuretenants", options);
      return tenantsInGroup.value.Count > 0;
    }
  }
}