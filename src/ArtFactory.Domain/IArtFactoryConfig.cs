﻿namespace ArtFactory.Domain
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
  }
}