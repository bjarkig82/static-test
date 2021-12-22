using System.Collections.Generic;
using Newtonsoft.Json;

namespace Annata.Docs
{

  // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
  public class DirectorySizeQuota
  {
    public int used { get; set; }
    public int total { get; set; }
  }

  public class VerifiedDomain
  {
    public string capabilities { get; set; }
    public bool isDefault { get; set; }
    public bool isInitial { get; set; }
    public string name { get; set; }
    public string type { get; set; }
  }

  public class Value
  {
    public string id { get; set; }
    public object deletedDateTime { get; set; }
    public List<object> businessPhones { get; set; }
    public object city { get; set; }
    public object country { get; set; }
    public object countryLetterCode { get; set; }
    public object createdDateTime { get; set; }
    public string displayName { get; set; }
    public object isMultipleDataLocationsForServicesEnabled { get; set; }
    public List<object> marketingNotificationEmails { get; set; }
    public object onPremisesLastSyncDateTime { get; set; }
    public object onPremisesSyncEnabled { get; set; }
    public object postalCode { get; set; }
    public object preferredLanguage { get; set; }
    public List<object> securityComplianceNotificationMails { get; set; }
    public List<object> securityComplianceNotificationPhones { get; set; }
    public object state { get; set; }
    public object street { get; set; }
    public List<object> technicalNotificationMails { get; set; }
    public string tenantType { get; set; }
    public DirectorySizeQuota directorySizeQuota { get; set; }
    public object privacyProfile { get; set; }
    public List<object> assignedPlans { get; set; }
    public List<object> provisionedPlans { get; set; }
    public List<VerifiedDomain> verifiedDomains { get; set; }
  }

  public class Organization
  {
    [JsonProperty("@odata.context")]
    public string OdataContext { get; set; }
    public List<Value> value { get; set; }
  }


}
