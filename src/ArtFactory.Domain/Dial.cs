namespace ArtFactory.Domain
{
  using System;

  public class Dial
  {
    public string Name { get; }
    public string Type { get; }

    public Dial(string name, string type)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

      if (string.IsNullOrWhiteSpace(type))
        throw new ArgumentException("Value cannot be null or whitespace.", nameof(type));

      Name = name;
      Type = type;
    }
  }
}