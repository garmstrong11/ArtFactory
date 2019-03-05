namespace ArtFactory.Proofer.XmPie
{
  using ArtFactory.Domain;
  using ArtFactory.Proofer.XmPie.JobTicket;

  public class OvJobTicketService
  {
    private JobTicket_SSPSoap TicketSvc { get; }
    private IArtFactoryConfig Config { get; }

    public OvJobTicketService(JobTicket_SSPSoap ticketSvc, IArtFactoryConfig config)
    {
      TicketSvc = ticketSvc;
      Config = config;
    }

    public string CreateTicketForDocument(int documentId, string recipientTableName)
    {
      return TicketSvc.CreateNewTicketForDocument(
        Config.UproduceUsername,
        Config.UproduceUserPassword,
        documentId.ToString(),
        recipientTableName,
        false);
    }
  }
}