namespace ArtFactory.Data
{
  using System.Configuration;
  using System.Data.SqlClient;
  using ArtFactory.Proofer.XmPie;
  using Dapper;

  public class XmpieDocumentService : IDocumentService
  {
    private static string XmpieConnectionString { get; }

    static XmpieDocumentService() =>
      XmpieConnectionString
        = ConfigurationManager.ConnectionStrings["xmpdb2"].ConnectionString;

    public XmpieDocument GetDocument(int documentId)
    {
      XmpieDocument doc;

      using (var conn = new SqlConnection(XmpieConnectionString))
      {
        conn.Open();

        doc = conn.QuerySingleOrDefault<XmpieDocument>(
          Queries.GetDocument, new { DocumentId = documentId });
      }

      return doc;
    }
  }
}