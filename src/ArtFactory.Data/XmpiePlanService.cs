namespace ArtFactory.Data
{
  using System.Configuration;
  using System.Data.SqlClient;
  using ArtFactory.Domain;
  using Dapper;

  public class XmpiePlanService : IPlanService
  {
    private static string XmpieConnectionString { get; }
    private IArtFactoryConfig Config { get; }

    static XmpiePlanService()
    {
      XmpieConnectionString 
        = ConfigurationManager.ConnectionStrings["xmpdb2"].ConnectionString;
    }

    public XmpiePlanService(IArtFactoryConfig config)
    {
      Config = config;
    }

    public Uplan GetPlanForDocument(int documentId)
    {
      using (var conn = new SqlConnection(XmpieConnectionString)) {
        conn.Open();

        return conn.QuerySingleOrDefault<Uplan>(
          Queries.GetPlanPath, new {DocumentId = documentId});
      }
    }
  }
}