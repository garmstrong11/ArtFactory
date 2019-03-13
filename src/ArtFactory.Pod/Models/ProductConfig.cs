namespace ArtFactory.Pod.Models
{
  using System.Collections.Generic;
  using Newtonsoft.Json;

  public class ProductConfig
  {
    [JsonProperty("accountId")]
    public int AccountId { get; set; }

    [JsonProperty("campaignId")]
    public int CampaignId { get; set; }

    [JsonProperty("documentId")]
    public int DocumentId { get; set; }

    [JsonProperty("dials")]
    public List<Dial> Dials { get; set; }
  }
}