namespace ArtFactory.Data
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Xml.Linq;
  using ArtFactory.Proofer.XmPie;

  public class XmpieDialService : IDialService
  {
    public IEnumerable<XmpieDial> GetDialsForPlan(string planPath)
    {
      if (planPath == null) throw new ArgumentNullException(nameof(planPath));

      var planInfo = new FileInfo(planPath);
      if (!planInfo.Exists) throw new FileNotFoundException(
        $"Unable to locate a plan file at '{planInfo.FullName}'");

      XDocument planDoc;

      using (var stream = planInfo.OpenRead())
      {
        planDoc = XDocument.Load(stream);
      }

      var dials =
        from d in planDoc.Descendants("VARIABLE")
        let attrs = d.Attributes().Select(p => p.Name)
        where attrs.Contains("CampaignDial") && attrs.Contains("Name") && attrs.Contains("Type")
        select new XmpieDial(d.Attribute("Name")?.Value, d.Attribute("Type")?.Value);

      return dials;
    }
  }
}