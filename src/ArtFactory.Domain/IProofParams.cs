namespace ArtFactory.Domain
{
  using System.Collections.Generic;

  public interface IProofParams
  {
    int DocumentId { get; set; }
    Dictionary<string, string> Fields { get; set; }
  }
}