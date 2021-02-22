using Values;
namespace Nodes
{
  public class PlusNode : INode
  {
    INode value;
    public string type = "Plus";


    public PlusNode(INode value)
    {
      this.value = value;
    }

    public ExtendedFraction computeValue()
    {
      return value.computeValue();
    }

    public override string ToString()
    {
      return $"(+{value})";
    }
  }
}