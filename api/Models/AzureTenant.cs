// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Value
{
  [JsonProperty("@odata.etag")]
  public string OdataEtag { get; set; }
  public string annata_description { get; set; }
  public string _owningbusinessunit_value { get; set; }
  public int annata_azureregion { get; set; }
  public int annata_docsaccess { get; set; }
  public int statecode { get; set; }
  public int statuscode { get; set; }
  public string annata_tenantid { get; set; }
  public string _createdby_value { get; set; }
  public string annata_name { get; set; }
  public string _ownerid_value { get; set; }
  public string _annata_account_value { get; set; }
  public DateTime modifiedon { get; set; }
  public string _owninguser_value { get; set; }
  public string _modifiedby_value { get; set; }
  public int versionnumber { get; set; }
  public string annata_azuretenantid { get; set; }
  public DateTime createdon { get; set; }
  public int annata_type { get; set; }
  public object _createdonbehalfby_value { get; set; }
  public object _owningteam_value { get; set; }
  public object timezoneruleversionnumber { get; set; }
  public object _annata_contact_value { get; set; }
  public object _modifiedonbehalfby_value { get; set; }
  public object overriddencreatedon { get; set; }
  public object utcconversiontimezonecode { get; set; }
  public object importsequencenumber { get; set; }
}

public class AzureTenant
{
  [JsonProperty("@odata.context")]
  public string OdataContext { get; set; }
  public List<Value> value { get; set; }
}

