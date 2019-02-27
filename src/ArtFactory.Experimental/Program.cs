namespace ArtFactory.Experimental
{
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Linq;
  using System.Threading;
  using ArtFactory.Experimental.Job;
  using ArtFactory.Experimental.JobTicket;
  using ArtFactory.Experimental.Production;
  using Newtonsoft.Json;

  internal class Program
  {
    private static void Main(string[] args)
    {
      var json = File.ReadAllText(args[0]);
      var pa = JsonConvert.DeserializeObject<ProofArgs>(json);

      const string u = "garmstrong";
      const string pw = "9ol8ikm";
      var xmpieDocumentId = pa.DocumentId;

      var ticketSvc = new JobTicket_SSP();
      var productionSvc = new Production_SSP();
      var jobSvc = new Job_SSP();

      var ticketId = ticketSvc.CreateNewTicketForDocument(
        u,
        pw,
        xmpieDocumentId.ToString(),
        "FakeData",
        false);

      var jobParams = new []
      {
        new Parameter {m_Name = "JPG_RES", m_Value = "300"},
        new Parameter {m_Name = "BLEED_USE_DOCUMENT_DEF", m_Value = "1"},
        new Parameter {m_Name = "NATIVE_PDF_OPTIONS", m_Value = "XMPiEQualityProof"}
      };

      ticketSvc.SetOutputInfo(
        u, pw, ticketId,
        "JPG", 1, "", ticketId, jobParams);

      ticketSvc.SetJobType(u, pw, ticketId, "PROOF");

      ticketSvc.SetRI(u, pw, ticketId,
        new RecipientsInfo { m_FilterType = 4 },
        new Connection { m_Type = "MSQL", m_ConnectionString = "_" });

      var stringCustomizations = pa.GetStringCustomizations();
      var nonStringCustomizations = pa.GetNonStringCustomizations();

      ticketSvc.SetCustomizations(
        u,
        pw,
        ticketId,
        nonStringCustomizations,
        false);

      ticketSvc.SetCustomizations(
        u, 
        pw,
        ticketId, 
        stringCustomizations, 
        true);

      var jobId = productionSvc.SubmitJob(
        u,
        pw,
        ticketId,
        "0",
        "",
        null);

      var targetDir = $@"\\uimeapp0003\XMPie\XMPieOutput\6\{jobId}";

      Console.Write("Job ID is {0}, Polling for completion", jobId);

      int status;

      do {
        status = jobSvc.GetStatus(u, pw, jobId);
        Console.WriteLine($"Status: {status}");
        Thread.Sleep(250);
      } while (status != 3 && status != 4);


      switch (status)
      {
        case 3:
          Console.WriteLine();
          Console.WriteLine("Success! file @ {0}", targetDir);
          Directory.EnumerateFiles(targetDir)
            .ToList()
            .ForEach(f => Process.Start(f));
          break;
        case 4:
          Console.WriteLine("Job Failed");
          Console.WriteLine(jobSvc.GetMessages(u, pw, jobId));
          break;
        default:
          Console.WriteLine("Unknown result");
          break;
      }

      Console.ReadLine();
    }
  }
}
