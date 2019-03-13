namespace ArtFactory.Proofer.XmPie
{
  using System;
  using System.Threading.Tasks;
  using ArtFactory.Domain;
  using ArtFactory.Proofer.XmPie.JobTicket;
  using JetBrains.Annotations;

  public class OvJobTicketService
  {
    private JobTicket_SSPSoap TicketSvc { get; }
    private IArtFactoryConfig Config { get; }
    private XmpieDocument Document { get; }

    public OvJobTicketService(
      [NotNull] JobTicket_SSPSoap ticketSvc,
      [NotNull] IArtFactoryConfig config,
      [NotNull] XmpieDocument document)
    {
      TicketSvc = ticketSvc ?? throw new ArgumentNullException(nameof(ticketSvc));
      Config = config ?? throw new ArgumentNullException(nameof(config));
      Document = document ?? throw new ArgumentNullException(nameof(document));
    }

    public async Task<string> CreateProofTicket(ArtifactType artifact)
    {
      var tkt = await TicketSvc.LoadJobTicketAsync(
        Config.UproduceUsername, Config.UproduceUserPassword, Document.ProofSettingsJobId.ToString());

      return tkt;
    }

    public async Task<bool> SetCustomizations([NotNull] string jobId, [NotNull] Customization[] customizations)
    {
      if (customizations == null) throw new ArgumentNullException(nameof(customizations));

      if (string.IsNullOrWhiteSpace(jobId))
        throw new ArgumentException(
          @"Value cannot be null or whitespace.", nameof(jobId)
          );
      
      var request = new SetCustomizationsRequest(
        Config.UproduceUsername, 
        Config.UproduceUserPassword,
        jobId, 
        customizations, 
        false);

      var response = await TicketSvc.SetCustomizationsAsync(request);

      return response.SetCustomizationsResult;
    }
  }
}