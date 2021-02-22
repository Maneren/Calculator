using System;
using Values;
namespace Nodes
{
  public class NumberNode : INode
  {
    double value;
    public string type = "Number";

    public NumberNode(string num)
    {
      value = Double.Parse(num);
    }

    public ExtendedFraction computeValue()
    {
      return new ExtendedFraction(value);
    }

    public override string ToString()
    {
      return $"{value}";
    }
  }
}