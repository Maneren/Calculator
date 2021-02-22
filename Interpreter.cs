using System;
using System.Collections.Generic;
using Values;
using Nodes;
namespace Calculator
{
  class Interpreter
  {
    // readonly Dictionary<string, Action<INode>> visitMap = new Dictionary<string, Action<INode>>
    // {
    //   {"Add", node => VisitAddNode(node)},
    //   {"Subtract", node => VisitSubtractNode(node)}
    // };

    static public IFraction Visit(INode node)
    {
      return node.computeValue();
    }

    // static IValue VisitAddNode(INode node)
    // {

    // }
  }
}