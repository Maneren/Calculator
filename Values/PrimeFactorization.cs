using System;
using System.Collections.Generic;
namespace Values
{
  public class PrimeFactorization
  {
    Dictionary<int, BasicFraction> Factors;
    bool IsNegative = false;
    public PrimeFactorization(int x)
    {
      if (x < 0) IsNegative = true;
      Factors = PrimeFactorization.getPrimeFactors(x);
    }

    public PrimeFactorization(Dictionary<int, BasicFraction> factors, bool isNegative = false)
    {
      IsNegative = isNegative;
      Factors = factors;
      Reduce();
    }

    public override string ToString()
    {
      var result = new List<string>();
      bool negative = false;
      foreach (var factor in Factors)
      {
        if (factor.Key == -1) negative = true;
        else if (factor.Value.Value == 1) result.Add($"{factor.Key}");
        else
        {
          if (factor.Value.Value < 0)
          {
            result.Add($"{factor.Key}^({factor.Value})");
          }
          else
          {
            result.Add($"{factor.Key}^{factor.Value}");
          }
        }
      }
      if (negative) return $"-({string.Join('*', result)})";
      return string.Join('*', result);
    }
    public int Value => PrimeFactorization.CalcValue(Factors, IsNegative);

    static int CalcValue(Dictionary<int, BasicFraction> factors, bool negative)
    {
      int total = 1;
      if (negative) total = -1;
      foreach (var factor in factors)
      {
        var base_ = factor.Key;
        var power = factor.Value;

        total *= (int)Math.Pow(base_, power.Value);
      }
      return total;
    }

    public PrimeFactorization Reduce()
    {
      var newFactors = new Dictionary<int, BasicFraction>();

      foreach (var factor in Factors)
      {
        if (factor.Value.Value != 0)
        {
          if (factor.Key == 1) continue;
          if (factor.Key == -1)
          {
            if (factor.Value.Value % 2 == 1) newFactors[factor.Key] = Fraction.From(1);
            continue;
          }
          newFactors[factor.Key] = factor.Value;
        }
      }
      if (newFactors.Count == 0) newFactors = new Dictionary<int, BasicFraction>() { { 1, Fraction.From(1) } };
      Factors = newFactors;
      return this;
    }

    static Dictionary<int, BasicFraction> getPrimeFactors(double n)
    {
      if (n == 0) return new Dictionary<int, BasicFraction>() { { 0, Fraction.From(1) } };
      if (n == 1) return new Dictionary<int, BasicFraction>() { { 1, Fraction.From(1) } };
      if (n == 2) return new Dictionary<int, BasicFraction>() { { 2, Fraction.From(1) } };
      var factors = new Dictionary<int, BasicFraction>();
      if (n < 0) factors.Add(-1, Fraction.From(1));
      int divisor = 2;

      while (n >= 2)
      {
        if (n % divisor == 0)
        {
          try
          {
            factors[divisor] = factors[divisor] + Fraction.From(1);
          }
          catch
          {
            factors[divisor] = Fraction.From(1);
          }
          n = n / divisor;
        }
        else
        {
          divisor++;
        }
      }
      return factors;
    }

    public static PrimeFactorization gcd(PrimeFactorization x, PrimeFactorization y)
    {
      var newFactors = new Dictionary<int, BasicFraction>();
      foreach (var factor in x.Factors)
      {
        var key = factor.Key;
        if (y.Factors.ContainsKey(key))
        {
          var power1 = factor.Value;
          var power2 = y.Factors[key];
          if (power1.Value > power2.Value)
          {
            newFactors[key] = power2;
          }
          else
          {
            newFactors[key] = power1;
          }
        }
      }
      return new PrimeFactorization(newFactors);
    }

    public static PrimeFactorization operator -(PrimeFactorization num)
    {
      if (num.IsNegative) return new PrimeFactorization(num.Factors, false);
      return new PrimeFactorization(num.Factors, true);
    }

    public static PrimeFactorization operator +(PrimeFactorization num)
    {
      return num;
    }


    public static PrimeFactorization operator +(PrimeFactorization x, PrimeFactorization y)
    {
      return new PrimeFactorization(x.Value + y.Value);
    }

    public static PrimeFactorization operator -(PrimeFactorization x, PrimeFactorization y)
    {
      return x + -y;
    }

    public static PrimeFactorization operator *(PrimeFactorization x, PrimeFactorization y)
    {
      var newFactors = new Dictionary<int, BasicFraction>();
      foreach (var factor in x.Factors)
      {
        var key = factor.Key;
        newFactors[key] = factor.Value;
      }

      foreach (var factor in y.Factors)
      {
        var key = factor.Key;
        try
        {
          newFactors[key] += factor.Value;
        }
        catch
        {
          newFactors[key] = factor.Value;
        }
      }
      return new PrimeFactorization(newFactors);
    }

    public static PrimeFactorization operator /(PrimeFactorization x, PrimeFactorization y)
    {
      var newFactors = x.Factors;
      foreach (var factor in y.Factors)
      {
        var key = factor.Key;
        try
        {
          newFactors[key] -= factor.Value;
        }
        catch
        {
          newFactors[key] = -factor.Value;
        }
      }
      return new PrimeFactorization(newFactors);
    }

    public static PrimeFactorization operator ^(PrimeFactorization x, PrimeFactorization y)
    {
      var newFactors = x.Factors;
      var power = y.Value;
      foreach (var factor in newFactors)
      {
        var key = factor.Key;
        newFactors[key] *= power;
      }
      return new PrimeFactorization(newFactors);
    }

    public static PrimeFactorization operator ^(PrimeFactorization x, ExtendedFraction y)
    {
      return x ^ y.ToBasic();
    }

    public static PrimeFactorization operator ^(PrimeFactorization x, BasicFraction y)
    {
      var keys = new int[x.Factors.Keys.Count];
      x.Factors.Keys.CopyTo(keys, 0);
      foreach (var key in keys)
      {
        x.Factors[key] *= y;
      }
      return new PrimeFactorization(x.Factors);
    }
  }

}