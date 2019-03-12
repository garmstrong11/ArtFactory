namespace ArtFactory.Pod.Models
{
  using System.Collections.Generic;
  using Newtonsoft.Json;

  public class ArtifactRequest
  {
    [JsonProperty("accountId")]
    public int AccountId { get; set; }

    [JsonProperty("campaignId")]
    public int CampaignId { get; set; }

    [JsonProperty("documentId")]
    public int DocumentId { get; set; }

    [JsonProperty("dial-values")]
    public Dictionary<string, string> DialValues { get; set; }
  }
}