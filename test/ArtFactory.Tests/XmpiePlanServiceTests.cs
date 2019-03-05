namespace ArtFactory.Tests
{
  using ArtFactory.Data;
  using ArtFactory.Domain;
  using Config.Net;
  using FluentAssertions;
  using NUnit.Framework;

  [TestFixture]
  public class XmpiePlanServiceTests
  {
    [Test]
    public void CanGetPlan()
    {
      var config = new ConfigurationBuilder<IArtFactoryConfig>()
        .UseAppConfig()
        .Build();

      var svc = new XmpiePlanService(config);
      var result = svc.GetPlanForDocument(150);

      result.DocumentId.Should().Be(150);
    }
  }
}