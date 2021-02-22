using Utils;
using System;
namespace Values
{
  public class BasicFraction : IFraction
  {
    public int a;
    public int b;

    public BasicFraction(double a)
    {
      if (a % 1 == 0)
      {
        this.a = (int)a;
        this.b = 1;
      }
      else
      {
        int integer = (int)Math.Floor(a);
        var parts = a.ToString().Split(new char[] { '.', ',' });
        var decimals = parts[1];
        int tens = (int)Math.Pow(10, decimals.Length);

        this.a = tens * integer + int.Parse(decimals);
        this.b = tens;

        Reduce();
      }
    }

    public BasicFraction(int a) : this(a, 1) { }

    public BasicFraction(int a, int b)
    {
      if (b == 0) throw new FractionException("Denominator can not be 0");
      this.a = a;
      this.b = b;

      Reduce();
    }

    public double Value => a / (double)b;

    public ExtendedFraction ToExtended()
    {
      return new ExtendedFraction(new PrimeFactorization(a), new PrimeFactorization(b));
    }

    void Reduce()
    {
      int coefficient = Main.gcd(a, b);
      a /= coefficient;
      b /= coefficient;
    }

    public override string ToString()
    {
      if (b == 1) return $"{a}";
      return $"({a}/{b})";
    }

    public BasicFraction Inverse()
    {
      return new BasicFraction(b, a);
    }

    public static BasicFraction operator -(BasicFraction num)
    {
      return num * new BasicFraction(-1);
    }

    public static BasicFraction operator +(BasicFraction num)
    {
      return num;
    }


    public static BasicFraction operator +(BasicFraction x, BasicFraction y)
    {
      var nominator = (x.a * y.b) + (y.a * x.b);
      var denominator = (x.b * y.b);
      return new BasicFraction(nominator, denominator);
    }

    public static BasicFraction operator -(BasicFraction x, BasicFraction y)
    {
      return x + (-y);
    }

    public static BasicFraction operator *(BasicFraction x, BasicFraction y)
    {
      return new BasicFraction(x.a * y.a, x.b * y.b);
    }

    public static BasicFraction operator *(BasicFraction x, int y)
    {
      return x * new BasicFraction(y);
    }

    public static BasicFraction operator /(BasicFraction x, BasicFraction y)
    {
      return x * y.Inverse();
    }

    public static ExtendedFraction operator ^(BasicFraction x, BasicFraction y)
    {
      return new ExtendedFraction((x.a ^ y), (x.b ^ y));
    }

    public static PrimeFactorization operator ^(int x, BasicFraction y)
    {
      var pf = new PrimeFactorization(x);
      var frac = y.ToExtended();
      var newX = pf ^ frac;
      return newX;
    }
  }
}