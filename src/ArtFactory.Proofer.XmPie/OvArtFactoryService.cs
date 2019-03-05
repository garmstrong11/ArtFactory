namespace ArtFactory.Proofer.XmPie
{
  using System;
  using System.Collections.Generic;
  using ArtFactory.Domain;
  using ArtFactory.Proofer.XmPie.Job;
  using ArtFactory.Proofer.XmPie.JobTicket;
  using ArtFactory.Proofer.XmPie.Production;

  public class OvArtFactoryService
  {
    private IXmPieUser User { get; }
    private Job_SSPSoap JobSvc { get; }
    private JobTicket_SSPSoap TicketSvc { get; }
    private Production_SSPSoap ProductionSvc { get; }
    private IPlanService PlanService { get; }
    private IArtFactoryConfig Config { get; }

    public OvArtFactoryService(
      IArtFactoryConfig config,
      IXmPieUser user, 
      Job_SSPSoap jobSvc, 
      JobTicket_SSPSoap ticketSvc, 
      Production_SSPSoap productionSvc, 
      IPlanService planService)
    {
      Config = config;
      User = user ?? throw new ArgumentNullException(nameof(user));
      JobSvc = jobSvc ?? throw new ArgumentNullException(nameof(jobSvc));
      TicketSvc = ticketSvc ?? throw new ArgumentNullException(nameof(ticketSvc));
      ProductionSvc = productionSvc ?? throw new ArgumentNullException(nameof(productionSvc));
      PlanService = planService;
    }

    public IEnumerable<string> BuildProofArt(IProofParams proofParams)
    {
      var planDef = PlanService.GetPlanForDocument(proofParams.DocumentId);

      if (planDef is null)
        throw new KeyNotFoundException($"Document {proofParams.DocumentId} does not exist");

      var planPath = planDef.BuildPlanPath(Config.UproduceServerUncName);


      return new[] {"Front", "Back"};
    }
  }
}