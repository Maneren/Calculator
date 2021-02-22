using Values;
namespace Nodes
{
  public class MinusNode : INode
  {
    INode value;
    public string type = "Minus";


    public MinusNode(INode value)
    {
      this.value = value;
    }

    public ExtendedFraction computeValue()
    {
      return -value.computeValue();
    }

    public override string ToString()
    {
      return $"(-{value})";
    }
  }
}