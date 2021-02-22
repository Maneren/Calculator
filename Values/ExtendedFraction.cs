using System;
namespace Values
{
  public class ExtendedFraction : IFraction
  {
    PrimeFactorization a;
    PrimeFactorization b;
    public ExtendedFraction(double a)
    {
      if (a % 1 == 0)
      {
        this.a = new PrimeFactorization((int)a);
        this.b = new PrimeFactorization(1);
      }
      else
      {
        int integer = (int)Math.Floor(a);
        string decimals = a.ToString().Split('.')[1];
        int tens = (int)Math.Pow(10, decimals.Length);

        this.a = new PrimeFactorization(tens * integer + int.Parse(decimals));
        this.b = new PrimeFactorization(tens);

        Reduce();
      }
    }

    public ExtendedFraction(PrimeFactorization a, PrimeFactorization b)
    {
      if (b.Value == 0) throw new FractionException("Denominator can not be 0");

      this.a = a;
      this.b = b;

      Reduce();
    }

    public double Value => a.Value / b.Value;

    public BasicFraction ToBasic()
    {
      return new BasicFraction(a.Value, b.Value);
    }

    public override string ToString()
    {
      if (b.Value == 1) return a.ToString();
      return $"({a}/{b})";
    }
    void Reduce()
    {
      PrimeFactorization coefficient = PrimeFactorization.gcd(a, b);
      a /= coefficient;
      b /= coefficient;
    }

    public ExtendedFraction Inverse()
    {
      return new ExtendedFraction(b, a);
    }

    public static ExtendedFraction operator -(ExtendedFraction num)
    {
      return num * new ExtendedFraction(-1);
    }

    public static ExtendedFraction operator +(ExtendedFraction num)
    {
      return num;
    }


    public static ExtendedFraction operator +(ExtendedFraction x, ExtendedFraction y)
    {
      var nominator = (x.a * y.b) + (y.a * x.b);
      var denominator = (x.b * y.b);
      return new ExtendedFraction(nominator, denominator);
    }

    public static ExtendedFraction operator -(ExtendedFraction x, ExtendedFraction y)
    {
      return x + (-y);
    }

    public static ExtendedFraction operator *(ExtendedFraction x, ExtendedFraction y)
    {
      return new ExtendedFraction(x.a * y.a, x.b * y.b);
    }

    public static ExtendedFraction operator *(ExtendedFraction x, int y)
    {
      return x * new ExtendedFraction(y);
    }

    public static ExtendedFraction operator /(ExtendedFraction x, ExtendedFraction y)
    {
      return x * y.Inverse();
    }

    public static ExtendedFraction operator ^(ExtendedFraction x, ExtendedFraction y)
    {
      return new ExtendedFraction(x.a ^ y, x.b ^ y);
    }

  }
  public class FractionException : System.Exception
  {
    public FractionException() { }
    public FractionException(string message) : base(message) { }
    public FractionException(string message, System.Exception inner) : base(message, inner) { }
    protected FractionException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}