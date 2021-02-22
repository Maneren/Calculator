using System;
using Nodes;
namespace Calculator
{
  class Parser
  {
    Token[] tokens;
    int i = 0;

#nullable enable
    Token? current;

    public Parser(Token[] tokens)
    {
      this.tokens = tokens;
    }

    void Move()
    {
      if (i >= tokens.Length) current = null;
      else current = tokens[i];
      i++;
    }

    public INode? parse()
    {
      Move();
      if (current == null) return null;
      INode result = Expression();
      if (current != null) throw new Parser.ParseException();
      return result;
    }

    bool IsInArray<T>(T[] array, T value)
    {
      return Array.Exists(array, x => x == null ? false : x.Equals(value));
    }

    INode Expression()
    {
      INode result = Term();
      TokenType[] validOperators = { TokenType.Plus, TokenType.Minus };

      while (current != null && IsInArray(validOperators, current.type))
      {
        var token = current;
        Move();
        INode termB = Term();
        if (token.type == TokenType.Plus) result = new Nodes.AddNode(result, termB);
        else result = new Nodes.AddNode(result, termB);
      }

      return result;
    }

    INode Term()
    {
      INode result = Factor();
      TokenType[] validOperators = { TokenType.Multiply, TokenType.Divide };

      while (current != null && IsInArray(validOperators, current.type))
      {
        var token = current;
        Move();
        INode factorB = Factor();
        if (token.type == TokenType.Multiply) result = new Nodes.MultiplyNode(result, factorB);
        else result = new Nodes.DivideNode(result, factorB);
      }
      return result;
    }

    INode Factor()
    {
      INode result = Atom();

      while (current != null && current.type == TokenType.Power)
      {
        var token = current;
        Move();
        INode factorB = Atom();
        result = new Nodes.PowerNode(result, factorB);
      }
      return result;
    }

    INode Atom()
    {
      if (current != null)
      {
        INode result;
        if (current.type == TokenType.LParen)
        {
          Move();
          result = Expression();
          if (current.type != TokenType.RParen) throw new Parser.ParseException("Missing closing parenthesis");
          Move();
        }
        else if (current.type == TokenType.Number)
        {
          result = new Nodes.NumberNode(current.value);
          Move();
        }
        else if (current.type == TokenType.Plus)
        {
          Move();
          result = new Nodes.PlusNode(Factor());
        }
        else if (current.type == TokenType.Minus)
        {
          Move();
          result = new Nodes.MinusNode(Factor());
        }
        else throw new Parser.ParseException();
        return result;
      }
      else throw new Parser.ParseException();
    }

    public class ParseException : System.Exception
    {
      public ParseException() : base("Invalid syntax") { }
      public ParseException(string message) : base(message) { }
      public ParseException(string message, System.Exception inner) : base(message, inner) { }
      protected ParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
  }

}