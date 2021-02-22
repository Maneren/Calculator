using System;

namespace Calculator
{
  enum TokenType
  {
    Number, Plus, Minus, Multiply, Divide, Power, RParen, LParen
  }

  class Token
  {
    public TokenType type;

#nullable enable
    public string? value;

    public Token(TokenType type, string? value = null)
    {
      this.type = type;
      this.value = value;
    }

    public override string ToString()
    {
      if (value == null) return $"{type.ToString()}";
      else return $"{type.ToString()}:{value}";
    }
  }
}