namespace Values
{
  public class Fraction
  {
    public static BasicFraction From()
    {
      return Fraction.From(1);
    }

    public static BasicFraction From(int a)
    {
      return Fraction.From(a, 1);
    }

    public static BasicFraction From(int a, int b)
    {
      return new BasicFraction(a, b);
    }

    public static ExtendedFraction From(double a)
    {
      return new ExtendedFraction(a);
    }

    public static ExtendedFraction From(PrimeFactorization a, PrimeFactorization b)
    {
      return new ExtendedFraction(a, b);
    }
  }
}