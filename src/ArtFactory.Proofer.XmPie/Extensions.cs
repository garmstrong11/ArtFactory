namespace ArtFactory.Proofer.XmPie
{
  using ArtFactory.Proofer.XmPie.JobTicket;

  public static class Extensions
  {
    /// <summary>
    /// Converts a dial instance into an XMPie customization instance
    /// </summary>
    /// <param name="source">The source XmpieDial instance.</param>
    /// <param name="expression">The customization's value</param>
    /// <returns></returns>
    public static Customization AsCustomization(this XmpieDial source, string expression) =>
      new Customization
      {
        m_Expression = expression ?? string.Empty,
        m_IOType = "R",
        m_Name = source.Name,
        m_Type = source.Type
      };
  }
}