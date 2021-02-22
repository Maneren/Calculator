using Values;
namespace Nodes
{
  public interface INode
  {
    string ToString();
    ExtendedFraction computeValue();
  }
}