namespace ArtFactory.Proofer.XmPie
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using ArtFactory.Proofer.XmPie.JobTicket;
  using JetBrains.Annotations;

  public class XmpieDocument
  {
    // These are all set implicitly by Dapper
    public int AccountId { get; [UsedImplicitly] private set; }
    public int CampaignId { get; [UsedImplicitly] private set; }
    public int DocumentId { get; [UsedImplicitly] private set; }
    public int PlanId { get; [UsedImplicitly] private set; }
    public string AccountName { get; [UsedImplicitly] private set; }
    public string CampaignName { get; [UsedImplicitly] private set; }
    public string DocumentName { get; [UsedImplicitly] private set; }
    public string PlanName { get; [UsedImplicitly] private set; }
    public decimal PageWidth { get; [UsedImplicitly] private set; }
    public decimal PageHeight { get; [UsedImplicitly] private set; }
    public int PageCount { get; [UsedImplicitly] private set; }
    public int PrintSettingsJobId { get; [UsedImplicitly] private set; }
    public int ProofSettingsJobId { get; [UsedImplicitly] private set; }
    private ISet<XmpieDial> Dials { get; } = new HashSet<XmpieDial>();

    public string BuildPlanPath(string serverUncName)
    {
      if (string.IsNullOrWhiteSpace(serverUncName))
        throw new ArgumentException(
          @"Value cannot be null or whitespace.", nameof(serverUncName));

      return $@"{serverUncName}\XMPie\XMPieData\{AccountId}\{CampaignId}\{PlanName}";
    }

    public int GetResolutionForTargetSize(int targetSize)
    {
      var max = Math.Max(PageWidth, PageHeight) * 72.0M;
      return (int)Math.Ceiling(targetSize / max);
    }

    public void AddDials(IEnumerable<XmpieDial> dials)
    {
      Dials.UnionWith(dials);
    }

    public Customization[] GetCustomizations(IDictionary<string, string> fields)
    {
      var customizations =
        from dial in Dials
        from field in fields.DefaultIfEmpty()
        select dial.AsCustomization(field.Value);

      return customizations.ToArray();
    }
  }
}