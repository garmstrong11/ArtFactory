namespace ArtFactory.Domain
{
  public interface IArtFactoryConfig
  {
    /// <summary>
    /// The username for the uProduce user under whose authority services will run.
    /// </summary>
    string UproduceUsername { get; }

    /// <summary>
    /// The password for the uProduce user under whose authority services will run.
    /// </summary>
    string UproduceUserPassword { get; }

    /// <summary>
    /// The UNC name of the Uproduce server
    /// </summary>
    string UproduceServerUncName { get; }

    string ProofResolution { get; }
  }
}