using System;
namespace Utils
{
  class Main
  {
    // Helper functions
    // Lowest common multiple
    public static int lcm(int x, int y)
    {
      return Math.Abs(x * y / gcd(x, y));
    }

    // Greatest common divisor
    public static int gcd(int x, int y)
    {
      x = Math.Abs(x);
      y = Math.Abs(y);
      while (y > 0)
      {
        int t = y;
        y = x % y;
        x = t;
      }
      return x;
    }
  }
}