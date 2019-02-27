namespace ArtFactory.Proofer.XmPie
{
  using ArtFactory.Domain;

  public class JobExecutionContext
  {
    public JobExecutionContext(IXmPieUser user)
    {
      User = user;
    }

    public IXmPieUser User;
    public string JobId { get; set; }
  }
}