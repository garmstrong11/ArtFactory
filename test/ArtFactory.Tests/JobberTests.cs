namespace ArtFactory.Tests
{
  using System;
  using System.Threading.Tasks;
  using ArtFactory.Domain;
  using ArtFactory.Proofer.XmPie;
  using ArtFactory.Proofer.XmPie.Job;
  using FakeItEasy;
  using FluentAssertions;
  using NUnit.Framework;

  [TestFixture]
  public class JobberTests
  {
    [Test]
    public void CanInstantiateJobber()
    {
      var svc
      var usr = A.Fake<IXmPieUser>();
      A.CallTo(() => usr.UserName).Returns("garmstrong");
      A.CallTo(() => usr.Password).Returns("9ol8ikm");

      //var svc = A.Fake<Job_SSPSoap>();
      //A.CallTo(() => svc.GetStatusAsync(A<string>._, A<string>._, A<string>._))
      //  .Returns(Task.FromResult(2))
      //  .NumberOfTimes(3)
      //  .Then
      //  .Returns(3);

      var jobber = new Jobber(svc, usr);
      var result = jobber.MonitorJobCompletion("32141", TimeSpan.FromMilliseconds(500));
      result.Should().Be(JobStatus.Completed);
    }
  }
}