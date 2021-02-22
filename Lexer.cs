using System;
using System.Collections.Generic;

namespace Calculator
{
  class Lexer
  {
    string input = "";
    int i = 0;
    char? current;

    readonly char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    readonly char[] operators = { '+', '-', '*', '/', '^', '(', ')' };
    readonly char[] whitespaces = { ' ', '\n', '\t' };
    Dictionary<char, TokenType> operatorMapping = new Dictionary<char, TokenType> {
        {'+', TokenType.Plus},
        {'-', TokenType.Minus},
        {'*', TokenType.Multiply},
        {'/', TokenType.Divide},
        {'^', TokenType.Power},
        {'(', TokenType.LParen},
        {')', TokenType.RParen}
    };

    public Lexer(string input)
    {
      this.input = input;
      Move();
    }

    void Move()
    {
      if (i >= input.Length) current = null;
      else current = input[i];
      i++;
    }

    bool IsDigit(char? ch)
    {
      return Array.Exists(digits, x => x == ch);
    }

    bool IsDecimalPoint(char? ch)
    {
      return ch == '.';
    }

    bool IsOperator(char? ch)
    {
      return Array.Exists(operators, x => x == ch);
    }

    bool IsWhitespace(char? ch)
    {
      return Array.Exists(whitespaces, x => x == ch);
    }

    public Token[] Tokenize()
    {
      var tokens = new List<Token> { };
      while (current != null)
      {
        if (IsDigit(current) || IsDecimalPoint(current)) tokens.Add(LoadNumber());

        else if (IsOperator(current)) tokens.Add(LoadOperator());
        else if (IsWhitespace(current)) Move();
        else throw new Lexer.LexerException($"Unexpected character: {current} at position {i + 1}");
      }
      return tokens.ToArray();
    }

    Token LoadNumber()
    {
      string value = "";
      bool isDecimal = false;
      while (IsDigit(current) || IsDecimalPoint(current))
      {

        if (IsDecimalPoint(current))
        {
          if (isDecimal) throw new Lexer.LexerException($"Unexpected character: {current} at position {i + 1}");
          isDecimal = true;
        }
        value += current;
        Move();

      }

      if (value.StartsWith('.') && value.EndsWith('.')) throw new Lexer.LexerException($"Unexpected character: {current} at position {i + 1}");

      if (value.StartsWith('.')) value = '0' + value;
      if (value.EndsWith('.')) value = value + '0';

      return new Token(TokenType.Number, value);
    }

    Token LoadOperator()
    {
      TokenType type = operatorMapping[current ?? ' '];
      Move();
      return new Token(type);
    }

    public class LexerException : System.Exception
    {
      public LexerException() { }
      public LexerException(string message) : base(message) { }
      public LexerException(string message, System.Exception inner) : base(message, inner) { }
      protected LexerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
  }
}