using Values;
namespace Nodes
{
  public class DivideNode : INode
  {
    INode nodeA;
    INode nodeB;
    public string type = "Divide";


    public DivideNode(INode a, INode b)
    {
      nodeA = a;
      nodeB = b;
    }

    public ExtendedFraction computeValue()
    {
      return nodeA.computeValue() / nodeB.computeValue();
    }

    public override string ToString()
    {
      return $"({nodeA}/{nodeB})";
    }
  }
}