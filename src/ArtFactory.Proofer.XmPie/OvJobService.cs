namespace ArtFactory.Proofer.XmPie
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using ArtFactory.Domain;
  using Job;
  using Polly;

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
    private IXmPieUser User { get; }

    public OvJobService(Job_SSPSoap jobSvc, IXmPieUser user)
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

    /// <summary>
    /// Run a periodic check for job completion.
    /// </summary>
    /// <param name="jobId"></param>
    /// <param name="retryInterval"></param>
    /// <returns>A JobStatus value that indicates the job completed (Success, Failure, or Abort)</returns>
    public async Task<JobStatus> MonitorJobCompletion(string jobId, TimeSpan retryInterval)
    {
      var policy = Policy
        .HandleResult<JobStatus>(j => j == JobStatus.Waiting)
        .OrResult(j => j == JobStatus.InProgress)
        .WaitAndRetryForeverAsync((i, t) => retryInterval);

      var status = await policy.ExecuteAsync(async () =>
       {
         var result = await JobSvc.GetStatusAsync(User.UserName, User.Password, jobId);
         Console.WriteLine("Result: {0}", result);
         return (JobStatus)result;
       });

      return status;

      //do
      //{
      //  status = (JobStatus) JobSvc.GetStatus(User.UserName, User.Password, jobId);
      //  Thread.Sleep(retryInterval);
      //}
      //while (status != JobStatus.Completed && status != JobStatus.Failed);

      //if (status == JobStatus.Failed) {
      //  var messages = await JobSvc.GetMessagesAsync(User.UserName, User.Password, jobId);
      //  proofResult.AddJobMessages(messages);
      //  return proofResult;
      //}

      //var paths = await JobSvc.GetOutputResultsAsync(User.UserName, User.Password, jobId);
      //proofResult.AddJobPaths(paths);

      //return proofResult;
    }
  }
}