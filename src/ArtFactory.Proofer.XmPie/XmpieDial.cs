namespace ArtFactory.Proofer.XmPie
{
  using System;

  public class XmpieDial
  {
    public string Name { get; }
    public string Type { get; }

    public XmpieDial(string name, string type)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

      if (string.IsNullOrWhiteSpace(type))
        throw new ArgumentException("Value cannot be null or whitespace.", nameof(type));

      Name = name;
      Type = type;
    }

    public void Deconstruct(out string name, out string type)
    {
      name = Name;
      type = Type;
    }

    protected bool Equals(XmpieDial other)
    {
      var (name, type) = other;
      return string.Equals(Name, name) && string.Equals(Type, type);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((XmpieDial)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (Name.GetHashCode() * 397) ^ Type.GetHashCode();
      }
    }

    public static bool operator ==(XmpieDial left, XmpieDial right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(XmpieDial left, XmpieDial right)
    {
      return !Equals(left, right);
    }
  }
}