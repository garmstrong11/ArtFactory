namespace ArtFactory.Tests
{
  using System;
  using System.ServiceModel;
  using System.Threading.Tasks;
  using ArtFactory.Domain;
  using ArtFactory.Proofer.XmPie;
  using ArtFactory.Proofer.XmPie.Job;
  using FakeItEasy;
  using FluentAssertions;
  using NUnit.Framework;

  [TestFixture]
  public class OvJobServiceTests
  {
    private string U { get; set; }
    private string P { get; set; }
    private string J { get; set; }

    [OneTimeSetUp]
    public void OneTime()
    {
      U = "garmstrong";
      P = "9ol8ikm";
      J = "46583";
    }

    [Test]
    public void PollForStatus_ThreeCallsBeforeCompletion_Completes()
    {
      var usr = new XmpieUser(U, P);

      var svc = A.Fake<Job_SSPSoap>();
      A.CallTo(() => svc.GetStatus(U, P, J))
        .Returns(2)
        .NumberOfTimes(3)
        .Then
        .Returns(3);
      
      var jobber = new OvJobService(svc, usr);
      var result = jobber.PollForStatus(J, TimeSpan.FromMilliseconds(500));
      A.CallTo(() => svc.GetStatus(U,P,J))
        .MustHaveHappened(17, Times.Exactly);

      var answer = Task.Run(() =>
      {
        var kronk = jobber.PollForStatus(J, TimeSpan.FromMilliseconds(500));
        return kronk;
      });

      answer.Result.Should().Be(JobStatus.Completed);
      result.Should().Be(JobStatus.Completed);
    }

    [Test]
    public async Task GetJobResultUrl_CanReturnJobUrl()
    {
      var client = new Job_SSPSoapClient(
        new BasicHttpBinding(),
        new EndpointAddress("http://uimeapp0003/xmpiewsapi/Job_SSP.asmx")
      );

      var doc = await client.GetOutputResultsInfoAsync(U, P, J);

      doc.GetOutputResultsInfoResult[0]
        .m_FileName
        .Should()
        .Be("L5 Lock Tag_46583_r00001_p001.jpg");
    }
  }
}