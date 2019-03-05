namespace ArtFactory.Experimental {
  using System.Collections.Generic;
  using ArtFactory.Experimental.JobTicket;

  public interface IProofArgs {
    int DocumentId { get; set; }
    Dictionary<string, string> Fields { get; set; }
    Customization[] GetStringCustomizations();
    Customization[] GetNonStringCustomizations();
  }
}