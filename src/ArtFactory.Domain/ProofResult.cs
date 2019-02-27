namespace ArtFactory.Domain
{
  using System.Collections.Generic;

  public class ProofResult
  {
    private List<string> JobPaths { get; } = new List<string>();
    private List<string> JobMessages { get; } = new List<string>();

    public void AddJobPaths(IEnumerable<string> paths) =>
      JobPaths.AddRange(paths);

    public void AddJobMessages(IEnumerable<string> messages) =>
      JobMessages.AddRange(messages);

    public IEnumerable<string> ProofPaths => JobPaths.AsReadOnly();
    public IEnumerable<string> ProofMessages => JobMessages.AsReadOnly();
  }
}