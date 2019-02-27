namespace ArtFactory.Proofer.XmPie
{
  using ArtFactory.Domain;

  public class BossUser : IXmPieUser
  {
    public BossUser(string userName, string password)
    {
      UserName = userName;
      Password = password;
    }

    public string UserName { get; }
    public string Password { get; }
  }
}