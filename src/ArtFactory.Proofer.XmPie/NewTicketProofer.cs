namespace ArtFactory.Proofer.XmPie
{
  using System;
  using ArtFactory.Proofer.XmPie.JobTicketService;
  using ArtFactory.Proofer.XmPie.ProductionService;

  public class NewTicketProofer
  {
    public void BuildProof(int documentId)
    {
      const string u = "garmstrong";
      const string pw = "9ol8ikm";
      const int xmpieDocumentId = 193;

      var ticketSvc = new JobTicket_SSP();
      var productionSvc = new Production_SSP();
      var proofId = $"{Guid.NewGuid().ToString().Substring(0, 8)}.jpg";

      var ticketId = ticketSvc.CreateNewTicketForDocument(
        u,
        pw,
        xmpieDocumentId.ToString(),
        "FakeData",
        false);

      var jobParams = new[]
      {
        new Parameter {m_Name = "JPG_RES", m_Value = "300"},
        new Parameter {m_Name = "BLEED_USE_DOCUMENT_DEF", m_Value = "1"},
        new Parameter {m_Name = "NATIVE_PDF_OPTIONS", m_Value = "XMPiEQualityProof"}
      };

      ticketSvc.SetOutputInfo(
        u, pw, ticketId,
        "JPG", 1, "", proofId, jobParams);

      ticketSvc.SetJobType(u, pw, ticketId, "PROOF");

      ticketSvc.SetRI(u, pw, ticketId,
        new RecipientsInfo { m_FilterType = 4 },
        new Connection { m_Type = "MSQL", m_ConnectionString = "_" });

      var stringCustomizations = new[]
      {
        new Customization
          { m_Name = "_growerName", m_Expression = "Costa Nursery", m_IOType = "R", m_Type = "VAR"},
        new Customization
          { m_Name = "_pricePoint", m_Expression = "$13.95", m_IOType = "R", m_Type = "VAR"},
        new Customization
          {m_Name = "_growerAddress", m_Expression = "Citrus Heights, CA", m_IOType = "R", m_Type = "VAR"},
        new Customization
          { m_Name = "_upc", m_Expression = "092852079020", m_IOType = "R", m_Type = "VAR"},
        new Customization
          { m_Name = "_weightsAndMeasures", m_Expression = "1.6GAL/.36L", m_IOType = "R", m_Type = "VAR"},
        new Customization
          { m_Name = "_lowesItemNumber", m_Expression = "", m_IOType = "R", m_Type = "VAR"},
        new Customization
          { m_Name = "_productDescription", m_Expression = "Mr. Ghost's Plant", m_IOType = "R", m_Type = "VAR"},
        new Customization
          { m_Name = "_customerStockNumber", m_Expression = "777", m_IOType = "R", m_Type = "VAR"},
        new Customization
          { m_Name = "Cust.Material#", m_Expression = "", m_IOType = "R", m_Type = "VAR"}
      };

      var nonStringCustomizations = new[]
      {
        new Customization
          { m_Name = "plantId", m_Expression = @"86136", m_IOType = "R", m_Type = "VAR"},
        new Customization
          { m_Name = "_greenPriceBox", m_Expression = "True", m_IOType = "R", m_Type = "VAR"},
        new Customization
          { m_Name = "_prop65", m_Expression = "True", m_IOType = "R", m_Type = "VAR"}
      };

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
    }
  }
}