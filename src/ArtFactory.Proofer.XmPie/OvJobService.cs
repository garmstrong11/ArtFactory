namespace ArtFactory.Proofer.XmPie
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using ArtFactory.Domain;
  using Job;

  /// <summary>
  /// A wrapper over the XMPie uProduce WSAPI Job_SSP WebService.
  /// </summary>
  public class OvJobService
  {
    /// <summary>
    /// The XMPie WSAPI service implementation.
    /// </summary>
    private Job_SSPSoap JobSvc { get; }

    /// <summary>
    /// A user with production privileges on uProduce
    /// </summary>
    private XmpieUser User { get; }

    public OvJobService(Job_SSPSoap jobSvc, XmpieUser user)
    {
      JobSvc = jobSvc;
      User = user;
    }

    public JobStatus PollForStatus(string jobId, TimeSpan retryInterval)
    {
      var result = (JobStatus) JobSvc.GetStatus(User.UserName, User.Password, jobId);

      while (result != JobStatus.Completed && result != JobStatus.Failed) {
        Thread.Sleep(retryInterval);
        result = (JobStatus) JobSvc.GetStatus(User.UserName, User.Password, jobId);
      }

      return result;
    }

    public async Task<string> GetJobResultUrlAsync(string jobId)
    {
      return "foo";
    }
  }
}