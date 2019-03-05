namespace ArtFactory.Domain
{
  using System;
  using System.Collections.Generic;

  public class Uplan
  {
    public int AccountId { get; private set; }
    public int CampaignId { get; private set; }
    public int DocumentId { get; private set; }
    public string PlanName { get; private set; }

    private string BuildPlanPath(string serverUnc)
    {
      if (string.IsNullOrWhiteSpace(serverUnc))
        throw new ArgumentException(
          "Value cannot be null or whitespace.", nameof(serverUnc));

      return $@"{serverUnc}\XMPie\XMPieData\{AccountId}\{CampaignId}\{PlanName}";
    }

    private IEnumerable<Dial> GetDials
  }
}