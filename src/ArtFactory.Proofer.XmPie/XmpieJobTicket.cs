namespace ArtFactory.Proofer.XmPie
{
  using System;
  using ArtFactory.Domain;
  using ArtFactory.Proofer.XmPie.JobTicket;
  using JetBrains.Annotations;

  public class XmpieJobTicket
  {
    private JobTicket_SSPSoap Client { get; }
    private IArtFactoryConfig Config { get; }

    public XmpieJobTicket([NotNull] JobTicket_SSPSoap client, [NotNull] IArtFactoryConfig config)
    {
      Client = client ?? throw new ArgumentNullException(nameof(client));
      Config = config ?? throw new ArgumentNullException(nameof(config));
    }
  }
}