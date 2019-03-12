namespace ArtFactory.Proofer.XmPie
{
  public class XmpieUser
  {
    public XmpieUser(string userName, string password)
    {
      UserName = userName;
      Password = password;
    }

    public string UserName { get; }
    public string Password { get; }

    public void Deconstruct(out string userName, out string password)
    {
      userName = UserName;
      password = Password;
    }
  }
}