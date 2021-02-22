using Values;
namespace Nodes
{
  public class PowerNode : INode
  {
    INode nodeA;
    INode nodeB;
    public string type = "Power";


    public PowerNode(INode a, INode b)
    {
      nodeA = a;
      nodeB = b;
    }

    public ExtendedFraction computeValue()
    {
      return nodeA.computeValue() ^ nodeB.computeValue();
    }

    public override string ToString()
    {
      return $"({nodeA}^{nodeB})";
    }
  }
}