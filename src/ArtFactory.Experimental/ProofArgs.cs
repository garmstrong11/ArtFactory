namespace ArtFactory.Experimental
{
  using System.Collections.Generic;
  using System.Linq;
  using ArtFactory.Experimental.JobTicket;

  public class ProofArgs
  {
    private static List<string> NonStringFieldNames { get; }

    public int DocumentId { get; set; }
    public Dictionary<string, string> Fields { get; set; }

    static ProofArgs()
    {
      NonStringFieldNames = new List<string>
      {
        "plantId", "_greenPriceBox", "_prop65"
      };
    }
  
    public Customization[] GetStringCustomizations()
    {
      return Fields
        .Where(f => !NonStringFieldNames.Contains(f.Key))
        .Select(f => new Customization {m_Name = f.Key, m_Expression = f.Value, m_IOType = "R", m_Type = "VAR"})
        .ToArray();
    }

    public Customization[] GetNonStringCustomizations()
    {
      return Fields
        .Where(f => NonStringFieldNames.Contains(f.Key))
        .Select(f => new Customization {m_Name = f.Key, m_Expression = f.Value, m_IOType = "R", m_Type = "VAR"})
        .ToArray();
    }
  }
}