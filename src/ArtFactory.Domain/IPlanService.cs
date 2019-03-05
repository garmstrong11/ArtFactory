namespace ArtFactory.Domain
{
  public interface IPlanService
  {
    Uplan GetPlanForDocument(int documentId);
  }
}