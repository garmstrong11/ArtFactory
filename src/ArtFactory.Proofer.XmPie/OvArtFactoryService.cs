namespace ArtFactory.Proofer.XmPie
{
  using System;
  using System.Collections.Generic;
  using ArtFactory.Domain;
  using ArtFactory.Proofer.XmPie.Job;
  using ArtFactory.Proofer.XmPie.JobTicket;
  using ArtFactory.Proofer.XmPie.Production;
  using JetBrains.Annotations;

  public class OvArtFactoryService
  {
    private Job_SSPSoap JobSvc { get; }
    private JobTicket_SSPSoap TicketSvc { get; }
    private Production_SSPSoap ProductionSvc { get; }
    private IDocumentService DocumentSvc { get; }
    private IDialService DialSvc { get; }
    private IArtFactoryConfig Config { get; }

    public OvArtFactoryService(
      IArtFactoryConfig config,
      Job_SSPSoap jobSvc, 
      JobTicket_SSPSoap ticketSvc, 
      Production_SSPSoap productionSvc,
      [NotNull] IDocumentService documentSvc,
      [NotNull] IDialService dialSvc)
    {
      Config = config;
      JobSvc = jobSvc ?? throw new ArgumentNullException(nameof(jobSvc));
      TicketSvc = ticketSvc ?? throw new ArgumentNullException(nameof(ticketSvc));
      ProductionSvc = productionSvc ?? throw new ArgumentNullException(nameof(productionSvc));
      DocumentSvc = documentSvc ?? throw new ArgumentNullException(nameof(documentSvc));
      DialSvc = dialSvc ?? throw new ArgumentNullException(nameof(dialSvc));
    }

    public IEnumerable<string> BuildProofArt(IProofParams proofParams)
    {
      if (proofParams == null) throw new ArgumentNullException(nameof(proofParams));

      var doc = DocumentSvc.GetDocument(proofParams.DocumentId);
      var planInfo = doc.BuildPlanPath(Config.UproduceServerUncName);
      var dials = DialSvc.GetDialsForPlan(planInfo);
      doc.AddDials(dials);



      return new[] {"Front", "Back"};
    }
  }
}