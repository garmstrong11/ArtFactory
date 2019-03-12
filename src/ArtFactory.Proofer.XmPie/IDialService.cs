namespace ArtFactory.Proofer.XmPie {
  using System.Collections.Generic;

  public interface IDialService {
    IEnumerable<XmpieDial> GetDialsForPlan(string planPath);
  }
}