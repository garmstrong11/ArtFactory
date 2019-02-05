namespace ArtFactory.Domain
{
  public enum JobStatus
  {
    Waiting = 1,
    InProgress = 2,
    Completed = 3,
    Failed = 4,
    Aborting = 5,
    Aborted = 6,
    Deployed = 7,
    Suspended = 8
  }
}