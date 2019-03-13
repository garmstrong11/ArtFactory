namespace ArtFactory.Pod.Models
{
  using Newtonsoft.Json;
  using Newtonsoft.Json.Converters;

  public class Dial
  {
    [JsonProperty("dial-name")]
    public string DialName { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("control-type")]
    public ControlType ControlType { get; set; }
  }
}