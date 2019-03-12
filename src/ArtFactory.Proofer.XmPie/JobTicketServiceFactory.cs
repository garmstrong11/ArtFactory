namespace ArtFactory.Proofer.XmPie
{
  using System;
  using System.ComponentModel;
  using System.Threading.Tasks;
  using ArtFactory.Domain;
  using ArtFactory.Proofer.XmPie.JobTicket;
  using JetBrains.Annotations;

  public class JobTicketServiceFactory
  {
    private JobTicket_SSPSoap TicketSvc { get; }
    private IArtFactoryConfig Config { get; }

    public JobTicketServiceFactory(JobTicket_SSPSoap ticketSvc, IArtFactoryConfig config)
    {
      TicketSvc = ticketSvc;
      Config = config;
    }

    public OvJobTicketService CreateJobTicket([NotNull] XmpieDocument document, ArtifactType artifactType)
    {
      if (document == null) throw new ArgumentNullException(nameof(document));

      switch (artifactType) {
        case ArtifactType.Proof:
          break;
        case ArtifactType.Print:
          break;
        default:
          throw new InvalidEnumArgumentException(
            nameof(artifactType),
            (int)artifactType,
            typeof(ArtifactType));

      }

      return new OvJobTicketService(TicketSvc, Config, jobId);
    }
  }
}